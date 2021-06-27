# DataIngestion.TestAssignment

## Run application
- Set ASPNETCORE_ENVIRONMENT environment variable.
- Set your Google API Key at appsettings.[Environment].json file in the GoogleDriveAccessKey field.

### Example for how to setup application settings:
- If your ASPNETCORE_ENVIRONMENT environment variable value has been set to **Product**, then:
  - Add **appsettings.Product.json** file in the DataIngestion.TestAssignment project root and set your API Key to GoogleDriveAccessKey field inside this file.
- If you did not set any value for the ASPNETCORE_ENVIRONMENT environment variable, then you must set your API Key inside the appsettings.json which is not recommended. Since the appsettings.json file has already added to the GitHub repository, your API Key may be abused

### Tools, design patterns, and approaches:
- Visual Studio 2019 as IDE
- Domain-driven design
- Repository pattern
- Unit Of Work pattern
- Using Nest package for communicating to ElasticSearch
- Using RestSharp for calling Google Drive APIs
- Using NUnit and Moq packages for Unit-Tests
