using magaroBack.Interface;
using magaroBack.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins", builder =>
    {
        builder
        .WithOrigins("http://localhost:4200")
        .AllowAnyHeader()        
        .AllowAnyMethod()        
        .AllowCredentials()
        .SetIsOriginAllowed(origin => true);
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddScoped<IChatHub,ChatHub>();
//builder.Services.AddTransient<ChatHub>(provider => new ChatHub(provider.GetRequiredService<ChatService>()));
var app = builder.Build();
app.UseCors("AllowOrigins");
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ChatHub>("/chathub");
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();
