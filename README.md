# DataIngestion
This repository is developed just as a template for how to communicate with Google Drive APIs, Elasticsearch, and implement some concepts such as DRY, SOLID Principles, and design patterns.

# Senario
Download 4 zip files from a public Google Drive folder, unzip them, read flat files, join records, and insert results in the Elasticsearch database. Each flat file contains almost 1 million records.

## Application settings
All settings are set except Google API Key. However, you can change all the settings inside the appsettings.json.
- Set Download and Unzip temp folders, and other settings in the appsettings.json file (default values have been already set).
- Set ElasticSearch URL in the appsettings.json file (default value is http://localhost:9200).

## Google Drive API Key
In order to prevent the release of API Key in the public repository:
- Set ASPNETCORE_ENVIRONMENT environment variable.
- Set your Google API Key at appsettings.[Environment].json file in the GoogleDriveAccessKey field.

### Example for how to setup API Key in the application settings:
- If your ASPNETCORE_ENVIRONMENT environment variable value has been set to **Product**, then:
  - Add **appsettings.Product.json** file in the DataIngestion.TestAssignment project root and set your API Key to GoogleDriveAccessKey field inside this file.
- If you did not set any value for the ASPNETCORE_ENVIRONMENT environment variable, then you must set your API Key inside the appsettings.json which is not recommended. Since the appsettings.json file has already added to the GitHub repository, your API Key may be abused

## Tools, design patterns, and approaches:
- Visual Studio 2019 as IDE
- Domain-driven design
- Repository pattern
- Unit Of Work pattern
- Using Nest package for communicating to ElasticSearch
- Using RestSharp for calling Google Drive APIs
- Using NUnit and Moq packages for Unit-Tests
