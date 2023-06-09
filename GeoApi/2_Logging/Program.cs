var builder = WebApplication.CreateBuilder(args);


// servis kayıtları 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();// default olarak loglama yapılandırmamız var
builder.Logging.AddConsole();
builder.Logging.AddDebug();


var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

// amacımız Product ortamında warning seviyesinde, development ortamında information seviyesinde bilgi almak 
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();