using Veterinary.Data;
using Microsoft.EntityFrameworkCore;
using Veterinary.Repository.Appointments;
using Veterinary.Services.Appointments;


var builder = WebApplication.CreateBuilder(args);

// ---------------- // ----------------
builder.Services.AddDbContext<VeterinaryDataBaseContext>(Options =>
    Options.UseMySql(
        builder.Configuration.GetConnectionString("VeterinaryDataBaseConnection"),
        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")));
// ------------------------------------

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------- // ----------------
builder.Services.AddControllers();
builder.Services.AddCors(options => 
    {
        options.AddPolicy("AllowAnyOrigin", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
            }
        );
    }
);
// ---------------- // ----------------
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IAppointmentServices, AppointmentServices>();

// ------------------------------------


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ---------------- // ----------------
app.MapControllers();
// ------------------------------------

app.Run();