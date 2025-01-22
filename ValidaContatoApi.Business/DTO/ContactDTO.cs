namespace ValidaContatoApi.Business.DTO;

public class ContactDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Gender { get; set; }
    public int Age { get; set; }
}
