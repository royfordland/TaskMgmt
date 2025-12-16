
# TaskMgmt

In this app:

- New users can register to get access
- Registered users can log in and add or update tasks
- Regular users can create tasks and update the status of existing tasks
- When moving a task from Pending to another state, the user who performs this action will be assigned to the task
- Admin users can change the title, description, status, and assigned user of a task
- Admin users can also add and update task status values

# Project Information

## Database

- **Database:** PostgreSQL 18
- To run the scripts:
	1. First, run the `CREATE DATABASE` command.
	2. Then, execute the remaining part of the script in `create_database.sql`.

## Solution Structure

- **Api project:** .NET 8 controllers
- **Service project:** .NET 8 services
- **Service.Tests project:** Unit tests for Service project
- **Frontend:** Blazor frontend

## Running the Solution

- You need **two instances of Visual Studio (or your IDE of preference to run this code)** to run this solution:
  - One instance to run the **Api** project
  - One instance to run the **Frontend** project
- All APIs can also be accessed through the **Swagger UI** page included with the Api project.
# TaskMgmt