using _4_EfCore.Repositories;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.ClearProviders();// default olarak loglama yap�land�rmam�z var
builder.Logging.AddConsole();
builder.Logging.AddDebug();



// ioc ye db context tan�m� yapt�k - uygulama i�erisindeki farkl� s�n�flar�n DbContext'e ba��ml�l���n� azalt�r
builder.Services.AddDbContext<RepositoryContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("sqlConnection")));



var app = builder.Build();

/*// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/


// amac�m�z Product ortam�nda warning seviyesinde, development ortam�nda information seviyesinde bilgi almak 
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
