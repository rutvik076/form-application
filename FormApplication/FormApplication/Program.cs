using FormApplication.Repositories;
using FormApplication.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Azure.Cosmos;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Load Cosmos DB settings from appsettings.json
var cosmosDbConfig = builder.Configuration.GetSection("CosmosDbSettings");
var cosmosClient = new CosmosClient(cosmosDbConfig["EndPointUri"], cosmosDbConfig["PrimaryKey"]);

try
{
    var database = await cosmosClient.CreateDatabaseIfNotExistsAsync(cosmosDbConfig["DatabaseName"]);
    Console.WriteLine($"Connected to Cosmos DB. Database Id: {database.Database.Id}");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to connect to Cosmos DB: {ex.Message}");
}

// Register CosmosClient as Singleton
builder.Services.AddSingleton(cosmosClient);

// Register repositories
builder.Services.AddSingleton<IQuestionRepository>(provider =>
    new QuestionRepository(
        cosmosClient,
        cosmosDbConfig["DatabaseName"],
        cosmosDbConfig["QuestionsContainer"]
    )
);

builder.Services.AddSingleton<IApplicationRepository>(provider =>
    new ApplicationRepository(
        cosmosClient,
        cosmosDbConfig["DatabaseName"],
        cosmosDbConfig["ApplicationsContainer"]
    )
);

// Register services
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
