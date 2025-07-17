namespace FlowAPI.Domain;

public class FlowSchema
{
    public string Id { get; set; } = string.Empty;
    public List<NodeState> States { get; set; } = new();
    public List<TransitionRule> Actions { get; set; } = new();
}
