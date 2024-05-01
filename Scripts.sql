-- darshan Report start
ALTER PROCEDURE [dbo].[SP_GetAnnadanRec]
    @strFDate NVARCHAR(50),
    @strTDate NVARCHAR(50),
    @intCtrMachId BIGINT
AS
BEGIN
    SELECT 
        MST.Entered_By AS Name,
        COUNT(*) AS Totalrecpts,
        SUM(CASE WHEN MST.GUEST = 1 THEN 0 ELSE ISNULL(MST.AMOUNT, 0) END) AS Cash,
        NULL AS Heading,
        @strFDate + ' to ' + @strTDate AS period,
        NULL AS subheading,
        'Accounting Period : ' + FY.DISPLAY_VALUE AS AccountingPeriod
    FROM 
        MESS_PRINT_RECEIPT_MST_T MST
        INNER JOIN COM_FINANCIAL_YEAR_MST_T FY ON FY.Financial_Year_Id = MST.FY_ID
    WHERE 
        (MST.CTR_MACH_ID = @intCtrMachId OR @intCtrMachId = 0)
        AND (MST.PR_DATE BETWEEN CONVERT(DATETIME, @strFDate, 103) AND CONVERT(DATETIME, @strTDate, 103))
    GROUP BY 
        MST.Entered_By, FY.DISPLAY_VALUE;
END
GO

ALTER PROCEDURE [dbo].[SP_GetChequeReptDetail]
    @Date NVARCHAR(50),
    @CtrMachId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

        SELECT
            ISNULL(th.heading, '') AS HEADING,
            ISNULL(th.subheading, '') AS SUBHEADING,
            dmt.TYPE,
            SUM(den.AMOUNT) AS AMOUNT,
            @Date AS prntdate,
            (
                SELECT ISNULL(umt.User_First_Name, '') + ' ' + ISNULL(umt.User_Last_Name, '')
                FROM DEN_DENGI_RECEIPT_MST_T dmt
                INNER JOIN sec_user_mst_t umt ON dmt.user_id = umt.user_id
                WHERE dmt.DENGI_RECEIPT_ID = (
                        SELECT MAX(dbo.DEN_DENGI_RECEIPT_MST_T.DENGI_RECEIPT_ID)
                        FROM DEN_DENGI_RECEIPT_MST_T
                        WHERE DR_DATE = CONVERT(DATETIME, @Date, 103)
                    )
            ) AS USER_NAME,
            'Accounting Period : ' + fy.DISPLAY_VALUE AS ACCOUNTING_PERIOD,
            loc.Location_Name AS LOCATION_NAME,
            cmst.COUNTER_MACHINE_TITLE AS COUNTER_MACHINE_TITLE,
            dept.Department_Name AS DEPT_NAME,
            (
                SELECT MAX(dbo.DEN_DENGI_RECEIPT_MST_T.DENGI_RECEIPT_ID)
                FROM DEN_DENGI_RECEIPT_MST_T
                WHERE DR_DATE = CONVERT(DATETIME, @Date, 103)
            ) AS LAST_RECEIPT_NO,
            (
                SELECT MIN(dbo.DEN_DENGI_RECEIPT_MST_T.DENGI_RECEIPT_ID)
                FROM DEN_DENGI_RECEIPT_MST_T
                WHERE DR_DATE = CONVERT(DATETIME, @Date, 103)
            ) AS FIRST_RECEIPT_NO
        FROM dbo.DEN_DENGI_MASTER_T dmt
        INNER JOIN dbo.DEN_DENGI_RECEIPT_MST_T den ON dmt.DENGI_MST_ID = den.DENGI_MST_ID
        INNER JOIN COM_LOCATION_MST_T loc ON den.LOC_ID = loc.LOCATION_ID
        INNER JOIN COM_COUNTER_MACHINE_MST_T cmst ON den.CTR_MACH_ID = cmst.CTR_MACH_ID
        INNER JOIN COM_DEPARTMENT_MST_T dept ON cmst.DEPT_ID = dept.DEPARTMENT_ID
        INNER JOIN COM_FINANCIAL_YEAR_MST_T fy ON fy.Financial_Year_Id = den.FY_ID
        CROSS JOIN tbl_print_heading th
        WHERE
            (@CtrMachId = 0 OR den.CTR_MACH_ID = @CtrMachId)
            AND (@Date = '' OR den.DR_DATE = CONVERT(DATETIME, @Date, 103))
            AND dmt.TYPE IN ('Dengi', 'Imarat Nidhi')
            AND PAYMENT_TYPE_ID = 7
        GROUP BY
		th.heading,th.subheading,
            loc.Location_Name,
            cmst.COUNTER_MACHINE_TITLE,
            dmt.TYPE,
            dept.Department_Name,
            fy.DISPLAY_VALUE;
