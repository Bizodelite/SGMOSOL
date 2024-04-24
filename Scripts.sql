-- darshan Bhakt niwas start



GO
PRINT N'Altering Procedure [dbo].[SP_GetMaxSerialNo]...';


GO
ALTER PROCEDURE [SP_GetMaxSerialNo]
    @lngCtrMachId BIGINT = 0,
    @lngComId BIGINT = 0,
    @lngLocId BIGINT = 0,
    @lngDeptId BIGINT = 0,
    @lngFYId BIGINT = 0
AS
BEGIN
    SELECT ISNULL(MAX(MST.SERIAL_NO), 0) AS SerialNo
    FROM LOCK_LOCKER_CHECK_IN_MST_T MST
    WHERE 1 = 1
        AND (@lngComId = 0 OR MST.COM_ID = @lngComId)
        AND (@lngLocId = 0 OR MST.LOC_ID = @lngLocId)
        AND (@lngDeptId = 0 OR MST.DEPT_ID = @lngDeptId)
        AND (@lngFYId = 0 OR MST.FY_ID = @lngFYId);
END
GO
PRINT N'Altering Procedure [dbo].[SP_GetRoomCheckInMstId]...';


GO
ALTER PROCEDURE SP_GetRoomCheckInMstId
    @MachineName NVARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CHECK_IN_MST_ID AS CheckInMstId,
           SERIAL_NO AS SerialNo,
           Entered_On AS EnteredOn
    FROM BN_ROOM_CHECK_IN_MST_T
    WHERE MACHINE_NAME = @MachineName OR @MachineName = ''
    ORDER BY CHECK_IN_MST_ID DESC;
END
GO
PRINT N'Altering Procedure [dbo].[SP_GetTIDByCounterId]...';


GO
ALTER PROCEDURE [dbo].[SP_GetTIDByCounterId]
    @CounterId INT
AS
BEGIN
    SELECT isnull(tidNo,'') AS tidNo, isnull(Tid,0) AS Tid 
    FROM COM_TID_MST_T
    WHERE counter_id = @CounterId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_CheckRoomAvailability]...';


GO
CREATE PROCEDURE SP_CheckRoomAvailability
    @RoomId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT AVAILABLE_STATUS
    FROM BN_ROOM_MST_T
    WHERE ROOM_ID = @RoomId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_DeleteDamagedRoom]...';


GO
CREATE PROCEDURE SP_DeleteDamagedRoom
    @RoomId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM BN_DAMAGED_ROOMS
    WHERE ROOM_ID = @RoomId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_DeleteRoomCheckInDet]...';


GO
CREATE PROCEDURE SP_DeleteRoomCheckInDet
    @CheckInMstId INT,
    @LockerId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM BN_ROOM_CHECK_IN_DET_T
    WHERE CHECK_IN_MST_ID = @CheckInMstId
      AND ROOM_ID = @LockerId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_DeleteRoomCheckOutDet]...';


GO
CREATE PROCEDURE SP_DeleteRoomCheckOutDet
    @CheckOutDetId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM BN_ROOM_CHECK_OUT_DET_T
    WHERE CHECK_OUT_DET_ID = @CheckOutDetId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_DeleteRoomLocked]...';


GO
CREATE PROCEDURE SP_DeleteRoomLocked
    @BookingId NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM BN_ROOM_LOCK_T
    WHERE BookingID = @BookingId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_findRoom]...';


GO
CREATE PROCEDURE SP_findRoom
    @CheckInMstId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ROOM_ID
    FROM BN_ROOM_CHECK_IN_DET_T
    WHERE CHECK_IN_MST_ID = @CheckInMstId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetAnnadan]...';


GO
CREATE PROCEDURE SP_GetAnnadan
    @bhaktid INT,
    @locId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT UPPER(Fullname) AS Name,
           Mobile,
           City,
           NoOfDays,
           Donner_Id AS DonnerId
    FROM BN_ROOM_DONNER_T
    WHERE BHAKT_TYPE = @bhaktid
      AND LOC_ID = @locId
    ORDER BY Donner_Id;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetAuthPersons]...';


GO
CREATE PROCEDURE SP_GetAuthPersons
AS
BEGIN
    SET NOCOUNT ON;

    SELECT id AS Id,
           UPPER(Name) AS Name
    FROM BN_AUTHORISED_PERSON_DETAIL_T;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetBedCheckInMstId]...';


GO
CREATE PROCEDURE [SP_GetBedCheckInMstId]
    @MachineName NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT CHECK_OUT_MST_ID AS CheckInMstId,
           SERIAL_NO AS SerialNo,
           Entered_On AS EnteredOn
    FROM BED_CHECK_OUT_MST_T
    WHERE (@MachineName IS NULL OR MACHINE_NAME = @MachineName)
    ORDER BY CHECK_OUT_MST_ID DESC;
END;
GO
PRINT N'Creating Procedure [dbo].[SP_GetCheckOutWarningGrid]...';


GO
CREATE PROCEDURE SP_GetCheckOutWarningGrid
    @CtrMachId INT,
    @Date VARCHAR(20),
    @LocId INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IMST.SERIAL_NO,
           (SELECT Department_Short_Name FROM COM_DEPARTMENT_MST_T DEPT WHERE DEPT.Department_Id = IMST.SUBLOC_ID) AS DEPT_NAME,
           ROOM_NAME,
           NAME,
           ISNULL(MOB_NO, '') AS MOB_NO,
           CONVERT(VARCHAR, IN_DATE, 103) AS DATE,
           CONVERT(VARCHAR, IN_TIME, 108) AS TIME,
           DAYS,
           DATEDIFF(HOUR, CAST(OUT_DATE AS DATE) + CAST(OUT_TIME AS DATETIME), GETDATE()) AS DATDIFF,
           RENT + IMST.ADVANCE AS AMOUNT
    FROM BN_ROOM_CHECK_IN_MST_T IMST
    INNER JOIN BN_ROOM_CHECK_IN_DET_T IDET ON IMST.CHECK_IN_MST_ID = IDET.CHECK_IN_MST_ID
    INNER JOIN BN_ROOM_MST_T LMT ON LMT.ROOM_ID = IDET.ROOM_ID
    WHERE IMST.CHECK_IN_MST_ID NOT IN (SELECT CHECK_IN_MST_ID FROM BN_ROOM_CHECK_OUT_MST_T)
      AND DATEADD(DAY, IMST.DAYS, (IMST.IN_DATE + IMST.IN_TIME)) < DATEADD(HOUR, 2, GETDATE())
      AND IMST.LOC_ID = @LocId
    ORDER BY IMST.CHECK_IN_MST_ID;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDataForFillingFromOnline]...';


GO
CREATE PROCEDURE SP_GetDataForFillingFromOnline
    @Id NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT
        BMT.ROOM_BOOKING_MST_ID,
        REG.REG_FNAME + ' ' + REG.REG_MNAME + ' ' + REG.REG_LNAME AS Name,
        REG.REG_MOBILE,
        BMT.SERIAL_NO,
        BDT.NO_OF_DAYS,
        BMT.NO_OF_PERSON,
        BMT.NO_OF_ROOM,
        Address.PER_ADDRESS_LINE1
    FROM
        BNOL_ROOM_BOOKING_MST_T BMT
    INNER JOIN
        BNOL_ROOM_BOOKING_DET_T BDT ON BMT.ROOM_BOOKING_MST_ID = BDT.ROOM_BOOKING_MST_ID
    INNER JOIN
        BNOL_REG_T REG ON REG.REG_ID = BMT.REG_ID
    INNER JOIN
        BNOL_REG_ADDRESS_T Address ON Address.REG_ID = BMT.REG_ID
    WHERE
        BMT.SERIAL_NO = @Id;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDaysForAnnadan]...';


GO
CREATE PROCEDURE SP_GetDaysForAnnadan
    @DonerId SMALLINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT do.NoOfDays - ISNULL(SUM(co.days * co.no_of_rooms), 0) AS NoOfDays
    FROM BN_ROOM_CHECK_IN_MST_T co
    RIGHT JOIN BN_ROOM_DONNER_T do ON co.Donner_Id = do.Donner_Id
    WHERE do.BHAKT_TYPE = 5 AND do.Donner_Id = @DonerId
    GROUP BY do.NoOfDays;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDaysForDonner]...';


GO
CREATE PROCEDURE SP_GetDaysForDonner
    @RoomId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ISNULL(D_NoOfRooms, 0) AS NoOfDays
    FROM BN_ROOM_MST_T
    WHERE ROOM_ID = @RoomId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDaysForDonnerextend]...';


GO
CREATE PROCEDURE SP_GetDaysForDonnerextend
    @SerialNo BIGINT,
    @CheckInId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ISNULL(mst.D_NoOfRooms, 0) AS NoOfDays
    FROM BN_ROOM_MST_T mst
    INNER JOIN BN_ROOM_CHECK_IN_MST_T cmst ON mst.DONER_ID = cmst.Donner_Id
    WHERE cmst.SERIAL_NO = @SerialNo
      AND cmst.CHECK_IN_MST_ID = @CheckInId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDmgedRoomsForGrid]...';


