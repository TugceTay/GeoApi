var builder = WebApplication.CreateBuilder(args);


// servis kay�tlar� 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();// default olarak loglama yap�land�rmam�z var
builder.Logging.AddConsole();
builder.Logging.AddDebug();


var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// amac�m�z Product ortam�nda warning seviyesinde, development ortam�nda information seviyesinde bilgi almak 
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();