namespace DermaKlinik.API.Core.Entities
{
    public class Log : AuditableEntity
    {
        public string Level { get; set; }
        public string Message { get; set; }
        public string? Exception { get; set; }
        public string? StackTrace { get; set; }
        public string? Source { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? RequestPath { get; set; }
        public string? RequestMethod { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponseBody { get; set; }
        public int? StatusCode { get; set; }
        public string? IpAddress { get; set; }
        public DateTime Timestamp { get; set; }
        public string? AdditionalData { get; set; }
    }
}