GO
CREATE PROCEDURE SP_GetDmgedRoomsForGrid
    @CtrMachId INT,
    @LocID INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ROOM_NAME,
        CONVERT(VARCHAR, DATE, 103) AS DATE,
        CONVERT(VARCHAR, IMST.ENTERED_ON, 108) AS TIME,
        IMST.ENTERED_BY,
        REASON
    FROM
        BN_DAMAGED_ROOMS IMST
    INNER JOIN
        BN_ROOM_MST_T IDET ON IMST.ROOM_ID = IDET.ROOM_ID
    WHERE
        AVAILABLE_STATUS = 191
        AND (@LocID = 0 OR IDET.LOC_ID = @LocID);
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDonnerRoomId]...';


GO
CREATE PROCEDURE SP_GetDonnerRoomId
    @CheckInMstId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ISNULL(Donner_room_Id, 0) as Donner_room_Id
    FROM BN_ROOM_CHECK_IN_MST_T
    WHERE CHECK_IN_MST_ID = @CheckInMstId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDonners]...';


GO
CREATE PROCEDURE SP_GetDonners
    @bhaktid INT,
    @locId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
         isnull(ROOM_NAME,'') as ROOM_NAME,upper(Fullname) as Name 
        , Mobile
        , City
        , NoOfDays
        , ROOM_ID,Donner_Id as DonnerId
         FROM 
         BN_ROOM_DONNER_T MST inner join BN_ROOM_MST_T RMST
         ON MST.Donner_Id=RMST.DONER_ID
         WHERE 1=1 
         and BHAKT_TYPE = @bhaktid
         and RMST.LOC_ID = @locId
         ORDER BY MST.Donner_Id,ROOM_ID
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDrRoomChangeMst]...';


GO
CREATE PROCEDURE SP_GetDrRoomChangeMst
    @lngLockerCheckInMstId BIGINT = 0,
    @strDate VARCHAR(20) = '',
    @lngSerialNo VARCHAR(50) = '',
    @lngComId BIGINT = 0,
    @lngLocId BIGINT = 0,
    @lngFYId BIGINT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT MST.CHECK_IN_MST_ID AS CheckInMstId,
           MST.COM_ID AS ComId,
           MST.LOC_ID AS LocId,
           MST.DEPT_ID AS DeptId,
           MST.FY_ID AS FyId,
           MST.IN_DATE AS InDate,
           CONVERT(DATETIME, MST.IN_TIME, 108) AS InTime,
           MST.SERIAL_NO AS SerialNo,
           MST.BHAKT_TYPE AS BhaktType,
           MST.APP_NO AS AppNo,
           MST.NAME AS Name,
           MST.PLACE AS Place,
           ISNULL(MST.BARCODE, '') AS BARCODE,
           ISNULL(MST.MOB_NO, '') AS MOB_NO,
           MST.DAYS AS Days,
           MST.NO_OF_ROOMS AS NoOfRooms,
           MST.OUT_DATE AS OutDate,
           CONVERT(DATETIME, MST.OUT_TIME, 108) AS OutTime,
           MST.ADVANCE AS Advance,
           MST.RENT AS Rent,
           MST.SERVER_NAME AS ServerName,
           MST.Entered_By AS EnteredBy,
           MST.Entered_On AS EnteredOn,
           MST.Modified_By AS ModifiedBy,
           MST.Modified_On AS ModifiedOn,
           MST.Machine_Name AS MachineName,
           MST.USER_ID AS UserId,
           ISNULL(MST.Record_Modified_Count, 0) AS RecordModifiedCount,
           ISNULL(SUBLOC_ID, 0) AS SUBLOC_ID,
           ISNULL(MST.SUBLOCATION, '') AS SUBLOCATION,
           ISNULL(MST.IsOnline, '') AS IsOnline,
           ISNULL((
               SELECT COUNT(*)
               FROM BN_ROOM_CHECK_IN_DET_T CINDET
               WHERE CINDET.CHECK_IN_MST_ID = MST.CHECK_IN_MST_ID
                 AND ROOM_ID NOT IN (
                     SELECT ROOM_ID
                     FROM BN_ROOM_CHECK_OUT_DET_T CODET
                     INNER JOIN BN_ROOM_CHECK_OUT_MST_T COMST ON CODET.CHECK_OUT_MST_ID = COMST.CHECK_OUT_MST_ID
                     WHERE COMST.CHECK_IN_MST_ID = MST.CHECK_IN_MST_ID
                 )
           ), 0) AS PendRoomCount
    FROM BN_ROOM_CHECK_IN_MST_T MST
    WHERE 1 = 1
      AND (@lngLockerCheckInMstId = 0 OR CHECK_IN_MST_ID = @lngLockerCheckInMstId)
      AND (@lngComId = 0 OR COM_ID = @lngComId)
      AND (@lngLocId = 0 OR LOC_ID = @lngLocId)
      AND (@lngFYId = 0 OR FY_ID = @lngFYId)
      AND (@strDate = '' OR IN_DATE = @strDate)
      AND (@lngSerialNo = '' OR SERIAL_NO = @lngSerialNo)
    ORDER BY IN_DATE DESC, IN_TIME DESC, SERIAL_NO DESC;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDrRoomCheckChangeInDet]...';


GO
CREATE PROCEDURE SP_GetDrRoomCheckChangeInDet
    @lngRoomCheckInMstId BIGINT = 0,
    @lngCtrMachId BIGINT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT L.ROOM_ID AS RoomId,
           L.ROOM_NAME AS RoomName,
           ISNULL(L.Record_Modified_Count, 0) AS RecordModifiedCount
    FROM BN_ROOM_CHECK_IN_DET_T DET
    INNER JOIN BN_ROOM_MST_T L ON DET.ROOM_ID = L.ROOM_ID
    WHERE 1 = 1
      AND (@lngRoomCheckInMstId = 0 OR DET.CHECK_IN_MST_ID = @lngRoomCheckInMstId)
      AND L.ROOM_ID NOT IN (
          SELECT ROOM_ID
          FROM BN_ROOM_CHECK_OUT_DET_T CODET
          INNER JOIN BN_ROOM_CHECK_OUT_MST_T COMST ON CODET.CHECK_OUT_MST_ID = COMST.CHECK_OUT_MST_ID
          WHERE COMST.CHECK_IN_MST_ID = @lngRoomCheckInMstId
      )
      --AND (@lngCtrMachId = 0 OR L.CTR_MACH_ID = @lngCtrMachId)
    GROUP BY L.ROOM_ID, L.ROOM_NAME, L.Record_Modified_Count
    ORDER BY L.ROOM_NAME;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDrRoomCheckInDet]...';


GO
CREATE PROCEDURE SP_GetDrRoomCheckInDet
    @lngRoomCheckInMstId BIGINT = 0,
    @blnAll BIT = 0,
    @lngDeptId BIGINT = 0
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sqlQuery NVARCHAR(MAX)

    SET @sqlQuery = 'SELECT CHECK_IN_DET_ID AS RoomCheckInDetId, ' +
                    'CHECK_IN_MST_ID AS RoomCheckInMstId, ' +
                    'L.ROOM_ID AS RoomId, ' +
                    'L.ROOM_NAME AS RoomName, ' +
                    'ISNULL(L.Record_Modified_Count, 0) AS RecordModifiedCount ' +
                    'FROM BN_ROOM_CHECK_IN_DET_T DET '

    IF @blnAll = 1
        SET @sqlQuery = @sqlQuery + 'RIGHT JOIN BN_ROOM_MST_T L ON DET.ROOM_ID = L.ROOM_ID '
    ELSE
        SET @sqlQuery = @sqlQuery + 'INNER JOIN BN_ROOM_MST_T L ON DET.ROOM_ID = L.ROOM_ID '

    SET @sqlQuery = @sqlQuery + 'WHERE 1=1 '

    IF @lngDeptId <> 0
        SET @sqlQuery = @sqlQuery + 'AND L.DEPT_ID = @lngDeptId '

    IF @lngRoomCheckInMstId <> 0
    BEGIN
        SET @sqlQuery = @sqlQuery + 'AND (CHECK_IN_MST_ID = @lngRoomCheckInMstId '

        IF @blnAll = 1
            SET @sqlQuery = @sqlQuery + 'OR (ISNULL(CHECK_IN_MST_ID, 0) = 0 AND L.AVAILABLE_STATUS = 177)) '
        ELSE
            SET @sqlQuery = @sqlQuery + ') '
    END

    SET @sqlQuery = @sqlQuery + 'ORDER BY DET.CHECK_IN_MST_ID DESC, L.ROOM_NAME'

    EXEC sp_executesql @sqlQuery,
                       N'@lngRoomCheckInMstId BIGINT, @blnAll BIT, @lngDeptId BIGINT',
                       @lngRoomCheckInMstId = @lngRoomCheckInMstId,
                       @blnAll = @blnAll,
                       @lngDeptId = @lngDeptId
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDrRoomCheckInMst]...';


