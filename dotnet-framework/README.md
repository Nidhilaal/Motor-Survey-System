# Motor Survey System (ASP.NET Framework)

This folder contains the ASP.NET Framework implementation of the Motor Survey System.

## Overview

The Motor Survey System is designed to manage motor insurance claims efficiently. The system has two primary roles: the **Surveyor** and the **User**. Users initiate claims, surveyors conduct surveys and submit reports, and users or employees approve the surveys.

## Technologies Used

- **Backend**: .NET Framework
- **Frontend**: ASP.NET Web Forms
- **Database**: Oracle Database
- **Server**: IIS (Internet Information Services)

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

- .NET Framework
- Oracle Database
- IIS (Internet Information Services)

### Steps

1. **Clone the repository**:
    ```bash
    git clone https://github.com/your-username/motor-survey-system.git
    cd motor-survey-system/dotnet_framework
    ```

2. **Setup the Database**:
   - Run the SQL scripts located in the `database` folder to set up the Oracle database.

3. **Configure the Connection String**:
   - Update the connection string in the `web.config` file to point to your Oracle database instance.

4. **Build the Project**:
   - Open the solution in Visual Studio and build the project.

5. **Deploy to IIS**:
   - Publish the project to IIS.


## Contact

For any inquiries or issues, please contact [Nidhilal](mailto:msnidhilal@16gmail.com).
