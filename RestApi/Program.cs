using Microsoft.EntityFrameworkCore;
using Persistence.orm;
using Persistence.orm.repos;

const string CORS_POLICY = "cors_policy";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy(CORS_POLICY, builder =>
    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

builder.Services.AddDbContext<AgenciesContext>(options => options
    .UseSqlServer(connectionString: System.Configuration.ConfigurationManager.AppSettings["connectionString"]));

builder.Services.AddScoped<TripDbOrmRepo>();

var app = builder.Build();
app.UseCors(CORS_POLICY);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
