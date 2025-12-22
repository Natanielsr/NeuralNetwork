using Domain.Utils;

namespace Application.Assembly.Weight;

public record class WeightGenData
{
    public double[] parent1 { get; private set; }
    public double[] parent2 { get; private set; }
    public double mutationRate { get; private set; }
    public double mutationStrength { get; private set; }

    public WeightGenData(
        double[] parent1,
        double[] parent2,
        double mutationRate,
        double mutationStrength
        )
    {
        if (parent1 == null || parent2 == null)
        {
            throw new NullReferenceException("parent1 and parent2 cant be null");
        }

        if (parent1.Length <= 0 || parent2.Length <= 0)
        {
            throw new InvalidDataException("parent1 and parent2 lenght cant be bigger than 0 ");
        }

        for (int i = 0; i < parent1.Length; i++)
        {
            ValidatorDomain.ValidateValue(parent1[i]);
            ValidatorDomain.ValidateValue(parent2[i]);
        }
        ValidatorDomain.ValidatePercentage(mutationRate);
        ValidatorDomain.ValidatePercentage(mutationStrength);

        this.parent1 = parent1;
        this.parent2 = parent2;
        this.mutationRate = mutationRate;
        this.mutationStrength = mutationStrength;
    }

    public static WeightGenData Create(
        double[] parent1,
        double[] parent2,
        double mutationRate,
        double mutationStrength)
    {
        return new WeightGenData(
            parent1,
            parent2,
            mutationRate,
            mutationStrength
        );
    }
}
