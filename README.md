```markdown
Neural Network in c#
```

## Estrutura de pastas do projeto

```
NeuralNetwork.sln
README.md
Application/
  Application.csproj
  Assembly/
    BiasGen/
      BiasGenData.cs
      BiasGenerator.cs
    NeuronFactory/
      NeuronAssembly.cs
      NeuronGenData.cs
    WeightGen/
      WeightGenData.cs
      WeightGenerator.cs
  Generation/
  Training/
    Rank.cs
    Ranking.cs
    TrainingData.cs
    TrainingField.cs
Domain/
  Domain.csproj
  Main.cs
  Entities/
    Bias.cs
    Connection.cs
    Input.cs
    Neuron.cs
    Node.cs
    Output.cs
    ValueObject.cs
    Weight.cs
  Exceptions/
    InvalidPercentageException.cs
    InvalidValueException.cs
    NeuronZeroInputConnectionsException.cs
    NullNeuronInputConnectionReference.cs
  Utils/
    FormatNeuron.cs
    ValidatorDomain.cs
Tests/
  Tests.csproj
  GlobalUsings.cs
  ActivationTest.cs
  BiasGenerationTest.cs
  MainTest.cs
  NeuronAssemblyTest.cs
  NeuronTest.cs
  TraningTest.cs
  WeightGeneratorTest.cs
  NetworkSetup/
    NetworkAssembly.cs

```
Neural Network in c#