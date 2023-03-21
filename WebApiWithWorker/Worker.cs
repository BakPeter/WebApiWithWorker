using System.Text.Json;

namespace WebApiWithWorker;

public class Worker: BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var counter = -1;

        while (stoppingToken.IsCancellationRequested is false)
        {
            counter++;

            var topic = $"DemoTopic_{counter % 11}";
            var key = $"DemoKye_{counter}";
            var value = new Dictionary<string, string>
            {
                { "counter", counter.ToString() },
                { "counter*2", (counter * 2).ToString() },
                { "counter even", (counter % 2 == 0).ToString() }
            };

            var data = new Data { Topic = topic, Key = key, Value = value };
            
            _logger.LogInformation($"DeliveryResult: {JsonSerializer.Serialize(data)}");

            await Task.Delay(1000, stoppingToken);
        }
    }
}

public class Data
{
    public string Topic { get; set; }
    public string Key { get; set; }
    public Dictionary<string, string> Value { get; set; }
}