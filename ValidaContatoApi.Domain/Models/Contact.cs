using System.ComponentModel.DataAnnotations.Schema;
using ValidaContatoApi.Domain.Enum;

namespace ValidaContatoApi.Domain.Models;

public class Contact : Entity
{
    public string? Name { get; set; }
    public bool Status { get; set; } = true;
    public GenderEnum Gender { get; set; }
    public DateTime BirthDate { get; set; }

    [NotMapped]
    public int Age { get; set; }
}
