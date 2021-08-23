using CheckItApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace CheckItApp.Services
{
    class CheckItApi
    {
        private HttpClient client;
        private string baseUri;
        private static CheckItApi proxy = null;
        internal static object CreateProxy()
        {
            throw new NotImplementedException();
        }
    }
}
