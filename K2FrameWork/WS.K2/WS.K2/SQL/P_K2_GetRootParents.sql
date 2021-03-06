USE [K2FrameWorkCMC]
GO
/****** Object:  StoredProcedure [dbo].[sp_Recursion_GetRootParents]    Script Date: 04/05/2012 10:32:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================


ALTER proc [dbo].[P_K2_GetRootParents]
@DstProcInstID int
as
SET NOCOUNT ON;
    declare @t table(SrcProcInstID int)
    insert @t select SrcProcInstID from K2ServerLog.dbo._IPC IPC where IPC.DstProcInstID = @DstProcInstID
    while @@rowcount > 0
        insert @t select a.SrcProcInstID from K2ServerLog.dbo._IPC as a 
        inner join @t as b on a.DstProcInstID = b.SrcProcInstID and a.SrcProcInstID not in(select SrcProcInstID from @t)
   
   Declare @IDS nvarchar(100)
   Select top 1 @IDS=Convert(nvarchar(50),SrcProcInstID) from @t order by SrcProcInstID   
   
   select IsNull(@IDS,@DstProcInstID) as IDS
    --select * from @t


