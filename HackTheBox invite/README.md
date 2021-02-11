# Challenge
```
Before you can access the website for HackTheBox.EU , you will need an invite and to do this you just need to simply 
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
And instantly after formatting it with pretty print we have some very interesting strings such as "makeInviteCode" & "Generate"
```
![Alt text](https://i.imgur.com/cdJoAvI.png "Example")

```
Admin@hvh.site
```
