using StudentApi.Models;
using StudentApi.Repositories;
using StudentApi.Services;
using StudentApi.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Register MVC/Web API controllers.
builder.Services.AddControllers();

// Register Swagger/OpenAPI services.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Repository and Service.
builder.Services.AddSingleton<IRepository<Student>, InMemoryRepository<Student>>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddTransient<PercentageGradeStrategy>();
builder.Services.AddTransient<GpaGradeStrategy>();
builder.Services.AddSingleton<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITeacherService, TeacherService>();

var app = builder.Build();

// Enable Swagger.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();