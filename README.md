# MyWeatherApp

Contents :

1. MyWeatherApp(Folder) - Consists of a solution created using dotNetCore v3.1. It is a command line tool that gives you details about the temperature and wind speed of the city when you enter the city name.

2. myWeatherAppDB.bacpac - It is a DB created in MS SQL which has data for numerous cities around the world along with their Lat & Long value. 

3. worldCitiesData.csv - CSV file having data for all the most of the cities around the world. The exact CSV has been imported into the database.

Steps to run the code :

1. Using the URL from git try to clone the repository on your local machine.
2. Open SSMS and right click on Databases and you will find an option "Import Data-tier Application". Follow the next steps and select the .bacpac file from the source and import it on your local machine.
3. Go to the MyWeatherApp folder and double click on the '.sln' file. 
4. The application will work on VisualStudio2019. 
5. For VisualStudio 2022 you may have to install dotNetCore v3.1 as it does not support the version.
6. After the solution file opens and everything is setup change the connection string as per the new server where the database is imported locally.
7. The final step is to run the application and enter the city name.

**NOTE : Please change the connection string in the Main Program.cs file when the data is imported on any other SQL server accordingly.**

THANK YOU.
