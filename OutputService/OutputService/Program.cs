using OutputService.Service.Abstraction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IOutputService, OutputService.Service.OutputService>();
builder.Services.AddCors(options =>
    {
        options.AddPolicy("MyPolicy",
            policyBuilder =>
            {
                policyBuilder
                    .WithOrigins("http://localhost:3000", "http://unitgenius-webui")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
            });
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
