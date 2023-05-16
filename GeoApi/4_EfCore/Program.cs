using _4_EfCore.Repositories;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.ClearProviders();// default olarak loglama yapýlandýrmamýz var
builder.Logging.AddConsole();
builder.Logging.AddDebug();



// ioc ye db context tanýmý yaptýk - uygulama içerisindeki farklý sýnýflarýn DbContext'e baðýmlýlýðýný azaltýr
builder.Services.AddDbContext<RepositoryContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("sqlConnection")));



var app = builder.Build();

/*// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/


// amacýmýz Product ortamýnda warning seviyesinde, development ortamýnda information seviyesinde bilgi almak 
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
