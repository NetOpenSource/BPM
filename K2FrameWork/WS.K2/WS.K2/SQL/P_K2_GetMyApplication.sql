USE [SohuK2FrameWork]
GO
/****** Object:  StoredProcedure [dbo].[P_K2_GetMyApplication_CMC]    Script Date: 04/06/2012 15:48:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[P_K2_GetMyApplication]
	@ActionerName nvarchar(50),
	@Folio nvarchar(300)='',
	@StartTime nvarchar(50)='',
	@EndTime nvarchar(50)='',
	@ProcFullName nvarchar(100)='',
	@PageNum int =1,
	@PageSize int =100
AS
BEGIN
SET NOCOUNT ON;
SET @ActionerName='k2:'+@ActionerName;
	create table #TempActioner(ProcInstID int,Actioner nvarchar(500))
	create table #TempActioners(ProcInstID int,Actioners nvarchar(MAX))
	create table #TempTB(
						 rowID int
						,ProcSetID int
						,ProcName nvarchar(200)
						,ProcFullName nvarchar(200)
						,ProcInstID int
						,Status tinyint
						,ProcessStatus nvarchar(50)
						,StartDate datetime
						,FinishDate datetime
						,Originator nvarchar(100)
						,Folio nvarchar(200)
						,ActivityName nvarchar(200)
						,CallBackProcInstID ntext
						)
	insert into #TempActioner
	select distinct aair.procInstID,a.ActionerName
	from k2server.dbo._ActionActInstRights aair 
	inner join k2server.dbo._Actioners a on a.id=aair.ActionerID
	inner join K2server.dbo._ProcInst pInst on pInst.id=aair.procInstID 
	inner join K2serverlog.dbo._ActInst ai on ai.id=aair.actinstID and ai.ProcinstID=aair.procinstid
	where aair.[execute]=1 and ai.status=2
	
	insert into #TempActioners
	SELECT * FROM
	(
		SELECT DISTINCT ProcInstID FROM #tempactioner
	)A
	OUTER APPLY(
		SELECT [Actioners]= STUFF(REPLACE(REPLACE(
				(
					SELECT Actioner FROM #tempactioner N
					WHERE procInstID = A.procInstID
					FOR XML AUTO
				), '<N Actioner="', ';'), '"/>', ''), 1, 1, '')

	)N
	
	create table #TempActivity(ProcInstID int,Name nvarchar(100))
	create table #TempActivitys(ProcInstID int,Name nvarchar(500))
	
	insert into #TempActivity
	SELECT  _ActInst.ProcInstID ,_Act.Name
			FROM k2serverlog.dbo._ActInst
			INNER JOIN k2serverlog.dbo._Act ON _ActInst.ActID = _Act.ID 
			inner join K2server.dbo._ProcInst pInst on pInst.id=_ActInst.procInstID 
			WHERE _ActInst.Status = 2
					
	insert into #TempActivitys
	SELECT *
	FROM
	(
		SELECT DISTINCT ProcInstID  FROM #TempActivity
	)A
	OUTER APPLY(
		SELECT [ActivityNames]= STUFF(REPLACE(REPLACE(REPLACE(
				(
					SELECT Name FROM #TempActivity where ProcInstID= A.procInstID
					FOR XML AUTO
				), '_x0023_TempActivity Name="', ''), '"/><', ';'),'"/>',''), 1, 1, '')

	)N;
	
	
	with MyApplication as
	(
		SELECT row_number() over( order by  pinst.StartDate DESC) as RowID
		,ps.ID ProcSetID
		,ps.Name
		,ps.FullName
		,pinst.ID ProcInstID
		,case when WS.Status IS null then pInst.Status else WS.Status end as Status
		,s.Status ProcessStatus
		,pinst.StartDate
		,pinst.FinishDate
		,pinst.Originator
		,pinst.Folio
		,Isnull(tempActivitys.Name,'结束') AS CurrentActivityName		
		FROM k2serverlog.dbo._ProcSet ps
		INNER JOIN k2serverlog.dbo._Proc p ON ps.ID = p.ProcSetID 
		INNER JOIN k2serverlog.dbo._ProcInst pInst ON p.ID = pinst.ProcID 
		left JOIN (SELECT distinct [ProcInstID],Status FROM [K2Server].[dbo].[_WorklistSlot] ) WS ON  WS.ProcInstID = pInst.ID 
		INNER JOIN k2serverlog.dbo._Status s ON pinst.Status = s.StatusID AND s.GroupName= 'Process' 		
		left join #TempActivitys tempActivitys on tempActivitys.ProcInstID=pinst.id
		WHERE pinst.Originator = @ActionerName
		AND pinst.Folio LIKE N'%' + @Folio+ '%'	
		AND pinst.StartDate >= CASE @StartTime WHEN '' THEN cast( '1910-1-1 00:00:00 ' as datetime) ELSE cast( @StartTime + ' 00:00:00 ' as datetime) END
		AND pinst.FinishDate <= CASE @EndTime WHEN '' THEN getdate() ELSE cast( @EndTime + ' 23:59:59 ' as datetime) END
		AND ps.FullName in( N'WF\SCF')
	)
	
	insert into #TempTB
	select MyApplication.* 	
		,Isnull((SELECT [Value] FROM k2serverlog.dbo._ProcInstData ProcInstData 
				WHERE ProcInstData.[ProcInstID]=MyApplication.ProcInstID and ProcInstData.[Name]='CallBackProcInstID'),'0') as CallBackProcInstID	
	from MyApplication
	
	select #temptb.*,Isnull(temp.Actioners,'无') AS  CurrentActioners	
	from #temptb
	left join #TempActioners temp on temp.ProcInstID=#temptb.ProcInstID
	where rowid between ((@pagenum-1) * @pagesize +1) and (@pagenum * @pagesize)
	
    select ProcFullName AS ProcessName,count(ProcFullName) as Num from #TempTB 
    where rowid between ((@pagenum-1) * @pagesize+1) and (@pagenum * @pagesize)
	group by ProcFullName
	
	declare @TotalNum int
	declare @PageCount int
	select @TotalNum = count(*) from #TempTB
	
	if((@Totalnum % @pagesize)>0)
		set @pagecount = (@Totalnum/@pagesize) + 1
	else
		set @pagecount = @totalnum/@pagesize

	select @Totalnum TotalNum,@PageCount PageCount

	drop table #tempactioner
	drop table #tempactioners
	drop table #tempTB
	drop table #TempActivity
	drop table #TempActivitys

end
