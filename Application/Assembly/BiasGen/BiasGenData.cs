using Domain.Entities;
using Domain.Utils;

namespace Application.Assembly.BiasGen;

public record class BiasGenData
{
    public Bias fatherBias { get; private set; }
    public Bias motherBias { get; private set; }
    public double mutationRate { get; private set; }
    public double mutationStrength { get; private set; }

    public BiasGenData(
        Bias fatherBias,
        Bias motherBias,
        double mutationRate,
        double mutationStrength
        )
    {
        ValidatorDomain.ValidatePercentage(mutationRate);
        ValidatorDomain.ValidatePercentage(mutationStrength);

        this.fatherBias = fatherBias;
        this.motherBias = motherBias;
        this.mutationRate = mutationRate;
        this.mutationStrength = mutationStrength;
    }

    public static BiasGenData Create(
        Bias fatherBias,
        Bias motherBias,
        double mutationRate,
        double mutationStrength)
    {
        return new BiasGenData(
            fatherBias,
            motherBias,
            mutationRate,
            mutationStrength
        );
    }

    public static BiasGenData Create(
        double fatherBias,
        double motherBias,
        double mutationRate,
        double mutationStrength)
    {
        return new BiasGenData(
            Bias.Create(fatherBias),
            Bias.Create(motherBias),
            mutationRate,
            mutationStrength
        );
    }
}
