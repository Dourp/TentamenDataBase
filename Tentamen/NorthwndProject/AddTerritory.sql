-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nisse
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE AddTerritory 
	-- Add the parameters for the stored procedure here
	@TerritoryDescription nvarchar(20) = 0, 
	@RegionID int = 0,
	@TerritoryID nvarchar(20) = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Insert into dbo.Territories
					([TerritoryDescription], [RegionID], [TerritoryID])

				values (@TerritoryDescription, @RegionID, @TerritoryID)

END
GO


exec AddTerritory 'Erik', 1 , '11111'