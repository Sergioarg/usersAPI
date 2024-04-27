# User Management Service Project Documentation

## Introduction

This project is a user management service that allows for the creation, updating, deletion, and retrieval of user information. It is built on .NET Core and uses Entity Framework Core for data persistence. The API is documented with Swagger and can be accessed through the following endpoints.

## Configuration

To configure the project, the database connection string must be set in the `appsettings.json` or `appsettings.Development.json` file. The default connection string is `Data Source=Users.db`.

```json
{
 "ConnectionStrings": {
    "Users": "Data Source=Users.db"
 }
}
```
# Running the User Management Service Locally

This guide will walk you through the process of cloning the User Management Service repository and running it on your local machine. This will allow you to interact with the API and test its functionalities.

## Prerequisites

- .NET Core SDK installed. You can download it from [here](https://dotnet.microsoft.com/download).
- A code editor or IDE, such as Visual Studio Code, which is recommended for this project.

## Step 1: Clone the Repository

Open your terminal or command prompt and navigate to the directory where you want to clone the repository. Then, run the following command:

```bash
git clone https://github.com/Sergioarg/usersAPI.git
```

## Step 2: Navigate to the Project Directory

After cloning the repository, navigate to the project directory:

```bash
cd usersAPI
```

## Step 3: Install Dependencies

Before running the project, you need to restore the dependencies. Run the following command:

```bash
dotnet restore
```

## Step 4: Configure the Database

The project uses SQLite as its database. Ensure you have SQLite installed on your machine. If not, you can download it from [here](https://www.sqlite.org/download.html).

The default connection string is `Data Source=Users.db`. You can configure this in the `appsettings.json` or `appsettings.Development.json` file if you wish to use a different database or connection string.

## Step 5: Run the Migrations

Before running the project, you need to apply the database migrations. This will create the necessary tables in your database. Run the following command:

```bash
dotnet ef database update
```

## Step 6: Run the Project

Now, you can run the project. Execute the following command:

```bash
dotnet run
```

The application will start, and you should see output indicating that it's running. By default, the application runs on `http://localhost:5155`.

**Note:** Check if the port on which the project is running becomes different from `5155` make sure to change it when making API calls you can check which port it is running on after running `dotnet run`.

```
Compiling...
info: Microsoft.Hosting.Lifetime[14]
    Now listening on: http://localhost:5155 <- Here
```

## Step 7: Access the API

You can now access the API using your preferred tool (e.g., Postman, cURL, or a web browser) to interact with the endpoints as documented in the API documentation.

## API Endpoints

| Endpoint | Method | Description | Response | Request Body | Parameters |
|----------|--------|-------------|-----------|--------------|------------|
| `/users` | GET | Retrieves a list of all users. | An array of `User` objects. | N/A | N/A |
| `/users/{id}` | GET | Retrieves a specific user by their ID. | A `User` object or `404 Not Found` if the user does not exist. | N/A | `id`: The ID of the user. |
| `/users` | POST | Creates a new user. | `201 Created` with the created `User` object and the location of the new resource. | A `User` object. | N/A |
| `/users/{id}` | PUT | Updates an existing user. | `204 No Content` if the update is successful, or `404 Not Found` if the user does not exist. | A `User` object with the fields to update. | `id`: The ID of the user to update. |
| `/users/{id}` | DELETE | Deletes a specific user by their ID. | `200 OK` if the deletion is successful, or `404 Not Found` if the user does not exist. | N/A | `id`: The ID of the user to delete. |

## Example Usage

To interact with the API, tools like Postman or cURL can be used. Below is an example of how to create a new user using cURL:

```bash
curl -X POST http://localhost:5155/users \
-H "Content-Type: application/json" \
-d '{
    "name": "John Doe",
    "birthdate": "1990-01-01",
    "active": true
}'
```

## API Documentation

The API documentation can be accessed through the Swagger UI interface at `http://localhost:5155/swagger`. Here, all available endpoints can be viewed, and tests can be performed directly from the browser.
