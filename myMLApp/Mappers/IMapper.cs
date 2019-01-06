using Microsoft.ML.Runtime.Data;

using myMLApp.DataStructures.Input;

namespace myMLApp.Mappers
{
    public interface IMapper
    {
        TextLoader.Arguments Map<TSrc>(TSrc data);
    }
}
