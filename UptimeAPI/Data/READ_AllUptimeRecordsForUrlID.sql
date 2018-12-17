SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Query all uptime records for a URL ID
CREATE PROCEDURE [dbo].[READ_AllUptimeRecordsForUrlID]
    @INT_URLID INT
AS

BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT *
      FROM dbo.Uptime U
     WHERE U.URL_ID = @INT_URLID
END    

GO