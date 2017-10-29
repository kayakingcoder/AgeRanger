AgeRanger is a world leading application designed to identify person's age group!

To help AgeRanger to conquer the world I've developed the following app that communicates with the DB mentioned above, and does the following:

 - Allows user to add a new person - every person has the first name, last name, and age;
 - Displays a list of people in the DB with their First and Last names, age and their age group. The age group should be determened based on the AgeGroup DB table - a person belongs to the age group where person's age >= 
 than group's MinAge and < than group's MaxAge. Please note that MinAge and MaxAge can be null;
 - Allows user to search for a person by his/her first or last name and displays all relevant information for the person - first and last names, age, age group.
 - Allows user to edit existing person records by clicking on the record in the list.
 - exposes a WEB API which is linked to on the homepage

 - AgeRanger is a single page application.
 - Currently AgeRanger still uses the Sqlite database, but migrating to SQL server will be limited the AgeRanger.Data project where only the AgeRangerContext will need to be changed to use SqlServer.
 
# Technologies
AgeRanger is built using .Net Core, WebPack and AngularJS 4 (using dotnet new's SPA templates)
Swagger is used to document and expose the API
PrimeNG client library is used for user controls in Angular

# Known Issues

1) Currently there is no validation or exception handling. PerconController.Update(..) shows how we might pass an error from the service class (PersonService.Update(..)) back tot he calling app.

2) AgeControllerContext assumes the .db file will be in AgeRanger.Web's folder, which will not work when deployed and put a circular dependency between AgeRanger.Data and AgeRanger.Web.
Sounds worse than it is!!! DB file just needs to be deployed and linked to correctly in AgeRangerContext.

3) Something wrong in person.component.ts->save(). Shouldn't need to reload the whole list from the backend after adding a single record.

4) Currently there's no paging. PrimeNG's DataTable has nice paging support and the API's get method allows for offset and limit, these two just need to be tied together in Angular code.

5) No Unit tests (yet)

