# Followers
Description :

Write an API endpoint that accepts a GitHub ID and returns Follower GitHub ID’s (up to 5 Followers total) associated with the passed in GitHub ID. Retrieve data up to 3 levels deep, repeating the process of retrieving Followers (up to 5 Followers total) for each Follower found. Data should be returned in JSON format.

Setup: Prerequisites:

Visual Studio: Either Community 2017+ (Windows) or Code (Windows, MacOS)

.NET basic knowledge with C# 

Asp.net WEBAPI knowledge 

ADO.NET knowledge(using DataTable)

Running locally: Clone this project or download it. To clone it via command line, use the following (Terminal on MacOSX/Linux, Git Bash on Windows):

git clone https://github.com/Sravani0825/GitHub-Followers

Cloning a repository to GitHub Desktop:

See https://help.github.com/en/desktop/contributing-to-projects/cloning-a-repository-from-github-to-github-desktop for step by step procedure to clone it using GitHub Desktop

Insights into the code and how to run it using Visual Studio (Windows):

->First step is to open the project in Visual Studio

Insights:

->This retrieves upto 5 followers of a github account

string apiUrl = "https://api.github.com/users/USERID/followers?per_page=5"; apiUrl = apiUrl.Replace("USERID", userid);

->Web Request :

Header: Used basic authentication here and passed in the username and password

request.Headers.Add(HttpRequestHeader.Authorization, "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes("Username:Password")));

Username:Password Here pass in username and password of your GitHub account

->After the http call is made, api response will be retreived in JSON format,converted to datatable and this data will be looped in and the function will be called to retreive the data upto 3 levels deep(upto 5 followers of each github account)

->Build and start the project

You can get data upto 3 levels deep(upto 5 followers of each github account) : Route : /api/Followers/GetFollowers/{id}

id - is the GitHub id we pass in to get the followers of that account

Example : http://localhost:50995/api/Followers/GetFollowers/michel-kraemer
