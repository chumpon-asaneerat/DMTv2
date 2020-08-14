CREATE VIEW UserView
AS
	SELECT [User].*
		 , TRIM(PRINTF("%s %s %s %s"
		      , [User].PrefixEN
			  , [User].FirstNameEN
			  , [User].MiddleNameEN
			  , [User].LastNameEN)) AS FullNameEN
		 , TRIM(PRINTF("%s %s %s %s"
		      , [User].PrefixTH
			  , [User].FirstNameTH
			  , [User].MiddleNameTH
			  , [User].LastNameTH)) AS FullNameTH
		 , [Role].RoleNameEN, [Role].RoleNameTH, [Role].GroupId
	  FROM [User]
	     , [Role]
	 WHERE [User].RoleId = [Role].RoleId
