
USE [Restaurant]

IF OBJECT_ID('dbo.Table','U') IS NOT NULL
	DROP TABLE [dbo].[Table];

CREATE TABLE [dbo].[Table]( table_num INT  NOT NULL,
							table_info VARCHAR(20) NOT NULL,
							CONSTRAINT prim_table PRIMARY KEY(table_num));



IF OBJECT_ID('dbo.Casher','U') IS NOT NULL
	DROP TABLE [dbo].[Casher];

CREATE TABLE [dbo].[Casher] ( casher_id INT NOT NULL,
							  casher_name VARCHAR(30) NOT NULL,
							  casher_surname VARCHAR(30) NOT NULL,
							  casher_login VARCHAR(20) NOT NULL,
							  casher_password BINARY(64) NOT NULL,
							  casher_guid UNIQUEIDENTIFIER NOT NULL,
							  CONSTRAINT prim_casher PRIMARY KEY(casher_id));

IF OBJECT_ID('dbo.Group','U') IS NOT NULL
	DROP TABLE [dbo].[Group];

CREATE TABLE [dbo].[Group]( group_id INT NOT NULL,
							group_name VARCHAR(20) NOT NULL,
							CONSTRAINT prim_group PRIMARY KEY(group_id));


IF OBJECT_ID('dbo.Menu','U') IS NOT NULL
	DROP TABLE [dbo].[Menu];

CREATE TABLE [dbo].[Menu]( item_id INT NOT NULL,
						   item_name VARCHAR(30) NOT NULL,
						   item_description TEXT NOT NULL,
						   item_price FLOAT NOT NULL,
						   item_group INT NOT NULL,
						   CONSTRAINT prim_item PRIMARY KEY(item_id),
						   CONSTRAINT foreign_group FOREIGN KEY(item_group)
						   REFERENCES [Group](group_id)
						   ON UPDATE CASCADE ON DELETE CASCADE);

IF OBJECT_ID('dbo.Order','U') IS NOT NULL
	DROP TABLE [dbo].[Order];


CREATE TABLE [dbo].[Order]( order_id INT NOT NULL,
							waiter INT NOT NULL,
							table_number INT NOT NULL,
							order_date DATETIME NOT NULL,
							CONSTRAINT prim_order PRIMARY KEY(order_id),
							CONSTRAINT foreign_waiter FOREIGN KEY(waiter)
							REFERENCES [Casher](casher_id)
							ON UPDATE CASCADE ON DELETE CASCADE,
							CONSTRAINT foreign_table FOREIGN KEY(table_number)
							REFERENCES [Table](table_num)
							ON UPDATE CASCADE ON DELETE CASCADE);


IF OBJECT_ID('dbo.OrderDetail','U') IS NOT NULL
	DROP TABLE [dbo].[OrderDetail];

CREATE TABLE OrderDetail ( id INT NOT NULL,
						   item_id INT NOT NULL,
						   amount INT NOT NULL,
						   order_id INT NOT NULL,
						   CONSTRAINT prim_id PRIMARY KEY(id),
						   CONSTRAINT foreign_item FOREIGN KEY(item_id)
						   REFERENCES Menu(item_id)
						   ON UPDATE CASCADE ON DELETE CASCADE,
						   CONSTRAINT fareign_order FOREIGN KEY(order_id)
						   REFERENCES [Order](order_id)
						   ON UPDATE CASCADE ON DELETE CASCADE);

IF OBJECT_ID('dbo.Bill','U') IS NOT NULL
	DROP TABLE [dbo].[Bill];

CREATE TABLE [dbo].[Bill] ( bill_number INT NOT NULL,
							order_id INT NOT NULL,
							total FLOAT NOT NULL,
							bill_date DATETIME NOT NULL,
							CONSTRAINT prim_bill PRIMARY KEY(bill_number),
							CONSTRAINT foreign_order FOREIGN KEY(order_id)
							REFERENCES [Order](order_id)
							ON UPDATE CASCADE ON DELETE CASCADE);
							
