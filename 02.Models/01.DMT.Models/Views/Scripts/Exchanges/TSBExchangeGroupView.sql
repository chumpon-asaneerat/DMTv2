CREATE VIEW TSBExchangeGroupView
AS
	SELECT TSBExchange.* 
		 , TSB.TSBNameEN
		 , TSB.TSBNameTH
		 --, UserView.FullNameEN, UserView.FullNameTH
	  FROM TSBExchange
		 , TSB
		 --, UserView
	 WHERE TSBExchange.TSBId = TSB.TSBId
	   --AND TSBExchange.UserId = UserView.UserId
