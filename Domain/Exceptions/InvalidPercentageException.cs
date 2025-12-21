using System;

namespace Domain.Exceptions;

public class InvalidPercentageException : Exception
{
    public InvalidPercentageException()
        : base($"Invalid percentage out of range 0.0 to 1.0")
    {
    }
}
