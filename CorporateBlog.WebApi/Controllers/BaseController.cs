using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CorporateBlog.WebApi.Controllers
{
    public abstract class BaseController:ApiController
    {
        protected class ForbiddenResult : IHttpActionResult
        {
            private readonly HttpRequestMessage _request;
            private readonly string _reason;

            public ForbiddenResult(HttpRequestMessage request, string reason)
            {
                _request = request;
                _reason = reason;
            }

            public ForbiddenResult(HttpRequestMessage request)
            {
                _request = request;
                _reason = "Forbidden";
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = _request.CreateResponse(HttpStatusCode.Forbidden, _reason);
                return Task.FromResult(response);
            }
        }

        protected virtual ForbiddenResult Forbidden(string message)
        {
            return new ForbiddenResult(new HttpRequestMessage(), message);
        }
    }
}