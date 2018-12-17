SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Query all uptime records for a URL ID
CREATE PROCEDURE [dbo].[READ_AllUptimeRecordsForURL]
    @STR_URL NVARCHAR(MAX)
AS

BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- DECLARE @INT_URLID INT = 0

    -- IF NOT EXISTS
    -- (
    --     SELECT TOP(1) U.ID
    --     FROM dbo.URL U
    --     WHERE U.Address = @STR_URL
    -- )

    -- BEGIN

    -- SET @INT_URLID = SELECT TOP(1)ID
    --     FROM dbo.URL
    --     WHERE Address = @STR_URL

    -- END

    SELECT *
      FROM dbo.Uptime Up
      LEFT JOIN dbo.URL U
      ON U.ID = Up.URL_ID
      WHERE U.Address = @STR_URL

END    

GO