using Usuario.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Services
// =======================
builder.Services
    .AddApiServices(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddJwtAuthentication(builder.Configuration)
    .AddSwaggerDocumentation();

var app = builder.Build();

// =======================
// Pipeline
// =======================
app.UseApplicationPipeline(app.Environment);

app.Run();
