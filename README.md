# LibraryAPI

This is clear Api for Modsen Library task in which i realised:
  -Clean Architecture pattern
  -Jwt + refresh token authentication
  -Policy based authorization
  -Unit of work pattern
  -Global exception handling middleware
  -Fluent Validation
  -Swagger test endpoint api functionality

To work with this BE project u need first of all to update migrations in ur IDE through packet manager console or .net cli commands in order:
1. General Migration(by using ReposContext)
2. AuthorizationMigration (by using AuthContext)
   
Example(for Package Manager Console):
update-database -m 20240820221155_InitialGeneralMigrarion -Context ReposContext

update-database -m 20240820221208_InitialAuthorizationMigration -Context AuthContext

After that u can use my Postman test Endpoint template to make it easier and not to fullfil all the data for request by ur self:

https://www.postman.com/docking-module-geologist-10895237/workspace/modsen/collection/28485565-3f2789cd-e481-4ad1-8a6d-1a88f420bb61?action=share&creator=28485565
