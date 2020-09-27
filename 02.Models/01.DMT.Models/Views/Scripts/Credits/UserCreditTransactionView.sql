CREATE VIEW UserCreditTransactionView
AS
	SELECT UserCreditTransaction.*
	     , TSB.TSBNameEN, TSB.TSBNameTH
		 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction
	     --, UserView.FullNameEN, UserView.FullNameTH 
		 , UserCreditBalance.UserCreditDate
		 , UserCreditBalance.[State], UserCreditBalance.BagNo, UserCreditBalance.BeltNo
	  FROM UserCreditTransaction, TSB, PlazaGroup
	     --, UserView
		 , UserCreditBalance
	 WHERE PlazaGroup.TSBId = TSB.TSBId
	   AND UserCreditBalance.TSBId = TSB.TSBId
	   AND UserCreditBalance.PlazaGroupId = PlazaGroup.PlazaGroupId
	   AND UserCreditTransaction.UserId = UserCreditBalance.UserId
	   AND UserCreditTransaction.UserCreditId = UserCreditBalance.UserCreditId
