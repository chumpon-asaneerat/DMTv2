CREATE VIEW TSBExchangeTransactionView
AS
	SELECT TSBExchangeTransaction.* 
		 , TSB.TSBNameEN
		 , TSB.TSBNameTH
		 , UserView.FullNameEN, UserView.FullNameTH
	  FROM TSBExchangeTransaction
		 , TSB
		 , UserView
	 WHERE TSBExchangeTransaction.TSBId = TSB.TSBId
	   AND TSBExchangeTransaction.UserId = UserView.UserId
