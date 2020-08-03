CREATE VIEW UserCoupon80SummaryView
AS
	SELECT TSB.TSBId /*, TSB.TSBNameEN, TSB.TSBNameTH */
		 , [User].UserId /*, [User].FullNameEN, [User].FullNameTH */
		 , (
			SELECT IFNULL(COUNT(*), 0) 
			  FROM UserCouponTransaction
			 WHERE UserCouponTransaction.TransactionType = 1 -- Borrow
			   AND UserCouponTransaction.TSBId = TSB.TSBId
			   AND UserCouponTransaction.UserId = [User].UserId
			   AND UserCouponTransaction.CouponType = 80
		   ) AS CountBorrow80
		 , (
			SELECT IFNULL(COUNT(*), 0) 
			  FROM UserCouponTransaction
			 WHERE UserCouponTransaction.TransactionType = 2 -- Return
			   AND UserCouponTransaction.TSBId = TSB.TSBId
			   AND UserCouponTransaction.UserId = [User].UserId
			   AND UserCouponTransaction.CouponType = 80
		   ) AS CountReturn80
	  FROM TSB, [User], UserCouponTransaction
	 WHERE UserCouponTransaction.UserId = [User].UserId
	   AND UserCouponTransaction.TSBId = TSB.TSBId
	 GROUP BY UserCouponTransaction.TSBId, UserCouponTransaction.UserId
