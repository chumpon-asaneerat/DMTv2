CREATE VIEW UserShiftRevenueView
AS
	SELECT UserShiftRevenue.*
		 , TSB.TSBNameEN, TSB.TSBNameTH
		 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction
		 , [Shift].ShiftNameEN, [Shift].ShiftNameTH
		 --, UserView.FullNameEN, UserView.FullNameTH
	  FROM UserShiftRevenue
		 , TSB
	     , PlazaGroup
		 , [Shift]
		 --, UserView
		 , UserShift
	 WHERE PlazaGroup.TSBId = TSB.TSBId
	   AND UserShift.TSBId = TSB.TSBId
	   AND UserShift.ShiftId = [Shift].ShiftId
	   --AND UserShift.UserId = UserView.UserId
	   AND UserShiftRevenue.TSBId = TSB.TSBId
	   AND UserShiftRevenue.PlazaGroupId = PlazaGroup.PlazaGroupId
	   AND UserShiftRevenue.ShiftId = [Shift].ShiftId
	   --AND UserShiftRevenue.UserId = UserView.UserId
	   AND UserShiftRevenue.UserId = UserShift.UserId
