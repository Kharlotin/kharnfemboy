USE demo;
GO

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Login NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(20) NOT NULL CHECK (Role IN ('Администратор', 'Пользователь')),
    IsBlocked BIT DEFAULT 0,
    FailedAttempts INT DEFAULT 0
);

INSERT INTO Users (Login, Password, Role) VALUES
(N'admin', N'admin123', N'Администратор'),
(N'user1', N'user123', N'Пользователь');

CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(50) UNIQUE NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Unit NVARCHAR(20) NOT NULL
);

INSERT INTO Products (Code, Name, Unit) VALUES
(N'НФ-00000003', N'Сосиски молочные 850г.', N'шт'),
(N'НФ-00000006', N'Пельмени "Сибирские" 900г.', N'шт'),
(N'НФ-00000007', N'Пельмени "Из говядины" 900г.', N'шт'),
(N'НФ-00000008', N'Сосиски венские 850г.', N'шт');

CREATE TABLE Materials (
    MaterialID INT PRIMARY KEY IDENTITY(1,1),
    Code NVARCHAR(50) UNIQUE NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    Unit NVARCHAR(20) NOT NULL
);

INSERT INTO Materials (Code, Name, Unit) VALUES
(N'НФ-00000004', N'Молоко нормализованное', N'кг'),
(N'НФ-00000005', N'Говядина', N'кг'),
(N'НФ-00000009', N'Соль', N'кг'),
(N'НФ-00000010', N'Оболочка натуральная', N'шт');

CREATE TABLE Prices (
    PriceID INT PRIMARY KEY IDENTITY(1,1),
    ItemType NVARCHAR(20) NOT NULL CHECK (ItemType IN ('Product', 'Material')),
    ItemCode NVARCHAR(50) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    DateSet DATETIME DEFAULT GETDATE()
);

INSERT INTO Prices (ItemType, ItemCode, Price) VALUES
('Material', N'НФ-00000005', 370.00),
('Material', N'НФ-00000004', 34.00),
('Material', N'НФ-00000010', 20.00),
('Product', N'НФ-00000007', 370.00),
('Product', N'НФ-00000006', 450.00),
('Material', N'НФ-00000009', 60.00),
('Product', N'НФ-00000008', 570.00),
('Product', N'НФ-00000003', 560.00);


CREATE TABLE ProductComposition (
    CompositionID INT PRIMARY KEY IDENTITY(1,1),
    ProductCode NVARCHAR(50) FOREIGN KEY REFERENCES Products(Code),
    MaterialCode NVARCHAR(50) FOREIGN KEY REFERENCES Materials(Code),
    Quantity DECIMAL(18,3) NOT NULL
);

INSERT INTO ProductComposition (ProductCode, MaterialCode, Quantity) VALUES
(N'НФ-00000003', N'НФ-00000004', 0.4),
(N'НФ-00000003', N'НФ-00000005', 1.0),
(N'НФ-00000003', N'НФ-00000009', 0.003),
(N'НФ-00000003', N'НФ-00000010', 0.1);


CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerCode NVARCHAR(20) UNIQUE NOT NULL,
    Name NVARCHAR(255) NOT NULL,
    INN NVARCHAR(20),
    Address NVARCHAR(255),
    Phone NVARCHAR(20),
    IsSalesman BIT,
    IsBuyer BIT
);

INSERT INTO Customers (CustomerCode, Name, INN, Address, Phone, IsSalesman, IsBuyer) VALUES
(N'000000001', N'ООО "Поставка"', N'', N'г.Пятигорск', N'+79198634592', 1, 1),
(N'000000002', N'ООО "Кинотеатр Квант"', N'26320045123', N'г. Железноводск, ул. Мира, 123', N'+79884581555', 1, 0),
(N'000000008', N'ООО "Новый JDTO"', N'26320045111', N'г. Железноводсу', N'+79884581555', 1, 0),
(N'000000003', N'ООО "Ромашка"', N'4140784214', N'г. Омск, ул. Строителей, 294', N'+79882584546', 0, 1),
(N'000000009', N'ООО "Ипподром"', N'5874045632', N'г. Уфа, ул. Набережная,  37', N'+79627486389', 1, 1),
(N'000000010', N'ООО "Ассоль"', N'2629011278', N'г. Калуга, ул. Пушкина, 94', N'+79184572398', 0, 1);


CREATE TABLE Orders (
    OrderID INT PRIMARY KEY IDENTITY(1,1),
    OrderNumber NVARCHAR(50) NOT NULL,
    OrderDate DATE NOT NULL,
    Executor NVARCHAR(255) NOT NULL,
    CustomerCode NVARCHAR(20) FOREIGN KEY REFERENCES Customers(CustomerCode)
);

INSERT INTO Orders (OrderNumber, OrderDate, Executor, CustomerCode) VALUES
(N'Заказ покупателя № 2', '2025-06-06', N'ООО "Мясной цех №1"', N'000000010');


CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY IDENTITY(1,1),
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
    ProductCode NVARCHAR(50) FOREIGN KEY REFERENCES Products(Code),
    Quantity INT NOT NULL,
    Price DECIMAL(18,2) NOT NULL
);

INSERT INTO OrderItems (OrderID, ProductCode, Quantity, Price) VALUES
(1, N'НФ-00000006', 4, 450.00),
(1, N'НФ-00000007', 8, 510.00),
(1, N'НФ-00000008', 3, 370.00);


CREATE TABLE Productions (
    ProductionID INT PRIMARY KEY IDENTITY(1,1),
    ProductionNumber NVARCHAR(50) NOT NULL,
    ProductionDate DATE NOT NULL
);

INSERT INTO Productions (ProductionNumber, ProductionDate) VALUES
(N'Производство № 1', '2025-06-09');


CREATE TABLE ProductionItems (
    ProductionItemID INT PRIMARY KEY IDENTITY(1,1),
    ProductionID INT FOREIGN KEY REFERENCES Productions(ProductionID),
    ItemType NVARCHAR(20) NOT NULL CHECK (ItemType IN ('Product', 'Material')),
    ItemCode NVARCHAR(50) NOT NULL,
    Quantity DECIMAL(18,3) NOT NULL,
    Unit NVARCHAR(20) NOT NULL
);

INSERT INTO ProductionItems (ProductionID, ItemType, ItemCode, Quantity, Unit) VALUES
(1, 'Product', N'НФ-00000003', 1, N'шт'),
(1, 'Material', N'НФ-00000004', 0.4, N'кг'),
(1, 'Material', N'НФ-00000005', 1, N'кг'),
(1, 'Material', N'НФ-00000009', 0.003, N'кг'),
(1, 'Material', N'НФ-00000010', 0.1, N'шт');