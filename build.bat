@echo off
cd C:\Users\tamli\source\repos\aws\moolah
dotnet tool update -g Amazon.Lambda.Tools

rem cd C:\Users\tamli\source\repos\aws\moolah\moolah.api.customer
rem dotnet lambda package

cd C:\Users\tamli\source\repos\aws\moolah\moolah.api.account
dotnet lambda package

rem cd C:\Users\tamli\source\repos\aws\moolah\moolah.api.transaction
rem dotnet lambda package

cd  C:\Users\tamli\source\repos\aws\moolah
