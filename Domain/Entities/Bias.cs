namespace Domain.Entities;

public class Bias : ValueObject
{
    public Bias(double Value, bool HasMutation) : base(Value, HasMutation)
    {
    }

    public static Bias Create(double value)
    {
        return new Bias(value, false);
    }
    public static Bias Create(double value, bool hasMutation)
    {
        return new Bias(value, hasMutation);
    }
}
