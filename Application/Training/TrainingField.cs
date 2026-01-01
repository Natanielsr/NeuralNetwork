using Application.Assembly.NeuronFactory;
using Domain.Entities;

namespace Application.Training;

public class TrainingField
{
    private TrainingData trainingData;
    private Neuron[] currentGeneration;

    public TrainingField(TrainingData trainingData)
    {
        this.trainingData = trainingData;

        currentGeneration = [];
    }

    public Neuron[] Train(int generationsCount)
    {
        for (int i = 0; i < generationsCount; i++)
        {
            RunGeneration();
        }

        return currentGeneration;
    }

    private void RunGeneration()
    {
        assemblyGeneration();
        var ranking = Ranking.RankTheBest(trainingData.Inputs, currentGeneration, trainingData.ExpectedOutput);
        currentGeneration = ranking.Select(r => r.Neuron).ToArray();
    }

    private void assemblyGeneration()
    {
        if (currentGeneration.Length == 0)
        {
            currentGeneration = NeuronAssembly.AssembleRandomNeurons(trainingData.Inputs.Length, trainingData.NeuronsSize);
        }
        else
        {
            CreateNewGeneration();
        }
    }

    private void CreateNewGeneration()
    {
        var father = currentGeneration[0];
        var mother = currentGeneration[1];

        var newGeneration = new Neuron[trainingData.NeuronsSize];
        newGeneration[0] = father;
        newGeneration[1] = mother;

        var genData = NeuronGenData.Create(father, mother, trainingData.MutationRate, trainingData.MutationStrength);

        NeuronAssembly assembler = new NeuronAssembly(genData);

        for (int i = 2; i < trainingData.NeuronsSize; i++)
        {
            Neuron child = assembler.GenerateNeuronChild();
            newGeneration[i] = child;
        }

        currentGeneration = newGeneration;
    }
}
