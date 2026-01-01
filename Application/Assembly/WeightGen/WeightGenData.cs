using Domain.Entities;
using Domain.Utils;

namespace Application.Assembly.WeightGen;

public record class WeightGenData
{
    public Weight[] parentWeights1 { get; private set; }
    public Weight[] parentWeights2 { get; private set; }
    public double mutationRate { get; private set; }
    public double mutationStrength { get; private set; }

    public WeightGenData(
        Weight[] parentWeights1,
        Weight[] parentWeights2,
        double mutationRate,
        double mutationStrength
        )
    {
        if (parentWeights1 == null || parentWeights2 == null)
        {
            throw new NullReferenceException("parent1 and parent2 cant be null");
        }

        if (parentWeights1.Length <= 0 || parentWeights2.Length <= 0)
        {
            throw new InvalidDataException("parent1 and parent2 lenght cant be bigger than 0 ");
        }


        ValidatorDomain.ValidatePercentage(mutationRate);
        ValidatorDomain.ValidatePercentage(mutationStrength);

        this.parentWeights1 = parentWeights1;
        this.parentWeights2 = parentWeights2;
        this.mutationRate = mutationRate;
        this.mutationStrength = mutationStrength;
    }

    public static WeightGenData Create(
        Weight[] parentWeights1,
        Weight[] parentWeights2,
        double mutationRate,
        double mutationStrength)
    {
        return new WeightGenData(
            parentWeights1,
            parentWeights2,
            mutationRate,
            mutationStrength
        );
    }

    public static WeightGenData Create(
        double[] parentWeightValues1,
        double[] parentWeightValues2,
        double mutationRate,
        double mutationStrength)
    {
        return new WeightGenData(
            Weight.Create(parentWeightValues1),
            Weight.Create(parentWeightValues2),
            mutationRate,
            mutationStrength
        );
    }
}