GO
CREATE PROCEDURE SP_GetDrRoomCheckInMst
    @lngRoomCheckInMstId INT = 0,
    @strDate NVARCHAR(50) = '',
    @lngSerialNo INT = 0,
    @lngCtrMachId INT = 0,
    @lngComId INT = 0,
    @lngLocId INT = 0,
    @lngDeptId INT = 0,
    @lngFYId INT = 0,
    @strUserName NVARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;

    SELECT MST.CHECK_IN_MST_ID AS CheckInMstId,
           MST.COM_ID AS ComId,
           MST.LOC_ID AS LocId,
           MST.DEPT_ID AS DeptId,
           MST.FY_ID AS FyId,
           MST.IN_DATE AS InDate,
           CONVERT(datetime, MST.IN_TIME, 108) AS InTime,
           MST.SERIAL_NO AS SerialNo,
           ISNULL(MST.APP_NO, 0) AS AppNo,
           ISNULL(MST.BARCODE, '') AS BARCODE,
           MST.NAME AS Name,
           MST.IMAGE AS IMAGE,
           MST.ScanImageName AS ScanImageName,
           MST.IsOnline AS IsOnline,
           MST.BookingID AS BookingID,
           MST.PLACE AS Place,
           MST.COUNTRY_ID AS COUNTRY_ID,
           MST.COUNTRY_NAME AS COUNTRY_NAME,
           MST.STATE_ID AS STATE_ID,
           MST.STATE AS STATE,
           MST.DISTRICT_ID AS DISTRICT_ID,
           MST.DISTRICT AS DISTRICT,
           MST.BHAKT_TYPE AS BHAKT_TYPE,
           MST.Donner_Id AS Donner_Id,
           (SELECT CONVERT(datetime, '12/31/9998 00:00:00', 101)) AS MaxDate,
           DATEDIFF(MINUTE, in_date + in_time, GETDATE()) AS CALC_HOUR,
           ISNULL(MST.MOB_NO, '') AS MOB_NO,
           MST.DAYS AS Days,
           ISNULL(MST.VEHICLE_NO, '') AS VEHICLE_NO,
           MST.NO_OF_ROOMS AS NoOfRooms,
           ISNULL(MST.NO_OF_PERSONS, 0) AS NoOfPersons,
           MST.OUT_DATE AS OutDate,
           CONVERT(datetime, MST.OUT_TIME, 108) AS OutTime,
           MST.ADVANCE AS Advance,
           MST.RENT AS Rent,
           MST.SUBLOC_ID AS SUBLOC_ID,
           MST.SUBLOCATION AS SUBLOCATION,
           MST.SERVER_NAME AS ServerName,
           ISNULL(MST.REMARK, '') AS REMARK,
           MST.Entered_By AS EnteredBy,
           MST.Entered_On AS EnteredOn,
           MST.Modified_By AS ModifiedBy,
           MST.Modified_On AS ModifiedOn,
           MST.Machine_Name AS MachineName,
           MST.USER_ID AS UserId,
           ISNULL(COMTD.tidNo, '') AS tidNo,
           MST.Record_Modified_Count AS RecordModifiedCount,
           ISNULL(CTDT.Token_Detail_Name, '') AS payment_name,
           ISNULL(MST.Tid, 0) AS Tid,
           ISNULL(MST.PaymentType, 0) AS PaymentType,
           ISNULL(MST.Invoice, '') AS Invoice,
           ISNULL(MST.CHQ_BANK_NAME, '') AS CHQ_BANK_NAME,
           ISNULL(MST.CHQ_NO, '') AS CHQ_NO,
           MST.CHQ_DATE AS CHQ_DATE,
           ISNULL(MST.KYCDocType, '') AS KycDocType,
           ISNULL(MST.KYCDocDetail, '') AS KycDocDetail,
           ISNULL((
               SELECT COUNT(*)
               FROM BN_ROOM_CHECK_IN_DET_T CINDET
               WHERE CINDET.CHECK_IN_MST_ID = MST.CHECK_IN_MST_ID
                 AND ROOM_ID NOT IN (
                     SELECT ROOM_ID
                     FROM BN_ROOM_CHECK_OUT_DET_T CODET
                     INNER JOIN BN_ROOM_CHECK_OUT_MST_T COMST ON CODET.CHECK_OUT_MST_ID = COMST.CHECK_OUT_MST_ID
                     WHERE COMST.CHECK_IN_MST_ID = MST.CHECK_IN_MST_ID
                 )
           ), 0) AS PendRoomCount
    FROM BN_ROOM_CHECK_IN_MST_T MST
    LEFT OUTER JOIN com_token_det_t CTDT ON MST.PaymentType = CTDT.Token_Detail_Id
    LEFT OUTER JOIN COM_TID_MST_T COMTD ON COMTD.Tid = MST.Tid
    WHERE 1 = 1
      AND (@lngRoomCheckInMstId = 0 OR CHECK_IN_MST_ID = @lngRoomCheckInMstId)
      AND (@lngComId = 0 OR COM_ID = @lngComId)
      AND (@lngLocId = 0 OR MST.LOC_ID = @lngLocId)
      AND (@lngFYId = 0 OR FY_ID = @lngFYId)
      AND (@strDate = '' OR IN_DATE = @strDate)
      AND (@lngSerialNo = 0 OR Serial_No = @lngSerialNo)
    ORDER BY IN_DATE DESC, IN_TIME DESC, SERIAL_NO DESC;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetDrRoomIds]...';


GO
CREATE PROCEDURE SP_GetDrRoomIds
    @DonerId INT,
    @IntDeptId SMALLINT = 0,
    @IntAvailableStatus SMALLINT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Room_Id AS RoomId,
           Room_Name AS RoomName,
           Descr,
           Status,
           AVAILABLE_STATUS AS AvailableStatus,
           ISNULL(RECORD_MODIFIED_COUNT, 0) AS RecordModifiedCount
    FROM BN_ROOM_MST_T
    WHERE (@DonerId = 0 OR DONER_ID = @DonerId)
          AND (@IntDeptId = 0 OR DEPT_ID = @IntDeptId)
          AND (@IntAvailableStatus = 0 OR AVAILABLE_STATUS = @IntAvailableStatus);
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetLastEnteredName]...';


GO
CREATE PROCEDURE SP_GetLastEnteredName
    @intCtrId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 [name] AS LastEnteredName,
                 PLACE
    FROM BN_ROOM_CHECK_IN_MST_T
    WHERE CTR_MACH_ID = @intCtrId
    ORDER BY CHECK_IN_MST_ID DESC;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetMaxRoomCheckOutSerialNo]...';


GO
CREATE PROCEDURE SP_GetMaxRoomCheckOutSerialNo
    @CtrMachId BIGINT,
    @ComId BIGINT,
    @LocId BIGINT,
    @DeptId BIGINT,
    @FYId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT isnull(MAX(SERIAL_NO),0) as SERIAL_NO
    FROM BN_ROOM_CHECK_OUT_MST_T
    WHERE
        (@CtrMachId = 0 OR CTR_MACH_ID = @CtrMachId) AND
        (@ComId = 0 OR COM_ID = @ComId) AND
        (@LocId = 0 OR LOC_ID = @LocId) AND
        (@DeptId = 0 OR DEPT_ID = @DeptId) AND
        (@FYId = 0 OR FY_ID = @FYId);
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRent]...';


GO
CREATE PROCEDURE SP_GetRent
    @Id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ISNULL(RATE, 0) AS RentPerDay, ISNULL(Advance, 0) AS Advance
    FROM BN_ROOM_MST_T
    WHERE ROOM_ID = @Id;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomCheckInMaxAmount]...';


GO
CREATE PROCEDURE SP_GetRoomCheckInMaxAmount
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Max_Amount
    FROM BN_ROOM_CHECK_IN_MAX_AMT_T;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomCheckInMaxSerialNo]...';


GO
CREATE PROCEDURE [SP_GetRoomCheckInMaxSerialNo]
    @lngComId INT = 0,
    @lngLocId INT = 0,
    @lngDeptId INT = 0,
    @lngFYId INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT isnull(MAX(MST.SERIAL_NO),0) AS SerialNo
    FROM BN_ROOM_CHECK_IN_MST_T MST
    WHERE (@lngComId = 0 OR COM_ID = @lngComId)
      AND (@lngLocId = 0 OR LOC_ID = @lngLocId)
      AND (@lngDeptId = 0 OR DEPT_ID = @lngDeptId)
      AND (@lngFYId = 0 OR FY_ID = @lngFYId);
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomCheckOutDet]...';


