using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Lib.Utilities.Http
{
    public interface IHttpClientUtil
    {
        Task<HttpClient> CreateAuthenticatedHttpClient();
        HttpClient CreatePublicHttpClient();
    }
}