END
GO

ALTER PROCEDURE [dbo].[SP_GetDailyDengi]
    @strDate NVARCHAR(50),
    @intCtrMachId BIGINT
AS
BEGIN
        SELECT 
            ISNULL((SELECT HEADING FROM TBL_PRINT_HEADING), '') AS HEADING,
            ISNULL((SELECT SUBHEADING FROM TBL_PRINT_HEADING), '') AS SUBHEADING,
            DMT.TYPE,
            SUM(den.AMOUNT) AS AMOUNT,
            @strDate AS prntdate,
            ISNULL(UMT.User_First_Name, '') + ' ' + ISNULL(UMT.User_Last_Name, '') AS USER_NAME,
            'Accounting Period : ' + FY.DISPLAY_VALUE AS ACCOUNTING_PERIOD,
            LOC.Location_Name AS LOCATION_NAME,
            CMST.COUNTER_MACHINE_TITLE AS COUNTER_MACHINE_TITLE,
            DEPT.Department_Name AS DEPT_NAME,
            (SELECT MAX(dbo.DEN_DENGI_RECEIPT_MST_T.DENGI_RECEIPT_ID) FROM DEN_DENGI_RECEIPT_MST_T WHERE DR_DATE = CONVERT(DATETIME, @strDate, 103)) AS LAST_RECEIPT_NO,
            (SELECT MIN(dbo.DEN_DENGI_RECEIPT_MST_T.DENGI_RECEIPT_ID) FROM DEN_DENGI_RECEIPT_MST_T WHERE DR_DATE = CONVERT(DATETIME, @strDate, 103)) AS FIRST_RECEIPT_NO
        FROM
            dbo.DEN_DENGI_MASTER_T DMT
        INNER JOIN
            dbo.DEN_DENGI_RECEIPT_MST_T DEN ON DMT.DENGI_MST_ID = DEN.DENGI_MST_ID
        INNER JOIN
            COM_LOCATION_MST_T LOC ON DEN.LOC_ID = LOC.LOCATION_ID
        INNER JOIN
            COM_COUNTER_MACHINE_MST_T CMST ON DEN.CTR_MACH_ID = CMST.CTR_MACH_ID
        INNER JOIN
            COM_DEPARTMENT_MST_T DEPT ON CMST.DEPT_ID = DEPT.DEPARTMENT_ID
        INNER JOIN
            COM_FINANCIAL_YEAR_MST_T FY ON FY.Financial_Year_Id = DEN.FY_ID
        LEFT JOIN
            sec_user_mst_t UMT ON DEN.user_id = UMT.user_id
        WHERE
            (DEN.CTR_MACH_ID = @intCtrMachId OR @intCtrMachId = 0)
            AND (DR_DATE = CONVERT(DATETIME, @strDate, 103) OR @strDate = '')
        GROUP BY
            LOC.Location_Name, CMST.COUNTER_MACHINE_TITLE, DMT.TYPE, DEPT.Department_Name, FY.DISPLAY_VALUE,ISNULL(UMT.User_First_Name, '') + ' ' + ISNULL(UMT.User_Last_Name, '');

END
GO

