using Domain.Entities;

namespace Application.Training;

public record class TrainingData
{
    public Input[] Inputs { get; private set; }
    public int NeuronsSize { get; private set; }
    public Output ExpectedOutput { get; private set; }
    public double MutationRate { get; private set; }
    public double MutationStrength { get; private set; }

    public TrainingData(
        Input[] inputs,
        int neuronsSize,
        Output expectedOutput,
        double mutationRate,
        double mutationStrength
        )
    {
        if (neuronsSize < 3)
        {
            throw new Exception();
        }


        Inputs = inputs;
        NeuronsSize = neuronsSize;
        ExpectedOutput = expectedOutput;
        MutationRate = mutationRate;
        MutationStrength = mutationStrength;
    }

    public static TrainingData Create(
        Input[] inputs,
        int neuronsSize,
        Output expectedOutput,
        double mutationRate,
        double mutationStrength)
    {
        return new TrainingData(
            inputs,
            neuronsSize,
            expectedOutput,
            mutationRate,
            mutationStrength);
    }
}
