APIWithAuth
This is a webapi with JWT and custom identity.
I didnt use the built-in identity instead I created the identity from scratch. And I found a problem.
when I code the identity from scratch, the schemes for the authentication had one problem.
It somehow doesn't work if you set the scheme for the controller in program.cs services. If I try to call a api, it returns 404 instead of 401.
It works if you put it in the controller [Authorize] attribute. And if you call it now, it returns 401. And everything works.
My guess is since it returns 404 instead of 401 it should not have anything to do with JWt because 404 means the api doesnt exist.
Somehow if you do it manually, you have to configure the path for the api? I'm not sure, still needs to be solved.
