# Project Setup and Running Instructions

## Prerequisites

Before you start, ensure that you have the following installed on your machine:

- **Node.js**: [Download and install Node.js](https://nodejs.org/) if you don't have it already.
- **Docker**: [Download and install Docker](https://www.docker.com/products/docker-desktop) if you don't have it already.

## Setup Instructions

### 1. Clone the Repository to your local machine:

### 2. Start Docker Containers

Navigate to the root directory of the solution and start the Docker containers with this command:
```
docker compose up -d
```
This command will initialise local SQL Server database and API containers with `docker-compose.yml` config file.

#### Note: A database may need up to a minute to spin up when script has finished, so relax and wait a little.

### 3. Install Dependencies and Start the Application

Next, navigate to the `src/seoranktracker.react.ui` directory and install the necessary Node.js dependencies:
```
cd src/seoranktracker.react.ui
npm install
```
Once the dependencies are installed, start the application with this script:
```
npm run dev:docker
```

This command will start the Vite development server with the Docker-specific environment settings.

## Summary

1. **Ensure Node.js and Docker are installed.**
2. **Run `docker-compose up -d` in the root directory to start the API, Database and run migrations**
3. **Navigate to `src/seoranktracker.react.ui` and run `npm install` to install dependencies.**
4. **Run `npm run dev:docker` to start the React-frontend application.**

For any issues or additional help, please refer to the project documentation or contact me at **yefimov.ruslan@proton.me**
