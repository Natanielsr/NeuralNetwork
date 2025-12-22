using Domain.Entities;
using Domain.Utils;

namespace Application.Assembly.NeuronFactory;

public record class NeuronGenData
{
    public Neuron parent1 { get; private set; }
    public Neuron parent2 { get; private set; }
    public double mutationRate { get; private set; }
    public double mutationStrength { get; private set; }

    public NeuronGenData(
        Neuron parent1,
        Neuron parent2,
        double mutationRate,
        double mutationStrength
        )
    {
        if (parent1 == null || parent2 == null)
        {
            throw new NullReferenceException("parent1 and parent2 cant be null");
        }

        ValidatorDomain.ValidatePercentage(mutationRate);
        ValidatorDomain.ValidatePercentage(mutationStrength);

        this.parent1 = parent1;
        this.parent2 = parent2;
        this.mutationRate = mutationRate;
        this.mutationStrength = mutationStrength;
    }

    public static NeuronGenData Create(
        Neuron parent1,
        Neuron parent2,
        double mutationRate,
        double mutationStrength)
    {
        return new NeuronGenData(
            parent1,
            parent2,
            mutationRate,
            mutationStrength
        );
    }
}
