CREATE VIEW UserCouponSummaryView
AS
	SELECT TSB.TSBId, TSB.TSBNameEN, TSB.TSBNameTH
	     , User.UserId, User.FullNameEN, User.FullNameTH 
		 , UserCoupon35SummaryView.CountBorrow35, UserCoupon35SummaryView.CountReturn35
		 , UserCoupon80SummaryView.CountBorrow80, UserCoupon80SummaryView.CountReturn80
	  FROM TSB, [User], UserCoupon35SummaryView, UserCoupon80SummaryView
	 WHERE UserCoupon35SummaryView.TSBId = TSB.TSBId
	   AND UserCoupon80SummaryView.TSBId = TSB.TSBId
	   AND UserCoupon35SummaryView.UserId = User.UserId
	   AND UserCoupon80SummaryView.UserId = User.UserId
	   AND UserCoupon35SummaryView.CountBorrow35 > UserCoupon35SummaryView.CountReturn35
	   AND UserCoupon80SummaryView.CountBorrow80 > UserCoupon80SummaryView.CountReturn80
