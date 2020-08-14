CREATE VIEW UserCouponSummaryView
AS
	SELECT TSB.TSBId, TSB.TSBNameEN, TSB.TSBNameTH
	     , UserView.UserId, UserView.FullNameEN, UserView.FullNameTH 
		 , UserCoupon35SummaryView.CountBorrow35, UserCoupon35SummaryView.CountReturn35
		 , UserCoupon80SummaryView.CountBorrow80, UserCoupon80SummaryView.CountReturn80
	  FROM TSB, UserView, UserCoupon35SummaryView, UserCoupon80SummaryView
	 WHERE UserCoupon35SummaryView.TSBId = TSB.TSBId
	   AND UserCoupon80SummaryView.TSBId = TSB.TSBId
	   AND UserCoupon35SummaryView.UserId = UserView.UserId
	   AND UserCoupon80SummaryView.UserId = UserView.UserId
	   AND UserCoupon35SummaryView.CountBorrow35 > UserCoupon35SummaryView.CountReturn35
	   AND UserCoupon80SummaryView.CountBorrow80 > UserCoupon80SummaryView.CountReturn80
