using System.Text.Json.Serialization;
using ToDo_Api.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddOptions();

builder.AddSwaggerConfiguration();
builder.AddDatabase();
builder.AddCorsConfiguration();
builder.AddLocalServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/0.0.1/swagger.json", "ToDo API");
});
app.UseRouting();
app.MapControllers();
app.Run();