using System.ComponentModel.DataAnnotations;

namespace Bongo.Models.ModelValidations;

public class DateInFutureAttribute : ValidationAttribute
{
    private readonly Func<DateTime> _dateTimeNowProvider;

    public DateInFutureAttribute()
        : this(() => DateTime.Now)
    {
    }

    public DateInFutureAttribute(Func<DateTime> dateTimeNowProvider)
    {
        _dateTimeNowProvider = dateTimeNowProvider;
        ErrorMessage = "Date must be in the future";
    }

    public override bool IsValid(object? value)
    {
        var isValid = false;

        if (value is DateTime dateTime) isValid = dateTime > _dateTimeNowProvider();

        return isValid;
    }
}