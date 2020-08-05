CREATE VIEW TSBCouponSummarryView
AS
	SELECT TSB.TSBId
		 , TSB.TSBNameEN
		 , TSB.TSBNameTH
		 , ((
			 SELECT IFNULL(COUNT(*), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 0 
					 OR TSBCouponTransaction.TransactionType = 1
					) -- Received = 0, UserOnHand = 1
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 35
			) -
			(
			 SELECT IFNULL(COUNT(*), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 35
			)) AS CouponBHT35
		 , ((
			 SELECT IFNULL(COUNT(*), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 0 
					 OR TSBCouponTransaction.TransactionType = 1
					) -- Received = 0, UserOnHand = 1
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 80
			) -
			(
			 SELECT IFNULL(COUNT(*), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 80
			)) AS CouponBHT80
		 , ((
			 SELECT IFNULL(SUM(Factor), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 0 
					 OR TSBCouponTransaction.TransactionType = 1
					) -- Received = 0, UserOnHand = 1
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 35
			) +
			(
			 SELECT IFNULL(SUM(Factor), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 0 
					 OR TSBCouponTransaction.TransactionType = 1
					) -- Received = 0, UserOnHand = 1
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 80
			) -
			(
			 SELECT IFNULL(SUM(Factor), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 35
			) -
			(
			 SELECT IFNULL(SUM(Factor), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Sold = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 80
			)) AS CouponBHTTotal
	  FROM TSB
