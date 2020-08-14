CREATE VIEW RevenueEntryView
AS
	SELECT RevenueEntry.*
		 , TSB.TSBNameEN, TSB.TSBNameTH
		 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction
		 , [Shift].ShiftNameEN, [Shift].ShiftNameTH
		 , UserView.FullNameEN, UserView.FullNameTH
		 , Sup.FullNameEN AS SupervisorNameEN, Sup.FullNameTH AS SupervisorNameTH
	  FROM RevenueEntry
		 , TSB
	     , PlazaGroup
		 , [Shift]
		 , UserView
		 , UserView as Sup
	 WHERE PlazaGroup.TSBId = TSB.TSBId
	   AND RevenueEntry.TSBId = TSB.TSBId
	   AND RevenueEntry.PlazaGroupId = PlazaGroup.PlazaGroupId
	   AND RevenueEntry.ShiftId = [Shift].ShiftId
	   AND RevenueEntry.UserId = UserView.UserId
	   AND RevenueEntry.SupervisorId = Sup.UserId
