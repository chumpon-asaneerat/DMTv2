CREATE VIEW TSBShiftView
AS
	SELECT TSBShift.*
		 , TSB.TSBNameEN, TSB.TSBNameTH
		 , [Shift].ShiftNameEN, [Shift].ShiftNameTH
		 --, UserView.FullNameEN, UserView.FullNameTH
	  FROM TSBShift
		 , TSB
	     , [Shift]
		 --, UserView
	 WHERE TSBShift.TSBId = TSB.TSBId
	   AND TSBShift.ShiftId = [Shift].ShiftId
	   --AND TSBShift.UserId = UserView.UserId
