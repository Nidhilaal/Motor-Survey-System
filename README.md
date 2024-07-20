# Motor-Survey-System-Framework

The Motor Survey System is a web-based application developed using the .NET Framework, Oracle Database, and ASP.NET Web Forms. The system facilitates the initiation, survey, and approval of motor insurance claims, streamlining the entire process for both surveyors and users.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Architecture](#architecture)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [Contact](#contact)

## Introduction

The Motor Survey System is designed to manage motor insurance claims efficiently. The system has two primary roles: the **Surveyor** and the **User**. Users initiate claims, surveyors conduct surveys and submit reports, and users or employees approve the surveys.

## Features

- **User Side**:
  - Initiate motor insurance claims.
  - View the status of submitted claims.
  - Approve or reject survey reports.

- **Surveyor Side**:
  - Receive and view assigned claims.
  - Conduct surveys and submit reports.
  - Update the status of claims.

## Architecture

The system is built using the following technologies:

- **Backend**: .NET Framework
- **Database**: Oracle
- **Frontend**: ASP.NET Web Forms

The application follows a multi-tier architecture to separate concerns and enhance maintainability.

## Installation

### Prerequisites

- .NET Framework
- Oracle Database
- IIS (Internet Information Services)

### Steps

1. **Clone the repository**:
    ```bash
    git clone https://github.com/Nidhilaal/motor-survey-system.git
    cd motor-survey-system
    ```

2. **Setup the Database**:
   - Run the SQL scripts located in the `DbScripts` folder to set up the Oracle database.

3. **Configure the Connection String**:
   - Update the connection string in the `web.config` file to point to your Oracle database instance.

4. **Build the Project**:
   - Open the solution in Visual Studio and build the project.

5. **Deploy to IIS**:
   - Publish the project to IIS.

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
   - Navigate to the "Claims" section.

2. **Submit Survey**:
   - Conduct the survey and fill in the survey report.
   - Submit the survey report.

## Contributing

Contributions are welcome! Please contact me to contribute to this project.

## Contact

For any inquiries or issues, please contact [Nidhilal](mailto:msnidhilal@16gmail.com).
