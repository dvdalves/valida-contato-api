using ValidaContatoApi.Domain.Enum;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Tests.ContatoTests;

public static class ContactMoq
{
    private static Contact GetContactForMoq(string name)
    {
        return new Contact
        {
            BirthDate = DateTime.Parse("05-10-1995"),
            Gender = GenderEnum.Male,
            Name = name,
            Status = true,
        };
    }

    private static List<Contact> GetContactList(int quantity)
    {
        var list = new List<Contact>();

        for (int i = 0; i < quantity; i++)
            list.Add(GetContactForMoq($"{nameof(GetMoq_For_GetAllContacts)}{i}"));

        return list;
    }

    public static List<Contact> GetMoq_For_GetAllContacts()
    {
        return GetContactList(10);
    }

    public static List<Contact> GetMoq_For_UpdateContact()
    {
        return GetContactList(10);
    }

    public static List<Contact> GetMoq_For_GetContactById()
    {
        var list = new List<Contact>
        {
            GetContactForMoq(nameof(GetMoq_For_GetContactById))
        };
        return list;
    }
}
