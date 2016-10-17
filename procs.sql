USE [Restaurant]
---------------------------------------------
GO
CREATE PROC sp_GetAllMenuItems
AS BEGIN
	SELECT [Menu].item_id, [Menu].item_name, [Menu].item_description, [Menu].item_price, [Group].group_id, [Group].group_name
	FROM [Menu] INNER JOIN [Group] ON [Menu].item_group = [Group].group_id;
END
-----------------------------------------------
GO
CREATE PROC sp_GetMenuItemById(@item_id INT)
AS BEGIN
	SELECT [Menu].item_id, [Menu].item_name, [Menu].item_description, [Menu].item_price, [Menu].item_group,
	[Group].group_id,[Group].group_name
	FROM [Menu] INNER JOIN [Group] ON [Menu].item_group = [Group].group_id
	WHERE [Menu].item_id = @item_id;
END

-----------------------------------------------

GO
CREATE PROC sp_GetMenuItemsByGroup(@group_name VARCHAR(20))
AS BEGIN
	SELECT [Menu].item_id, [Menu].item_name, [Menu].item_description, [Menu].item_price, [Group].group_id,[Group].group_name
	FROM [Menu] INNER JOIN [Group]
	ON [Menu].item_group = [Group].group_id
	WHERE[Group].group_name = @group_name;
END
-----------------------------------------------

GO
CREATE PROC sp_GetAllGroups
AS BEGIN
	SELECT [Group].group_id, [Group].group_name FROM [Group];
END
----------------------------------------------
GO
CREATE PROC sp_GetGroupById(@id INT)
AS BEGIN
	SELECT [Group].group_name FROM [Group] WHERE [Group].group_id = @id;
END

-----------------------------------------------
GO
CREATE PROC sp_GetOrdersByDate(@date DATETIME)
AS BEGIN
	SELECT [Order].order_id,[Order].order_date,[Order].table_number,[Order].waiter,[Casher].casher_name,[Casher].casher_surname
	 FROM [Order] INNER JOIN [Casher]
	 ON [Order].waiter = [Casher].casher_id
	 WHERE DATEPART(DD,[Order].order_date) =  DATEPART(DD,@date) AND
		  DATEPART(MM,[Order].order_date) =  DATEPART(MM,@date) AND
		  DATEPART(YY,[Order].order_date) =  DATEPART(YY,@date);
END

----------------------------------------------

GO
CREATE PROC sp_GetOrdersByMonth(@month INT, @year INT)
AS BEGIN
	SELECT [Order].order_id,[Order].order_date,[Order].table_number,[Order].waiter,[Casher].casher_name,[Casher].casher_surname
	 FROM [Order] INNER JOIN [Casher]
	 ON [Order].waiter = [Casher].casher_id
	WHERE DATEPART(MM,[Order].order_date) = @month AND DATEPART(YYYY,[Order].order_date) = @year;
END

--------------------------------------------

GO
CREATE PROC sp_GetTableByStatus(@status VARCHAR(20))
AS BEGIN
	SELECT [Table].table_num
	FROM [Table]
	WHERE [Table].table_info = @status;
END


------------------------------------------

GO
CREATE PROC sp_GetOrderById(@order_id INT)
AS BEGIN
	SELECT [Order].order_id,[Order].waiter, [Casher].casher_name, [Casher].casher_surname, [Order].table_number, [Order].order_date
	FROM [Order] INNER JOIN [Casher] ON [Order].waiter = [Casher].casher_id
	WHERE [Order].order_id = @order_id;
END
------------------------------------------

GO
CREATE PROC sp_GetCasherById(@casher_id INT)
AS BEGIN
	SELECT [Casher].casher_name, [Casher].casher_surname
	FROM [Casher]
	WHERE [Casher].casher_id = @casher_id;
END
-------------------------------------------

GO
CREATE PROC sp_InsertMenuItem(@id INT,
							  @item_name VARCHAR(30),
							  @item_description TEXT,
							  @item_price FLOAT,
							  @item_group INT)
AS BEGIN
	
	INSERT  INTO [Menu] VALUES (@id,@item_name ,@item_description, @item_price, @item_group);
END

-------------------------------------------

GO
CREATE PROC sp_UpdateMenuItemPrice(@item_id INT,
								   @item_price FLOAT)
AS BEGIN
	UPDATE [Menu]
		SET [Menu].item_price = @item_price
		WHERE [Menu].item_id = @item_id;
END

-----------------------------------------

GO
CREATE PROC sp_DeleteMenuItem(@item_id INT)
AS BEGIN
	DELETE FROM [Menu]
	WHERE [Menu].item_id = @item_id;
END

-----------------------------------------

GO
CREATE PROC sp_InsertOrderDetailItem(@id INT,
									 @item_id INT,
									 @amount INT,
									 @order_id INT)
AS BEGIN
	INSERT INTO [OrderDetail] VALUES (@id,@item_id,@amount,@order_id);
END

-----------------------------------------

GO
CREATE PROC sp_UpdateOrderInfo(@order_id INT,
							   @item_id INT,
							   @amount INT)
