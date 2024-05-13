# DataBalkAPI
The API is a RESTful API designed using ASP.NET Core. To open the source code in Visual Studio or any software, .NET 7.0 is required. The API comprises two main entities: Actors, which are used for CRUD operations, and Users, which handle security and authentication.

Authorization is performed using a bearer token, which is generated when the user provides the correct username and password during login. Tokens are generated using JWT (JSON Web Tokens). Passwords are encrypted using a hashing method, enhancing security by ensuring that passwords are not stored in plain text in the database. Tokens expire 15 minutes after their creation time.

The data model was created using EF (Entity Framework). For more information, find the connection string in the appsettings.json file.

Best regards, Papy
