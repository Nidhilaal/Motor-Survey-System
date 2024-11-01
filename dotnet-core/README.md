# Motor Survey System (ASP.NET Core)

This folder contains the ASP.NET Core implementation of the Motor Survey System.

## Overview

The Motor Survey System is designed to manage motor insurance claims efficiently. The system has two primary roles: the **Surveyor** and the **User**. Users initiate claims, surveyors conduct surveys and submit reports, and users or employees approve the surveys.

## Architecture

- **Backend**: .NET Core 6.0
- **Frontend**: ASP.NET Razor Pages / MVC
- **Database**: Oracle Database

## Features

- **User Side**:
  - Initiate motor insurance claims.
  - View the status of submitted claims.
  - Approve or reject survey reports.

- **Surveyor Side**:
  - Receive and view assigned claims.
  - Conduct surveys and submit reports.
  - Update the status of claims.

## Installation

### Prerequisites

- .NET Core SDK
- Oracle Database

### Steps

1. **Clone the repository**:
    ```bash
    git clone https://github.com/your-username/motor-survey-system.git
    cd motor-survey-system/dotnet_core
    ```

2. **Setup the Database**:
   - Run the SQL scripts located in the `database` folder to set up the Oracle database.

3. **Configure the Connection String**:
   - Update the connection string in the `appsettings.json` file to point to your Oracle database instance.

4. **Build and Run the Project**:
    ```bash
    dotnet build
    dotnet run
    ```

## Usage

### User Side

1. **Initiate a Claim**:
   - Log in to the application.
   - Navigate to the "Initiate Claim" section.
   - Fill in the required details and submit.

2. **Approve Survey**:
   - Review submitted survey reports.
   - Approve or reject the survey.

### Surveyor Side

1. **View Assigned Claims**:
   - Log in to the application.
   - Navigate to the "Assigned Claims" section.

2. **Submit Survey**:
   - Conduct the survey and fill in the survey report.
   - Submit the survey report.

## Contact

For any inquiries or issues, please contact [Nidhilal](mailto:msnidhilal6@gmail.com).
