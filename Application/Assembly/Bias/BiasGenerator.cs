namespace Application.Assembly.Bias;

public class BiasGenerator
{
    const double _MUTATION_RATE = 0;
    const double _MUTATION_STRENGTH = 0.05;
    const double _MIN_BIAS = -1.0;
    const double _MAX_BIAS = 1.0;

    public static double CrossoverBias(
    double fatherBias,
    double motherBias)
    {
        var rng = new Random();

        // crossover (média)
        double bias = (fatherBias + motherBias) / 2.0;

        // mutation
        if (rng.NextDouble() < _MUTATION_RATE)
        {
            double noise = (rng.NextDouble() * 2.0 - 1.0) * _MUTATION_STRENGTH;
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
