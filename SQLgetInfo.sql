USE demo;
GO
-- общая стомисоть материалов для продукта
SELECT
    p.Name AS ProductName,
    m.Name AS MaterialName,
    pc.Quantity,
    m.Unit,
    pr.Price AS MaterialPrice,
    (pc.Quantity * pr.Price) AS MaterialCost
FROM
    ProductComposition pc
    JOIN Products p ON pc.ProductCode = p.Code
    JOIN Materials m ON pc.MaterialCode = m.Code
    JOIN Prices pr ON m.Code = pr.ItemCode AND pr.ItemType = 'Material'
WHERE
    p.Code = N'НФ-00000003';


-- количество продукции в заказе
SELECT
    o.OrderNumber,
    o.OrderDate,
    c.Name AS CustomerName,
    p.Name AS ProductName,
    oi.Quantity,
    oi.Price AS UnitPrice,
    (oi.Quantity * oi.Price) AS TotalPrice
FROM
    Orders o
    JOIN Customers c ON o.CustomerCode = c.CustomerCode
    JOIN OrderItems oi ON o.OrderID = oi.OrderID
    JOIN Products p ON oi.ProductCode = p.Code
WHERE
    o.OrderNumber = N'Заказ покупателя № 2';

-- Общая сумма заказа
SELECT
    o.OrderNumber,
    SUM(oi.Quantity * oi.Price) AS OrderTotal
FROM
    Orders o
    JOIN OrderItems oi ON o.OrderID = oi.OrderID
WHERE
    o.OrderNumber = N'Заказ покупателя № 2'
GROUP BY o.OrderNumber;