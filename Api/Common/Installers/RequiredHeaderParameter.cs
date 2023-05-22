namespace Api.Installers
{
    using System.Collections.Generic;
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Util.Common;

    public class RequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<OpenApiParameter>();

            //operation.Parameters.Add(new OpenApiParameter()
            //{
            //    Name = EHeaders.DeviceMobile.ToString(),
            //    In = ParameterLocation.Header,
            //    Required = false,
            //    Example = new OpenApiString("82d66876-52c6-4e35-9b45-bce4cb61fde4")
            //});

            //operation.Parameters.Add(new OpenApiParameter()
            //{
            //    Name = EHeaders.CodeClient.ToString(),
            //    In = ParameterLocation.Header,
            //    Required = false,
            //    Example = new OpenApiString("e03b7ab7")
            //});
        }
    }
}


