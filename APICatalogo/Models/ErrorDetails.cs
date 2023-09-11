using System.Text.Json;

namespace APICatalogo.Models;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string Trace { get; set; }

    public override string ToString()
    {
        //return JsonConvert.SerializeObject(this); método do newtonjson
        return JsonSerializer.Serialize(this);
    }
}
