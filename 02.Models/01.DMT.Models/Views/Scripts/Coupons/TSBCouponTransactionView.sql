CREATE VIEW TSBCouponTransactionView
AS
	SELECT TSBCouponTransaction.* 
		 , TSB.TSBNameEN
		 , TSB.TSBNameTH
		 , usr.FullNameEN, usr.FullNameTH
		 , sup.FullNameEN AS SoldByFullNameEN, sup.FullNameTH AS SoldByFullNameTH
	  FROM TSBCouponTransaction
		 , TSB
		   LEFT JOIN [User] usr ON (TSBCouponTransaction.UserId = usr.UserId) 
		   LEFT JOIN [User] sup ON (TSBCouponTransaction.SoldBy = sup.UserId) 
	 WHERE TSBCouponTransaction.TSBId = TSB.TSBId
