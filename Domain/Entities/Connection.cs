namespace Domain.Entities;

public class Connection
{
    public Node InputNode { get; private set; }
    public Node OutputNode { get; private set; }

    public Connection(Node input, Node output)
    {
        InputNode = input;
        OutputNode = output;
    }
}