ALTER PROCEDURE [dbo].[SP_GetDailyDengiUserNames]
    @strDate NVARCHAR(50),
    @intCtrMachId BIGINT
AS
BEGIN
        SELECT DISTINCT ISNULL(MST.ENTERED_BY, '') AS ENTERED_BY
        FROM MESS_PRINT_RECEIPT_MST_T MST
        WHERE
            (MST.CTR_MACH_ID = @intCtrMachId OR @intCtrMachId = 0)
            AND (MST.PR_DATE = CONVERT(DATETIME, @strDate, 103) OR @strDate = '');
        
END
GO

ALTER PROCEDURE [dbo].[SP_GetGameDailyReceiptData]
    @strFDate NVARCHAR(50),
    @intCtrMachId BIGINT
AS
BEGIN
        SELECT
            NULL AS Heading,
            NULL AS subheading,
            FIRST_RECEIPT_NO,
            LAST_RECEIPT_NO,
            TOTAL_AMOUNT as AMOUNT,
            COUNTER_MACHINE_TITLE,
            ISNULL(USER_NAME, '') AS USERNAME,
            CASE
                WHEN ISNULL(Location_Name, '') = '' THEN ''
                ELSE 'Location :' + Location_Name
            END AS Location_Name,
            CASE
                WHEN ISNULL(Department_Name, '') = '' THEN ''
                ELSE 'Sublocation :' + Department_Name
            END AS Department_Name,
            @strFDate AS prntdate,
            'Accounting Period : ' + DISPLAY_VALUE AS AccountingPeriod
        FROM
            GAM_GAME_DAILY_RECEIPT_LIST
        WHERE
            (@intCtrMachId = 0 OR CTR_MACH_ID = @intCtrMachId)
            AND (@strFDate = '' OR GM_DATE = CONVERT(DATETIME, @strFDate, 103));

END
GO

ALTER PROCEDURE [dbo].[SP_GetGameUserWiseData]
    @strFDate NVARCHAR(50),
    @strTDate NVARCHAR(50),
    @intCtrMachId BIGINT
AS
BEGIN
        SELECT DISTINCT
            NULL AS Heading,
            NULL AS subheading,
            CMT.COUNTER_MACHINE_TITLE,
            ISNULL(UMT.User_First_Name, '') + ' ' + ISNULL(UMT.User_Last_Name, '') AS UserName,
            SUM(ISNULL(TDT.AMOUNT, 0)) AS TOTAL_AMOUNT,
            COUNT(ISNULL(TDT.GAME_DET_ID, 0)) AS TOTAL_RECEIPT,
            @strFDate + ' to ' + @strTDate AS period,
            'Accounting Period : ' + FY.DISPLAY_VALUE AS AccountingPeriod,
            SUM((ISNULL(TDT.ADULT_NO, 0) + ISNULL(TDT.CHILD_NO, 0) + ISNULL(TDT.TRIP_NO, 0))) AS USER_COUNT
        FROM
            GAM_GAME_DET_T TDT
        JOIN
            COM_COUNTER_MACHINE_MST_T CMT ON TDT.CTR_MACH_ID = CMT.CTR_MACH_ID
        JOIN
            COM_FINANCIAL_YEAR_MST_T FY ON FY.Financial_Year_Id = TDT.FY_ID
        JOIN
            sec_user_mst_t UMT ON UMT.User_Id = TDT.USER_ID
        WHERE
            (@intCtrMachId = 0 OR TDT.CTR_MACH_ID = @intCtrMachId)
            AND (@strFDate = '' OR TDT.GM_DATE >= CONVERT(DATETIME, @strFDate, 103))
            AND (@strTDate = '' OR TDT.GM_DATE <= CONVERT(DATETIME, @strTDate, 103))
        GROUP BY
            CMT.COUNTER_MACHINE_TITLE, FY.DISPLAY_VALUE, TDT.USER_ID, UMT.User_Last_Name, UMT.User_First_Name;

END
GO



-- Darshan Report end