namespace SagaOrchestration.MoveFile.Masstransit.Contracts
{
    public class ReadFileSubmitted
    {
        public Guid FileId { get; set; }
        public string File { get; set; }

        public ReadFileSubmitted(Guid fileId, string file)
        {
            FileId = fileId;
            File = file;
        }
    }
}
