CREATE VIEW LanePaymentView
AS
	SELECT LanePayment.*
		 , TSB.TSBNameEN, TSB.TSBNameTH
		 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction
		 , Plaza.PlazaNameEN, Plaza.PlazaNameTH
		 , Lane.LaneNo
		 --, UserView.FullNameEN, UserView.FullNameTH
		 , Payment.PaymentNameEN, Payment.PaymentNameTH
	  FROM LanePayment
		 , TSB
	     , PlazaGroup
	     , Plaza
		 , Lane
		 --, UserView
		 , Payment
	 WHERE PlazaGroup.TSBId = TSB.TSBId
	   AND Plaza.TSBId = TSB.TSBId
	   AND Plaza.PlazaGroupId = PlazaGroup.PlazaGroupId
	   AND Lane.TSBId = TSB.TSBId
	   AND Lane.PlazaGroupId = PlazaGroup.PlazaGroupId
	   AND Lane.PlazaId = Plaza.PlazaId
	   AND LanePayment.TSBId = TSB.TSBId
	   AND LanePayment.PlazaGroupId = PlazaGroup.PlazaGroupId
	   AND LanePayment.PlazaId = Plaza.PlazaId
	   AND LanePayment.LaneId = Lane.LaneId
	   --AND LanePayment.UserId = UserView.UserId
	   AND LanePayment.PaymentId = Payment.PaymentId
