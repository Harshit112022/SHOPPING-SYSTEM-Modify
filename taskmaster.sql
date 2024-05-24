CREATE TABLE Orders(
	OrderId INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	OrderDate Date NOT NULL,
	CustomerId INT NOT NULL ,
	FOREIGN KEY (CustomerId)REFERENCES Customers(CustomerId)
);
EXEC sp_RENAME 'Orders.OrderID' , 'OrderId', 'COLUMN'
INSERT INTO Orders (OrderDate,CustomerId)VALUES('2024-05-23',1)


CREATE TABLE OrderDetails
(
	OrderDetailId  INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	OrderID INT NOT NULL ,
	ProductId INT NOT NULL ,
	Quantity INT NOT NULL,
	FOREIGN KEY (OrderId)REFERENCES Orders(OrderId),
	FOREIGN KEY (ProductId)REFERENCES Products(ProductId)
);
EXEC sp_RENAME 'OrderDetails.OrderID' , 'OrderId', 'COLUMN'
INSERT INTO OrderDetails (OrderID,ProductId,Quantity)values
(1,1,12);
;


SELECT * FROM Orders
SELECT * FROM OrderDetails

CREATE OR ALTER PROC CountOrderId
@Count INT OUTPUT
/*
***********************************************************
1. harshit 23may2024 count the recode of Orders table 
DECLARE @VAR INT 
EXECUTE CountOrderId @VAR OUTPUT
PRINT @VAR
***********************************************************
*/
AS

BEGIN
	 SELECT @Count=(SELECT COUNT(*) FROM Orders) ;
END
    
      