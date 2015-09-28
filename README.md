<snippet>
  <content><![CDATA[
# ${1: Intercom Invitation}

Tech Screener for Intercom written in C#

## Running

This is a windows console application.

To run the application execute /bin/IntercomInvitation.Application.exe path_to_file from command line

## Description

This application consists of a number modules 

### Domain
This module contains the domain logic. It is not concerned with input file types or formatting the output. It defines two interfaces, a provider and a writer, which the application must implement.

### Application
This is the main console application. It provides implementations for the provider and the writer. 

### Tests
There are two unit test projects one for each of the main modules and an integration test project. This integration test defines a simple test which can be used to run the application without mocking any dependencies.

## External Dependencies 
The application has external dependencies on two packages, NUnit and Newtonsoft.Json which are resolved using Nuget.    

