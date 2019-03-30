namespace DemoSentimentContainer.Models
{
    public class DocumentResponse
    {
        public Document[] documents { get; set; }
        public Error[] errors { get; set; }
    }

    public class Document
    {
        public string id { get; set; }
        public float score { get; set; }
    }

    public class Error
    {
        public string code { get; set; }
        public InnerError innerError { get; set; }
        public string message { get; set; }
    }

    public class InnerError
    {
        public string requestId { get; set; }
    }
}
