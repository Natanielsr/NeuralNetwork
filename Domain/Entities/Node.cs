using Domain.Utils;

namespace Domain.Entities;

public abstract class Node
{
    public Guid Id { get; private set; }
    public List<Node> Outputs { get; private set; }
    public double Value { get; private set; }

    public Node()
    {
        Id = Guid.NewGuid();
    }

    public Node(double value)
    {
        Id = Guid.NewGuid();
        ValidatorDomain.ValidateValue(value);
        this.Value = value;
    }

    protected void SetValue(double value)
    {
        ValidatorDomain.ValidateValue(value);
        this.Value = value;
    }
}
