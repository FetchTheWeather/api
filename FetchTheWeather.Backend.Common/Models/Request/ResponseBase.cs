namespace FetchTheWeather.Backend.Common.Models.Request;

public class ResponseBase
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }

    public string? ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
}