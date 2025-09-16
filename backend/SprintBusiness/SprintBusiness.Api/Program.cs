using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Events;
using SprintBusiness.Api.Extensions;
using SprintBusiness.Api.Mappers;
using SprintBusiness.Shared.Configurations;
using SprintBusiness.Shared.Helpers;
using SprintBusiness.Shared.Hubs;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(configure =>
    {
        configure.JsonSerializerOptions.ReferenceHandler =
    ReferenceHandler.IgnoreCycles;
        //configure.JsonSerializerOptions.PropertyNamingPolicy = null;
    });


builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(cors =>
    {
        cors
        .WithOrigins("http://localhost:5173", "http://20.21.106.9:9120", "https://whatsapp.tadawigroup.com")
        .AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//builder.Services.AddHangfire(configuration =>
//{
//    configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//    .UseSimpleAssemblyNameTypeSerializer()
//    .UseRecommendedSerializerSettings()
//    .UseSqlServerStorage(builder.Configuration.GetConnectionString("Default"), new SqlServerStorageOptions
//    {
//        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//        QueuePollInterval = TimeSpan.Zero,
//        UseRecommendedIsolationLevel = true,
//        DisableGlobalLocks = true
//    });
//});

// إعداد الـ Dashboard والـ Background jobs
//builder.Services.AddHangfireServer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext(builder.Configuration);
//builder.Services.AddIdentity();
builder.Services.AddDependencies();
builder.Services.AddMediatR(cfg =>
{
    //cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssemblies(DependenciesExtension.LoadAssemblies());
});

builder.Services.AddAutoMapper(typeof(AppMapper));
builder.Services.AddAuth(builder.Configuration);

var whatsappConfiguration =
    builder.Configuration.GetSection("WhatsappProvider").Get<WhatsappApiConfiguration>();

var authorizationConfiguration =
    builder.Configuration.GetSection("AuthSettings").Get<AuthorizationConfiguration>();

var apiSettingsConfiguration =
    builder.Configuration.GetSection("ApiSettings").Get<ApiSettingsConfiguration>();

var oAuthConfigurations =
    builder.Configuration.GetSection("OAuthConfigurations").Get<OAuthConfigurations>();

if (whatsappConfiguration is null || authorizationConfiguration is null || oAuthConfigurations is null)
    throw new NullReferenceException();

builder.Services.AddSingleton(whatsappConfiguration);
builder.Services.AddSingleton(authorizationConfiguration);
builder.Services.AddSingleton(apiSettingsConfiguration!);
builder.Services.AddSingleton(oAuthConfigurations);

ApiEnvironment.Url = apiSettingsConfiguration?.Url ?? throw new NullReferenceException();

//Directory.CreateDirectory("Logs");
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
    .MinimumLevel.Override("System", LogEventLevel.Fatal)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddSignalR();

var app = builder.Build();

// await app.RunSeedersAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        if (exceptionHandlerPathFeature?.Error != null)
        {
            Log.Error(exceptionHandlerPathFeature.Error, "Unhandled exception caught by global middleware");
        }

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An unexpected error occurred.");
    });
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunSeedersAsync();

//app.UseHangfireDashboard();

//app.UseSerilogRequestLogging();

app.MapHub<DefaultHub>("/chat");

app.MapHub<ChatConversationHub>("/chat-conversation");

Log.Information("Whatsapp Business is running ..");

app.Run();