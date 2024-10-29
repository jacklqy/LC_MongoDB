using MongoDBLib;

var builder = WebApplication.CreateBuilder(args);

// »ñÈ¡ÅäÖÃ
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();

//builder.Services.AddMongoDBService(options =>
//{
//    options.ConnHost = configuration["MongoDB:ConnHost"];
//    options.ConnPort = Convert.ToInt32(configuration["MongoDB:ConnPort"]);
//    options.UserName = configuration["MongoDB:UserName"];
//    options.Password = configuration["MongoDB:Password"];
//});

builder.Services.AddMongoDBService(options =>
{
    options.ConnectionString = configuration["MongoDB:ConnectionString"];
    options.Database = configuration["MongoDB:Database"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
