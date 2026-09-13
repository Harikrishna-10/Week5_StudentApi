# Week5_StudentApi

# Student API

An unsecured ASP.NET Core Web API developed as part of Week 5 Web API Fundamentals.

The project demonstrates RESTful CRUD operations, validation, DTOs, Dependency Injection, Repository and Service patterns, the Strategy pattern, Swagger/OpenAPI documentation, logging, LINQ-based search, and automated testing.

## Technologies

* .NET 8
* ASP.NET Core Web API
* C#
* Swagger / OpenAPI
* xUnit
* Moq
* Coverlet
* LINQ

## Architecture

```text
Controller
    ↓
Service
    ↓
Repository
```

The application separates HTTP concerns, business/application logic, and data access.

## Features

### Students

* Get all students
* Get student by ID
* Create student
* Update student
* Delete student
* Search students by name
* Calculate grades using a selectable Strategy

### Teachers

* Get all teachers
* Get teacher by ID
* Create teacher
* Update teacher
* Delete teacher

## Student API Endpoints

| Method | Endpoint                                    | Description       |
| ------ | ------------------------------------------- | ----------------- |
| GET    | `/api/Students`                             | Get all students  |
| GET    | `/api/Students/{id}`                        | Get student by ID |
| POST   | `/api/Students`                             | Create student    |
| PUT    | `/api/Students/{id}`                        | Update student    |
| DELETE | `/api/Students/{id}`                        | Delete student    |
| GET    | `/api/Students/search?name=John`            | Search students   |
| GET    | `/api/Students/grade?marks=85&strategy=gpa` | Calculate grade   |

## Teacher API Endpoints

| Method | Endpoint             | Description      |
| ------ | -------------------- | ---------------- |
| GET    | `/api/Teachers`      | Get all teachers |
| GET    | `/api/Teachers/{id}` | Get all teachers |
| POST   | `/api/Teachers`      | Create teacher   |
| PUT    | `/api/Teachers/{id}` | Update teacher   |
| DELETE | `/api/Teachers/{id}` | Delete teacher   |

## HTTP Status Codes

| Status Code     | Meaning                               |
| --------------- | ------------------------------------- |
| 200 OK          | Successful GET/search/grade request   |
| 201 Created     | Resource successfully created         |
| 204 No Content  | Successful update/delete              |
| 400 Bad Request | Invalid request or validation failure |
| 404 Not Found   | Requested resource does not exist     |

## DTOs

The API uses separate DTOs for input and output.

`StudentCreateDto` is used for incoming student data.

`StudentReadDto` is used for outgoing student data.

The Student entity contains an internal-only `InternalNotes` property. This property is deliberately excluded from `StudentReadDto` and therefore never appears in API responses.

## Strategy Pattern

The API uses the Strategy pattern for grade calculation.

Available strategies:

* Percentage
* GPA

Example:

```text
GET /api/Students/grade?marks=85&strategy=percentage
```

Example response:

```json
{
  "marks": 85,
  "strategy": "percentage",
  "grade": "85%"
}
```

GPA:

```text
GET /api/Students/grade?marks=85&strategy=gpa
```

Example response:

```json
{
  "marks": 85,
  "strategy": "gpa",
  "grade": "3.5"
}
```

## Search

Student search is case-insensitive and implemented using LINQ in the service layer.

Example:

```text
GET /api/Students/search?name=John
```

A search with no matches returns:

```json
[]
```

with `200 OK`.

## Swagger

Swagger/OpenAPI is enabled for interactive API documentation.

Swagger URL:

`<INSERT YOUR SWAGGER URL HERE>`

Example:

`http://localhost:5063/swagger`

## Running the Application

Clone the repository:

```bash
git clone <REPOSITORY_URL>
```

Navigate to the project:

```bash
cd StudentApi
```

Run the application:

```bash
dotnet run
```

Open Swagger:

```text
<SWAGGER_URL>
```

## Running Tests

Run all tests:

```bash
dotnet test
```

The test project uses:

* xUnit
* Moq
* Coverlet

## Code Coverage

Generate coverage:

```bash
dotnet test --collect:"XPlat Code Coverage"
```

Target coverage:

```text
≥ 80%
```

Coverage focuses primarily on:

* StudentService
* Student search
* Percentage strategy
* GPA strategy
* Strategy selection
* Controller status codes
* DTO mapping

## Project Structure

```text
StudentApi
├── Controllers
├── DTOs
├── Models
├── Repositories
├── Services
├── Strategies
└── Program.cs

StudentApi.Tests
├── Controllers
├── DTOs
├── Services
└── Strategies
```

## Security

This project is intentionally unsecured as required by the Week 5 assignment.

Authentication and authorization are not implemented.

## Assignment

This project was completed as the Week 5 Student API assignment, including the stretch TeachersController task.



