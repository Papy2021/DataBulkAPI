# DataBulkAPI
the API is a resful API design using Asp.net core, to be abble to open the soucre code on the visual studio or an software the required .NET 7.0
the API got 2 main entities the Actors that was used the create CRUD operations and the Users that's use for security and authentification.
the Authorized is a bearer Token that is generated when the user provided the correct username and password while login into the app.
the tokens are generated using JWT.
the password is encrypted using an method HashMethod, that added more security as the password will not be saved in plain-text to the database. 
the generated token expiry 15mins from the time it was created.

the data model was created using EF, find the connection string at the appsetting.json for more information.


regards Papy
