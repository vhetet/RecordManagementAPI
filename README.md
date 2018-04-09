This API allow to manage records of person, those record hold name, company, profile image, email, birthdate, phone number (work, personal) and address. 

A noSQL database is used, so akk the record are just json objects with a key, in this case the key is a integer that auto generated whenh a record is added.

The image are stored separately in the fileStorage, there name is the index of the record they are linked to. As of now if you want a record and trhe image you need to do two calls, one for the record and one for the image.

Other than that this is very basic and allow to create, read, update, delete the records. They can be searched by id, but also email and phone number. If two records use the same phone number or email they both will be retrieved.

### Why doing it like that

The point of this project is to experiment with new framework I have not got the opportunity to work with yet, such as .net core, docker and liteDB. This is also the opportunity to build an API, I have often used API but I never build one.

### language and framework used:
* .net core 2.0
* c#
* docker
* liteDB

### State of the project

As of now this project is largely incomplete and lacks a lot of element:
* the is no data validation, as of now it's just assumed that the users will never make mistakes... (I know that is really optimistic :) )
* it's not tested
* it has not been deployed anywhere beside my local environment, but I plan to use azure for that. Also the point of using docker waht to make it easy to deploy so I need to look at that in details.

### Other infos

This was developed with the latest update of VS2017 (15.6) and the latest version of docker for windows (18.03.0).
I also used postman to test the endpoints manually

### resource used

http://www.litedb.org/
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api
