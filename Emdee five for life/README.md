# Challenge
```
This hack the box challenge is very simple , you just need to submit the MD5 string of the text displayed and the only catch is
based on the "speed" , no matter how fast you encrypt the string and paste it , you will always see a "Too slow". The best way
to solve this issue is writing our own program to automate the process of "MD5" hashing and submitting values for us.
```
![Alt text](https://i.imgur.com/u5rUWWN.png "Example")
![Alt text](https://i.imgur.com/CD9VvpB.png "Example")
# Steps
```
1. Make a GET request to domain (get response body)
2. Regex match response and look for "Value" to hash
3. Hash the value with MD5
4. Make a POST request to domain with "MD5" hash and validate it
5. Regex match response for the HTB token :)
```


# C# example breakdown #1
```
Make a GET request to domain via Leaf.xnet (save response to string)
Attempt to match regex expression '[<]*h3 align='center'>[A-Z0-9a-z]{1,30}'
If regex match is a success replace the "HTML tags" with nothing (get plain value)
Once we have value that needs hashing simply hash it with MD5
Make a POST request to domain to vlaidate MD5 (post data = hash=MD5hash)
Now we use Regex expressions to locate HTB tokens '[>]*HTB[\d \w . {!} _ - & * $]{1,45}'
```
![Alt text](https://i.imgur.com/59EY31H.png "Example")

```
Admin@hvh.site
```
