CREATE VIEW UserCoupon35SummaryView
AS
	SELECT TSB.TSBId /*, TSB.TSBNameEN, TSB.TSBNameTH */
		 --, UserView.UserId /*, UserView.FullNameEN, UserView.FullNameTH */
		 , UserCouponTransaction.UserId, UserCouponTransaction.FullNameEN, UserCouponTransaction.FullNameTH
		 , (
			SELECT IFNULL(COUNT(*), 0) 
			  FROM UserCouponTransaction
			 WHERE UserCouponTransaction.TransactionType = 1 -- Borrow
			   AND UserCouponTransaction.TSBId = TSB.TSBId
			   --AND UserCouponTransaction.UserId = UserView.UserId
			   AND UserCouponTransaction.CouponType = 35
		   ) AS CountBorrow35
		 , (
			SELECT IFNULL(COUNT(*), 0) 
			  FROM UserCouponTransaction
			 WHERE UserCouponTransaction.TransactionType = 2 -- Return
			   AND UserCouponTransaction.TSBId = TSB.TSBId
			   --AND UserCouponTransaction.UserId = UserView.UserId
			   AND UserCouponTransaction.CouponType = 35
		   ) AS CountReturn35
	  FROM TSB, UserView, UserCouponTransaction
	 WHERE UserCouponTransaction.TSBId = TSB.TSBId
	   --AND UserCouponTransaction.UserId = UserView.UserId
	 GROUP BY UserCouponTransaction.TSBId, UserCouponTransaction.UserId
