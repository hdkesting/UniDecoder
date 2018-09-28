UnicodeDecoder
==============

A simple app with three functions:

 1. Display information about all characters in a provided string
 2. Search for characters by name (or codepoint)
 3. Show all characters in a unicode block

Based on the unicode database provided by the [UnicodeInformation](https://www.nuget.org/packages/UnicodeInformation/) nuget package.

Several implementations:
* A winforms app to run locally
  * Project: Unidecoder
* A website using async javascript and a download of the full character list 
  * Project: UnidecoderWeb
* A website using Azure functions to do the action 
  * Projects: UnidecoderWebStatic, Unidecoder.Functions (.Test)
  * Currently deployed to https://unidecoder.azurewebsites.net

