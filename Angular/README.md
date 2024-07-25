# Motor Survey System (Angular)

This folder contains the Angular implementation of the Motor Survey System with a .NET Core API backend.

## Overview

The Motor Survey System is designed to manage motor insurance claims efficiently. The system has two primary roles: the **Surveyor** and the **User**. Users initiate claims, surveyors conduct surveys and submit reports, and users or employees approve the surveys.

## Technologies Used

- **Frontend**: Angular
- **Backend**: .NET Core API
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

- Node.js and npm
- Angular CLI
- .NET Core SDK
- Oracle Database

### Steps

1. **Clone the repository**:
    ```bash
    git clone https://github.com/your-username/motor-survey-system.git
    cd motor-survey-system/angular
    ```

2. **Setup the Database**:
   - Run the SQL scripts located in the `database` folder to set up the Oracle database.

3. **Backend Setup**:
   - Navigate to the `api` folder:
     ```bash
     cd api
     ```
   - Configure the connection string in the `appsettings.json` file to point to your Oracle database instance.
   - Build and run the API:
     ```bash
     dotnet build
     dotnet run
     ```

4. **Frontend Setup**:
   - Navigate to the `frontend` folder:
     ```bash
     cd ../frontend
     ```
   - Install Angular dependencies:
     ```bash
     npm install
     ```
   - Serve the Angular application:
     ```bash
     ng serve
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

## License

This project is licensed under the MIT License. See the [LICENSE](../LICENSE) file for details.

## Contact

For any inquiries or issues, please contact [your-email@example.com](mailto:your-email@example.com).
