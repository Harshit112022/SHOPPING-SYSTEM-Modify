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

ALTER TABLE Orders
ADD 	
	IsActive BIT DEFAULT  1,
	CreatedBy VARCHAR (MAX),
	CreatedOn DATETIME,
	LastModifiedBy VARCHAR (MAX),
	LastModifiedOn DATETIME;

UPDATE Orders
SET CreatedBy ='admin',
SET CreatedOn = '2024-05-24'
where OrderId=1

ALTER TABLE OrderDetails
ADD 	
	IsActive BIT DEFAULT  1,
	CreatedBy VARCHAR (MAX),
	CreatedOn DATETIME,
	LastModifiedBy VARCHAR (MAX),
	LastModifiedOn DATETIME;

UPDATE OrderDetails
SET IsActive =1
--SET CreatedBy ='admin'
--SET CreatedOn = '2024-05-24'
where OrderId=1



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
	 SELECT @Count=IDENT_CURRENT('Orders') ;
END
    

DELETE Orders
WHERE OrderId=2
SELECT IDENT_CURRENT('Orders') AS LastIdentityValue;


CREATE PROC OrdersInsert ,'2024-05-05'
@CustomerId int,
@Date DATE

AS
--SELECT * FROM Orders
BEGIN
	INSERT INTO Orders(OrderDate,CustomerId,IsActive,CreatedBy,CreatedOn)
	VALUES(@Date,@CustomerId,1,'ADMIN',GETDATE())
END
ALTER OR CREATE PROC InsertOrderWithDetails 
 @CustomerId INT,
 @Date DATETIME,
@OrderDetails XML
AS 
/*
select * from Orders
SELECT * FROM OrderDetails
EXEC InsertOrderWithDetails 
    @CustomerId = 1,
    @Date = '2024-02-01',
    @OrderDetails = [{"ProductId":1,"Quantity":11},{"ProductId":1,"Quantity":11}]
DECLARE @XMLDATA XML;
SET @XMLDATA ='<ProductOrderList>
  <ProductOrder>
    <ProductId>1</ProductId>
    <Quantity>5</Quantity>
  </ProductOrder>
  <ProductOrder>
    <ProductId>2</ProductId>
    <Quantity>3</Quantity>
  </ProductOrder>
</ProductOrderList>'
(SELECT 1,
	T.C.value('(ProductId)[1]','INT'),
	T.C.value('(Quantity)[1]','INT')
	FROM
	@XMLDATA.nodes('/ProductOrderList/ProductOrder') AS T(C)) 

*/

BEGIN
	INSERT INTO Orders (OrderDate,CustomerId,IsActive,CreatedBy,CreatedOn)
	values(@Date,@CustomerId,1,'admin',GETDATE())
	DECLARE @OderId int  = IDENT_CURRENT('Orders')
	 INSERT INTO OrderDetails (OrderId, ProductId, Quantity,IsActive,CreatedBy,CreatedOn)
	(SELECT @OderId,
	T.C.value('(ProductId)[1]','INT'),
	T.C.value('(Quantity)[1]','INT'),
	1,
	'admin',
	GETDATE()
	FROM
	@OrderDetails.nodes('/ProductOrderList/ProductOrder') AS T(C)) 
END
