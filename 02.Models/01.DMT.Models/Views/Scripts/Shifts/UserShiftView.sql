CREATE VIEW UserShiftView
AS
	SELECT UserShift.*
		 , TSB.TSBNameEN, TSB.TSBNameTH
		 , [Shift].ShiftNameEN, [Shift].ShiftNameTH
		 , UserView.FullNameEN, UserView.FullNameTH
	  FROM UserShift
		 , TSB
	     , [Shift]
		 , UserView
	 WHERE UserShift.TSBId = TSB.TSBId
	   AND UserShift.ShiftId = [Shift].ShiftId
	   AND UserShift.UserId = UserView.UserId
