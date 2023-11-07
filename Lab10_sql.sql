SELECT ProductName, UnitPrice, CategoryName 
FROM Products p
JOIN Categories c ON p.CategoryID = c.CategoryID
ORDER BY CategoryName, ProductName
GO

SELECT c.CustomerID, c.CompanyName, COUNT(*) AS NumOfOrders
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerID, c.CompanyName
ORDER BY NumOfOrders DESC 
GO

SELECT e.EmployeeID, e.FirstName, e.LastName, t.TerritoryDescription
FROM EmployeeTerritories et
JOIN Employees e ON et.EmployeeID = e.EmployeeID
JOIN Territories t ON et.TerritoryID = t.TerritoryID
GO

--Extra challenges:
SELECT c.CustomerID, c.CompanyName, convert(decimal(10,2), SUM(od.UnitPrice * od.Quantity * (1-od.Discount))) AS SumOfTotalOrderValue
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
JOIN [Order Details] od on o.OrderID = od.OrderID
GROUP BY c.CustomerID, c.CompanyName
ORDER BY SumOfTotalOrderValue DESC 
GO