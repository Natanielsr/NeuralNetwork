namespace Domain.Exceptions;

public class NeuronZeroInputConnectionsException : Exception
{
    public NeuronZeroInputConnectionsException(Guid NeuronId)
        : base($"Neuron '{NeuronId}' with zero input connections")
    {
    }
}
