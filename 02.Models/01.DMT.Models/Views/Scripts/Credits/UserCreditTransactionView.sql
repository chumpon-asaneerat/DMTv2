CREATE VIEW UserCreditTransactionView
AS
	SELECT UserCreditTransaction.*
	     , TSB.TSBNameEN, TSB.TSBNameTH
		 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction
	     , UserView.FullNameEN, UserView.FullNameTH 
		 , UserCredit.UserCreditDate
		 , UserCredit.[State], UserCredit.BagNo, UserCredit.BeltNo
	  FROM UserCreditTransaction, TSB, PlazaGroup, UserView, UserCredit
	 WHERE PlazaGroup.TSBId = TSB.TSBId
	   AND UserCredit.TSBId = TSB.TSBId
	   AND UserCredit.PlazaGroupId = PlazaGroup.PlazaGroupId
	   AND UserCredit.UserId = UserView.UserId
	   AND UserCouponTransaction.UserCreditId = UserCredit.UserCreditId
