using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ValidaContatoApi.Business.Interface;
using ValidaContatoApi.Business.ViewModels;
using ValidaContatoApi.Domain.Enum;
using ValidaContatoApi.Domain.Models;

namespace ValidaContatoApi.Tests.ContatoTests;

public class ContactTests : BaseTest
{
    public IContactService _contactService;

    public ContactTests()
    {
        _contactService = _serviceProvider.GetRequiredService<IContactService>();
    }

    #region Tests
    [Test]
    public async Task GetAll_Success()
    {
        //expected
        var expectedStatusCode = 200;
        var expectedMessage = "Success";

        //arrange
        PopulateDatabase(ContactMoq.GetMoq_For_GetAllContacts());

        //act
        var resultAct = await _contactService.GetAll();

        //assert    
        Assert.That(resultAct.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(resultAct.Message, Is.EqualTo(expectedMessage));
        Assert.That(resultAct.IsSuccess, Is.True);
    }

    [Test]
    public async Task GetAll_Failure()
    {
        // expected
        var expectedStatusCode = 204;
        var expectedMessage = "No records found!";

        // arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        // act
        var resultAct = await _contactService.GetAll();

        // assert    
        Assert.That(resultAct.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(resultAct.Message, Is.EqualTo(expectedMessage));
        Assert.That(resultAct.IsSuccess, Is.False);
    }

    [Test]
    public async Task GetById_Failure()
    {
        //expected
        var expectedStatusCode = 204;
        var expectedMessage = "Contact not found!";

        //arrange
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        //act
        var resultAct = await _contactService.GetById(Guid.NewGuid());

        //assert
        Assert.That(resultAct.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(resultAct.Message, Is.EqualTo(expectedMessage));
        Assert.That(resultAct.IsSuccess, Is.False);
    }

    [Test]
    public async Task GetById_Success()
    {
        //expected  
        var expectedStatusCode = 200;
        var expectedMessage = "Contact retrieved successfully!";

        //arrange
        PopulateDatabase(ContactMoq.GetMoq_For_GetContactById());

        //act
        var contact = await _context.Contatos.FirstOrDefaultAsync();
        var resultAct = await _contactService.GetById(contact!.Id);

        //assert
        Assert.That(resultAct.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(resultAct.Message, Is.EqualTo(expectedMessage));
        Assert.That(resultAct.IsSuccess, Is.True);
    }

    [Test]
    public async Task Add_Success()
    {
        //expected
        var expectedStatusCode = 200;
        var expectedMessage = "Contact added successfully!";

        //arrange 
        var createContact = new CreateContactVM
        {
            BirthDate = DateTime.Parse("05-10-1995"),
            Gender = (int)GenderEnum.Male,
            Name = "David",
        };

        //act 
        var resultAct = await _contactService.Create(createContact);
        var contactExists = await _context.Contatos.AnyAsync(p => p.Name == createContact.Name);

        //assert
        Assert.That(resultAct.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(resultAct.Message, Is.EqualTo(expectedMessage));
        Assert.That(contactExists, Is.True);
        Assert.That(resultAct.IsSuccess, Is.True);
    }

    [Test]
    public async Task Add_InvalidAge_Failure()
    {
        //expected
        var expectedStatusCode = 400;
        var expectedMessage = "Contact cannot be underage!";

        //arrange
        var createContact = new CreateContactVM
        {
            BirthDate = DateTime.Now,
            Gender = (int)GenderEnum.Male,
            Name = "David",
        };

        //act
        var resultAct = await _contactService.Create(createContact);
        var contactExists = await _context.Contatos.AnyAsync(p => p.Name == createContact.Name);

        //assert    
        Assert.That(resultAct.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(resultAct.Message, Is.EqualTo(expectedMessage));
        Assert.That(resultAct.IsSuccess, Is.False);
        Assert.That(contactExists, Is.False);
    }

    [Test]
    public async Task Add_InvalidDate_Failure()
    {
        //expected
        var expectedStatusCode = 400;
        var expectedMessage = "Cannot select a date greater than or equal to the current date!";

        //arrange
        var createContact = new CreateContactVM
        {
            BirthDate = DateTime.Now.AddDays(1),
            Gender = (int)GenderEnum.Male,
            Name = "David",
        };

        //act
        var resultAct = await _contactService.Create(createContact);
        var contactExists = await _context.Contatos.AnyAsync(p => p.Name == createContact.Name);

        //assert    
        Assert.That(resultAct.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(resultAct.Message, Is.EqualTo(expectedMessage));
        Assert.That(resultAct.IsSuccess, Is.False);
        Assert.That(contactExists, Is.False);
    }

    [Test]
    public async Task Toggle_Activate_Success()
    {
        // expected
        var expectedStatusCode = 200;
        var expectedMessage = "Contact activated successfully!";

        // arrange
        var contactId = Guid.NewGuid();
        var contact = new Contact { Id = contactId, Status = false };

        PopulateDatabase(new List<Contact> { contact });

        // act
        var result = await _contactService.Toggle(contactId);

        // assert
        Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(result.Message, Is.EqualTo(expectedMessage));
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(contact.Status, Is.True);
    }

    [Test]
    public async Task Toggle_Deactivate_Success()
    {
        // expected
        var expectedStatusCode = 200;
        var expectedMessage = "Contact deactivated successfully!";

        // arrange
        var contactId = Guid.NewGuid();
        var contact = new Contact { Id = contactId, Status = true };

        PopulateDatabase(new List<Contact> { contact });

        // act
        var result = await _contactService.Toggle(contactId);

        // assert
        Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(result.Message, Is.EqualTo(expectedMessage));
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(contact.Status, Is.False);
    }

    [Test]
    public async Task Toggle_ContactNotFound_Failure()
    {
        // expected
        var expectedStatusCode = 404;
        var expectedMessage = "Contact not found!";

        // arrange
        var contactId = Guid.NewGuid();

        // act
        var result = await _contactService.Toggle(contactId);

        // assert
        Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(result.Message, Is.EqualTo(expectedMessage));
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public async Task Update_Success()
    {
        PopulateDatabase(ContactMoq.GetMoq_For_UpdateContact());
        // expected
        var expectedStatusCode = 200;
        var expectedMessage = "Contact updated successfully!";

        // Arrange
        var updatedContact = await _context.Contatos.FirstOrDefaultAsync();
        var contact = _mapper.Map<UpdateContactVM>(updatedContact);
        contact.Name = "David 5";

        // Act
        var result = await _contactService.Update(contact);
        var verification = await _context.Contatos.FirstAsync(p => p.Id == contact.Id);

        // Assert
        Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(result.Message, Is.EqualTo(expectedMessage));
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(verification.Name, Is.EqualTo(contact.Name));
    }

    [Test]
    public async Task Update_ContactNotFound_Failure()
    {
        //expected
        var expectedStatusCode = 404;
        var expectedMessage = "Contact does not exist!";

        //arrange 
        var updateContact = new UpdateContactVM
        {
            Id = Guid.NewGuid(),
            BirthDate = DateTime.Parse("05-10-1995"),
            Gender = GenderEnum.Male,
            Name = "David",
        };

        //act 
        var resultAct = await _contactService.Update(updateContact);
        var contactExists = await _context.Contatos.AnyAsync(p => p.Id == updateContact.Id);

        //assert
        Assert.That(resultAct.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(resultAct.Message, Is.EqualTo(expectedMessage));
        Assert.That(contactExists, Is.False);
        Assert.That(resultAct.IsSuccess, Is.False);
    }

    [Test]
    public async Task Update_InvalidAge_Failure()
    {
        //expected
        var expectedStatusCode = 400;
        var expectedMessage = "Contact cannot be underage!";

        //arrange
        var updatedContact = await _context.Contatos.FirstOrDefaultAsync();
        var contact = _mapper.Map<UpdateContactVM>(updatedContact);
        contact.BirthDate = DateTime.Now;
        contact.Name = "David 5";

        //act
        var resultAct = await _contactService.Update(contact);
        var updatedContactEntity = await _context.Contatos.FirstOrDefaultAsync(p => p.Id == contact.Id);

        //assert    
        Assert.That(resultAct.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(resultAct.Message, Is.EqualTo(expectedMessage));
        Assert.That(resultAct.IsSuccess, Is.False);
        Assert.That(updatedContactEntity, Is.Not.Null);
        Assert.That(updatedContactEntity!.Name, Is.Not.EqualTo(contact.Name));
    }

    [Test]
    public async Task Update_InvalidDate_Failure()
    {
        //expected
        var expectedStatusCode = 400;
        var expectedMessage = "Cannot select a date greater than or equal to the current date!";

        // Arrange
        var updatedContact = await _context.Contatos.FirstOrDefaultAsync();
        var contact = _mapper.Map<UpdateContactVM>(updatedContact);
        contact.BirthDate = DateTime.Now.AddDays(1);
        contact.Name = "David 5";

        // Act
        var resultAct = await _contactService.Update(contact);
        var updatedContactEntity = await _context.Contatos.FirstOrDefaultAsync(p => p.Id == contact.Id);

        // Assert
        Assert.That(resultAct.IsSuccess, Is.False);
        Assert.That(resultAct.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(resultAct.Message, Is.EqualTo(expectedMessage));
        Assert.That(updatedContactEntity, Is.Not.Null);
        Assert.That(updatedContactEntity!.Name, Is.Not.EqualTo(contact.Name));
    }

    [Test]
    public async Task Delete_ContactNotFound_Failure()
    {
        // expected
        var expectedStatusCode = 404;
        var expectedMessage = "Contact not found!";

        // arrange
        var contactId = Guid.NewGuid();

        // act
        var result = await _contactService.Delete(contactId);

        // assert
        Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(result.Message, Is.EqualTo(expectedMessage));
        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public async Task Delete_ContactExists_Success()
    {
        // expected
        var expectedStatusCode = 200;
        var expectedMessage = "Contact deleted successfully!";

        // arrange
        var contactId = Guid.NewGuid();
        var contact = new Contact { Id = contactId };

        PopulateDatabase(new List<Contact> { contact });

        // act
        var result = await _contactService.Delete(contactId);

        // assert
        Assert.That(result.StatusCode, Is.EqualTo(expectedStatusCode));
        Assert.That(result.Message, Is.EqualTo(expectedMessage));
        Assert.That(result.IsSuccess, Is.True);
    }
}
#endregion
