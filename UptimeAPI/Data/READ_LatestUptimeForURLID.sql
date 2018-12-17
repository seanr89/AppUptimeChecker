

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- Operation to query the latest uptime record for a specific URL ID
CREATE PROCEDURE [dbo].[READ_LatestUptimeForURLID]
	@INT_URLID INT
AS

BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT TOP 1 *
      FROM dbo.Uptime
      WHERE URL_ID = @INT_URLID
      ORDER BY EventDate desc
END    


GO