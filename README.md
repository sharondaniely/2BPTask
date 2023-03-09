Welcome!

This Project is a small project that represents organization structure.

Backend: .Net 6
Frontend: Angular
DB: MongoDB (cloud)

In details:

    Backend:
        I decided to keep the code as simple as possible and use MVCS (Model View Controller Service) 
        where the View (FronEnd) implemented in Angular in different project.
        more about MVC: https://en.wikipedia.org/wiki/Model%E2%80%93view%E2%80%93controller
        more about MVCS:
            https://twosixmitch.medium.com/a-practical-guide-to-game-development-with-mvcs-419188721cd3
            https://pvha.hashnode.dev/mvcs-architecture

        DB selected for that project is MongoDB where the mapping to the DB done in code-first mechanism.
        I used the Mongo cloud DB to simplfy the project installation.

        I used Swagger to handle the backend.

    FrontEnd:

        Written in Angular 15 and include two main components:
            1. The employee list component - show all the Employees and Managers in the organization
            2. The employee page component- show the employee data includes info, tasks and subordinates (if there are).

        FrontEnd is suitable for Desktops.

        Using @angular/common/http called by the emplyee.service the FrontEnd communicate with the Backend.
        Backend allows the UI to communicate with it through its CORS (more: https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS)

    
    I assumed that the EmployeeList is valid and in case of error it will log in the console

        



System requirements in order to run it:
1. Angular 15
2. Node 18
3. .Net 6


How to run the app:
1. Clone the project from the git- I used Visual Studio for the API (Backend) and VSCode for the Frontend 
2. build and run the API- available on https://localhost:7261/swagger/index.html 
3. serve the frontend using "ng serve"- the app will be available at  http://localhost:4200/ 

in case you have miss some libraries please run "npm i"




 