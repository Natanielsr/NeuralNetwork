using Domain.Entities;

namespace Application.Training;

public record Rank
{
    public Neuron Neuron { get; private set; }
    public double Pontuation { get; private set; }

    public Rank(Neuron neuron, double pontuation)
    {
        Neuron = neuron;
        Pontuation = pontuation;
    }
}
