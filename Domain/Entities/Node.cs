using Domain.Exceptions;
using Domain.Utils;

namespace Domain.Entities;

public abstract class Node
{
    public Guid Id { get; private set; }
    public List<Connection> InputConnections { get; private set; } = new List<Connection>();
    public List<Connection> OutputConnections { get; private set; } = new List<Connection>();
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


    public Node AddConnection(Node nextNode)
    {
        var conn = new Connection(this, nextNode);
        OutputConnections.Add(conn);
        nextNode.InputConnections.Add(conn);

        return this;
    }


}
