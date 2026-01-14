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
            parts.Add($"{weight.Value}");

            i++;
        }
        return $"M:{(neuron.HasMutation() ? "True" : "False")} B:{neuron.GetBias().Value} W:[" + string.Join(", ", parts) + "]";
    }
}
