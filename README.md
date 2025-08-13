

# TestRestApi

A simple REST API built with **ASP.NET Core** that provides authentication and CRUD operations for categories, items, and orders.

## 📜 Overview

This API includes:
- User registration and login
- Categories management
- Items management
- Orders management

Swagger UI is integrated for easy API exploration.
![WhatsApp Image 2025-08-13 at 03 18 46_6d3dc5bb](https://github.com/user-attachments/assets/0a2ebff0-854a-47c2-9430-bbf2d6211ced)

---

## 🚀 Getting Started

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/YourUsername/TestRestApi.git
````

### 2️⃣ Navigate to the Project

```bash
cd TestRestApi
```

### 3️⃣ Update the Connection String

Edit the `appsettings.json` file to point to your SQL Server database.

```json
"ConnectionStrings": {
  "MyConnection": "Server=.;Database=TestRestApiDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

### 4️⃣ Run Migrations

```bash
dotnet ef database update
```

### 5️⃣ Run the Project

```bash
dotnet run
```

The API will be available at:

```
https://localhost:7074
```

---

## 📚 API Documentation

Swagger is available at:

```
https://localhost:7074/swagger
```

![Swagger Screenshot](swagger-screenshot.jpg)

---

## 🔑 Authentication

| Method | Endpoint                | Description                  |
| ------ | ----------------------- | ---------------------------- |
| POST   | `/api/Account/Register` | Register a new user          |
| POST   | `/api/Account/Login`    | Authenticate and get a token |

---

## 📂 Categories

| Method | Endpoint               | Description                 |
| ------ | ---------------------- | --------------------------- |
| GET    | `/api/Categories`      | Get all categories          |
| POST   | `/api/Categories`      | Create a new category       |
| PUT    | `/api/Categories`      | Update an existing category |
| GET    | `/api/Categories/{id}` | Get a category by ID        |
| PATCH  | `/api/Categories/{id}` | Partially update a category |
| DELETE | `/api/Categories/{id}` | Delete a category           |

---

## 📦 Items

| Method | Endpoint                                   | Description           |
| ------ | ------------------------------------------ | --------------------- |
| GET    | `/api/Items`                               | Get all items         |
| POST   | `/api/Items`                               | Create a new item     |
| GET    | `/api/Items/{id}`                          | Get item by ID        |
| PUT    | `/api/Items/{id}`                          | Update item           |
| DELETE | `/api/Items/{id}`                          | Delete item           |
| GET    | `/api/Items/categories/{categoryId}/items` | Get items by category |

---

## 🛒 Orders

| Method | Endpoint                                 | Description                    |
| ------ | ---------------------------------------- | ------------------------------ |
| GET    | `/api/Orders`                            | Get all orders                 |
| POST   | `/api/Orders`                            | Create a new order             |
| GET    | `/api/Orders/{orderId}`                  | Get order by ID                |
| GET    | `/api/Orders/GetOrderItembyId/{orderId}` | Get items for a specific order |

---

## 🛠 Technologies Used

* **ASP.NET Core 9.0**
* **Entity Framework Core**
* **Swagger / Swashbuckle**
* **SQL Server**

---

## 📌 Notes

* Make sure SQL Server is running before starting the project.
* For production, configure proper authentication and authorization.
* Replace the connection string with environment variables for security.

---

**Author:** Mohamed Abdelstar

```

