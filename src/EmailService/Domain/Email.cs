namespace EmailWorkerService.Domain;

public class Email : AuditEntity
{
    public EmailType EmailType { get; set; }
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
