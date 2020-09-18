CREATE VIEW TSBCreditTransactionView
AS
	/*
	SELECT TSBCreditTransaction.*
	     , TSB.TSBNameEN, TSB.TSBNameTH
	  FROM TSBCreditTransaction, TSB
	 WHERE TSBCreditTransaction.TSBId = TSB.TSBId
	 */
	SELECT TSBCreditTransaction.*
	     , TSB.TSBNameEN, TSB.TSBNameTH
		 , IFNULL(sup.FullNameEN, '') AS SupervisorNameEN
		 , IFNULL(sup.FullNameTH, '') AS SupervisorNameTH
	  FROM TSBCreditTransaction
	     , TSB
	       LEFT JOIN [UserView] sup ON (TSBCreditTransaction.SupervisorId = sup.UserId) 
	 WHERE TSBCreditTransaction.TSBId = TSB.TSBId