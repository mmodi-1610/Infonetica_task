namespace FlowAPI.Domain;

// A single state/node in the workflow
public class NodeState
{
    public string Id { get; set; } = string.Empty;
    public bool IsInitial { get; set; }
    public bool IsFinal { get; set; }
    public bool Enabled { get; set; } = true;
}
