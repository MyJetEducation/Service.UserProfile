using System.ServiceModel;
using System.Threading.Tasks;
using Service.UserProfile.Grpc.Models;

namespace Service.UserProfile.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}