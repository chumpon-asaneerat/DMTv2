CREATE VIEW LaneAttendanceView
AS
	SELECT LaneAttendance.*
		 , TSB.TSBNameEN, TSB.TSBNameTH
		 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction
		 , Plaza.PlazaNameEN, Plaza.PlazaNameTH
		 , Lane.LaneNo
		 , [User].FullNameEN, [User].FullNameTH
	  FROM LaneAttendance
		 , TSB
	     , PlazaGroup
	     , Plaza
		 , Lane
		 , [User]
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
	   AND LaneAttendance.UserId = [User].UserId
