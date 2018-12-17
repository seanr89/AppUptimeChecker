SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Operation to query all uptime records
CREATE PROCEDURE [dbo].[READ_AllUptimeRecords]
AS

BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT *
      FROM dbo.Uptime
END    


GO