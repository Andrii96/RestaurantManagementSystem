USE [Restaurant]

DROP TABLE [dbo].[Table];
DROP TABLE [dbo].[Casher];
DROP TABLE [dbo].[Group];
DROP TABLE [dbo].[Menu];
DROP TABLE [dbo].[Order];
DROP TABLE [dbo].[OrderDetail];
DROP TABLE [dbo].[Bill];
----------------------------------------------------

DROP PROC sp_GetAllMenuItems;
DROP PROC sp_GetMenuItemById;
DROP PROC sp_GetMenuItemsByGroup;
DROP PROC sp_GetAllGroups;
DROP PROC sp_GetGroupById;
DROP PROC sp_GetOrdersByDate;
DROP PROC sp_GetOrdersByMonth;
DROP PROC sp_GetTableByStatus;
DROP PROC sp_GetOrderById;
DROP PROC sp_GetCasherById;
DROP PROC sp_InsertMenuItem;
DROP PROC sp_UpdateMenuItemPrice;
DROP PROC sp_DeleteMenuItem;
DROP PROC sp_InsertOrderDetailItem;
DROP PROC sp_UpdateOrderInfo;
DROP PROC sp_InsertBillrecord;
DROP PROC sp_DeleteBillRecord;
DROP PROC sp_InsertCasherRecord;
DROP PROC sp_GetCasherByLoginAndPass;
DROP PROC sp_DeleteCasherRecord;
DROP PROC sp_InsertGroupRecord;
DROP PROC sp_DeleteGroupRecord;
DROP PROC sp_InsertOrderRecord;
DROP PROC sp_DeleteOrderRecord;
DROP PROC sp_UpdateTableInfo;
DROP PROC sp_GetAllOrders;
DROP PROC sp_GetTableByNumber;
DROP PROC sp_GetOrderByTable;
DROP PROC sp_GetBillByOrder;
DROP PROC sp_GetOrderDetail;
DROP PROC sp_GetAllOrderDetail;
