namespace Application.Assembly.Weight;

public class WeightGenerator
{
    const double _MIN_WEIGHT = -1.0;
    const double _MAX_WEIGHT = 1.0;

    WeightGenData data;

    Random random;

    public WeightGenerator(WeightGenData data)
    {
        this.data = data;
        random = new Random();
    }

    public double[] GenerateWeights()
    {
        if (data.parent1.Length == 0 || data.parent2.Length == 0)
        {
            throw new ArgumentException("parent lenght 0");
        }

        if (data.parent1.Length != data.parent2.Length)
        {
            throw new ArgumentException("parent lenght different");
        }

        if (data.parent1.Length > 1)
        {
            return MutateWeights(divideWeights());
        }
        else
        {
            return MutateWeights(oneWeightChild());
        }
    }

    private double[] oneWeightChild()
    {
        bool resultado = random.NextDouble() < 0.5;

        if (resultado)
        {
            return data.parent1;
        }
        else
        {
            return data.parent2;
        }
    }

    private double[] divideWeights()
    {
        int length = data.parent1.Length;
        int half = length / 2;

        // índices disponíveis
        var indices = Enumerable.Range(0, length).ToList();

        // sorteia metade dos índices
        var indicesFromParent1 = indices
            .OrderBy(_ => random.Next())
            .Take(half)
            .ToHashSet();

        var child = new double[length];

        for (int i = 0; i < length; i++)
        {
            child[i] = indicesFromParent1.Contains(i)
                ? data.parent1[i]
                : data.parent2[i];
        }

        return child;
    }

    private double[] MutateWeights(double[] weights)
    {
        var mutateWeights = new double[weights.Length];
        for (int i = 0; i < weights.Length; i++)
        {
            mutateWeights[i] = MutateWeight(weights[i]);
        }
        return mutateWeights;
    }

    public double MutateWeight(double weight)
    {
        if (random.NextDouble() < data.mutationRate)
        {
            // Valor entre -mutationStrength e +mutationStrength
            double delta = ((random.NextDouble() * 2) - 1) * data.mutationStrength;
            weight += delta;
        }

        return Clamp(weight);
    }

    private double Clamp(double weight)
    {
        if (weight < _MIN_WEIGHT) return _MIN_WEIGHT;
        if (weight > _MAX_WEIGHT) return _MAX_WEIGHT;
        return weight;
    }
}
