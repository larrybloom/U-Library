using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Library.API.Extensions;

public static class SwaggerConfiguration
{
    public static void AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(setupAction =>
        {
            setupAction.SwaggerDoc("LibraryAPI", new OpenApiInfo
            {
                Title = "Library API",
                Version = "1",
                Description = ""
            });

            setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            });
            setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
            setupAction.OperationFilter<FileUploadOperation>();
        });
    }
}

public class FileUploadOperation : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.OperationId == "MyOperation")
        {
            operation.Parameters.Clear();
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "uploadedFile",
                In = ParameterLocation.Header,
                Description = "Upload File",
                Required = true,
                Schema = new OpenApiSchema
                {
                    Type = "file",
                    Format = "binary"
                }
            });
            var uploadFileMediaType = new OpenApiMediaType()
            {
                Schema = new OpenApiSchema()
                {
                    Type = "object",
                    Properties =
                    {
                        ["uploadedFile"] = new OpenApiSchema()
                        {
                            Description = "Upload File",
                            Type = "file",
                            Format = "binary"
                        }
                    },
                    Required = new HashSet<string>()
                    {
                        "uploadedFile"
                    }
                }
            };
            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    ["multipart/form-data"] = uploadFileMediaType
                }
            };
        }
    }
}