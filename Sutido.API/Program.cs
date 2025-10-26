using Microsoft.EntityFrameworkCore;
using Sutido.Model.Data;
using Sutido.Model.Settings;
using Sutido.Repository.Generic;
using Sutido.Repository.Implementations;
using Sutido.Repository.Interfaces;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Implementations;
using Sutido.Service.Interfaces;
using Sutido.Service.Mappings;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Hiển thị Enum dưới dạng string thay vì số
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


// DbContext
builder.Services.AddDbContext<SutidoProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Supabase
builder.Services.Configure<SupabaseSettings>(
    builder.Configuration.GetSection("Supabase"));

// Repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepo<>),typeof(GenericRepo<>));
builder.Services.AddScoped<IUserRepo, UserRepository>();
builder.Services.AddScoped<ITutorProfileRepo, TutorProfileRepository>();
builder.Services.AddScoped<ICertificationRepo, CertificationRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITutorProfileService, TutorProfileService>();
builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<ICertificationService, CertificationService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

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
