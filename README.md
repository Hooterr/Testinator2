## Welcome to Testinator project

Authors: [Patryk Mikulski](https://github.com/Minorsonek), [Maksymilian Lach](https://github.com/Hooterr)

The idea of the project is to help teachers (but not only) automize their examination process.

Project consists of a Server and Client application. 

Server is an editor-like application that lets you create, edit and manage your quiz questions, scoring criteria and quiz templates.
When you have a test ready ask participants to open the client app on their machines and connect to the same LAN network. Once the connections is established you may start the test session. After you start the session you can observe the progress of every participant and have access to special features e.g. allow late-commers to join the session or kick a participant from the session, more to come in the future. 
After everyone is finished participants receive their individual results according to the scoring criteria you've created and you get the full package of participants answers and their grades for safekeeping as it is required from the teachers (at least in primary school and high school) to keep that information for maximum of 2 years. That's why we kept backwards compatibility in mind while creating the the app.

The client app's job is to receive a test from the server, present it to the user and send status reports to the server. There are some special features implemented e.g. full screen lock so the participant can't open any other windows, alt-tab, or close the application. At the end of the session show the paritipants how they scored along with correct answers for the questions (or don't if the quiz settings say so).

The project is far from finished but it managed to grew to a considerable size and complexity. We are currently in the process of refactoring core features of the app which means that the project is not buildable at this point or if they are they may not work correctly. 

Last buildable version is on branch /Release/v1.0
