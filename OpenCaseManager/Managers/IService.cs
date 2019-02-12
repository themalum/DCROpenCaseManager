using OpenCaseManager.Models;
using RestSharp;

namespace OpenCaseManager.Managers
{
    public interface IService
    {
        RestRequest GetRequest(ServiceModel input);
        IRestResponse GetResponse(ServiceModel model, RestRequest request);
        IRestResponse GetNodeJSServiceResponse(ServiceModel input);
    }
}
