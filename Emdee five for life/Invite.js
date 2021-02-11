// Script made by Nullcheats \\

/*  
As you can see here there is not many variables we declare as they are not needed
Simply the import for "Request module" and "Domain" for the sign up endpoint
*/
const request = require('request');
const Domain = "https://www.hackthebox.eu/api/invite/generate";

/*
This is the main function to generate a code
As you can see it first makes POST request to domain with no content using "Request" module
Then it will look for the encoded token (Parse this via JSON)
Once encoded token is located it will then decode the base64 encoded string
and finally you will have the sign up token :)
*/
const GenerateCode = () =>
{
    console.log("Attempting to make POST request for encoded string !");
    request.post({url:Domain}, function optionalCallback(error, httpResponse, body) {
        if(error)
        {
            console.log("Error making POST request :(");
        }
        else
        {
            console.log("Recieved response - Attempting to parse encoded string");
            var GetEncodedToken = ParseJson(body);
            console.log("Located Token -> " + GetEncodedToken);
            console.log("Attempting to decode Base64 string");
            var Code = DecodeBase64(GetEncodedToken);
            console.log("Located sign up key -> " + Code);
        }
    });
}

/*
This function simply parses the JSON string from the passed parameter
It will use the "Data.code" to pull just the encoded string from JSON response
*/
const ParseJson = (string) =>
{
    var Json2String = JSON.parse(string);
    return Json2String.data.code;
}

/*
This is another very basic function that will simply decode base 64
As you can see the aim of the function it to decode the string from parameter
*/
const DecodeBase64 = (string) =>
{
    let code = Buffer.from(string, 'base64');  
    let codeEnc = code.toString('utf-8');
    return codeEnc;
}

/*
Finally we call the "GenerateCode function" :)
I guess this is technically the "first" thing as this starts the creation process
*/
GenerateCode();