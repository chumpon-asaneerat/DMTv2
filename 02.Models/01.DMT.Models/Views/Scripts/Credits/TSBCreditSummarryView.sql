CREATE VIEW TSBCreditSummarryView
AS
	SELECT TSB.TSBId
		 , TSB.TSBNameEN
		 , TSB.TSBNameTH
		 , ((
			 SELECT (IFNULL(SUM(UserCreditTransaction.AmountST25), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountST50), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT1), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT2), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT5), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT10), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT20), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT50), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT100), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT500), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT1000), 0))
			   FROM UserCreditTransaction, UserCreditBalance
			  WHERE UserCreditTransaction.TransactionType = 1 -- Borrow = 1
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
				AND UserCreditBalance.TSBId = TSB.TSBId
			) -
			(
			 SELECT (IFNULL(SUM(UserCreditTransaction.AmountST25), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountST50), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT1), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT2), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT5), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT10), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT20), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT50), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT100), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT500), 0) +
					 IFNULL(SUM(UserCreditTransaction.AmountBHT1000), 0))
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditTransaction.TransactionType = 2 -- Returns = 2
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
				AND UserCreditBalance.TSBId = TSB.TSBId
			)) AS UserBHTTotal
		 , ((
			 SELECT IFNULL(SUM(CountST25), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountST25), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountST25), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountST25), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountST25
		 , ((
			 SELECT IFNULL(SUM(CountST50), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountST50), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountST50), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountST50), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountST50
		 , ((
			 SELECT IFNULL(SUM(CountBHT1), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountBHT1), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT1), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT1), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountBHT1
		 , ((
			 SELECT IFNULL(SUM(CountBHT2), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountBHT2), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT2), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT2), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountBHT2
		 , ((
			 SELECT IFNULL(SUM(CountBHT5), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountBHT5), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT5), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT5), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountBHT5
		 , ((
			 SELECT IFNULL(SUM(CountBHT10), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountBHT10), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT10), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT10), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountBHT10
		 , ((
			 SELECT IFNULL(SUM(CountBHT20), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountBHT20), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT20), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT20), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountBHT20
		 , ((
			 SELECT IFNULL(SUM(CountBHT50), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountBHT50), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT50), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT50), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountBHT50
		 , ((
			 SELECT IFNULL(SUM(CountBHT100), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountBHT100), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT100), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT100), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountBHT100
		 , ((
			 SELECT IFNULL(SUM(CountBHT500), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountBHT500), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT500), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT500), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountBHT500
		 , ((
			 SELECT IFNULL(SUM(CountBHT1000), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(CountBHT1000), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT1000), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.CountBHT1000), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS CountBHT1000
		 , ((
			 SELECT IFNULL(SUM(AmountST25), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountST25), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountST25), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountST25), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountST25
		 , ((
			 SELECT IFNULL(SUM(AmountST50), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountST50), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountST50), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountST50), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountST50
		 , ((
			 SELECT IFNULL(SUM(AmountBHT1), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountBHT1), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT1), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT1), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountBHT1
		 , ((
			 SELECT IFNULL(SUM(AmountBHT2), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountBHT2), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT2), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT2), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountBHT2
		 , ((
			 SELECT IFNULL(SUM(AmountBHT5), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountBHT5), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT5), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT5), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountBHT5
		 , ((
			 SELECT IFNULL(SUM(AmountBHT10), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountBHT10), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT10), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT10), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountBHT10
		 , ((
			 SELECT IFNULL(SUM(AmountBHT20), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountBHT20), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT20), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT20), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountBHT20
		 , ((
			 SELECT IFNULL(SUM(AmountBHT50), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountBHT50), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT50), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT50), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountBHT50
		 , ((
			 SELECT IFNULL(SUM(AmountBHT100), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountBHT100), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT100), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT100), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountBHT100
		 , ((
			 SELECT IFNULL(SUM(AmountBHT500), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountBHT500), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT500), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT500), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountBHT500
		 , ((
			 SELECT IFNULL(SUM(AmountBHT1000), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Received = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AmountBHT1000), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) - 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT1000), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 1 -- Borrow
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			) + 
			(
			 SELECT IFNULL(SUM(UserCreditTransaction.AmountBHT1000), 0) 
			   FROM UserCreditTransaction, UserCreditBalance 
			  WHERE UserCreditBalance.TSBId = TSB.TSBId
				AND UserCreditTransaction.TransactionType = 2 -- Return
				AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
			)) AS AmountBHT1000
		 , ((
			 SELECT IFNULL(SUM(AdditionalBHT), 0) 
			   FROM TSBCreditTransaction 
			  WHERE (   TSBCreditTransaction.TransactionType = 0 
					 OR TSBCreditTransaction.TransactionType = 1
					) -- Initial = 0, Borrow = 1
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			) -
			(
			 SELECT IFNULL(SUM(AdditionalBHT), 0) 
			   FROM TSBCreditTransaction 
			  WHERE TSBCreditTransaction.TransactionType = 2 -- Returns = 2
				AND TSBCreditTransaction.TSBId = TSB.TSBId
			)) AS AdditionalBHTTotal
	  FROM TSB
