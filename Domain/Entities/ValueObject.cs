using Domain.Utils;

namespace Domain.Entities;

public abstract class ValueObject
{
    public double Value { get; private set; }
    public bool HasMutation { get; private set; }

    private const double Epsilon = 1e-9;

    public ValueObject(double Value, bool HasMutation)
    {
        ValidatorDomain.ValidateValue(Value);
        this.Value = Value;
        this.HasMutation = HasMutation;
    }

    public bool NotEqual(ValueObject obj)
    {
        return !Equals(obj);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ValueObject otherValue)
            return false;

        return Math.Abs(Value - otherValue.Value) < Epsilon;
    }

    public override int GetHashCode()
    {
        return Math.Round(Value / Epsilon).GetHashCode();
    }
}