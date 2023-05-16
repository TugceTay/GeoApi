using _5_LayeredArchitecture.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Repositories.EfCore;
using NetTopologySuite;
using NetTopologySuite.IO;
using NetTopologySuite.IO.Converters;

var builder = WebApplication.CreateBuilder(args);

/*
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));*/


//cors
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyMethod().AllowAnyHeader()));



builder.Services.AddControllers()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly) 
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
        options.SerializerSettings.Converters.Add(new GeometryConverter(geometryFactory));
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.ClearProviders();// default olarak loglama yapýlandýrmamýz var
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.ConfigureSqlContext(builder.Configuration); 
builder.Services.ConfigureRepositoryManager(); 
builder.Services.ConfigureServiceManager();



/* extensions klasörüne aldýk 
// ioc ye db context tanýmý yaptýk - uygulama içerisindeki farklý sýnýflarýn DbContext'e baðýmlýlýðýný azaltýr
builder.Services.AddDbContext<RepositoryContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("sqlConnection")));*/


var app = builder.Build();


app.UseCors();
app.UseHttpsRedirection();


app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
