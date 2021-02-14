# Challenge
```
Before you can access the website for HackTheBox.EU, you will need an invite and to do this you just need to simply 
understand what you are looking for, I recommend you look yourself and away from this guide as it is quite fun 
solving the challenge with no help :).
```

# Initial recon
```
https://www.hackthebox.eu/invite | "Hack your code and enter it here" (shows we need to carry a task out)
Chrome inspect (F12 key) - Notice in the cosnole "The first step of the challenge!"
This page loads an interesting javascript file. See if you can find it :) 
Based on console log we know Javascript files are worth looking into
Initial look at JS scripts loaded ("InviteAPI.min.js) looks interesting
Upon looking at Invite.min.js we see "//This javascript code looks strange...is it obfuscated???" (good hint)
```

# HTB invite
```
We can assume we are looking for a code / some form of token based on image
Hint (check console) - this goes well as we already did this with inital browing / chrome inspection
```
![Alt text](https://i.imgur.com/kVvrnSZ.png "Example")

# Chrome inspection (sign up)
```
As you can see below in the console we are greeted with image an and a message "The first step of the challenge!"
We also see it mentions it loads a "interesting JavaScript file" so this is a big hint to what we should do next
```
![Alt text](https://imgur.com/3yW8j4n.png "Example")

# Chrome inspection (Filtering JS files)
```
At the top row we can simply filter to show only "Javascript" files the page uses so we easily hide anything not using JS
As you can see on the left side we can now view the JS files and based on initial names we can choose what to look into first
Based on names "invite.min.js" looks interesting and so fies "htb-frontend.min.js" - so we next check each of them 1 by 1
```
![Alt text](https://i.imgur.com/cdJoAvI.png "Example")

# Javascript file (inviteapi.min.js)
```
Upon checking this file we notice an interesting hint '//This javascript code looks strange...is it obfuscated???'
As its hard to read we use Inspect element 'PrettyPrint' to format it and make it much more easier to read
And instantly after formatting it with pretty print we have some very interesting strings such as "makeInviteCode"
```
![Alt text](https://i.imgur.com/X2hluiL.png "Example")

# Deobfuscating JS 
```
http://deobfuscatejavascript.com/# (Online deobfuscator for JS)
From the hint earlier we knew it could of been "Obfuscated" so we used a online tool to deobfuscate
Upon deobfuscating we see its a JS function called "MakeInviteCode" and it appears to make a POST request
POST endpoint -> /api/invite/how/to/generate -> response -> JSON
There is 2 things we can try here (POST request) or using JS function in chrome console
```
![Alt text](https://media.giphy.com/media/1OIhD1OkJYto54DDnF/giphy.gif "Example")

# POST request to endpoint (from JS)
```
As you can see if we POST to the endpoint we was given we get a response
Response code is 200|OK (good sign request was aknowledged)
Response body contains JSON data as expected
And response is "Encrypted" which we would also expect as this is a challenge :)
```
![Alt text](https://i.imgur.com/4xVJeoX.png "Example")

# Run JS function from console
```
As we know the functions name and what it does we can simply run "makeInviteCode()" in the console
Doing this will call the function and make a POST request for us 
The response is logged to console and you will notice , it is the exact same as making a POST request
```
![Alt text](https://i.imgur.com/fP13zju.png "Example")


# Analysing response (JS function)
```
As you can see the response is encoded and upon checking this a few times it appears to be either RTO13 or base64
In my case you can see we ROT13 as the encode type and a string "data" that we can assume is encoded with ROT13
As the hint says "Data is encrypted" and we should "Decrypt it"
```
![Alt text](https://i.imgur.com/hcaVCCp.png "Example")

# Decrypting ROT13 / response
```
As you can see my response was ROT13 so i used a web based ROT13 decoder to decode my text
As you can see the data string was valid ROT13 as the "enctype" stated
https://rot13.com/ (used to decode)
And the decrypted output provided us with a new POST endpoint '/api/invite/generate'
```
![Alt text](https://i.imgur.com/dY5LVvh.png "Example")

# POST request to new endpoint
```
Upon making a POST request to the newer endpoint yet again we got a response
Response status code was 200|OK so we know it was acknowledged
Response body provided us with another "encoded" string but this time with no hint to what it could be
As you can see below my "Code" ended with a "=" and this is usually a good hint towards "Base64" encoding
To test this I simply used a online base64 decoder and as suspected it was a valid base64 string
```
![Alt text](https://i.imgur.com/O562TMW.png "Example")

# BASE64 decoder online
```
As you can see below I suspected that it was a base64 string just by looking at it.
I used a B64 online decoder to get the decoded value and this looked more like a "Token or Key"
As this did not lead me to anywhere else I assumed this was the sign up key and it was :)
Codes appear to be valid for around 10-30 mins , so make sure you redeem them within that time frame <3
https://www.base64decode.org/
```
![Alt text](https://i.imgur.com/vNm72bp.png "Example")


```
Admin@hvh.site
```
