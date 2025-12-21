namespace Domain.Entities;

public class Input : Node
{
    public Input(double value) : base(value)
    {
    }

    public static Input CreateInput(double value)
    {
        return new Input(value);
    }

    public static Input[] CreateInputs(params double[] values)
    {
        var inputs = new Input[values.Length];
        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i] = new Input(values[i]);
        }

        return inputs;
    }
}
