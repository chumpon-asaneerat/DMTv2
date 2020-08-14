﻿CREATE VIEW TSBCouponLaneBalanceView
AS
	-- Stock = 1, Lane = 2, SoldByLane = 3, SoldByTSB = 4
	SELECT TSB.TSBId, TSB.TSBNameEN, TSB.TSBNameTH
		 , (
		     -- Count No of Coupon 35
			 SELECT IFNULL(COUNT(*), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Lane = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 35
				AND TSBCouponTransaction.FinishFlag = 1
		   ) AS CouponBHT35
		 , (
		     -- Count No of Coupon 80
			 SELECT IFNULL(COUNT(*), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Lane = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 80
				AND TSBCouponTransaction.FinishFlag = 1
		   ) AS CouponBHT80
		 , (
		     -- Calc Price of Coupon 35
			 SELECT IFNULL(SUM(Price), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Lane = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 35
				AND TSBCouponTransaction.FinishFlag = 1
		   ) AS PriceBHT35
		 , (
		     -- Calc Price of Coupon 80
			 SELECT IFNULL(SUM(Price), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Lane = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				AND TSBCouponTransaction.CouponType = 80
				AND TSBCouponTransaction.FinishFlag = 1
		   ) AS PriceBHT80
		 , (
		     -- Calc Price of Coupon all types
			 SELECT IFNULL(SUM(Price), 0) 
			   FROM TSBCouponTransaction 
			  WHERE TSBCouponTransaction.TransactionType = 2 -- Lane = 2
				AND TSBCouponTransaction.TSBId = TSB.TSBId
				--AND (   
				--        TSBCouponTransaction.CouponType = 35 
				--     OR TSBCouponTransaction.CouponType = 80
				--	)
				AND TSBCouponTransaction.FinishFlag = 1
		   ) AS CouponBHTTotal
	  FROM TSB
