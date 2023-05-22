namespace Api.Installers
{
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using System.Collections.Generic;
    using Util.Common;

    public class RequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = EHeaders.CodeClient.ToString(),
                In = ParameterLocation.Header,
                Required = false,
                Example = new OpenApiString("2a74a3ff-b1fb-4aee-940e-9db623a842a8")
            });
        }
    }
}


