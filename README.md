# 🏨 Advanced Hotel Reservation System

A console-based hotel reservation system built using **C#**, demonstrating strong Object-Oriented Programming (OOP) principles, clean architecture, and **database integration with Entity Framework Core and SQL Server**.

---

## 🚀 Features

* Add Guests, Rooms, and Employees
* Room Reservation System
* Cancel & Complete Reservations
* Prevent Duplicate Room IDs
* Limit Active Reservations per Guest
* Revenue Calculation
* Top Customers (Highest Spending)
* Top Rooms (Most Reserved)
* Discount System using Strategy Pattern
* **Persistent Data Storage using SQL Server**

---

## 🧠 Concepts Used

* OOP (Encapsulation, Inheritance, Abstraction, Polymorphism)
* LINQ (Filtering, Sorting, Aggregation)
* Design Patterns (Strategy Pattern)
* Exception Handling
* Clean Code Principles
* **Entity Framework Core (Code First Approach)**
* **Database Relationships (One-to-Many)**

---

## 💰 Discount System

Implemented using `IDiscountStrategy` interface:

* 5% discount for ≤ 5 nights
* 10% discount for 6–10 nights
* 15% discount for > 10 nights

---

## 🗄 Database Integration

* SQL Server (hosted on a Virtual Machine)
* Entity Framework Core
* Code First Migrations
* Tables:

  * Guests
  * Rooms
  * Reservations
  * Employees

---

## 🔧 Technical Highlights

* Configured **remote SQL Server connection (VM)**
* Enabled **TCP/IP and Port 1433**
* Implemented **Design-Time DbContext Factory**
* Solved real-world issues:

  * Network configuration
  * Firewall rules
  * Authentication (SQL Login)

---

## 📊 Sample Output

```
------ Top Customers ------
1. Ahmed → $500
2. Sara → $400
3. Ali → $300
--------------------------

------ Top Rooms ------
1. Room 101 → 8 reservations
2. Room 203 → 5 reservations
3. Room 305 → 3 reservations
--------------------------
```

---

## 🛠 Technologies

* C#
* .NET Console Application
* Entity Framework Core
* SQL Server

---

## 📌 Future Improvements

* Build REST API (ASP.NET Core)
* GUI (Windows Forms / WPF)
* Authentication & Authorization
* Invoice System

---

## 👨‍💻 Author

Mohammed Adel