GO
CREATE PROCEDURE SP_GetRoomCheckOutDet
    @lngRoomCheckOutMstId BIGINT = 0,
    @blnAll BIT = 0,
    @lngCtrMachId BIGINT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CODET.CHECK_OUT_DET_ID AS RoomCheckOutDetId,
        CODET.CHECK_OUT_MST_ID AS RoomCheckOutMstId,
        ISNULL(L.ROOM_ID, 0) AS RoomId,
        ISNULL(L.ROOM_NAME, ' ') AS RoomName,
        ISNULL(L.STATUS, 0) AS RoomStatus,
        ISNULL(L.AVAILABLE_STATUS, 0) AS AvailableStatus,
        ISNULL(L.Record_Modified_Count, 0) AS RecordModifiedCount
    FROM
        BN_ROOM_CHECK_IN_DET_T CIDET
    INNER JOIN
        BN_ROOM_MST_T L ON CIDET.ROOM_ID = L.ROOM_ID
    INNER JOIN
        BN_ROOM_CHECK_OUT_MST_T COMST ON CIDET.CHECK_IN_MST_ID = COMST.CHECK_IN_MST_ID
    RIGHT JOIN
        BN_ROOM_CHECK_OUT_DET_T CODET ON CODET.CHECK_OUT_MST_ID = COMST.CHECK_OUT_MST_ID
                                        AND CODET.ROOM_ID = CIDET.ROOM_ID
    WHERE
        (@lngRoomCheckOutMstId = 0 OR COMST.CHECK_OUT_MST_ID = @lngRoomCheckOutMstId OR ISNULL(COMST.CHECK_OUT_MST_ID, 0) = 0)
    ORDER BY
        CODET.CHECK_OUT_DET_ID DESC, L.ROOM_NAME;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomCheckOutMst]...';


GO
CREATE PROCEDURE SP_GetRoomCheckOutMst
    @RoomCheckOutMstId BIGINT,
    @Date DATETIME = null,
    @SerialNo BIGINT,
    @CtrMachId BIGINT,
    @ComId BIGINT,
    @LocId BIGINT,
    @DeptId BIGINT,
    @FYId BIGINT,
    @UserName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        COUT.CHECK_OUT_MST_ID AS CheckOutMstId,
        COUT.COM_ID AS ComId,
        COUT.LOC_ID AS LocId,
        COUT.DEPT_ID AS DeptId,
        COUT.CTR_MACH_ID AS CtrMachId,
        COUT.FY_ID AS FyId,
        COUT.OUT_DATE AS OutDate,
        CONVERT(TIME, COUT.OUT_TIME) AS OutTime,
        COUT.SERIAL_NO AS SerialNo,
        COUT.CHECK_IN_MST_ID AS CheckInMstId,
        COUT.DAYS AS Days,
        COUT.NO_OF_ROOMS AS NoOfLockers,
        COUT.ADVANCE AS Advance,
        COUT.RENT AS Rent,
        COUT.REFUND AS Refund,
        COUT.Record_Modified_Count AS RecordModifiedCount,
        COUT.USER_ID AS UserId,
        COUT.SERVER_NAME AS ServerName,
        COUT.Entered_By AS EnteredBy,
        COUT.Entered_On AS EnteredOn,
        COUT.Modified_By AS ModifiedBy,
        COUT.Modified_On AS ModifiedOn,
        COUT.Machine_Name AS MachineName,
        ISNULL(CIN.MOB_NO, '') AS MOBILE,
        CIN.ADVANCE AS InAdvance,
        CIN.Rent AS InRent,
        CIN.SERIAL_NO AS InSerialNo,
        CIN.IN_DATE AS InDate,
        CONVERT(TIME, CIN.IN_TIME) AS InTime,
        CIN.APP_NO AS AppNo,
        CIN.NAME AS Name,
        CIN.PLACE AS Place,
        CIN.DAYS AS InDays,
        CIN.Record_Modified_Count AS InRecordModifiedCount
    FROM 
        BN_ROOM_CHECK_OUT_MST_T COUT
    INNER JOIN 
        BN_ROOM_CHECK_IN_MST_T CIN ON COUT.CHECK_IN_MST_ID = CIN.CHECK_IN_MST_ID
    WHERE 
        (@RoomCheckOutMstId = 0 OR COUT.CHECK_OUT_MST_ID = @RoomCheckOutMstId) AND
        (@Date is null OR COUT.OUT_DATE = @Date) AND
        (@SerialNo = 0 OR COUT.SERIAL_NO = @SerialNo) AND
        (@CtrMachId = 0 OR COUT.CTR_MACH_ID = @CtrMachId) AND
        (@ComId = 0 OR COUT.COM_ID = @ComId) AND
        (@LocId = 0 OR COUT.LOC_ID = @LocId) AND
        (@DeptId = 0 OR COUT.DEPT_ID = @DeptId) AND
        (@FYId = 0 OR COUT.FY_ID = @FYId) AND
        (@UserName = '' OR COUT.USER_ID = @UserName)
    ORDER BY 
        COUT.OUT_DATE DESC, COUT.OUT_TIME DESC, COUT.SERIAL_NO DESC;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomCheckOutMstId]...';


GO
CREATE PROCEDURE SP_GetRoomCheckOutMstId
    @MachineName NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CHECK_OUT_MST_ID AS CheckOutMstId,
        SERIAL_NO AS SerialNo,
        Entered_On AS EnteredOn
    FROM
        BN_ROOM_CHECK_OUT_MST_T
    WHERE
        MACHINE_NAME = ISNULL(@MachineName, MACHINE_NAME)
    ORDER BY
        CHECK_OUT_MST_ID DESC;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomDataForGrid]...';


GO
CREATE PROCEDURE SP_GetRoomDataForGrid
    @intDeptId INT = 0,
    @Loc_ID INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT IMST.SERIAL_NO,
           (SELECT Department_Short_Name FROM COM_DEPARTMENT_MST_T DEPT WHERE DEPT.Department_Id = IMST.SUBLOC_ID) AS DEPT_NAME,
           ROOM_NAME AS ROOM_NAME,
           NAME,
           ISNULL(mob_no, '') AS mob_no,
           CONVERT(VARCHAR, IN_DATE, 103) AS [DATE],
           CONVERT(VARCHAR, IN_TIME, 108) AS [TIME],
           DATEDIFF(DAY, CAST(IN_DATE AS DATE) + CAST(IN_TIME AS DATETIME), GETDATE()) AS DATDIFF,
           RENT + IMST.ADVANCE AS AMOUNT
    FROM BN_ROOM_CHECK_IN_MST_T IMST
    INNER JOIN BN_ROOM_CHECK_IN_DET_T IDET ON IMST.CHECK_IN_MST_ID = IDET.CHECK_IN_MST_ID
    INNER JOIN BN_ROOM_MST_T LMT ON LMT.ROOM_ID = IDET.ROOM_ID
    WHERE 1 = 1
      AND (@intDeptId = 0 OR IMST.SUBLOC_ID = @intDeptId)
      AND (@Loc_ID = 0 OR IMST.LOC_ID = @Loc_ID)
      AND IMST.CHECK_IN_MST_ID NOT IN (SELECT CHECK_IN_MST_ID FROM BN_ROOM_CHECK_OUT_MST_T)
    ORDER BY IN_DATE;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomDaysLimit]...';


GO
CREATE PROCEDURE SP_GetRoomDaysLimit
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ISNULL(EXPIRY_DAYS, 0) AS EXPIRY_DAYS
    FROM BN_ROOM_SETTING_T;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomDetails]...';


GO
CREATE PROCEDURE SP_GetRoomDetails
    @strRoomSrch NVARCHAR(100) = '',
    @intDeptId INT = 0,
    @intAvailableStatus INT = 0,
    @Loc_ID INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Room_Id AS RoomId,
           Room_Name AS RoomName,
           Descr AS Description,
           Status,
           AVAILABLE_STATUS AS AvailableStatus,
           ISNULL(RECORD_MODIFIED_COUNT, 0) AS RecordModifiedCount
    FROM BN_ROOM_MST_T
    WHERE (@intDeptId = 0 OR DEPT_ID = @intDeptId)
      AND (@strRoomSrch = '' OR Room_Name LIKE @strRoomSrch + '%')
      AND (@Loc_ID = 0 OR LOC_ID = @Loc_ID)
      AND (@intAvailableStatus = 0 OR AVAILABLE_STATUS = @intAvailableStatus)
    ORDER BY RoomName;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomDetailsSummary]...';


