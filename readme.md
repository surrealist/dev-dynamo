# Dev Dynamo: Streamlined Project Management APIs

Dev Dynamo provides an intuitive Jira-like project management API interface, enabling seamless communication between business and technical teams. Its uniqueness lies in utilizing a mermaid script text file for workflow definitions, effectively functioning as a Domain-Specific Language (DSL). Each project is designed to align with a single, unique workflow definition.

## Core Entities

- **Project**: A single unit of work managed using Dev Dynamo.
- **Ticket**: Individual tasks or issues associated with a Project.
- **Workflow**: The sequence of statuses that a ticket can move through in the lifecycle of a Project.

## Features and Functionalities

- **Project Management**: Efficient tools for creating, updating, and querying Projects.
- **Ticket Management**: A streamlined system for managing Tickets within a Project, including creation, updates, and retrieval.
- **Ticket Status Management**: The status of Tickets can only be altered according to the specific rules defined in the Workflow, ensuring consistency and traceability.
- **Ticket History**: An accessible log for viewing the history of a Ticket, offering insights into status changes and progress.
