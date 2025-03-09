create database logicvault
use logicvault
use master
drop database logicvault

CREATE TABLE Business (
    BusinessID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL, 
	UserName NVARCHAR(100) NOT NULL,
	Password NVARCHAR(255) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
	Active BIT NOT NULL
);

CREATE TABLE Role (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);

CREATE TABLE Brand (
    BrandID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
	BusinessID INT NOT NULL,
	Active BIT NOT NULL,
	FOREIGN KEY (BusinessID) REFERENCES Business(BusinessID)
);

CREATE TABLE Category (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
	Description NVARCHAR (500) NOT NULL,
	BusinessID INT NOT NULL,
	Active BIT NOT NULL,
	FOREIGN KEY (BusinessID) REFERENCES Business(BusinessID)
);


CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
	Description NVARCHAR (500) NOT NULL,
    BrandID INT NOT NULL,
    CategoryID INT NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
	BusinessID INT NOT NULL,
	Active BIT NOT NULL,
    CreationDate DATETIME DEFAULT GETDATE(),
    UpdateDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (BrandID) REFERENCES Brand(BrandID),
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
	FOREIGN KEY (BusinessID) REFERENCES Business(BusinessID)
);

CREATE TABLE ProductSource (
    ProductSourceID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    SourceName NVARCHAR(100) NOT NULL, -- Ejemplo: 'Shein', 'Amazon', 'AliExpress'
    SourceSKU NVARCHAR(100) NOT NULL, -- SKU en la tienda de origen
    SourceProductName NVARCHAR(255) NOT NULL, -- Nombre del producto en la tienda de origen
    SourceURL NVARCHAR(500) NOT NULL, -- URL del producto en la tienda de origen
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID) ON DELETE CASCADE
);


CREATE TABLE [User] (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    UserName NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
	BusinessID int NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100),
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
	FOREIGN KEY (BusinessId) REFERENCES Business(BusinessID)
);

CREATE TABLE UserRole (
    UserRoleID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    RoleID INT NOT NULL,
    AssignedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES [User](UserID) ON DELETE CASCADE,
    FOREIGN KEY (RoleID) REFERENCES Role(RoleID) ON DELETE CASCADE,
    UNIQUE(UserID, RoleID) -- Evita duplicados para la misma combinación de User y Role
);

CREATE TABLE ProductLog (
    LogID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    Action NVARCHAR(50) NOT NULL,
    Date DATETIME DEFAULT GETDATE(),
    UserID INT NOT NULL,
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
	FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

CREATE TABLE Sale (
    SaleID INT PRIMARY KEY IDENTITY(1,1),
    BusinessID INT NOT NULL,
    UserID INT NOT NULL,
    SaleDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (BusinessID) REFERENCES Business(BusinessID),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

CREATE TABLE SaleDetail (
    SaleDetailID INT PRIMARY KEY IDENTITY(1,1),
    SaleID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    TotalPrice AS (Quantity * UnitPrice) PERSISTED,
    FOREIGN KEY (SaleID) REFERENCES Sale(SaleID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);


CREATE TABLE [Order] (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    BusinessID INT NOT NULL,
    UserID INT NOT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    FOREIGN KEY (BusinessID) REFERENCES Business(BusinessID),
    FOREIGN KEY (UserID) REFERENCES [User](UserID)
);

CREATE TABLE OrderDetail (
    OrderDetailID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    TotalPrice AS (Quantity * UnitPrice) PERSISTED,
    FOREIGN KEY (OrderID) REFERENCES [Order](OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

CREATE TABLE Supplier (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    BusinessID INT NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255),
    Phone NVARCHAR(20),
    Email NVARCHAR(100),
    FOREIGN KEY (BusinessID) REFERENCES Business(BusinessID)
);

CREATE TABLE Stock (
    ProductID INT,
    SupplierID INT,
    BusinessID INT,
    Quantity INT NOT NULL,
    PRIMARY KEY (ProductID, SupplierID, BusinessID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID),
    FOREIGN KEY (BusinessID) REFERENCES Business(BusinessID)
);

CREATE TABLE StockMovement (
    MovementID INT PRIMARY KEY IDENTITY(1,1),
    WarehouseID INT NOT NULL,
    ProductID INT NOT NULL,
    SupplierID INT,
    MovementType NVARCHAR(50) NOT NULL, -- Ej. 'Entrada', 'Salida', 'Ajuste'
    Quantity INT NOT NULL,
    MovementDate DATETIME DEFAULT GETDATE(),
	BusinessID INT NOT NULL,
    FOREIGN KEY (BusinessID) REFERENCES Business(BusinessID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID),
    FOREIGN KEY (SupplierID) REFERENCES Supplier(SupplierID)
);




SELECT TOP (1000) [ProductID]
      ,[Name]
      ,[Description]
      ,[BrandID]
      ,[CategoryID]
      ,[Price]
      ,[BusinessID]
      ,[Active]
      ,[CreationDate]
      ,[UpdateDate]
  FROM [logicvault].[dbo].[Product]


  INSERT INTO [logicvault].[dbo].[Product] 
    ([Name], [Description], [BrandID], [CategoryID], [Price], [BusinessID], [Active], [CreationDate], [UpdateDate]) 
VALUES 
    ('Laptop Gamer', 'Laptop con tarjeta gráfica RTX 4060', 4, 1, 1500.00, 1, 1, GETDATE(), GETDATE()),
    ('Teclado Mecánico', 'Teclado RGB con switches rojos', 4, 1, 120.50, 1, 1, GETDATE(), GETDATE()),
    ('Mouse Inalámbrico', 'Mouse ergonómico con sensor óptico', 4, 1, 45.99, 1, 1, GETDATE(), GETDATE()),
    ('Monitor 27 Pulgadas', 'Monitor 144Hz Full HD', 4, 1, 300.00, 1, 1, GETDATE(), GETDATE()),
    ('Silla Gamer', 'Silla ergonómica con soporte lumbar', 5, 1, 250.00, 1, 1, GETDATE(), GETDATE());