GO
CREATE PROCEDURE SP_GetRoomDetailsSummary
    @DeptId INT = 0,
    @LocId INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        (
            SELECT COUNT(ROOM_ID)
            FROM BN_ROOM_MST_T
            WHERE 1=1
                AND ( @DeptId = 0 OR DEPT_ID = @DeptId )
                AND ( @LocId = 0 OR LOC_ID = @LocId )
                AND STATUS = 3
        ) AS TOTAL,
        (
            SELECT COUNT(ROOM_ID)
            FROM BN_ROOM_MST_T
            WHERE 1=1
                AND ( @DeptId = 0 OR DEPT_ID = @DeptId )
                AND ( @LocId = 0 OR LOC_ID = @LocId )
                AND STATUS = 3
                AND AVAILABLE_STATUS = 191
        ) AS TOTAL_DAM,
        (
            SELECT COUNT(ROOM_ID)
            FROM BN_ROOM_MST_T
            WHERE 1=1
                AND ( @DeptId = 0 OR DEPT_ID = @DeptId )
                AND ( @LocId = 0 OR LOC_ID = @LocId )
                AND STATUS = 3
                AND AVAILABLE_STATUS = 178
        ) AS TOTAL_OCCU,
        (
            SELECT COUNT(ROOM_ID)
            FROM BN_ROOM_MST_T
            WHERE 1=1
                AND ( @DeptId = 0 OR DEPT_ID = @DeptId )
                AND ( @LocId = 0 OR LOC_ID = @LocId )
                AND STATUS = 3
                AND AVAILABLE_STATUS = 177
        ) AS TOTAL_AVL,
        (
            SELECT COUNT(ROOM_ID)
            FROM BN_ROOM_CHECK_IN_MST_T B1
            INNER JOIN BN_ROOM_CHECK_IN_DET_T B2 ON B1.CHECK_IN_MST_ID = B2.CHECK_IN_MST_ID
            WHERE B1.BHAKT_TYPE = 1
                AND ( @DeptId = 0 OR SUBLOC_ID = @DeptId )
                AND ( @LocId = 0 OR LOC_ID = @LocId )
                AND B1.CHECK_IN_MST_ID NOT IN (SELECT CHECK_IN_MST_ID FROM BN_ROOM_CHECK_OUT_MST_T WHERE B1.BHAKT_TYPE = 1)
        ) AS TOTAL_BHAKT,
        (
            SELECT COUNT(ROOM_ID)
            FROM BN_ROOM_CHECK_IN_MST_T B1
            INNER JOIN BN_ROOM_CHECK_IN_DET_T B2 ON B1.CHECK_IN_MST_ID = B2.CHECK_IN_MST_ID
            WHERE B1.BHAKT_TYPE = 2
                AND ( @DeptId = 0 OR SUBLOC_ID = @DeptId )
                AND ( @LocId = 0 OR LOC_ID = @LocId )
                AND B1.CHECK_IN_MST_ID NOT IN (SELECT CHECK_IN_MST_ID FROM BN_ROOM_CHECK_OUT_MST_T WHERE B1.BHAKT_TYPE = 2)
        ) AS TOTAL_GUEST,
        (
            SELECT COUNT(ROOM_ID)
            FROM BN_ROOM_CHECK_IN_MST_T B1
            INNER JOIN BN_ROOM_CHECK_IN_DET_T B2 ON B1.CHECK_IN_MST_ID = B2.CHECK_IN_MST_ID
            WHERE B1.BHAKT_TYPE = 3
                AND ( @DeptId = 0 OR SUBLOC_ID = @DeptId )
                AND ( @LocId = 0 OR LOC_ID = @LocId )
                AND B1.CHECK_IN_MST_ID NOT IN (SELECT CHECK_IN_MST_ID FROM BN_ROOM_CHECK_OUT_MST_T WHERE B1.BHAKT_TYPE = 3)
        ) AS TOTAL_GUEST1,
        (
            SELECT COUNT(ROOM_ID)
            FROM BN_ROOM_CHECK_IN_MST_T B1
            INNER JOIN BN_ROOM_CHECK_IN_DET_T B2 ON B1.CHECK_IN_MST_ID = B2.CHECK_IN_MST_ID
            WHERE B1.BHAKT_TYPE = 4
                AND ( @DeptId = 0 OR SUBLOC_ID = @DeptId )
                AND ( @LocId = 0 OR LOC_ID = @LocId )
                AND B1.CHECK_IN_MST_ID NOT IN (SELECT CHECK_IN_MST_ID FROM BN_ROOM_CHECK_OUT_MST_T WHERE B1.BHAKT_TYPE = 4)
        ) AS TOTAL_DONNER,
        (
            SELECT COUNT(ROOM_ID)
            FROM BN_ROOM_CHECK_IN_MST_T B1
            INNER JOIN BN_ROOM_CHECK_IN_DET_T B2 ON B1.CHECK_IN_MST_ID = B2.CHECK_IN_MST_ID
            WHERE B1.BHAKT_TYPE = 5
                AND ( @DeptId = 0 OR SUBLOC_ID = @DeptId )
                AND ( @LocId = 0 OR LOC_ID = @LocId )
                AND B1.CHECK_IN_MST_ID NOT IN (SELECT CHECK_IN_MST_ID FROM BN_ROOM_CHECK_OUT_MST_T WHERE B1.BHAKT_TYPE = 5)
        ) AS TOTAL_GUEST2;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomLimit]...';


GO
CREATE PROCEDURE SP_GetRoomLimit
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ISNULL(MAX_ROOMS, 0) AS MAX_ROOMS
    FROM BN_ROOM_SETTING_T;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomListDetails]...';


GO
CREATE PROCEDURE SP_GetRoomListDetails
    @DeptId INT = 0,
    @ActiveInactiveStatus INT = 0,
    @AvailableStatus INT = 0,
    @LocId INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        MST.ROOM_ID AS RoomId,
        MST.ROOM_Name AS RoomName,
        ISNULL(BCT.NAME, '') AS CAT_NAME
    FROM
        BN_ROOM_MST_T MST
    INNER JOIN
        BN_ROOM_CATEGORY_T BCT ON MST.ROOM_CAT_ID = BCT.ROOM_CAT_ID
    WHERE
        ( @DeptId = 0 OR MST.DEPT_ID = @DeptId )
        AND ( @ActiveInactiveStatus = 0 OR MST.STATUS = @ActiveInactiveStatus )
        AND ( @AvailableStatus = 0 OR MST.AVAILABLE_STATUS = @AvailableStatus )
        AND ( @LocId = 0 OR MST.LOC_ID = @LocId )
    ORDER BY
        MST.DEPT_ID, BCT.NAME, MST.ROOM_NAME;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomLockedDataforEdit]...';


GO
CREATE PROCEDURE SP_GetRoomLockedDataforEdit
    @strDate VARCHAR(50) = ''
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        RL.ROOM_LOCK_ID,
        RBM.SERIAL_NO,
        RBM.SERIAL_NO AS BookingNo,
        RL.BookingID,
        RM.ROOM_NAME AS Room_Name,
        RL.ROOM_ID
    FROM
        BN_ROOM_LOCK_T RL
    INNER JOIN
        BNOL_ROOM_BOOKING_MST_T RBM ON RBM.ROOM_BOOKING_MST_ID = RL.BookingID
    INNER JOIN
        BNOL_REG_T REG ON REG.REG_ID = RBM.REG_ID
    INNER JOIN
        BN_ROOM_MST_T RM ON RM.ROOM_ID = RL.ROOM_ID
    WHERE
        CONVERT(VARCHAR, RL.LOCK_DATE, 103) = @strDate;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomLockedRecords]...';


GO
CREATE PROCEDURE [dbo].[SP_GetRoomLockedRecords]
    @strDate DateTime
AS
BEGIN
    SET NOCOUNT ON;
	 select  RBDT.ROOM_BOOKING_MST_ID AS ID, RBDT.ROOM_BOOKING_DET_ID,(select ctd.Token_Detail_Name  
   from com_token_det_t ctd where  ctd.Token_Detail_Id  = rbdt.ROOM_CAT_ID ) AS ROOM_CAT ,
      RBDT.ADULT as ADULT,RBDT.CHILD as CHILD, RBDT.NO_OF_ROOM,RBDT.NO_OF_DAYS,RBDT.NO_OF_PERSON,RBDT.CHARGES,
      RBDT.NO_OF_BEDS,BLOC.Location_Name as LOC_NAME,det.Token_Detail_Name as BOOK_STATUS ,
      isnull(REG_IMAGE,'') as REG_IMAGE, RBM.SERIAL_NO AS BOOKING_NO,
        rbdt.ROOM_CAT_ID AS CatID,
       BLOC.Location_Id AS LocID
