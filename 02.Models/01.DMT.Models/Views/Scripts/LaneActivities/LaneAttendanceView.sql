CREATE VIEW LaneAttendanceView
AS
	SELECT LaneAttendance.*
		 , TSB.TSBNameEN, TSB.TSBNameTH
		 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction
		 , Plaza.SCWPlazaId, Plaza.PlazaNameEN, Plaza.PlazaNameTH
		 , Lane.LaneNo
		 --, UserView.FullNameEN, UserView.FullNameTH
	  FROM LaneAttendance
		 , TSB
	     , PlazaGroup
	     , Plaza
		 , Lane
		 --, UserView
	 WHERE PlazaGroup.TSBId = TSB.TSBId
	   AND Plaza.TSBId = TSB.TSBId
	   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId
	   AND Lane.TSBId = TSB.TSBId
	   AND Lane.PlazaGroupId = PlazaGroup.PlazaGroupId
	   AND Lane.PlazaId = Plaza.PlazaId
	   AND LaneAttendance.TSBId = TSB.TSBId
	   AND LaneAttendance.PlazaGroupId = PlazaGroup.PlazaGroupId
	   AND LaneAttendance.PlazaId = Plaza.PlazaId
	   AND LaneAttendance.LaneId = Lane.LaneId
	   --AND LaneAttendance.UserId = UserView.UserId
