The Next Generation of Web Applications
==============

In October 2013 I finally graduated, returning to my *alma mater* - Polytechnic of Rijeka (Croatia) from the TechPeaks startup accelerator in Trento (Italy) to defend my Specialist thesis "The Next Generation of Web Applications".

The primary goal of my Specialist thesis was to study how the current generation of tools and technologies used in building web applications evolved while following the changes of the web application ecosystem. By comparing the current generation with its predecessors based on various criteria, a more ambitious goal was to extrapolate a direction in which the tools and technologies will evolve in the years to come.

The thesis and its presentation are written in Croatian as required by the Polytechnic of Rijeka's Statute, and the application is written and commented in English.

##Content##

1. Introduction
2. Web application architecture
 - Three-tier architecture
 - MVC
 - MVVM
 - Multilayered architecture
3. Data layers and hosting
 - Cloud services: IaaS, PaaS, SaaS
 - Storage: SQL, NoSQL, file, column and graph
4. Logic and communication
 - Modern web frameworks: ASP.NET MVC4
 - Real-time web: SignalR
 - Server and client-side JavaScript: Node.js and Knockout.js
5. Presentation layer
 - HTML5, CSS3
 - CSS frameworks
 - CSS preprocessing: LESS
6. Building an application
 - Requirements engineering
 - Data and process modeling
 - Implementation
	- Creating a new ASP.NET MVC4 project
	- Creating models with Entity Framework Code First
	- OAuth2: sign in with Facebook and Google accounts
	- Real-time web with SignalR Web Sockets
	- REST services with WebAPI
	- HTML5 Geolocation API
	- LESS
7. Conclusion

## Running the application ##

1. Open the solution in Visual Studio 2012 or newer.
2. Open Package Manager Console and run *update-database* to create the database and seed the test data.
3. To get OAuth running with Microsoft, Twitter and Facebook user accounts, insert appropriate keys into *App_Start\AuthConfig.cs*.




