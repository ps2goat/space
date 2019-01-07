# Space Api
Pulls launch pad data from the SpaceX API. Allows filtering.

## Build and run
1. `dotnet restore` to pull down nuget packages.
2. `dotnet build` to build the solution.
3. Trust the dotnet core dev cert, if you haven't already: `dotnet dev-certs https --trust`
4. `dotnet run` to run the API.
5. `dotnet test` to run all unit tests in the solution.

## Endpoints
1. Get all launch pads: https://localhost:5001/api/launchpad
2. Filter launch pads by name: https://localhost:5001/api/launchpad?fullname=<filter>
    Launch pad names must _contain_ the filter (case insensitive)
3. Filter launch pads by status: https://localhost:5001/api/launchpad?status=<filter>
    Launch pad statuses must _contain_ the filter (case insensitive)
4. Filter launch pads by status _and_ full name: 
3. Filter launch pads by status: https://localhost:5001/api/launchpad?status=<filter>&fullname=<filter>

## External Configurations
1. The data url is configured in the appsettings.json files: "LaunchPadDataUrl"
2. The Logs are output to the debug console. Change the default log level to change what is logged.  It is currently set to "Debug". Change to "Information" or "Error", for example.
```
"Logging": {
      "LogLevel": {
        "Default": "Debug", // <-- this one
        "System": "Error",
        "Microsoft": "Error"
      }
    }
```

### TODO
1. Abstract the Microsoft built-in utilities for easier unit testing, such as `IConfiguration`.
2. Add additional unit tests around the repo and service (though the service is mostly pass-through)
3. The default logging system is used. It would be nice to tweak this with more options like a rolling log file, database/queue log, etc.

### Other considerations
Mapping from entities to data transfer objects (DTOs) is currently done at the controller level, with the consideration that this is a microservice and the returned models will likely be endpoint-specific.

DTOs are for data passed between domain layers, but in the case of a microservice, these are reserved for data passed to and from the client.

Entities are the object representation of stored data (either database or other source).  Entities should be mapped to a DTO prior to returning to the client, so there isn't a dependency between the client and the data source. (E.g., changing a property name on the entity only breaks a mapping, not an endpoint contract.)

Models are objects that may pass between layers, but aren't DTOs or Entities.

Replace the old ILaunchPadRepository with a new one, or just recode it, when updating the data source.

All data points were mapped, even though only 3 are returned to the client. A custom JSON deserializer is used in order to map from the snake-cased names to standard C# naming conventions.