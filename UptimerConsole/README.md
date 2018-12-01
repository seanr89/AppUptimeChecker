# Introduction 
A console application that can be used to record web application uptime response messages to a database if available
Application will be used primarily for web based applications on azure, consuming a custom built web api to store the data.

1. Simple C# netcore console application
    - Test version available: UptimerConsole
2. Currently a netcore web api for data storage
    - Test version maintained on azure devops to be re-acquired to here!
3. A SPA web app for monitoring the data!!

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test
TODO: Describe and show how to build your code and run the tests. 
run: <dotnet build> as a first test
run: <dotnet build -c Release>
run: <docker build -t uptimechecker .>
run: <docker tag uptimechecker seanraff89/uptimerconsole>
run: <docker push seanraff89/uptimerconsole:latest>

read logs: docker logs <containerID>


#How to run
docker run -i -t sixeyed/coreclr-uptimer https://blog.sixeyed.com 5000
docker run -t -d seanraff89/uptimerconsole //live with -i -t for local test

# -t stops the container from closing in the background

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://www.visualstudio.com/en-us/docs/git/create-a-readme). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)