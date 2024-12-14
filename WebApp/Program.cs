using Domain.Entities;
using Infratructure.DataContext;
using Infratructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IContext,DapperContext>();
builder.Services.AddScoped<IGenericService<Customer>, CustomerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "BankManagment"));
}

app.MapControllers();
app.Run();

