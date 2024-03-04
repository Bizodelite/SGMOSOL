
GO
PRINT N'Altering Procedure [dbo].[SP_GET_DEN_DENGI_RECEIPT_DATA]...';


GO

 ALTER PROCEDURE [dbo].[SP_GET_DEN_DENGI_RECEIPT_DATA]
(
    @RECEIPT_F_NO NUMERIC(20,0) = NULL,
    @RECEIPT_L_NO NUMERIC(20,0) = NULL,
    @NAME VARCHAR(50) = NULL,
    @FDATE DATETIME = NULL,
    @LDATE DATETIME = NULL,
    @MOBILE_NUMBER VARCHAR(50) = NULL,
    --@DENGI_TYPE INT,
    @LOC_ID INT,
    @DEPT_ID INT,
    @CTR_MACH_ID INT
)
AS
BEGIN
    SELECT   
	R.DENGI_RECEIPT_ID,
        R.DR_DATE as DATE, 
        R.SERIAL_NO AS [SERIAL NUMBER],
        MST.TYPE AS [DENGI TYPE], 
        R.NAME,
        R.AMOUNT,
        R.CONTACT AS [MOBILE NUMBER],
		R.DOC_DETAIL AS [DOCUMENT DETAILS],
        R.GOTRA_NAME AS GOTRA, 
        R.ADDRESS,
		T.Token_Detail_Name AS [PAYMENT TYPE]

    FROM 
        DEN_DENGI_RECEIPT_MST_T R
    INNER JOIN 
        com_token_det_t T ON R.PAYMENT_TYPE_ID = T.Token_Detail_Id
    INNER JOIN 
        DEN_DENGI_MASTER_T MST ON R.DENGI_MST_ID = MST.DENGI_MST_ID
    LEFT JOIN 
        tbl_gotra_master G ON R.GOTRA_ID = G.gotra_id
    WHERE 
        R.DR_DATE BETWEEN CONVERT(DATETIME, @FDATE, 103) AND CONVERT(DATETIME, @LDATE, 103)
        AND (@RECEIPT_F_NO IS NULL OR SERIAL_NO >= @RECEIPT_F_NO)
        AND (@RECEIPT_L_NO IS NULL OR SERIAL_NO <= @RECEIPT_L_NO)
        AND (@NAME IS NULL OR Name = @Name)
        --AND (@DENGI_TYPE IS NULL OR R.DENGI_MST_ID = @DENGI_TYPE)
        AND (@MOBILE_NUMBER IS NULL OR R.CONTACT = @MOBILE_NUMBER)
        AND LOC_ID = @LOC_ID
        AND DEPT_ID = @DEPT_ID
        AND CTR_MACH_ID = @CTR_MACH_ID
END
--exec SP_GET_DEN_DENGI_RECEIPT_DATA  322340, 326041, NULL, '2023-01-01', '2024-02-05',NULL,3,7,39,78;

--SELECT * FROM tbl_gotra_master


--select * from DEN_DENGI_RECEIPT_MST_T where SERIAL_NO>=827259 and DENGI_MST_ID=4
GO
PRINT N'Creating Procedure [dbo].[InsertDengiReceipt_LOG]...';


GO
CREATE PROCEDURE [dbo].[InsertDengiReceipt_LOG]
	@UserName varchar(300),
    @Amount numeric(18,2),
    @Serial_no bigint,
    @LastUserName varchar(300),
    @LastAmount numeric(18,2),
    @LastSerial_no bigint,
    @CreatedBy varchar(300),
    @MACId varchar(300),
	@ID NUMERIC(20,0) output

as
BEGIN

    INSERT INTO [dbo].[DEN_DENGI_RECEIPT_MST__Log] ([UserName],[Amount],[Serial_no],[LastUserName],[LastAmount],[LastSerial_no],[CreatedBy],[MACId])
     VALUES(@UserName,@Amount,@Serial_no,@LastUserName,@LastAmount,@LastSerial_no,@CreatedBy,@MACId);
    set @ID = SCOPE_IDENTITY();

END
GO
PRINT N'Update complete.';


GO
