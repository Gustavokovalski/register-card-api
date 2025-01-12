using System.Diagnostics.CodeAnalysis;

namespace RegisterCard.Application.Common.Exceptions;

[ExcludeFromCodeCoverage]
public static class ThrowIfNullException
{
    public static T ThrowIfNull<T>(this T argument)
    {
        if (argument != null)
        {
            return argument;
        }

        throw new ArgumentNullException(typeof(T).Name);
    }
}