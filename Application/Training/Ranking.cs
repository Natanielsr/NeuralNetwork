using Domain.Entities;

namespace Application.Training;

public static class Ranking
{
    public static List<Rank> RankTheBest(Input[] inputs, Neuron[] neurons, Output expectedOutput)
    {

        var classification = new List<Rank>();

        foreach (var neuron in neurons)
        {
            var output = neuron.Activation(inputs);
            var pontuation = expectedOutput.Value - output.Value;

            var rank = new Rank(neuron, pontuation);

            classification.Add(rank);
        }

        // ordena pelos mais próximos de zero
        var ordered = classification
            .OrderBy(x => Math.Abs(x.Pontuation))
            .ToList();

        return ordered;
    }
}
