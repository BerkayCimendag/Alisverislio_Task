# Alisverislio Task Project

## Table of Contents
1. [Project Overview](#project-overview)
2. [Technologies Used](#technologies-used)
3. [Installation](#installation)
4. [Configuration](#configuration)
5. [Running the Application](#running-the-application)
6. [Project Structure](#project-structure)
7. [Features](#features)
8. [Endpoints](#endpoints)


## Project Overview
The Alisverislio Task project is a comprehensive .NET Core web application designed for managing users, books, notes, and shares with a focus on real-time collaboration and security. It supports role-based authentication and authorization, allowing different levels of access for regular users and administrators.

## Technologies Used
- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- AutoMapper
- Serilog
- xUnit
- Swagger / OpenAPI
- JWT Authentication

## Installation
### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)


### Steps
1. Clone the repository:
    ```sh
    git clone https://github.com/yourusername/alisverislio-task.git
    cd alisverislio-task
    ```

2. Install the required .NET packages:
    ```sh
    dotnet restore
    ```

3. Set up the database:
    ```sh
    dotnet ef database update
    ```

## Configuration
### appsettings.json
Configure your database connection string and JWT settings in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
  },
  "Jwt": {
    "Key": "your_secret_key",
    "Issuer": "your_issuer",
    "Audience": "your_audience"
  }
}
```

## Features

### User Management
- **Register**: Allows users to create an account.
- **Login**: Enables users to log into their accounts.
- **Profile Management**: Users can manage their profile information.
- **Role-Based Access Control**: Differentiated access levels for regular users and admins.

### Book Management
- **Add, Edit, Delete Books**: Users can manage books, including uploading images.
- **Detailed Shelf Location Entry**: Provides detailed information about the book's shelf location.
- **Book Search and Filtering**: Allows users to search and filter books.

### Note Management
- **Add, Edit, Delete Notes**: Users can manage notes related to books.
- **Mark Notes as Private , Public or OnlyFriends**: Users can control the visibility of their notes.

### Share Management
- **Share Book Notes**: Users can share book notes with other users.
- **Share Privacy Settings**: Options for setting share privacy to public, only friends, or private.

### Purchase
- **Purchase System**: Users cannot share not if they didnot buy the book

### Logging
- **Centralized Logging**: Uses Serilog for centralized logging.


## API Endpoints

### Authentication
- **POST /api/user/register**: Register a new user.
- **POST /api/user/login**: Login and receive a JWT.

### Users
- **GET /api/user**: Get current user info.
- **PUT /api/user/updateProfile**: Update user profile.
- **POST /api/user/updateUserRole**: Update user role (admin only).

### Books
- **GET /api/book/{id}**: Get book by ID.
- **POST /api/book/add**: Add a new book (admin only).
- **PUT /api/book/{id}**: Update a book (admin only).
- **DELETE /api/book/{id}**: Delete a book (admin only).

### Notes
- **GET /api/note/{id}**: Get note by ID.
- **POST /api/note**: Add a new note.
- **PUT /api/note/{id}**: Update a note.
- **DELETE /api/note/{id}**: Delete a note.

### Shares
- **POST /api/share**: Share a note.
- **GET /api/share/note/{noteId}**: Get shares by note ID.
- **GET /api/share/user/{userId}**: Get shares by user ID.

### Purchases
- **POST /api/purchase**: Purchase a book.


