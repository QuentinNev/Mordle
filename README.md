# Mordle
## Description
This is a Wordle like game made with ASP.
## Requirements
- .NET 6.0
- SQLite
## Deployment schema
![](https://miro.medium.com/max/1400/1*Eb-dF845BnJYJwItGje08w.png)
## Migrations
To create migration use this command :
```dotnet ef migrations add [name] --context [context]```
To apply them use:
```dotnet ef database update --context [context]```