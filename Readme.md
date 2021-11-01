UnicodeDecoder
==============

A simple app with several unicode-related functions:

 1. Display information about all characters in a provided string
 2. Search for characters by name (or codepoint)
 3. Show all characters in a unicode block

Based on the unicode database provided by the [UnicodeInformation](https://www.nuget.org/packages/UnicodeInformation/) NuGet package.

Implementations
---------------

* A winforms app to run locally
  * Project: Unidecoder
* A website using async javascript and a download of the full character list 
  * Project: UnidecoderWeb
* A website using Azure functions to do the action 
  * Projects: UnidecoderWebStatic, Unidecoder.Functions (.Test)
* A website created in Angular, using Azure Functions as backend
  * Project: UnidecoderAngular
* A Blazor web application (Blazor Server) 
  * Currently deployed to https://unidecoder.azurewebsites.net
  * Project: UnidecoderBlazorServer
