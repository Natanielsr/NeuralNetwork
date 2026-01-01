namespace Domain.Entities;

public class Weight : ValueObject
{
    public Weight(double Value) : base(Value, false)
    { }
    public Weight(double Value, bool HasMutation) : base(Value, HasMutation)
    { }

    public static Weight[] Create(double[] weightsValues)
    {
        return Create(weightsValues, false);
    }

    public static Weight[] Create(double[] weightsValues, bool hasMutation)
    {
        Weight[] weights = new Weight[weightsValues.Length];
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = new Weight(weightsValues[i], hasMutation);
        }

        return weights;
    }
}
