 
using ChatterBox.Data;
using ChatterBox.Interfaces;
using ChatterBox.Profiles;
using ChatterBox.Repositories;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient("IgnoreSSL").ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder => builder
            .WithOrigins("http://localhost:3000") // Adjust to your React app's URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()); // Allow credentials
});

// Configure DbContext and repositories
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));
builder.Services.AddScoped<IBoxRepository, BoxRepository>();
builder.Services.AddScoped<IChatBoxRepository, ChatBoxRepository>();
builder.Services.AddScoped<IBoxCommentRepository, BoxCommentRepository>();

// Configure automapper 
builder.Services.AddAutoMapper(typeof(BoxProfile).Assembly, typeof(ChatBoxProfile).Assembly, typeof(BoxCommentProfile).Assembly);

var app = builder.Build();

EnsureDatabaseCreated(app);

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();

app.Run();

void EnsureDatabaseCreated(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<AppDbContext>();
        var retries = 5;
        var delay = TimeSpan.FromSeconds(5);

        while (retries > 0)
        {
            try
            {
                context.Database.Migrate();
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception while migrating database: {ex.Message}");
                retries--;

                if (retries == 0)
                {
                    throw;
                }

                System.Threading.Thread.Sleep(delay);
            }
        }
    }
}
