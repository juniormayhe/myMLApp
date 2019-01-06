using Microsoft.ML.Runtime.Data;

using myMLApp.DataStructures.Input;

namespace myMLApp.Mappers
{
    public class AdequacyLevelMapper : IMapper
    {
        public TextLoader.Arguments Map<TSrc>(TSrc inputData)
        {
            AdequacyLevelData data = inputData as AdequacyLevelData;
            {
                var args = new TextLoader.Arguments()
                {
                    Separator = ",",
                    HasHeader = true,
                    Column = new[]
                        {
                        new TextLoader.Column(nameof(data.YearsInAgile), DataKind.R4, 0),
                        new TextLoader.Column(nameof(data.YearsInNET), DataKind.R4, 1),
                        new TextLoader.Column(nameof(data.YearsInSQL), DataKind.R4, 2),
                        new TextLoader.Column(nameof(data.AdequacyLevelInAgile), DataKind.R4, 3),
                        new TextLoader.Column(nameof(data.AdequacyLevelInNET), DataKind.R4, 4),
                        new TextLoader.Column(nameof(data.AdequacyLevelInSQL), DataKind.R4, 5),
                        new TextLoader.Column(nameof(data.Label), DataKind.Text, 6)
                    }
                };
                return args;
            }
        }
    }
}
