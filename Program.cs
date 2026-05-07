
using MyApi;
using MyApi.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IProducts, Products>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHttpClient<IProducts, Products>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ThirdPartyApi:BaseUrl"]!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.MapControllers(); 

app.Run();

