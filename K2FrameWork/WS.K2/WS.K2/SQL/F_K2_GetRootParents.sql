USE [K2FrameWorkCMC]
GO
/****** Object:  UserDefinedFunction [dbo].[Recursion_GetRootParents]    Script Date: 04/05/2012 10:33:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
ALTER FUNCTION [dbo].[F_K2_GetRootParents]
(
	-- Add the parameters for the function here
	@DstProcInstID int
)
RETURNS int
AS
BEGIN
	-- Declare the return variable here
	declare @t table(SrcProcInstID int)
    insert @t select SrcProcInstID from K2ServerLog.dbo._IPC IPC where IPC.DstProcInstID = @DstProcInstID
    while @@rowcount > 0
        insert @t select a.SrcProcInstID from K2ServerLog.dbo._IPC as a 
        inner join @t as b on a.DstProcInstID = b.SrcProcInstID and a.SrcProcInstID not in(select SrcProcInstID from @t)
   
   Declare  @ParentProcInstID int
   Select top 1 @ParentProcInstID=SrcProcInstID from @t order by SrcProcInstID      

	-- Return the result of the function
	RETURN IsNull(@ParentProcInstID,@DstProcInstID)

END
