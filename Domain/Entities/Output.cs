namespace Domain.Entities;

public class Output : Node
{
    public Output(double value) : base(value)
    {
    }

    public static Output CreateOutput(double value)
    {
        return new Output(value);
    }
}
