using ValidaContatoApi.Domain.Enum;

namespace ValidaContatoApi.Business.ViewModels;

public class UpdateContactVM
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public GenderEnum Gender { get; set; }
    public DateTime BirthDate { get; set; }
}
