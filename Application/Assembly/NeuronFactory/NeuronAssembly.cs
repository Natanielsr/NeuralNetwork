using Application.Assembly.Bias;
using Application.Assembly.Weight;
using Domain.Entities;

namespace Application.Assembly.NeuronFactory;

public static class NeuronAssembly
{
    /// <summary>
    /// brings together 50% of the father and 50% of the mother to raise the child
    /// with mutation
    /// </summary>
    /// <param name="parent1">Father.</param>
    /// <param name="parent2">Mother.</param>
    /// <returns>returns the son.</returns>
    public static Neuron GenerateNeuronChild(NeuronGenData genData)
    {
        // juntar 50% do pai e 50% da mãe e criar o filho
        var childWeights = generateWeights(genData);
        var childBias = generateBias(genData);

        var childNeuron = new Neuron(childBias, childWeights);

        return childNeuron;

    }

    private static double generateBias(NeuronGenData genData)
    {
        var biasGenData = BiasGenData.Create(
            genData.parent1.GetBias(),
            genData.parent2.GetBias(),
            genData.mutationRate,
            genData.mutationStrength
        );

        var biasGen = new BiasGenerator(biasGenData);
        var childBias = biasGen.CrossoverBias();

        return childBias;
    }

    private static double[] generateWeights(NeuronGenData genData)
    {
        var parent1Weights = genData.parent1.GetWeights();
        var parent2Weights = genData.parent2.GetWeights();

        var weightGenData = WeightGenData.Create(
            parent1Weights,
            parent2Weights,
            genData.mutationRate,
            genData.mutationStrength);

        var weightGen = new WeightGenerator(weightGenData);
        return weightGen.GenerateWeights();
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
