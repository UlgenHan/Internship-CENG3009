# Football Simulator

## Overview

The **Football Simulator** is a desktop application developed using WPF (Windows Presentation Foundation). The application is structured as a monolithic, n-tier layered architecture, following the principles of Clean Architecture. This project was built to gain practical experience in designing scalable and maintainable software systems.

The application simulates football matches, providing detailed statistics, player performance metrics, and head-to-head comparisons between teams. The architecture is divided into multiple layers to ensure separation of concerns and to promote code reusability and testability.

## Architecture

### 1. Core Layer

The **Core Layer** contains the domain entities, business logic, and interfaces. This layer is independent of any external frameworks, which ensures that the business logic can be easily reused or tested in isolation.

- **Entities**: Represent the core objects of the domain (e.g., Team, Player, Match, Statistics).
- **Interfaces**: Define the contracts for services, repositories, and other dependencies.
- **Domain Services**: Contain business logic that is independent of the application's infrastructure.
- **Value Objects**: Immutable objects that describe aspects of the domain (e.g., Score, MatchResult).

### 2. Service Layer

The **Service Layer** acts as a bridge between the Core Layer and the Repository Layer. It contains the implementation of the business logic defined in the Core Layer and coordinates between the data access and presentation layers.

- **Application Services**: Handle the application's use cases and interact with repositories and domain services.
- **DTOs (Data Transfer Objects)**: Simplify the data exchange between layers by providing a clean and minimalistic representation of the data.

### 3. Repository Layer

The **Repository Layer** is responsible for data persistence and retrieval. It abstracts the data access logic and interacts with the underlying database or data storage.

- **Repositories**: Implement the data access logic, including CRUD operations, and ensure that the data is fetched or stored according to the needs of the application.
- **Data Context**: Manages the database connections and ensures that data is correctly synchronized between the application and the database.

### 4. Presentation Layer

The **Presentation Layer** is responsible for the user interface (UI) of the application. Built with WPF, it provides a rich and interactive experience for the user.

- **Views**: Represent the UI components, including windows, user controls, and dialogs.
- **View Models**: Implement the MVVM (Model-View-ViewModel) pattern, ensuring that the UI is loosely coupled with the underlying business logic.
- **Commands**: Handle user interactions and bind them to the View Models.
- **Data Binding**: Ensures that the UI elements are synchronized with the data in the View Models.

## Features
![wpffoog](https://github.com/user-attachments/assets/f6354e1a-2ea1-49f0-aa7d-38cc9f998f60)

- **Simulate Matches**: Create realistic football matches with detailed statistics, including goals, assists, possession, and more.
  ![sim](https://github.com/user-attachments/assets/f575c1d7-8022-4b76-ac21-1756c33d3a5f)
- **Team Management**: Manage teams, players, and formations, and customize strategies for each match.
  ![wpfplayers](https://github.com/user-attachments/assets/95a0258c-abfe-4b5e-b433-c43ca777e748)

- **Player Statistics**: Track player performance across matches, including goals, assists, cards, and match ratings.
- **Head-to-Head Comparison**: Compare two teams based on historical data and current form.
- **User-Friendly Interface**: Interactive and intuitive UI built with WPF, providing an engaging experience for users.

## Getting Started

### Prerequisites

- **.NET Framework**: Ensure that you have the .NET Framework installed on your machine.
- **Visual Studio**: Use Visual Studio for development, debugging, and building the application.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-repo/football-simulator.git
