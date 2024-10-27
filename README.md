# PetShop

## Usage (For the Tutor)

Before running this project, make sure that you have the `ASP.NET and web development` workload installed via the `Visual Studio Installer`, as this project uses Blazor + ASP.NET. Please also make sure that you have .NET 8 installed.

To run this project, you can open up the `PetShop.sln` file in Visual Studio. You can then run the `PetShop` project, and access it via the url provided in the command prompt. This should be `http://localhost:5000`.

You may also use `dotnet run` in the `PetShop` folder if you prefer to use the command line.

If you would like to reset the local SQLite database, simply extract and replace the `shop.db` file in the `PetShop` directory from the original `.zip` file this project was in whilst the program is NOT RUNNING.

There should be no further setup needed. The extra setup details below are simply for the development environment to setup the local SQLite database file and runs the TailwindCSS build step.

You do not need to do this, as the included `shop.db` file has already had all the EF Core migrations ran. Furthermore, the CSS file in `wwwroot/app.css` is already the version that contains all the necessary utility classes used in the project.


## Setup Dev Environment (For My Teammates)

First install the necessary development dependencies:

```bash
dotnet tool install --global dotnet-ef
npm i
```

Before running the application, make sure the database is up to date with:

```bash
dotnet ef database update
```

If you get errors, delete `shop.db` and retry again.

To run the application in watch mode:

```bash
npm run mix -- --watch
dotnet watch
```

This runs both Tailwind and Dotnet in watch mode.