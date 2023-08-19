namespace requestInspect.Model;
public class RequestEntity
{
    public string RequestId { get; set;} = Guid.NewGuid().ToString();
    public string? Method { get; set;}
    public Dictionary<string, string> Headers { get; set;} = new();
    public string Body { get; set;} = string.Empty;
    public DateTime dateTime{ get; set;} = DateTime.Now;
}
