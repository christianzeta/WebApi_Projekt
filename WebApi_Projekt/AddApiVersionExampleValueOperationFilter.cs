using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Versioning_With_Url.Swagger
{
    public class AddApiVersionExampleValueOperationFilter : IOperationFilter
    {
        // byt ut denna strängen mot vad du kallar din header/queryparameter
        private const string ApiVersionParameter = "v";
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiVersionParameter = operation.Parameters
                .SingleOrDefault(p => p.Name == ApiVersionParameter);
            if (apiVersionParameter == null)
            {
                throw new Exception($"\"{ApiVersionParameter}\" header or query parameter missing.");
            }

            // plocka ut [ApiVersion("'v'VVV")] attributet (reflection)
            var attribute = context?.MethodInfo?.DeclaringType?
              .GetCustomAttributes(typeof(ApiVersionAttribute), false)
              .Cast<ApiVersionAttribute>()
              .SingleOrDefault();
            // hämta ut verisionsvärdet som en sträng
            var version = attribute?.Versions?.SingleOrDefault()?.ToString();

            // Om det fanns en actionmetod med ett [ApiVersion()] attribut
            if (version != null)
            {
                apiVersionParameter.Example = new OpenApiString(version);
            }
        }
    }
}
