using Domain.Entities;

namespace Domain.Utils;

public static class FormatNeuron
{
    public static string ToString(Neuron neuron)
    {
        var parts = new List<string>();
        var i = 1;
        foreach (var weight in neuron.GetWeights())
        {
            var weightStr = $"w{i}: {weight}";

            parts.Add($"{{ {weight} }}");

            i++;
        }
        return $"B:{neuron.GetBias()} W: [" + string.Join(", ", parts) + "]";
    }
}
