using Domain.Exceptions;

namespace Domain.Utils;

public static class ValidatorDomain
{
    public static void ValidateValues(double[] values)
    {
        foreach (var value in values)
            ValidateValue(value);
    }
    public static void ValidateValue(double value)
    {
        if (value < -1.0 || value > 1.0)
            throw new InvalidValueException();
    }

    public static void ValidatePercentage(double value)
    {
        if (value < 0.0 || value > 1.0)
            throw new InvalidPercentageException();
    }
}
