CREATE VIEW UserCreditSummaryView
AS
	SELECT UserCredit.* 
		 , TSB.TSBNameEN
		 , TSB.TSBNameTH
		 , PlazaGroup.PlazaGroupNameEN, PlazaGroup.PlazaGroupNameTH, PlazaGroup.Direction 
		 , User.FullNameEN, User.FullNameTH 
		 , (
			 SELECT UserCreditBorrowSummaryView.CountST25 - UserCreditReturnSummaryView.CountST25
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountST25
		 , (
			 SELECT UserCreditBorrowSummaryView.CountST50 - UserCreditReturnSummaryView.CountST50
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountST50
		 , (
			 SELECT UserCreditBorrowSummaryView.CountBHT1 - UserCreditReturnSummaryView.CountBHT1
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountBHT1
		 , (
			 SELECT UserCreditBorrowSummaryView.CountBHT2 - UserCreditReturnSummaryView.CountBHT2
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountBHT2
		 , (
			 SELECT UserCreditBorrowSummaryView.CountBHT5 - UserCreditReturnSummaryView.CountBHT5
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountBHT5
		 , (
			 SELECT UserCreditBorrowSummaryView.CountBHT10 - UserCreditReturnSummaryView.CountBHT10
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountBHT10
		 , (
			 SELECT UserCreditBorrowSummaryView.CountBHT20 - UserCreditReturnSummaryView.CountBHT20
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountBHT20
		 , (
			 SELECT UserCreditBorrowSummaryView.CountBHT50 - UserCreditReturnSummaryView.CountBHT50
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountBHT50
		 , (
			 SELECT UserCreditBorrowSummaryView.CountBHT100 - UserCreditReturnSummaryView.CountBHT100
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountBHT100
		 , (
			 SELECT UserCreditBorrowSummaryView.CountBHT500 - UserCreditReturnSummaryView.CountBHT500
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountBHT500
		 , (
			 SELECT UserCreditBorrowSummaryView.CountBHT1000 - UserCreditReturnSummaryView.CountBHT1000
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS CountBHT1000
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountST25 - UserCreditReturnSummaryView.AmountST25
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountST25
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountST50 - UserCreditReturnSummaryView.AmountST50
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountST50
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountBHT1 - UserCreditReturnSummaryView.AmountBHT1
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountBHT1
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountBHT2 - UserCreditReturnSummaryView.AmountBHT2
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountBHT2
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountBHT5 - UserCreditReturnSummaryView.AmountBHT5
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountBHT5
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountBHT10 - UserCreditReturnSummaryView.AmountBHT10
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountBHT10
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountBHT20 - UserCreditReturnSummaryView.AmountBHT20
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountBHT20
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountBHT50 - UserCreditReturnSummaryView.AmountBHT50
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountBHT50
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountBHT100 - UserCreditReturnSummaryView.AmountBHT100
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountBHT100
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountBHT500 - UserCreditReturnSummaryView.AmountBHT500
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountBHT500
		 , (
			 SELECT UserCreditBorrowSummaryView.AmountBHT1000 - UserCreditReturnSummaryView.AmountBHT1000
			   FROM UserCreditBorrowSummaryView, UserCreditReturnSummaryView 
			  WHERE UserCreditBorrowSummaryView.UserCreditId = UserCredit.UserCreditId
				AND UserCreditReturnSummaryView.UserCreditId = UserCredit.UserCreditId
			) AS AmountBHT1000
	  FROM UserCredit, TSB, PlazaGroup, [User]
	 WHERE PlazaGroup.TSBId = TSB.TSBId 
	   AND UserCredit.TSBId = TSB.TSBId 
	   AND UserCredit.PlazaGroupId = PlazaGroup.PlazaGroupId 
	   AND UserCredit.UserId = User.UserId 
