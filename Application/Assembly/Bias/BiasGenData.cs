using Domain.Utils;

namespace Application.Assembly.Bias;

public record class BiasGenData
{
    public double fatherBias { get; private set; }
    public double motherBias { get; private set; }
    public double mutationRate { get; private set; }
    public double mutationStrength { get; private set; }

    public BiasGenData(
        double fatherBias,
        double motherBias,
        double mutationRate,
        double mutationStrength
        )
    {

        ValidatorDomain.ValidateValue(fatherBias);
        ValidatorDomain.ValidateValue(motherBias);
        ValidatorDomain.ValidatePercentage(mutationRate);
        ValidatorDomain.ValidatePercentage(mutationStrength);

        this.fatherBias = fatherBias;
        this.motherBias = motherBias;
        this.mutationRate = mutationRate;
        this.mutationStrength = mutationStrength;
    }

    public static BiasGenData Create(
        double fatherBias,
        double motherBias,
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
}
