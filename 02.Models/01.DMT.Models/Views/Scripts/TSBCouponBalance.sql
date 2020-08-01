CREATE VIEW TSBCouponBalance
AS
	SELECT TSB.TSBId
		 , TSB.TSBNameEN
		 , TSB.TSBNameTH
		 , ((
			 SELECT IFNULL(SUM(CouponBHT35), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 0 
					 OR TSBCouponTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCouponTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CouponBHT35), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
			)) AS CouponBHT35
		 , ((
			 SELECT IFNULL(SUM(CouponBHT80), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 0 
					 OR TSBCouponTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCouponTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CouponBHT80), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
			)) AS CouponBHT80
		 , ((
			 SELECT IFNULL(SUM((CouponBHT35 * CouponBHT35Factor) + (CouponBHT80 * CouponBHT80Factor)), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 0 
					 OR TSBCouponTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCouponTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM((CouponBHT35 * CouponBHT35Factor) + (CouponBHT80 * CouponBHT80Factor)), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
			)) AS CouponBHTTotal
	  FROM TSB
