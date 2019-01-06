using Microsoft.ML.Runtime.Data;

using myMLApp.DataStructures.Input;

namespace myMLApp.Mappers
{
    public class FlowerTypeMapper : IMapper
    {
        public TextLoader.Arguments Map<TSrc>(TSrc inputData)
        {
            FlowerTypeData data = inputData as FlowerTypeData;
            var args = new TextLoader.Arguments()
            {
                Separator = ",",
                HasHeader = true,
                Column = new[]
                    {
                        new TextLoader.Column(nameof(data.SepalLength), DataKind.R4, 0),
                        new TextLoader.Column(nameof(data.SepalWidth), DataKind.R4, 1),
                        new TextLoader.Column(nameof(data.PetalLength), DataKind.R4, 2),
                        new TextLoader.Column(nameof(data.PetalWidth), DataKind.R4, 3),
                        new TextLoader.Column(nameof(data.Label), DataKind.Text, 4)
                    }
            };
            return args;
        }
    }
}
