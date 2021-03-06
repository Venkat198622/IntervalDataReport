USE [Test]
GO
/****** Object:  StoredProcedure [dbo].[usp_intervaldata_select]    Script Date: 3/18/2020 3:55:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


ALTER PROC [dbo].[usp_intervaldata_select]  

 as
BEGIN  
	 SELECT   DeliveryPoint, [date]  Date , DATEPART(HOUR, TimeSlot)   AS TimeSlot , SUM(SlotVal)  AS SlotVal
FROM IntervalData
GROUP BY   DeliveryPoint , [date] , DATEPART(HOUR, TimeSlot);
END

--exec [dbo].[usp_intervaldata_select]  