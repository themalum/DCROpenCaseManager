using OpenCaseManager.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace OpenCaseManager.Managers
{
    public class Service : IService
    {
        public Service()
        {

        }

        public RestRequest GetRequest(ServiceModel model)
        {
            var request = new RestRequest
            {
                Resource = model.Url,
                Method = model.MethodType
            };
            return request;
        }

        /// <summary>
        /// Call to a rest service
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IRestResponse GetResponse(ServiceModel model, RestRequest request)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(model.BaseUrl),
                Authenticator = new HttpBasicAuthenticator(model.Username, model.Password)
            };

            IRestResponse response = client.Execute(request);
            if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(response.Content);
            }

            return response;
        }

        /// <summary>
        /// Call to a rest service
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IRestResponse GetNodeJSServiceResponse(ServiceModel input)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(input.BaseUrl),
            };

            var request = new RestRequest
            {
                Resource = input.Url,
                Method = input.MethodType
            };

            request.AddHeader("Accept", "application/x-www-form-urlencoded");
            request.Parameters.Clear();
            request.AddParameter("application/json", input.Body, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);
            if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(response.Content);
            }
            else if (response.StatusCode == 0)
            {
                throw new Exception("Please configure word node.js service.");
            }

            return response;
        }
    }
}