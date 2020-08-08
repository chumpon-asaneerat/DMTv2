CREATE VIEW TSBExchangeTransactionView
AS
	SELECT TSBExchangeTransaction.* 
		 , TSB.TSBNameEN
		 , TSB.TSBNameTH
		 , [User].FullNameEN, [User].FullNameTH
	  FROM TSBExchangeTransaction
		 , TSB
		 , [User]
	 WHERE TSBExchangeTransaction.TSBId = TSB.TSBId
	   AND TSBExchangeTransaction.UserId = User.UserId
