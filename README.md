# Board Game Planner

If you have ever played a board game that takes several sessions to finish,
you know the hardest part: getting everyone to show up on the same date. This little app helps you organise your game nights.

## Features

- Browse game nights and games without an account
- Register and log in with ASP.NET Core Identity
- Create game nights with a date, venue, guest limit, notes and a list of games
- Only the creator of a game night can edit or delete it
- Join and leave game nights, up to the guest limit
- Add your own games to the shared game list (only their creator can edit or delete them)

## Technologies

- .NET 10, ASP.NET Core MVC
- Entity Framework Core (code first) with SQL Server
- ASP.NET Core Identity
- Bootstrap, Razor layouts, partial views and sections
- Dependency injection with a service layer
## Architecture

- `Board Game Planner` - ASP.NET Core MVC controllers, views and configuration
- `BoardGamePlanner.Services.Core` - Business logic and service interfaces
- `BoardGamePlanner.Data` - Database context and migrations
- `BoardGamePlanner.Data.Models` - Entity models
- `BoardGamePlanner.ViewModels` - View models and validation
- `BoardGamePlanner.Common` - Shared constants and enums

## Getting started

**Requirements:** .NET 10 SDK and SQL Server (a local default instance).

1. Clone the repository.
2. Check the connection string in `Board Game Planner/appsettings.json`. It uses `Server=.`; change it to `.\SQLEXPRESS` or `(localdb)\MSSQLLocalDB` if that matches your setup.
3. Create the database. Either use the command line (install the tool once with `dotnet tool install --global dotnet-ef`):
   ```
   dotnet ef database update --project BoardGamePlanner.Data --startup-project "Board Game Planner"
   ```
   or, in Visual Studio, open the Package Manager Console, set the default project to `BoardGamePlanner.Data`, and run `Update-Database`.
4. Run the app:
   ```
   dotnet run --project "Board Game Planner"
   ```

The database is created with 4 venues and 7 games. There are no default accounts: register your own.

## Validation

- Required fields, length limits and ranges (client and server side)
- A game night must be in the future
- Max guests cannot exceed the venue's capacity
- Selected games cannot need more players than the max guests
- A game's minimum players cannot exceed its maximum
- At least one game must be selected when creating or editing a game night
- Max guests cannot be reduced below the number of existing signups

## Notes
- During the initial database seeding, the sample games were created without assigning an owner ID. As a result, these games cannot be edited or deleted through the application.
- The sample games are shared: nobody can edit or delete them.