using System.Net;

namespace LibraryApi.Server.Middleware.CustomExceptionHandle
{

    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);

}
