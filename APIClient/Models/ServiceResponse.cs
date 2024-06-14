namespace ApiClient.Models;

public class ServiceResponse
{
    public bool Flag { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Token { get; set; } = string.Empty;
}
