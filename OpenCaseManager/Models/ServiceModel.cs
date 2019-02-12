using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCaseManager.Models
{
    public class ServiceModel
    {
        public string BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public Method MethodType { get; set; }
        public dynamic Body { get; set; }
    }
}