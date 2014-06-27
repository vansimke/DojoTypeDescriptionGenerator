DojoTypeDescriptionGenerator
============================

Application written in ASP.NET (C#) and TypeScript to read the dojo api (http://dojotoolkit.org/api) and generate initial TypeScript definition files.

Requirements:
* Visual Studio 2012 or higher
** Original development was done in Professional edition. Not sure if Express will work or not.

Usage:
* Start the application and navigate to the site root
** The application will start to pull down the API documentation from http://dojotoolkit.org/api
*** This will take a while the first time (show the console to watch progress)
*** The results will be cached after the first time so that the network penalty is only incurred once
* After the initial download is done (about 2500 objects are pulled down), the four buttons will activate. Each will create a different .d.ts file (dojo, dijit, doh, and dojox)
** These files are stored in the project's Scripts/typings folder

This project was intended to serve as a first step toward creating type definitions for the massive dojo framework. The final .d.ts files comprise about 500 KLOC including comments, etc. This was never intended to get the type definition files all of the way there. After the generator runs, hand-polishing is required in order to address the natural errors that come from human generated help files being used to generate a strongly typed set of definitions. It should get you most of the way there.

Note: the application is currently hard coded to download version 1.9 of the API. To change this, find the path "data/1.9/tree.json" in the main.ts (line 53 as of this writing) and update to whatever you want.
