using Domain.Entities;

namespace Domain.Assembly;

public static class NeuronAssembly
{
    /// <summary>
    /// brings together 50% of the father and 50% of the mother to raise the child
    /// </summary>
    /// <param name="parent1">Father.</param>
    /// <param name="parent2">Mother.</param>
    /// <returns>returns the son.</returns>
    public static Neuron GenerateNeuronChild(Neuron parent1, Neuron parent2)
    {
        // juntar 50% do pai e 50% da mãe e criar o filho
        var parent1Weights = parent1.GetWeights();
        var parent2Weights = parent2.GetWeights();

        var data = WeightGenData.Create(parent1Weights, parent2Weights, 0, 0);
        var weightGen = new WeightGenerator(data);
        var childWeights = weightGen.GenerateWeights();

        var childBias = BiasGenerator.CrossoverBias(
            parent1.GetBias(),
            parent2.GetBias());

        var childNeuron = new Neuron(childBias, childWeights);

        return childNeuron;

    }

    public static Neuron[] AssembleRandomNeurons(int inputSize, int numberOfNeurons)
    {
        var neurons = new Neuron[numberOfNeurons];
        for (int i = 0; i < numberOfNeurons; i++)
        {
            var neuron = new Neuron(randomValue(), randomWeights(inputSize));
            neurons[i] = neuron;

        }
        return neurons;
    }

    private static double[] randomWeights(int size)
    {
        var weights = new double[size];
        for (int i = 0; i < size; i++)
        {
            weights[i] = randomValue();
        }
        return weights;
    }

    private static double randomValue()
    {
        var random = new Random();
        return random.NextDouble() * 2.0 - 1.0;
    }
}
