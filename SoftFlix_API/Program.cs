using SoftFlix_API.Data;
using SoftFlix_API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<SoftFlixUser, SoftFlixRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
ApplicationDbContext? context = app.Services.CreateScope().ServiceProvider.GetService<ApplicationDbContext>();
RoleManager<SoftFlixRole>? roleManager = app.Services.CreateScope().ServiceProvider.GetService<RoleManager<SoftFlixRole>>();
UserManager<SoftFlixUser>? userManager = app.Services.CreateScope().ServiceProvider.GetService<UserManager<SoftFlixUser>>();
DbInitializer dbInitializer = new DbInitializer(context, roleManager, userManager);


app.Run();

