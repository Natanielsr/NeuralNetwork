using Domain.Assembly;
using Domain.Entities;

namespace Application.Training;

public class TrainingField
{
    private Input[] inputs;
    private int neuronsSize;
    private Output expectedOutput;

    private Neuron[] currentGeneration;

    public TrainingField(
        Input[] inputs,
        int neuronsSize,
        Output expectedOutput)
    {
        if (neuronsSize < 3)
        {
            throw new Exception();
        }

        this.inputs = inputs;
        this.neuronsSize = neuronsSize;
        this.expectedOutput = expectedOutput;
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
        var ranking = Ranking.RankTheBest(inputs, currentGeneration, expectedOutput);
        currentGeneration = ranking.Select(r => r.Neuron).ToArray();
    }

    private void assemblyGeneration()
    {
        if (currentGeneration.Length == 0)
        {
            currentGeneration = NeuronAssembly.AssembleRandomNeurons(inputs.Length, neuronsSize);
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

        var newGeneration = new Neuron[neuronsSize];
        newGeneration[0] = father;
        newGeneration[1] = mother;

        for (int i = 2; i < neuronsSize; i++)
        {
            var child = NeuronAssembly.GenerateNeuronChild(father, mother);
            newGeneration[i] = child;
        }

        currentGeneration = newGeneration;
    }
}
