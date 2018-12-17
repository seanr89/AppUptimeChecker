
CREATE PROCEDURE [dbo].[READ_URLIDByName]
	@STR_URL NVARCHAR(255)
AS

BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT ID 
      FROM dbo.URL
      WHERE Address = @STR_URL
END    

