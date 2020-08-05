CREATE VIEW TSBCouponBalanceView
AS
	SELECT TSB.TSBId
		 , TSB.TSBNameEN
		 , TSB.TSBNameTH
		 , ((
			 SELECT IFNULL(COUNT(*), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 1 
					 OR TSBCouponTransaction.TransactionType = 2
					) -- Stock = 1, Lane = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 35
				AND TSBCouponTransaction.FinishFlag = 1
			) -
			(
			 SELECT IFNULL(COUNT(*), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 3
			         OR TSBCouponTransaction.TransactionType = 4
			        ) -- SoldByLane = 3, SoldByTSB = 4
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 35
				AND TSBCouponTransaction.FinishFlag = 1
			)) AS CouponBHT35
		 , ((
			 SELECT IFNULL(COUNT(*), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 1 
					 OR TSBCouponTransaction.TransactionType = 2
					) -- Stock = 1, Lane = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 80
				AND TSBCouponTransaction.FinishFlag = 1
			) -
			(
			 SELECT IFNULL(COUNT(*), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 3
			         OR TSBCouponTransaction.TransactionType = 4
			        ) -- SoldByLane = 3, SoldByTSB = 4
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 80
				AND TSBCouponTransaction.FinishFlag = 1
			)) AS CouponBHT80
		 , ((
			 SELECT IFNULL(SUM(Price), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 1 
					 OR TSBCouponTransaction.TransactionType = 2
					) -- Stock = 1, Lane = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 35
				AND TSBCouponTransaction.FinishFlag = 1
			) +
			(
			 SELECT IFNULL(SUM(Price), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 1 
					 OR TSBCouponTransaction.TransactionType = 2
					) -- Stock = 1, Lane = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 80
				AND TSBCouponTransaction.FinishFlag = 1
			) -
			(
			 SELECT IFNULL(SUM(Price), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 3
			         OR TSBCouponTransaction.TransactionType = 4
			        ) -- SoldByLane = 3, SoldByTSB = 4
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 35
				AND TSBCouponTransaction.FinishFlag = 1
			) -
			(
			 SELECT IFNULL(SUM(Price), 0) 
			   FROM TSBCouponTransaction 
			  WHERE (   TSBCouponTransaction.TransactionType = 3
			         OR TSBCouponTransaction.TransactionType = 4
			        ) -- SoldByLane = 3, SoldByTSB = 4
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 80
				AND TSBCouponTransaction.FinishFlag = 1
			)) AS CouponBHTTotal
	  FROM TSB