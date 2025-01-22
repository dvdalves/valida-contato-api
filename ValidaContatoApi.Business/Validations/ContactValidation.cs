namespace ValidaContatoApi.Business.Validations;

internal class ContactValidation
{
    public DateTime CurrentDate = DateTime.Now;

    internal bool ValidateDate(DateTime birthDate)
    {
        return birthDate.Date <= CurrentDate.Date;
    }

    internal bool ValidateIfOfLegalAge(DateTime birthDate)
    {
        var age = CalculateAge(birthDate);

        return age >= 18;
    }

    internal int CalculateAge(DateTime birthDate)
    {
        var difference = CurrentDate - birthDate;

        return (int)difference.TotalDays / 365;
    }
}