from
     BNOL_ROOM_BOOKING_MST_T as RBM INNER JOIN BNOL_ROOM_BOOKING_DET_T RBDT on RBM.ROOM_BOOKING_MST_ID = 
    RBDT.ROOM_BOOKING_MST_ID 
    INNER JOIN BNOL_REG_T BRT ON BRT.REG_ID = RBM.REG_ID 
    INNER JOIN COM_LOCATION_MST_T BLOC on BLOC.Location_Id = RBDT.LOC_ID INNER JOIN com_token_det_t det on 
    det.Token_Detail_Id = RBM.BOOK_STATUS INNER JOIN com_token_det_t det1 on det1.Token_Detail_Id = RBM.PaymentMode  
    --SELECT 
    --    RBDT.ROOM_BOOKING_MST_ID AS ID, 
    --    RBDT.ROOM_BOOKING_DET_ID,
    --    ctd.Token_Detail_Name AS ROOM_CAT,
    --    RBDT.ADULT AS ADULT,
    --    RBDT.CHILD AS CHILD,
    --    RBDT.NO_OF_ROOM,
    --    RBDT.NO_OF_DAYS,
    --    RBDT.NO_OF_PERSON,
    --    RBDT.CHARGES,
    --    RBDT.NO_OF_BEDS,
    --    BLOC.Location_Name AS LOC_NAME,
    --    det.Token_Detail_Name AS BOOK_STATUS,
    --    ISNULL(REG_IMAGE, '') AS REG_IMAGE,
    --    RBM.SERIAL_NO AS BOOKING_NO,
    --    rbdt.ROOM_CAT_ID AS CatID,
    --    BLOC.Location_Id AS LocID
    --FROM 
    --    BNOL_ROOM_BOOKING_MST_T AS RBM 
    --INNER JOIN 
    --    BNOL_ROOM_BOOKING_DET_T RBDT ON RBM.ROOM_BOOKING_MST_ID = RBDT.ROOM_BOOKING_MST_ID 
    --INNER JOIN 
    --    BNOL_REG_T BRT ON BRT.REG_ID = RBM.REG_ID 
    --INNER JOIN 
    --    COM_LOCATION_MST_T BLOC ON BLOC.Location_Id = RBDT.LOC_ID 
    --INNER JOIN 
    --    com_token_det_t det ON det.Token_Detail_Id = RBM.BOOK_STATUS 
    --INNER JOIN 
    --    com_token_det_t det1 ON det1.Token_Detail_Id = RBM.PaymentMode  
    --LEFT JOIN 
    --    com_token_det_t ctd ON ctd.Token_Detail_Id = rbdt.ROOM_CAT_ID 
    WHERE 
        RBDT.BOOKING_DATE = @strDate;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetRoomMoreThan3Days]...';


GO
CREATE PROCEDURE SP_GetRoomMoreThan3Days
    @intCtrMachId SMALLINT,
    @strDate VARCHAR(50),
    @LocID INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        IMST.SERIAL_NO,
        DEPT.Department_Short_Name AS DEPT_NAME,
        ROOM_NAME AS ROOM_NAME,
		IMST.NAME as 'BhaktName',
        ISNULL(mob_no, '') AS mob_no,
        CONVERT(VARCHAR, IN_DATE, 103) AS DATE,
        CONVERT(VARCHAR, IN_TIME, 108) AS TIME,
        DATEDIFF(DAY, CAST(IN_DATE AS DATE) + CAST(IN_TIME AS DATETIME), GETDATE()) AS DATDIFF,
        RENT + IMST.ADVANCE AS AMOUNT
    FROM 
        BN_ROOM_CHECK_IN_MST_T IMST
    INNER JOIN 
        BN_ROOM_CHECK_IN_DET_T IDET ON IMST.CHECK_IN_MST_ID = IDET.CHECK_IN_MST_ID
    INNER JOIN 
        BN_ROOM_MST_T LMT ON LMT.ROOM_ID = IDET.ROOM_ID
    INNER JOIN 
        COM_DEPARTMENT_MST_T DEPT ON DEPT.Department_Id = IMST.SUBLOC_ID
    WHERE 
        IMST.CHECK_IN_MST_ID NOT IN (SELECT CHECK_IN_MST_ID FROM BN_ROOM_CHECK_OUT_MST_T)
        AND DATEDIFF(DAY, CAST(IN_DATE AS DATE) + CAST(IN_TIME AS DATETIME), GETDATE()) > 3
        AND IMST.LOC_ID = @LocID
    ORDER BY 
        DATEDIFF(DAY, CAST(IN_DATE AS DATE) + CAST(IN_TIME AS DATETIME), GETDATE()) DESC;
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetSublocations]...';


GO
CREATE PROCEDURE SP_GetSublocations
    @LocationID INT,
    @ModTypeID INT = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Department_Id AS DeptId,
           Department_Short_Name AS Name
    FROM COM_DEPARTMENT_MST_T
    WHERE Location_Id = @LocationID
      AND (@ModTypeID = 0 OR MOD_TYPE = @ModTypeID);
END
GO
PRINT N'Creating Procedure [dbo].[SP_GetValidation]...';


GO
CREATE PROCEDURE SP_GetValidation
    @Id BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ROOM_LOCK_ID
    FROM dbo.BN_ROOM_LOCK_T
    WHERE ROOM_ID = @Id;
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertDamagedRoom]...';


GO
CREATE PROCEDURE SP_InsertDamagedRoom
    @RoomID INT,
    @Reason VARCHAR(255),
    @Date DATETIME,
    @EnteredBy VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_DAMAGED_ROOMS (ROOM_ID, REASON, DATE, ENTERED_BY)
    VALUES (@RoomID, @Reason, @Date, @EnteredBy);
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertIntoRoomHistory]...';


GO
CREATE PROCEDURE SP_InsertIntoRoomHistory
    @RoomCheckInMstId BIGINT,
    @LockerId INT,
    @UserName NVARCHAR(50),
    @MachineName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_ROOM_CHECK_IN_DET_T_HISTORY (CHECK_IN_MST_ID, ROOM_ID, ENTERED_BY, MACHINE_NAME, IsDelete)
    VALUES (@RoomCheckInMstId, @LockerId, @UserName, @MachineName, 0);
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertReprintCheckInReceipt]...';


GO
CREATE PROCEDURE SP_InsertReprintCheckInReceipt
    @CheckInReceiptId INT,
    @UserName NVARCHAR(255),
    @MachineName NVARCHAR(255),
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_REPRINT_CHECKIN_RCPT_T
    (
        CHECK_IN_MST_ID,
        ENTERED_BY,
        USER_ID,
        MACHINE_NAME
    )
    VALUES
    (
        @CheckInReceiptId,
        @UserName,
        @UserId,
        @MachineName
    );
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertRoomChange]...';


GO
CREATE PROCEDURE SP_InsertRoomChange
    @CheckInMstId BIGINT,
    @PrevRoomId INT,
    @Reason NVARCHAR(255),
    @UserId INT,
    @ServerName NVARCHAR(50),
    @EnteredBy NVARCHAR(50),
    @ModifiedBy NVARCHAR(50),
    @MachineName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_ROOM_CHANGE_T
    (
        CHECK_IN_MST_ID,
        PREV_ROOM_ID,
        REASON,
        USER_ID,
        SERVER_NAME,
        ENTERED_BY,
        MODIFIED_BY,
        MACHINE_NAME
    )
    VALUES
    (
        @CheckInMstId,
        @PrevRoomId,
        @Reason,
        @UserId,
        @ServerName,
        @EnteredBy,
        @ModifiedBy,
        @MachineName
    );
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertRoomCheckInDet]...';


GO
CREATE PROCEDURE SP_InsertRoomCheckInDet
    @RoomCheckInMstId INT,
    @LockerId INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_ROOM_CHECK_IN_DET_T (CHECK_IN_MST_ID, ROOM_ID)
    VALUES (@RoomCheckInMstId, @LockerId);
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertRoomCheckInDetHistory]...';


GO
CREATE PROCEDURE SP_InsertRoomCheckInDetHistory
    @RoomCheckInMstId INT,
    @LockerId INT,
    @EnteredBy NVARCHAR(255),
    @MachineName NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_ROOM_CHECK_IN_DET_T_HISTORY (CHECK_IN_MST_ID, ROOM_ID, ENTERED_BY, MACHINE_NAME, IsDelete)
    VALUES (@RoomCheckInMstId, @LockerId, @EnteredBy, @MachineName, 0);
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertRoomCheckInMst]...';


