using System.Text.Json;

namespace Challenge.Process.Aiq.WebApi.Abstractions;

public class LowerCasePolicy: JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        return name.ToLowerInvariant();
    }
}