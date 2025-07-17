# Workflow State Engine API<br>
A minimal and modular .NET 8 Web API for managing configurable workflows using state machines.<br>
Supports dynamic definition of workflow states, transitions, and execution of instances with validation logic.<br>

## Features<br>
- Define workflow schemas (states + transitions)<br>
- Start instances based on schema definitions<br>
- Execute transitions to move between valid states<br>
- Retrieve current state and action history for instances<br>
- In-memory processing (no database dependencies)<br>

## Requirements<br>
- .NET 8 SDK<br>
- REST client (e.g., Postman or curl)<br>

## Getting Started<br>
1. Clone the repository or copy the files into a `.NET` minimal API project<br>
2. Run the application:<br>

```bash
dotnet run
The API will be available at: http://localhost:5000
```
## API Endpoints<br>
### Define a new workflow
```bash
POST /flow/define
// Define states and transitions in a single request
```
### Get a workflow definition
```bash
GET /flow/define/{id}
```

### Start a new workflow instance
```bash
POST /flow/start/{schemaId}
```

### Execute an action on an instance
```bash
POST /flow/instance/{instanceId}/transition/{transitionId}
```

### Get instance state and history
```bash
GET /flow/instance/{instanceId}
```

Example JSON (POST /flow/define)<br>
```bash
json
{
  "id": "demo-flow",
  "states": [
    { "id": "start", "isInitial": true, "isFinal": false, "enabled": true },
    { "id": "complete", "isInitial": false, "isFinal": true, "enabled": true }
  ],
  "actions": [
    {
      "id": "proceed",
      "enabled": true,
      "fromStates": ["start"],
      "toState": "complete"
    }
  ]
}
```
## Project Structure<br>
Program.cs – Entry point with endpoint mapping<br>

Domain/ – Data models: FlowSchema, NodeState, TransitionRule, FlowInstance<br>

Logic/ – Core logic in ProcessEngine.cs<br>

Endpoints/ – API route handlers for definitions and instances<br>

## Notes<br>
No database or file storage – data resets on restart<br>

All identifiers are string-based (custom or GUIDs)<br>

Code is designed to be clear, extendable, and testable<br>
