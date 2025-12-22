namespace Domain.Exceptions;

public class InvalidValueException : Exception
{
    public InvalidValueException()
        : base($"Invalid value out of range -1.0 to 1.0")
    {
    }
}
