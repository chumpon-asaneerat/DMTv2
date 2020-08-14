CREATE VIEW UserView
AS
	SELECT [User].*
		 , [Role].RoleNameEN, [Role].RoleNameTH, [Role].GroupId
	  FROM [User]
	     , [Role]
	 WHERE [User].RoleId = [Role].RoleId
