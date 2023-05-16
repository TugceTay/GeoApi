var builder = WebApplication.CreateBuilder(args);


// Service (Container)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

if (app.Environment.IsDevelopment()) // development modda ise swagger kullan aksi durumda kullanma
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.Run();
