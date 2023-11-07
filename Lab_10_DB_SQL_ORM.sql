--Get name, price and category of all products. Sorted by category and product name.
SELECT ProductName, UnitPrice, CategoryName FROM Products
JOIN Categories ON Products.CategoryID = Categories.CategoryID
ORDER BY CategoryName, ProductName;

--Get all customers and their number of orders. Sorted by descending number of orders.
SELECT CompanyName, COUNT(OrderID) AS AmountOfOrders FROM Customers
JOIN Orders ON Orders.CustomerID = Customers.CustomerID
GROUP BY CompanyName
ORDER BY AmountOfOrders DESC;

--Get all employees and the territories they are managing.
SELECT FirstName + ' ' + LastName AS Managers, TerritoryDescription AS Territory FROM Employees
JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID
JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID

