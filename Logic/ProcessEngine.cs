using FlowAPI.Domain;

namespace FlowAPI.Logic;

// This class holds core engine logic for workflow definitions and instances.
public class ProcessEngine
{
    private readonly Dictionary<string, FlowSchema> _definitions = new();
    private readonly Dictionary<string, FlowInstance> _instances = new();

    public string AddDefinition(FlowSchema schema)
    {
        if (_definitions.ContainsKey(schema.Id))
            throw new Exception($"Duplicate workflow ID '{schema.Id}'.");

        if (schema.States.Count(s => s.IsInitial) != 1)
            throw new Exception("Workflow must have exactly one initial node.");

        _definitions[schema.Id] = schema;
        return schema.Id;
    }

    public FlowInstance InitializeInstance(string schemaId)
    {
        if (!_definitions.TryGetValue(schemaId, out var schema))
            throw new Exception($"Workflow '{schemaId}' not found.");

        var startNode = schema.States.FirstOrDefault(s => s.IsInitial && s.Enabled)
            ?? throw new Exception("No enabled initial node found.");

        var instance = new FlowInstance
        {
            SchemaId = schemaId,
            CurrentState = startNode.Id
        };

        _instances[instance.Id] = instance;
        return instance;
    }

    public void ApplyTransition(string instanceId, string transitionId)
    {
        if (!_instances.TryGetValue(instanceId, out var instance))
            throw new Exception("Instance not found.");

        if (!_definitions.TryGetValue(instance.SchemaId, out var schema))
            throw new Exception("Workflow definition is missing.");

        var currentNode = schema.States.First(s => s.Id == instance.CurrentState);
        if (currentNode.IsFinal)
            throw new Exception("Cannot transition from a final node.");

        var rule = schema.Actions.FirstOrDefault(a => a.Id == transitionId)
            ?? throw new Exception("Invalid transition.");

        if (!rule.Enabled)
            throw new Exception("Transition is disabled.");

        if (!rule.FromStates.Contains(instance.CurrentState))
            throw new Exception("Transition not allowed from current node.");

        var target = schema.States.FirstOrDefault(s => s.Id == rule.ToState)
            ?? throw new Exception("Target node does not exist.");

        if (!target.Enabled)
            throw new Exception("Target node is disabled.");

        instance.CurrentState = target.Id;
        instance.History.Add((transitionId, DateTime.UtcNow));
    }

    public FlowInstance? GetInstance(string id) => _instances.GetValueOrDefault(id);
    public FlowSchema? GetSchema(string id) => _definitions.GetValueOrDefault(id);
}
