using Domain.Entities;

namespace Application.Assembly.WeightGen;

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

    public Weight[] GenerateWeights()
    {
        if (data.parentWeights1.Length == 0 || data.parentWeights1.Length == 0)
        {
            throw new ArgumentException("parent weights lenght 0");
        }

        if (data.parentWeights1.Length != data.parentWeights1.Length)
        {
            throw new ArgumentException("parent weights lenght different");
        }

        if (data.parentWeights1.Length > 1)
        {
            return MutateWeights(divideWeights());
        }
        else
        {
            return MutateWeights(oneWeightChild());
        }
    }

    private Weight[] oneWeightChild()
    {
        bool resultado = random.NextDouble() < 0.5;

        if (resultado)
        {
            return data.parentWeights1;
        }
        else
        {
            return data.parentWeights2;
        }
    }

    private Weight[] divideWeights()
    {
        int length = data.parentWeights1.Length;
        int half = length / 2;

        // índices disponíveis
        var indices = Enumerable.Range(0, length).ToList();

        // sorteia metade dos índices
        var indicesFromParent1 = indices
            .OrderBy(_ => random.Next())
            .Take(half)
            .ToHashSet();

        Weight[] childWeights = new Weight[length];

        for (int i = 0; i < length; i++)
        {
            childWeights[i] = indicesFromParent1.Contains(i)
                ? data.parentWeights1[i]
                : data.parentWeights2[i];
        }

        return childWeights;
    }

    private Weight[] MutateWeights(Weight[] weights)
    {
        Weight[] mutateWeights = new Weight[weights.Length];
        for (int i = 0; i < weights.Length; i++)
        {
            mutateWeights[i] = MutateWeight(weights[i]);
        }
        return mutateWeights;
    }

    public Weight MutateWeight(Weight weight)
    {
        double weightValue = weight.Value;
        Weight newWeight;
        bool hasMutation = false;
        if (random.NextDouble() < data.mutationRate)
        {
            // Valor entre -mutationStrength e +mutationStrength
            double delta = ((random.NextDouble() * 2) - 1) * data.mutationStrength;
            weightValue += delta;
            hasMutation = true;
        }

        double clampedValue = Clamp(weightValue);

        newWeight = new Weight(clampedValue, hasMutation);

        return newWeight;

    }

    private double Clamp(double weight)
    {
        if (weight < _MIN_WEIGHT) return _MIN_WEIGHT;
        if (weight > _MAX_WEIGHT) return _MAX_WEIGHT;
        return weight;
    }
}