AS BEGIN
	
	UPDATE [OrderDetail]
		SET [OrderDetail].amount = @amount
		WHERE [OrderDetail].order_id = @order_id AND [OrderDetail].item_id = @item_id;
END
-----------------------------------------

GO
CREATE PROC sp_InsertBillrecord(@id INT,
								@order_id INT,
								@total FLOAT,
								@bill_date DATETIME)
AS BEGIN
	
	INSERT INTO [Bill] VALUES (@id,@order_id, @total, @bill_date);
END
-----------------------------------------

GO
CREATE PROC sp_DeleteBillRecord(@bill_id INT)
AS BEGIN
	DELETE FROM [Bill]
	WHERE [Bill].bill_number = @bill_id;
END

-----------------------------------------
GO
CREATE PROC sp_InsertCasherRecord(@id INT,
								  @casher_name VARCHAR(30),
								  @casher_surname VARCHAR(30),
								  @casher_login VARCHAR(30),
								  @casher_pass VARCHAR(30))
								  
AS BEGIN
	SET NOCOUNT ON
	DECLARE @guid UNIQUEIDENTIFIER = NEWID();

	 INSERT INTO [Casher] VALUES(@id,@casher_name,@casher_surname,@casher_login,
			HASHBYTES('SHA2_512',@casher_pass+CAST(@guid AS NVARCHAR(36))),@guid);
END
-----------------------------------------
GO
CREATE PROC sp_GetCasherByLoginAndPass(@login VARCHAR(30),
										@pass VARCHAR(30))
AS BEGIN

	SELECT * FROM [Casher] WHERE [Casher].casher_login = @login AND [Casher].casher_password = HASHBYTES('SHA2_512', @pass+CAST([Casher].casher_guid AS NVARCHAR(36)));
	
END
-----------------------------------------
GO
CREATE PROC sp_DeleteCasherRecord(@casher_id INT)
AS BEGIN
	DELETE FROM [Casher]
	WHERE [Casher].casher_id = @casher_id;
END
-----------------------------------------
GO
CREATE PROC sp_InsertGroupRecord(@id INT,@group_name VARCHAR(30))
AS BEGIN
	INSERT INTO [Group] VALUES (@id,@group_name);
END
-----------------------------------------
GO
CREATE PROC sp_DeleteGroupRecord(@group_id INT)
AS BEGIN
	DELETE FROM [Group]
	WHERE [Group].group_id = @group_id;
END
----------------------------------------
GO
CREATE PROC sp_InsertOrderRecord(@id INT,
								 @waiter_id INT,
								 @table_number INT,
								 @date DATETIME)
AS BEGIN
	INSERT INTO [Order] VALUES(@id,@waiter_id,@table_number,@date);
END
-----------------------------------------

GO
CREATE PROC sp_DeleteOrderRecord(@order_id INT)
AS BEGIN
	DELETE FROM [Order]
	WHERE [Order].order_id = @order_id;
END

-----------------------------------
GO
CREATE PROC sp_UpdateTableInfo(@id INT,@info VARCHAR(30))
AS BEGIN
	UPDATE [Table]
	SET [Table].table_info = @info
	WHERE [Table].table_num = @id;

END
--------------------------------------
GO
CREATE PROC sp_GetAllOrders
AS BEGIN
	SELECT [Order].order_id,[Order].order_date,[Order].table_number,[Order].waiter,[Casher].casher_name,[Casher].casher_surname
	 FROM [Order] INNER JOIN [Casher]
	 ON [Order].waiter = [Casher].casher_id;
END
----------------------------------------
GO
CREATE PROC sp_GetTableByNumber(@id INT)
AS BEGIN
	SELECT [Table].table_num, [Table].table_info
	FROM [Table] 
	WHERE [Table].table_num = @id;
END

--------------------------------------
GO
CREATE PROC sp_GetOrderByTable(@table_num INT)
AS BEGIN
	 SELECT [Order].order_id,[Order].order_date,[Order].table_number,[Order].waiter,[Casher].casher_name,[Casher].casher_surname
	 FROM [Order] INNER JOIN [Casher]
	 ON [Order].waiter = [Casher].casher_id
	 WHERE [Order].table_number = @table_num;
END
-----------------------------------------
GO
CREATE PROC sp_GetBillByOrder(@order_id INT)
AS BEGIN
	
	SELECT [Bill].bill_number, [Bill].total, [Bill].bill_date
	FROM [Bill] 					
	WHERE [Bill].order_id = @order_id;
END
--------------------------------------
GO
CREATE PROC sp_GetOrderDetail (@order_id INT)
AS BEGIN
	SELECT [OrderDetail].id, [OrderDetail].item_id, [OrderDetail].order_id, [OrderDetail].amount
	 FROM [OrderDetail]
	 WHERE [OrderDetail].order_id = @order_id;
END

-------------------------------------------
GO
CREATE PROC sp_GetAllOrderDetail
AS BEGIN
	SELECT [OrderDetail].id, [OrderDetail].item_id, [OrderDetail].order_id, [OrderDetail].amount
	 FROM [OrderDetail] 
END