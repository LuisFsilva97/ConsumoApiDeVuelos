API and Frontend Setup Guide 🚀
API Execution Requirements 🛠️
- Visual Studio version 2024
- Frontend Requirements 🌐
- Angular
- bash
- Copy code
- Package          

## Decisions
In-memory cache management is used to optimize the number of requests made to the API: https://recruiting-api.newshore.es/api/flights/2
SQL Server in-memory database is used to store a query history, facilitating local testing on faster devices without the need to install SQL Server.
A third-party service is used for currency conversion (https://www.exchangerate-api.com/).
-------------------------------
- @angular-devkit/architect       
- @angular-devkit/build-angular   
- @angular-devkit/core            
- @angular-devkit/schematics     
- @angular/cli                    
- @schematics/angular             
- rxjs                           
- typescript                      
- Node.js
- Newshore.Viajes
- Entrance Test for NEWSHORE

## Newshore.Viajes.WebApi
This folder contains the .NET Core API project. The API verifies if a flight route is present in the data received from the flight service, considering the search criteria entered by the user from the Frontend.

## newshore-viajes-web
This folder contains the Angular web project. The frontend makes requests to the aforementioned service to search for flight routes and displays the results on the screen. Once the search is complete, it allows the user to perform currency conversions (COP, EUR, and GBP).

## Tools and Instructions
Tools Used
- C# as an Object-Oriented Programming language (Backend)
- .NET Core 6 development platform
- Angular as the development framework for the Frontend
- Visual Studio 2022 IDE for API development
- Visual Studio Code IDE for Frontend development
- Swagger as a set of tools for building, documenting, and using/testing services/APIs
- Serilog for capturing logs in files
- Microsoft Test Framework as a tool for creating unit tests
- Steps for API Execution:
- To run the project locally, follow these steps:

## Clone or download the repository of the solution.
Verify/restore all NuGet packages in the solution.
Run the application.
Steps for Web Application Execution:
Clone or download the repository of the solution.
Validate the Angular versions specified earlier on the machine where you want to test the application.
- Run npm install to restore the project packages.
- Run ng serve --open to start the application.

## Architecture:

- Main Non-functional Requirements
- Availability
- Testability
- Maintainability







