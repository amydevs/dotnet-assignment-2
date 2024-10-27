# PetShop

## Usage (For the Tutor)

Before running this project, make sure that you have the `ASP.NET and web development` workload installed via the `Visual Studio Installer`, as this project uses Blazor + ASP.NET. Please also make sure that you have .NET 8 installed.

To run this project, you can open up the `PetShop.sln` file in Visual Studio. There will be to run configurations available to you. Make sure to select the `http` option when running the project, as it will be hosted on `localhost`.

You may also use `dotnet run` if you prefer to use the command line.

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