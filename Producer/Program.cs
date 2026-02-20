using Producer.RabbitMQ;
using Producer.RabbitMQ.Connection;

var builder = WebApplication.CreateBuilder(args);

var connection = await RabbitMqConnection.CreateAsync();
builder.Services.AddSingleton<IRabbitMqConnection>(connection);
builder.Services.AddScoped<IMessageProducer, MessageProducer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
