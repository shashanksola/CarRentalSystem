# **Car Rental System API**

This is a RESTful API for a car rental system, built using ASP.NET Core 8. The system provides features like user registration, car management, booking, and email notifications. The system supports role-based authorization and secure authentication with JWT and hashed passwords.

---

## **Features**

1. **User Management**
   - Register users with roles (`Admin`, `User`).
   - Authenticate users and issue JWT tokens.
   - Passwords are securely stored using hashing.

2. **Car Management** (Admin Only)
   - Add, update, delete, and retrieve cars.
   - View car availability.

3. **Booking System** (Accessible to Admin and Users)
   - Book cars for a specific period.
   - Send booking confirmation via email using **SendGrid**.

4. **Role-Based Authorization**
   - Admin-only actions for managing cars.
   - Users and Admins can book cars.

5. **Authentication**
   - JWT-based authentication with secure password hashing.

6. **Email Notifications**
   - Email confirmation of bookings using **SendGrid**.

---

## **Setup and Installation**

### Prerequisites
- **.NET 8 SDK**
- **SQL Server**
- **SendGrid API Key** (for email notifications)
- **Postman** or **curl** for testing the API

### Steps to Run the Application

1. **Clone the Repository**
   ```bash
   git clone https://github.com/your-repo/car-rental-system.git
   cd car-rental-system
   ```

2. **Set Up Database**
   - Update `appsettings.json` with your SQL Server connection string:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=CarRentalDB;Trusted_Connection=True;"
     }
     ```
   - Apply migrations in Nuget Console:
     ```bash
     Add-Migration "Initial Migration"
     Update-Database
     ```

3. **Set Up SendGrid API Key**
   - Add your SendGrid API key to Environment Variables:
     <img width="1280" alt="Screenshot 2024-11-27 120625" src="https://github.com/user-attachments/assets/42d88141-7d5f-4d94-91e8-461cffd9a79a">
     ```bash
     echo %SEND_GRID_API_KEY%
     ```

4. **Run the Application**
   ```bash
   dotnet run
   ```
    or
   run https in visual studio
   The application will start on `https://localhost:7262` or `http://localhost:5041`.

6. **Test the API**
   - Use Swagger at `https://localhost:7262/swagger` for interactive API testing.
   - Alternatively, use Postman or curl.

---

## **Authentication**

### **Login and Obtain JWT Token**
- **Endpoint**: `POST /api/Users/login`
- **Payload**:
  ```json
  {
    "email": "admin@example.com",
    "password": "your-password"
  }
  ```
- **Response**:
  ```json
  {
    "token": "your-jwt-token"
  }
  ```

### **Using the JWT Token**
Include the token in the `Authorization` header for subsequent requests:
```
Authorization: Bearer your-jwt-token
```

---

## **API Endpoints**

### **User Endpoints**
| Method | Endpoint               | Description                 | Roles Allowed |
|--------|------------------------|-----------------------------|---------------|
| POST   | `/api/Users/register`  | Register a new user         | All (Should be updated so that only admin can register another admin)         |
| POST   | `/api/Users/login` | Authenticate and get token | All           |

### **Car Endpoints**
| Method | Endpoint       | Description              | Roles Allowed |
|--------|----------------|--------------------------|---------------|
| GET    | `/api/Cars`    | Get all cars            | All           |
| POST   | `/api/Cars`    | Add a new car           | Admin         |
| PUT    | `/api/Cars/{id}` | Update car details     | Admin         |
| DELETE | `/api/Cars/{id}` | Delete a car           | Admin         |

### **Booking Endpoints**
| Method | Endpoint       | Description              | Roles Allowed  |
|--------|----------------|--------------------------|----------------|
| POST   | `/api/Book`    | Book a car for a period | Admin, User    |

---

## **Middleware**

### **JWT Authentication**
- Configured in `Program.cs` with `JwtBearerDefaults`.
- Validates tokens, checks expiration, and assigns claims.

### **Role-Based Authorization**
- Uses `[Authorize(Roles = "Admin")]` attributes on controller actions.

---

## **Configuration**

### **appsettings.json**
```json
"JwtSettings": {
  "Secret": "your-jwt-secret-key"
},
"ConnectionStrings": {
  "DefaultConnection": "Your SQL Server connection string"
},
"SendGrid": {
   "ApiKey": "yor_api_key_here"
}
```

```bash
SENDGRID_API_KEY=your_api_key_here
```

### **Environment Variables**
- For production, store secrets (e.g., JWT key, connection string, SendGrid API key) as environment variables.

---

## **Technologies Used**

- **ASP.NET Core 8**
- **Entity Framework Core**
- **SQL Server**
- **Swagger/OpenAPI**
- **JWT Authentication**
- **SendGrid Email API**

---

## **Troubleshooting**

### 1. **403 Forbidden Error**
   - Ensure the token contains the correct `role` claim.
   - Verify the `[Authorize(Roles = "Admin")]` or `[Authorize(Roles = "User")]` attribute matches the role exactly.

### 2. **Token Expired**
   - Check token expiration time in the payload.
   - Generate a new token by logging in again.

### 3. **Database Errors**
   - Ensure the connection string is correct.
   - Run migrations to create necessary tables.

### 4. **Email Not Sending**
   - Verify your SendGrid API key.
   - Ensure the email address provided is valid.

---

# Postman Testing

1. Registering a Admin/User
![image](https://github.com/user-attachments/assets/f158798e-d3ee-4bbb-b542-7e35aa380aa1)

Hashed Passwords are stored in SQL Server
![image](https://github.com/user-attachments/assets/399ce52c-9444-46f1-bc53-0c415f439da3)


2. Registering a Duplicate User/Admin
![image](https://github.com/user-attachments/assets/57eb01b5-e0e3-46a5-aba5-f110df534584)

3. Login as User/Admin
![image](https://github.com/user-attachments/assets/69b7223a-5852-4f94-b817-dc23d6e5b395)

4. Failed Login using Invalid Credentials
![image](https://github.com/user-attachments/assets/73ecd361-89aa-443a-a06e-93cee2d071d1)

5. Posting a Car as a Admin
![image](https://github.com/user-attachments/assets/2b9a12f0-d202-44ad-99ba-be35b25a6ce9)

Car Stored in SQL Server
![image](https://github.com/user-attachments/assets/2b101833-eaec-4c08-956b-466740698765)

6. Getting All Cars (Without ant Login)
![image](https://github.com/user-attachments/assets/36d2baf6-bf56-4af8-9ae7-6e3080cbdfcd)

7. Making an Unauthorized request as a user/ non logged in person
![image](https://github.com/user-attachments/assets/14c675f5-3d37-465a-9834-6b429e16aa8a)

8. Updating a car as a admin
![image](https://github.com/user-attachments/assets/44231bf1-2adf-4703-ba33-4497d0a25a0f)

9. Booking a car
![image](https://github.com/user-attachments/assets/d0b83abc-66c8-47e1-843f-f99f7536732a)

Email Confirmation
![image](https://github.com/user-attachments/assets/918e9152-c376-440f-9bb4-c4268e460dad)
