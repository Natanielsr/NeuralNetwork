using Domain.Entities;

namespace Application.Assembly.BiasGen;

public class BiasGenerator
{
    const double _MIN_BIAS = -1.0;
    const double _MAX_BIAS = 1.0;

    BiasGenData biasData;

    public BiasGenerator(BiasGenData biasData)
    {
        this.biasData = biasData;
    }

    public Bias CrossoverBias()
    {
        var rng = new Random();

        Bias bias;
        double biasValue;
        bool hasMutation = false;

        // crossover 50%
        var result = rng.Next(2) == 0;
        if (result)
            biasValue = biasData.fatherBias.Value;
        else
            biasValue = biasData.motherBias.Value;

        // mutation
        if (rng.NextDouble() < biasData.mutationRate)
        {
            double noise = (rng.NextDouble() * 2.0 - 1.0) * biasData.mutationStrength;
            biasValue += noise;
            hasMutation = true;
        }

        double clampedValue = Clamp(biasValue, _MIN_BIAS, _MAX_BIAS);
        bias = new(clampedValue, hasMutation);

        return bias;
    }

    static double Clamp(double value, double min, double max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}
