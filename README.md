**South Australian Weather Observation Service**

The implemented service is responsible for getting stations weather data in Adelaide and displaying the results in two different ways, a Console Application project and a Web API project. The projects both consume the weather observation API and calculate the average data. Here is a brief explanation of the technology stack and the project itself:

All the projects are developed in .Net 8 using Visual Studio 2022 v 17.9.1. It is pushed in the GitHub online repository. The following third-party packages are used:

•	AutoMapper

•	Serilog

•	Swashbuckle

There are four layers in this solution. I didn’t follow the clean architecture because we don’t have entities and data layers. Instead, I created four following projects:

**DEW.BIS.WCC.WeatherObservation**: This is our console application which can get the Station Id from input, make an API call to get the weather observation data, and display the average temperature of the provided station during the last 72 hours. It asks for other station ids when it completes its current request indefinitely until you press e ESC to leave the console. The default station id is 94672 which determines the Adelaide Airport.

**DEW.BIS.WCC.WeatherObservation.API**: This is our Web API project that utilizes Swagger to easily test and document the API. It has two GET endpoints. The first one is *GetWeatherForecast* which returns a list of data of a specific station that is mapped to a smaller object to show important fields. It also calculates the temperature in Fahrenheit and wind speed in Mph. The second endpoint is *GetStationAverageTemperature* which returns the average temperature of the provided station id. The client could choose the temperature unit in Fahrenheit or Celsius. The default station id is 94672 which determines the Adelaide Airport and the default temperature unit is Celsius.

**DEW.BIS.WCC.WeatherObservation.Services**: This project is responsible for making HTTP requests using the HttpClient class. It also has an extension class that provides extra functionality such as conversions and calculating the average temperature.

**DEW.BIS.WCC.WeatherObservation.Shared**: This layer of the solution provides shared models and enums for other projects. It doesn’t depend on any project, but others depend on it.

**Extra Functionalities:**

**Memory Cache**: To retrieve average temperature faster, the API project uses Memory Cache. It stores an object to get the result faster. The lifetime of stored objects is 30 minutes. Every new request checks whether the station id has been stored in the cache or not then compares the cached object date with the earliest weather data from API. If the time difference between them is less than 30 minutes, the cache is enabled in the settings, and if there is any matched value in the cache, the API returns the cached object.

**Options Pattern**: The solution uses the options pattern using the IOptions<T> generic interface to inject the settings in services and bind the JSON settings to our settings classes.

**AutoMapper**: To manage our mappings in a single structure and point, the AutoMapper package is used.

**Serilog**: To add logging functionality to the application, the Serilog library is used to help us log our events and information in the console or text file. It is configured to read its settings from appsettings to make changes easier. Please change the log file path once you want to test it on your machine.

**Swagger**: The API project uses Swagger as it documents all endpoints and their models and provides a suitable UI to work with APIs and send requests to test the actions more easily.
