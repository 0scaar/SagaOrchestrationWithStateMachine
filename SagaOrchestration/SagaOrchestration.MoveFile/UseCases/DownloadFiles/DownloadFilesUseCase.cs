using MassTransit;
using Microsoft.Extensions.Logging;
using SagaOrchestration.MoveFile.Masstransit.Contracts;
using Serilog;

namespace SagaOrchestration.MoveFile.UseCases.DownloadFiles
{
    internal class DownloadFilesUseCase : IDownloadFilesUseCase
    {
        private readonly ILogger<DownloadFilesUseCase> logger;
        private readonly IPublishEndpoint publishEndpoint;

        public DownloadFilesUseCase(IPublishEndpoint publishEndpoint, ILogger<DownloadFilesUseCase> logger)
        {
            this.publishEndpoint = publishEndpoint;
            this.logger = logger;
        }

        public Task Execute()
        {
            var path = Environment.GetEnvironmentVariable("INPUT_LOCAL_PATH") ?? "/app/local/IN";

            if (!string.IsNullOrEmpty(path))
            {
                var files = new DirectoryInfo(path).GetFiles("*.csv").ToList();

                logger.LogInformation($"JobFileTransferUseCase - Total files on server: {files.Count} - FileNames: {string.Join(" - ", files.Select(s => s.Name).ToList())} - Date: {DateTime.UtcNow}");
                Log.Information($"JobFileTransferUseCase - Total files on server: {files.Count} - FileNames: {string.Join(" - ", files.Select(s => s.Name).ToList())} - Date: {DateTime.UtcNow}");

                files.ForEach(async f =>
                {
                    var read = new ReadFileSubmitted(Guid.NewGuid(), f.FullName);
                    await publishEndpoint.Publish(read);
                });
            }

            return Task.CompletedTask;
        }
    }
}
