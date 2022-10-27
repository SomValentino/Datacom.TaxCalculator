# Datacom.TaxCalculator
## Assumptions
The assumptions made on this project are as follows:
* One file will be processed at a time.
* Only CSV files are allowed for upload
* The size of csv file to upload should be of size 5MB or less
* For testing purpose csv files are saved to web server event though this is not ideal for production environment.

## Projects

* Datacom.TaxCalculator.Domain: Class library project that contains the domin entities
* Datacom.TaxCalculator.Infrastructure: Class library project that contain data related actions like reading and writing to file
* Datacom.TaxCalculator.Logic: Class library project for handling business logic actions like tax calculations
* Datacom.TaxCalculator.Webpp: ASP.NET Core MVC app that is the presentation layer used for user interaction for performing processing for csv files
* Datacom.TaxCalculator.Tests: Unit Test project

## Running Solution in Visual Studio

Set the Datacom.TaxCalculator.Webpp project as your startup project and click f5 to run the webpp. The app starts with page for you to upload the csv file.
Once successfully you can process the csv file uploaded and the result of the processing is displayed on the next screen where you can download the output csv file.
