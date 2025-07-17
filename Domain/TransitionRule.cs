namespace FlowAPI.Domain;

// Represents a transition rule between states
public class TransitionRule
{
    public string Id { get; set; } = string.Empty;
    public bool Enabled { get; set; } = true;
    public List<string> FromStates { get; set; } = new();
    public string ToState { get; set; } = string.Empty;
}

