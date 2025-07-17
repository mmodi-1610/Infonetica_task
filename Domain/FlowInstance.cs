namespace FlowAPI.Domain;

public class FlowInstance
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string SchemaId { get; set; } = string.Empty;
    public string CurrentState { get; set; } = string.Empty;
    public List<(string ActionId, DateTime Timestamp)> History { get; set; } = new();
}
