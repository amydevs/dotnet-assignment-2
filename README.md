# PetShop

## Setup Dev Environment

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