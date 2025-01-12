using RegisterCard.Application.Common.Bases;

namespace RegisterCard.Application.Common.Exception;
public class ValidationExceptionCustom : System.Exception
{
    public IEnumerable<BaseError> Errors { get; }

    public ValidationExceptionCustom()
        : base("One or more validation failures have occured.")
    {
        Errors = new List<BaseError>();
    }

    public ValidationExceptionCustom(IEnumerable<BaseError> errors)
        : this()
    {
        Errors = errors;
    }
}