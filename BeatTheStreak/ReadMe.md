![Logo of the project](https://raw.githubusercontent.com/jehna/readme-best-practices/master/sample-logo.png)

# Beat The Streak
> Crunches the numbers and comes up with 2 batters who are most likely to get a hit tomorrow.

{VERBOSE DESCRIPTION}

## Development approach

Develop features on a Use Case basis employing TDD.

See the architecture decisions in the Doco folder for further information.

Basically the development process is one of designing Commands and testing them

### Commands so far 
 1. GetProbablePitchers
 2. PickBatters
 3. GetLineup

### Building

 1. Make sure all the test pass
 1. Build with mode set to Release

### Deploying / Publishing

 1. shut down the app if running in Prod
 1. xcopy deploy to Prod, see .bat file in Deployment folder
 1. run it

## Features

 1. Load Season - will be a way to intialise a season with teams that have their
 positions initally assigned.

## Backlog

 1. Allow for double headers
 2. Only pick 1, 2, 3 or 4 in the batting order

## Configuration
 1. The Stattleship API key, currently hard coded

## Solution Structure
 1.	Presentation (UI)
 2.	Application (abstractions of the Use Cases)
 3.	Domain (abstractions corresponding to the problem (business) domain)
 4.	Persistence (mechanism for storing the data)
 5.	Infrastructure (interface to the operating system and Third Party dependencies)
 6.	Common (cross cutting concerns)
 7.	Specification (acceptance tests)

## Development Notes
 1. Wrap calls to Statleship API with Repository Classes
 2. Repository classes should return view models
 3. Use US dates as this is what Statle ship uses
 4. Version number in BaseReport.cs