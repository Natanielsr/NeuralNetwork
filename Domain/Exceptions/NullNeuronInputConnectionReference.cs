using System;
using Domain.Entities;

namespace Domain.Exceptions;

public class NullNeuronInputConnectionReference : Exception
{
    public NullNeuronInputConnectionReference(Guid NeuronId, int inputPosition)
        : base($"The input connection of the neuron '{NeuronId}' at position {inputPosition} is null.")
    {
    }
}
