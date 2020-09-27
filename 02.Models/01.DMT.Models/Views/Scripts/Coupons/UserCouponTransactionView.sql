CREATE VIEW UserCouponTransactionView
AS
	SELECT UserCouponTransaction.*
	     , TSB.TSBNameEN, TSB.TSBNameTH
	     --, UserView.FullNameEN, UserView.FullNameTH 
	  FROM UserCouponTransaction, TSB
	     --, UserView
	 WHERE UserCouponTransaction.TSBId = TSB.TSBId
	   --AND UserCouponTransaction.UserId = UserView.UserId
