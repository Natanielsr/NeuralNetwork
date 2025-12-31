namespace Application.Assembly.Bias;

public class BiasGenerator
{
    const double _MIN_BIAS = -1.0;
    const double _MAX_BIAS = 1.0;

    BiasGenData biasData;

    public BiasGenerator(BiasGenData biasData)
    {
        this.biasData = biasData;
    }

    public double CrossoverBias()
    {
        var rng = new Random();

        double bias;

        // crossover 50%
        var result = rng.Next(2) == 0;
        if (result)
            bias = biasData.fatherBias;
        else
            bias = biasData.motherBias;

        // mutation
        if (rng.NextDouble() < biasData.mutationRate)
        {
            double noise = (rng.NextDouble() * 2.0 - 1.0) * biasData.mutationStrength;
            bias += noise;
        }

        // clamp
        return Clamp(bias, _MIN_BIAS, _MAX_BIAS);
    }

    static double Clamp(double value, double min, double max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}
