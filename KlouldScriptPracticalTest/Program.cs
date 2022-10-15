using KlouldScriptPracticalTest.Middleware;
using KlouldScriptRepository.Patient;
using KlouldScriptRepository.Plan;
using KlouldScriptService.Patient;
using KlouldScriptService.Plan;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Adding Services for Service/Repository
builder.Services.AddTransient<IPlanService, PlanService>();
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<IPlanRepository, PlanRepository>();
builder.Services.AddTransient<IPatientRepository, PatientRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandlingMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
