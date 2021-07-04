# TakeHomeAssignment-jc
Interview Assignment for JumpCloud
Contains a small library function to add actions as well as get stats on added actions.

# Requirements
Windows 10  
[Visual Studio Community 2019](https://visualstudio.microsoft.com/downloads/) or newer  
[.NET 5.0](https://dotnet.microsoft.com/download)

# Building
Clone the repo from GitHub
Open the TakeHomeAssignment-jc.sln file in Visual Studio
Build the solution using the ctrl+shift+B keybind

# Useage
To see output from this console application, I used the application arguments in Visual Studio  
To use this, right click on the solution file in the solution explorer and select properties  
Navigate to the Debug tab and enter the JSON strings into the arguments window  
![image](https://user-images.githubusercontent.com/61291740/124397040-f2d1a300-dcd2-11eb-91e5-79f79716a2c4.png)  
For convenience, I have provided a string of valid JSON to copy/paste into the window below.  
```"{\"action\":\"jump\", \"time\":100}" "{\"action\":\"run\", \"time\":75}" "{\"action\":\"jump\", \"time\":200}"```  
Save the argument (ctrl+s)
Click the play button at the top or press F5 to run the program.

# Testing
To test the functions, I used the same method as above.  
To ensure that I caught errors correctly, I passed in malformed JSON strings as the arguments  
I also used actions that were not provided, assuming that more actions would be used with the functions, that way I could ensure the functions could handle it.  
I have provided the string of JSON I used for testing below  
```"{\"action\":\"jump\", \"time\":100}" "{\"action\":\"run\", \"time\":75}" "{\"action\":\"jump\", \"time\":200}" "{\"ation\":\"jump\", \"time\":200}" "{\"action\":\"fall\", \"time\":150}" "{\"action\":\"fall\", \"time\":200}" "{\"action\":\"fall\", \"tie\":200}" "{\"action\":\"fall\", \"time\":-1}" "{\"action\":\"sprint\", \"time\":150}" "{\"action\":\"sprint\", \"time\":300}" "{\"action\":\"sprint\", \"time\":200}"```

# Expected Output
For the normal useage, expected output looks like this:  
![image](https://user-images.githubusercontent.com/61291740/124397281-ab4c1680-dcd4-11eb-99c5-593ff214a93c.png)  
For testing, the expected output looks like this:  
![image](https://user-images.githubusercontent.com/61291740/124397332-d9c9f180-dcd4-11eb-8234-b4252472939a.png)


