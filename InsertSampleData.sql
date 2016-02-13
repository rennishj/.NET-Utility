insert into dbo.MenuItemType
(
	Name
)
values
(
	'Link'
)

insert into dbo.MenuItem
(
	ParentId,
	DisplayName,
	Url,
	MenuItemTypeId
)
values
(
	null,
	'Home',
	'http://www.asp.net',
	1
),
(
	1,
	'Sql Helper',
	'http://www.asp.net',
	1
),
(
	1,
	'C# Helper',
	'http://www.asp.net',
	1
),
(
	1,
	'Xml Helper',
	'http://www.asp.net',
	1
)