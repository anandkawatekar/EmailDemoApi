# EmailDemoApi

This repository contains E-Mail REST Services, developed using dot net framework 4.5.x, Visual Studio 2015.
SQL Server is used as backend to store data. And DB Script is included in this repo.

Steps to Run Application:
1) Clone/Download the source code in to native PC.
2) Execute DB Script present in file(MailManagerDBScriptV1.sql) to Create DB with name 'MailManager' and to insert initial data.
3) Create a user in sql server for above database.
4) Open project solution in visual studio 2015 or higher version.
5) Then open Web.Config file and change connection string's data source name, user id and password with above created db user credentials.
6) Save file and run the project.