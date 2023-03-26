using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AmazonDynamoDB.Api.Middlewares;

public class CustomApiErrorExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomApiErrorExceptionMiddleware> _logger;

    public CustomApiErrorExceptionMiddleware(RequestDelegate next, ILogger<CustomApiErrorExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            //var obj = Result.Fail<string>(error);

            //switch (error)
            //{
            //    case ApiException e:
            //        // custom application error
            //        response.StatusCode = (int)HttpStatusCode.BadRequest;
            //        break;
            //    case ValidationException e:
            //        // custom application error
            //        response.StatusCode = (int)HttpStatusCode.BadRequest;
            //        //responseModel.Errors = e.Errors;
            //        break;
            //    case KeyNotFoundException e:
            //    case NoDataProvidedException ndpex:
            //        // not found error
            //        response.StatusCode = (int)HttpStatusCode.NotFound;
            //        break;
            //    default:
            //        // unhandled error
            //        response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        break;
            //}

            //string result = JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            string result = JsonConvert.SerializeObject(new { error = error.Message }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            await response.WriteAsync(result);
        }
    }
}
