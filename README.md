# PurpuraHR

## Project Summary

This portfolio project is an HR system where users can register, sign in and view essential information about the company they work at. Users can view the details of their current company or create a new one. Other employees of the company can also be seen. Goals can be set up and Annual Leave can be booked. Users can also view and update their own details.

## Tech Stack and Design
This project uses the following:
- C#
- .NET Core
- ASP.NET MVC
- JavaScript using jQuery
- SQL Server
- Entity Framework Core
- AutoMapper
- xUnit
- Moq
- HTML
- CSS

The project uses N-Tier architecture to separate the project into tiers and Repository Design Pattern using Unit Of Work to encapsulate database CRUD operations. All services have had unit tests written for them to make sure that the essential business logic is sound and bug-free. Most of the user registration system was set up using .NET's scaffolding system but has been further tailored for this project's use. The styling has been created with desktop, tablet and mobile views in mind and will responsively change depending on the viewport size. Authorisation measures are in place so that a user will be redirected to the login page if they try to access areas of the platform that require the user to be logged in and authenticated.

## Setup
This project has been built with .NET version 8, so the .NET Software Development Kit will be required to run the project.

To clone this project, navigate to the folder of your choice via a terminal and then type the following command:

`https://github.com/RoKen1000/PurpuraHR.git`

Then open the folder using the code editor of your choice.

Because this project uses Entity Framework Core and has several migrations in place, these will need to be seeded. This can be done by running `update-database` in the terminal (Package Manager Console if using Visual Studio). This will seed a user that has data already set up. If desired then a brand new user can register and login to start from scratch. 

If using Visual Studio simply run the project as normal by clicking the launch button in the toolbar. If using another code editor, such as Visual Studio Code, you can run the project with the following command:

`dotnet run`

## Future Updates
This project is actively being worked on and new features and improvements will be added from time to time.
