
CREATE PROCEDURE [dbo].[INSERT_Uptime] 
    @INT_URLID INT,
    @INT_ResponseCode INT,
    @INT_Duration INT,
    @STR_Date NVARCHAR(255)
AS

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO dbo.[Uptime] (URL_ID, ResponseCode, Duration, EventDate)
    VALUES(
        @INT_URLID
        ,@INT_ResponseCode
        ,@INT_Duration
        ,CAST(@STR_Date AS datetime)
    )
    RETURN 1
END