GO
CREATE PROCEDURE [dbo].[SP_InsertRoomCheckInMst]
    @ComId INT,
    @LocId INT,
    @DeptId INT,
    @CtrMachId INT,
    @SublocId INT,
    @Sublocation NVARCHAR(100),
    @DonnerRoomId INT,
    @FyId INT,
    @InDate DATE,
    @InTime TIME,
    @AppNo INT,
    @Name NVARCHAR(100),
    @Image NVARCHAR(100) = null,
    @Place NVARCHAR(100),
    @VehicleNo NVARCHAR(50),
    @Days INT,
    @NoOfRooms INT,
    @NoOfPersons INT,
    @OutDate DATE,
    @OutTime TIME,
    @Advance DECIMAL(18, 2),
    @Rent DECIMAL(18, 2),
    @UserId INT,
    @DonerId INT,
    @BhaktTypeId INT,
    @AuthPersonId INT,
    @ServerName NVARCHAR(100),
    @EnteredBy NVARCHAR(100),
    @ModifiedBy NVARCHAR(100),
    @MachineName NVARCHAR(100),
    @Remark NVARCHAR(200),
    @MobNo NVARCHAR(20),
    @ScanImageName NVARCHAR(100) = null,
    @IsOnline BIT = 0,
    @PaymentType INT,
    @Tid INT,
    @Invoice NVARCHAR(50) = '',
    @Barcode NVARCHAR(50),
    @BookingID INT = 0,
    @CountryID INT,
    @CountryName NVARCHAR(50) = '',
    @StateID INT,
    @State NVARCHAR(50) = '',
    @DistrictID INT,
    @District NVARCHAR(50) = '',
    @CHQBankName NVARCHAR(50) = '',
    @CHQNo NVARCHAR(50) = '',
    @CHQDate DATE = null,
    @KYCDocType NVARCHAR(50) = '',
    @KYCDocDetail NVARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_ROOM_CHECK_IN_MST_T
    (
        COM_ID, LOC_ID, DEPT_ID, CTR_MACH_ID, SUBLOC_ID, SUBLOCATION, Donner_room_Id, FY_ID,
        IN_DATE, IN_TIME, APP_NO, NAME, IMAGE, PLACE, VEHICLE_NO, DAYS, NO_OF_ROOMS, NO_OF_PERSONS,
        OUT_DATE, OUT_TIME, ADVANCE, RENT, USER_ID, Donner_Id, BHAKT_TYPE, AUTH_PER_ID, SERVER_NAME,
        ENTERED_BY, MODIFIED_BY, MACHINE_NAME, REMARK, MOB_NO, ScanImageName, IsOnline, PaymentType,
        Tid, Invoice, Barcode, BookingID, COUNTRY_ID, COUNTRY_NAME, STATE_ID, STATE, DISTRICT_ID,
        DISTRICT, CHQ_BANK_NAME, CHQ_NO, CHQ_DATE, KYCDocType, KYCDocDetail
    )
    VALUES
    (
        @ComId, @LocId, @DeptId, @CtrMachId, @SublocId, @Sublocation, @DonnerRoomId, @FyId,
        @InDate, @InTime, @AppNo, @Name, @Image, @Place, @VehicleNo, @Days, @NoOfRooms, @NoOfPersons,
        @OutDate, @OutTime, @Advance, @Rent, @UserId, @DonerId, @BhaktTypeId, @AuthPersonId, @ServerName,
        @EnteredBy, @ModifiedBy, @MachineName, @Remark, @MobNo, @ScanImageName, @IsOnline, @PaymentType,
        @Tid, @Invoice, @Barcode, @BookingID, @CountryID, @CountryName, @StateID, @State, @DistrictID,
        @District, @CHQBankName, @CHQNo, @CHQDate, @KYCDocType, @KYCDocDetail
    );
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertRoomCheckInMstHistory]...';


GO
CREATE PROCEDURE [dbo].[SP_InsertRoomCheckInMstHistory]
    @ComId INT,
    @LocId INT,
    @DeptId INT,
    @CtrMachId INT,
    @SublocId INT,
    @Sublocation NVARCHAR(255),
    @DonnerRoomId INT,
    @FyId INT,
    @InDate DATE,
    @InTime TIME,
    @AppNo INT,
    @Name NVARCHAR(255),
    @Image NVARCHAR(255) = null,
    @Place NVARCHAR(255),
    @VehicleNo NVARCHAR(50),
    @Days INT,
    @NoOfRooms INT,
    @NoOfPersons INT,
    @OutDate DATE,
    @OutTime TIME,
    @Advance DECIMAL(18,2),
    @Rent DECIMAL(18,2),
    @UserId INT,
    @DonerId INT,
    @BhaktTypeId INT,
    @AuthPersonId INT,
    @ServerName NVARCHAR(100),
    @EnteredBy NVARCHAR(100),
    @ModifiedBy NVARCHAR(100),
    @MachineName NVARCHAR(100),
    @Remark NVARCHAR(MAX),
    @MobNo NVARCHAR(20),
    @ScanImageName NVARCHAR(255)= null,
    @IsOnline BIT = 0,
    @PaymentType INT,
    @Tid INT,
    @Invoice NVARCHAR(50) = '',
    @Barcode NVARCHAR(255),
    @BookingID INT = 0,
    @CountryId INT,
    @CountryName NVARCHAR(100) = '',
    @StateId INT,
    @State NVARCHAR(100) = '',
    @DistrictId INT,
    @District NVARCHAR(100) = '',
    @ChqBankName NVARCHAR(100) = '',
    @ChqNo NVARCHAR(50) = '',
    @IsDelete BIT,
    @CheckInMstId INT,
    @ChqDate DATE = null
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_ROOM_CHECK_IN_MST_T_HISTORY
    (COM_ID, LOC_ID, DEPT_ID, CTR_MACH_ID, SUBLOC_ID, SUBLOCATION, Donner_room_Id, FY_ID, IN_DATE, IN_TIME, APP_NO, NAME, IMAGE, PLACE, VEHICLE_NO, DAYS, NO_OF_ROOMS, NO_OF_PERSONS, OUT_DATE, OUT_TIME, ADVANCE, RENT, USER_ID, Donner_Id, BHAKT_TYPE, AUTH_PER_ID, SERVER_NAME, ENTERED_BY, MODIFIED_BY, MACHINE_NAME, REMARK, MOB_NO, ScanImageName, IsOnline, PaymentType, Tid, Invoice, Barcode, BookingID, COUNTRY_ID, COUNTRY_NAME, STATE_ID, STATE, DISTRICT_ID, DISTRICT, CHQ_BANK_NAME, CHQ_NO, IsDelete, CHECK_IN_MST_ID, CHQ_DATE)
    VALUES
    (@ComId, @LocId, @DeptId, @CtrMachId, @SublocId, @Sublocation, @DonnerRoomId, @FyId, @InDate, @InTime, @AppNo, @Name, @Image, @Place, @VehicleNo, @Days, @NoOfRooms, @NoOfPersons, @OutDate, @OutTime, @Advance, @Rent, @UserId, @DonerId, @BhaktTypeId, @AuthPersonId, @ServerName, @EnteredBy, @ModifiedBy, @MachineName, @Remark, @MobNo, @ScanImageName, @IsOnline, @PaymentType, @Tid, @Invoice, @Barcode, @BookingID, @CountryId, @CountryName, @StateId, @State, @DistrictId, @District, @ChqBankName, @ChqNo, @IsDelete, @CheckInMstId, @ChqDate);
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertRoomCheckOutDet]...';


GO
CREATE PROCEDURE SP_InsertRoomCheckOutDet
    @RoomCheckOutMstId BIGINT,
    @RoomId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_ROOM_CHECK_OUT_DET_T (CHECK_OUT_MST_ID, ROOM_ID)
    VALUES (@RoomCheckOutMstId, @RoomId);
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertRoomCheckOutMst]...';


GO
CREATE PROCEDURE SP_InsertRoomCheckOutMst
    @ComId INT,
    @LocId INT,
    @DeptId INT,
    @CtrMachId INT,
    @FyId INT,
    @OutDate DATE,
    @OutTime TIME,
    @CheckInMstId INT,
    @Days INT,
    @NoOfRooms INT,
    @Advance DECIMAL(18, 2),
    @Rent DECIMAL(18, 2),
    @Refund DECIMAL(18, 2),
    @UserId INT,
    @ServerName NVARCHAR(100),
    @EnteredBy NVARCHAR(100),
    @ModifiedBy NVARCHAR(100),
    @MachineName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_ROOM_CHECK_OUT_MST_T
    (
        COM_ID,
        LOC_ID,
        DEPT_ID,
        CTR_MACH_ID,
        FY_ID,
        OUT_DATE,
        OUT_TIME,
        CHECK_IN_MST_ID,
        DAYS,
        NO_OF_ROOMS,
        ADVANCE,
        RENT,
        REFUND,
        USER_ID,
        SERVER_NAME,
        ENTERED_BY,
        MODIFIED_BY,
        MACHINE_NAME
    )
    VALUES
    (
        @ComId,
        @LocId,
        @DeptId,
        @CtrMachId,
        @FyId,
        @OutDate,
        @OutTime,
        @CheckInMstId,
        @Days,
        @NoOfRooms,
        @Advance,
        @Rent,
        @Refund,
        @UserId,
        @ServerName,
        @EnteredBy,
        @ModifiedBy,
        @MachineName
    );
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertRoomLock]...';


GO
CREATE PROCEDURE SP_InsertRoomLock
    @RoomId INT,
    @LockDate DATETIME,
    @DeptId INT,
    @LocId INT,
    @EnteredBy NVARCHAR(255),
    @ModifiedBy NVARCHAR(255),
    @EnteredOn DATETIME,
    @ModifiedOn DATETIME,
    @BookingId NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_ROOM_LOCK_T (ROOM_ID, LOCK_DATE, DEPT_ID, LOC_ID, ENTERED_BY, MODIFIED_BY, ENTERED_ON, MODIFIED_ON, BookingID)
    VALUES (@RoomId, @LockDate, @DeptId, @LocId, @EnteredBy, @ModifiedBy, @EnteredOn, @ModifiedOn, @BookingId);
