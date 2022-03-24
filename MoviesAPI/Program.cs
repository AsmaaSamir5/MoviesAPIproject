using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(name: "v1", info: new OpenApiInfo 
    {
        Version = "v1",
        Title = "Asmaa First API",
        Description = "API Web Application For Movies",
        TermsOfService = new Uri(uriString: "https://www.google.com"),
        Contact = new OpenApiContact
        {
            Name = "Asmaa Samir",
            Email = "asmaa.samir.elgharib@gmail.com",
            Url = new Uri(uriString: "https://www.google.com")
        },
        License = new OpenApiLicense
        {
            Name = "my licence",
            Url = new Uri(uriString: "https://www.google.com")

        }
    });
    options.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
    });
    options.AddSecurityRequirement(securityRequirement: new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader().AllowAnyMethode().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
