var builder = WebApplication.CreateBuilder(args);


// servis kayýtlarý 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();// default olarak loglama yapýlandýrmamýz var
builder.Logging.AddConsole();
builder.Logging.AddDebug();


var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// amacýmýz Product ortamýnda warning seviyesinde, development ortamýnda information seviyesinde bilgi almak 
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();