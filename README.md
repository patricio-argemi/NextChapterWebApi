# Resources
Included in the repo are a Postman request collection and a SQL Server DB backup to quickly test the API running.

Files are:

-NextChapter.postman_collection.json
-CustomerPricingTask_DB_Backup.bacpac

# NextChapterWebApi Task

NextChapter needs a RESTful API to manage specific prices for our customers.

# Platform choice:
.Net (Full Framework or Core) for the API
SQL database (MySQL, SQL Server, PostgreSQL...). Database should run on "." server and with database name "CustomerPricingTask"

# Task requirements:
Feel free to spend as much or as little time on the exercise as you like as long as the following requirements have been met.
* Please complete the requirements/user story described below.
* Initial data will be provided within "initial-data.json" file
* Your code should compile and run in one step.
* Feel free to use whatever frameworks / libraries / packages you like.
* You must include tests
* Please avoid including artifacts from your local build (such as NuGet packages or the bin folder(s)) in your final ZIP file

# Requirements:
Customer Pricing API should be able to manage products with theirs base prices and specific prices for some customers. 
If we request a price for a given customer & product the API should return specific price or base price if it does not exist.

# User Story:
As an user consuming the API
I can add a new product code with its price 
I can update a price for a given product code
I can request the basic price for a product code
I can add a new specific price for a customer code and a product code 
I can update an specific price for a customer code and a product code
I can delete an specific price for a customer code and a product code
I can request the price for a given customer code and a product code, if there is no specific price for that customer code - product code then base price should be returned
