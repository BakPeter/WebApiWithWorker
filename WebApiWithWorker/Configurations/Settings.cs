namespace WebApiWithWorker.Configurations;

public class Settings
{
    public string? ApplicationHttpUrl { get; set; }
    public string? ApplicationHttpsUrl { get; set; }
    public List<string> ApplicationUrls { get; set; }
}