END
GO
PRINT N'Creating Procedure [dbo].[SP_InsertRoomsExtend]...';


GO
CREATE PROCEDURE [dbo].[SP_InsertRoomsExtend]
    @LocId INT,
    @DeptId INT,
    @CtrMachId INT,
    @CheckInMstId BIGINT,
    @ExtendDate DateTime,
    @Rent DECIMAL(18, 2),
    @Days INT,
    @EnteredBy NVARCHAR(50),
    @ModifiedBy NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO BN_ROOM_EXTEND_T
    (
        LOC_ID,
        DEPT_ID,
        CTR_MACH_ID,
        CHECK_IN_MST_ID,
        EXTEND_DATE,
        RENT,
        DAYS,
        ENTERED_BY,
        MODIFIED_BY
    )
    VALUES
    (
        @LocId,
        @DeptId,
        @CtrMachId,
        @CheckInMstId,
        @ExtendDate,
        @Rent,
        @Days,
        @EnteredBy,
        @ModifiedBy
    );
END
GO
PRINT N'Creating Procedure [dbo].[SP_UpdateChangeRoomChanges]...';


GO
CREATE PROCEDURE [dbo].[SP_UpdateChangeRoomChanges]
    @CheckInMstId BIGINT,
    @Name NVARCHAR(100),
    @Sublocid INT,
    @Sublocation NVARCHAR(100),
    @Days INT,
    @NoOfRooms INT,
    @OutDate DATETIME,
    @OutTime DATETIME,
    @Advance DECIMAL(18, 2),
    @Rent DECIMAL(18, 2),
    @ScanDoc NVARCHAR(100) = null,
    @ModifiedBy NVARCHAR(50),
    @MachineName NVARCHAR(50),
    @MobNo NVARCHAR(20),
    @RecordModifiedCount INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BN_ROOM_CHECK_IN_MST_T
    SET 
        Name = @Name,
        SUBLOC_ID = @Sublocid,
        SUBLOCATION = @Sublocation,
        Days = @Days,
        NO_OF_ROOMS = @NoOfRooms,
        Out_Date = @OutDate,
        Out_Time = @OutTime,
        Advance = @Advance,
        Rent = @Rent,
        ScanImageName = @ScanDoc,
        Modified_By = @ModifiedBy,
        Modified_On = GETDATE(),
        Machine_Name = @MachineName,
        MOB_NO = @MobNo,
        Record_Modified_Count = @RecordModifiedCount
    WHERE CHECK_IN_MST_ID = @CheckInMstId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_UpdateCheckInMstRent]...';


GO
CREATE PROCEDURE SP_UpdateCheckInMstRent
    @Rent DECIMAL(18, 2),
    @Days INT,
    @ModifiedBy NVARCHAR(100),
    @CheckInMstId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BN_ROOM_CHECK_IN_MST_T
    SET
        RENT = @Rent,
        DAYS = @Days,
        Modified_By = @ModifiedBy,
        Modified_On = GETDATE()
    WHERE
        CHECK_IN_MST_ID = @CheckInMstId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_UpdateDaysForDonner]...';


GO
CREATE PROCEDURE SP_UpdateDaysForDonner
    @DnrRmnedDays INT,
    @RoomId INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BN_ROOM_MST_T
    SET D_NoOfRooms = D_NoOfRooms - @DnrRmnedDays
    WHERE ROOM_ID = @RoomId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_UpdateRoomCheckInDetHistory]...';


GO
CREATE PROCEDURE SP_UpdateRoomCheckInDetHistory
    @CheckInMstIds INT,
    @LockerIds INT,
    @UserName NVARCHAR(50),
    @MachineName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BN_ROOM_CHECK_IN_DET_T_HISTORY
    SET IsDelete = 1,
        Modified_By = @UserName,
        Modified_On = GETDATE()
    WHERE CHECK_IN_MST_ID = @CheckInMstIds
      AND ROOM_ID = @LockerIds;
END
GO
PRINT N'Creating Procedure [dbo].[SP_UpdateRoomCheckInMst]...';


GO
CREATE PROCEDURE SP_UpdateRoomCheckInMst
    @CheckInMstId INT,
    @Name NVARCHAR(255),
    @BhaktTypeId NVARCHAR(255),
    @Sublocid INT,
    @Sublocn NVARCHAR(255),
    @Days INT,
    @NoOfRooms INT,
    @OutDate DATE,
    @OutTime TIME,
    @Advance DECIMAL(18, 2),
    @Rent DECIMAL(18, 2),
    @ScanDoc NVARCHAR(255),
    @ModifiedBy NVARCHAR(255),
    @MachineName NVARCHAR(255),
    @PaymentType INT,
    @Tid INT,
    @CountryId INT,
    @Countryname NVARCHAR(255),
    @StateId INT,
    @Statename NVARCHAR(255),
    @DistrictId INT,
    @District NVARCHAR(255),
    @ChqBankName NVARCHAR(255),
    @ChqNo NVARCHAR(255),
    @ChqDate DATE,
    @MobNo NVARCHAR(255),
    @RecordModifiedCount INT,
    @IsOnline INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BN_ROOM_CHECK_IN_MST_T
    SET Name = @Name,
        BHAKT_TYPE = @BhaktTypeId,
        SUBLOC_ID = @Sublocid,
        SUBLOCATION = @Sublocn,
        Days = @Days,
        NO_OF_ROOMS = @NoOfRooms,
        Out_Date = @OutDate,
        Out_Time = @OutTime,
        Advance = @Advance,
        Rent = @Rent,
        ScanImageName = @ScanDoc,
        Modified_By = @ModifiedBy,
        Modified_On = GETDATE(),
        Machine_Name = @MachineName,
        PaymentType = @PaymentType,
        Tid = @Tid,
        COUNTRY_ID = @CountryId,
        COUNTRY_NAME = @Countryname,
        STATE_ID = @StateId,
        STATE = @Statename,
        DISTRICT_ID = @DistrictId,
        DISTRICT = @District,
        CHQ_BANK_NAME = @ChqBankName,
        CHQ_NO = @ChqNo,
        CHQ_DATE = @ChqDate,
        MOB_NO = @MobNo,
        Record_Modified_Count = @RecordModifiedCount,
        IsOnline = @IsOnline
    WHERE CHECK_IN_MST_ID = @CheckInMstId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_UpdateRoomCheckOutMst]...';


GO
CREATE PROCEDURE SP_UpdateRoomCheckOutMst
    @Days INT,
    @NoOfRooms INT,
    @Advance DECIMAL(18, 2),
    @Rent DECIMAL(18, 2),
    @Refund DECIMAL(18, 2),
    @ModifiedBy NVARCHAR(100),
    @MachineName NVARCHAR(100),
    @RecordModifiedCount INT,
    @CheckOutMstId BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BN_ROOM_CHECK_OUT_MST_T
    SET
        Days = @Days,
        NO_OF_ROOMS = @NoOfRooms,
        Advance = @Advance,
        Rent = @Rent,
        Refund = @Refund,
        Modified_By = @ModifiedBy,
        Modified_On = GETDATE(),
        Machine_Name = @MachineName,
        Record_Modified_Count = @RecordModifiedCount
    WHERE
        CHECK_OUT_MST_ID = @CheckOutMstId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_UpdateRoomLock]...';


GO
CREATE PROCEDURE SP_UpdateRoomLock
    @RoomLockId INT,
    @RoomId INT,
    @ModifiedOn DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.BN_ROOM_LOCK_T
    SET 
        ROOM_ID = @RoomId,
        Modified_On = @ModifiedOn
    WHERE
        ROOM_LOCK_ID = @RoomLockId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_UpdateRoomMst]...';


GO
CREATE PROCEDURE SP_UpdateRoomMst
    @RoomId INT,
    @IntActiveInactiveStatus SMALLINT = 0,
    @IntAvailableStatus SMALLINT = 0
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BN_ROOM_MST_T
    SET
        AVAILABLE_STATUS = @IntAvailableStatus,
        Modified_On = GETDATE()
    WHERE
        ROOM_ID = @RoomId;
END
GO
PRINT N'Creating Procedure [dbo].[SP_UpdateRoomStatus]...';


GO
CREATE PROCEDURE SP_UpdateRoomStatus
    @LockerId INT,
    @LockerAvailableStatus INT,
    @ModifiedBy NVARCHAR(255),
    @RecordModifiedCount INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE BN_ROOM_MST_T
    SET AVAILABLE_STATUS = @LockerAvailableStatus,
        Modified_By = @ModifiedBy,
        Modified_On = GETDATE(),
        Record_Modified_Count = @RecordModifiedCount
    WHERE ROOM_ID = @LockerId;
END
GO
PRINT N'Update complete.';


GO
-- Darshan bhakt niwas end