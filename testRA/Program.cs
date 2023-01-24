using Microsoft.EntityFrameworkCore;
using testRA.Domain.Entities;
using testRA.Infrastructure.Contexts;
using testRA.Infrastructure.Repositories;
using testRA.Service.Interfaces;
using testRA.Service.Service;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TestRedarborContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")
    ?? throw new InvalidOperationException("Connection string 'testRAContext' not found.")));
builder.Services.AddScoped<IGenericRespository<Candidates>, CandidatesRespository>();
builder.Services.AddScoped<ICandidateService,CandidateService>();
builder.Services.AddScoped<IGenericRespository<CandidateExperience>, CandidateExperiencesRespository>();
builder.Services.AddScoped<ICandidateExperienceService, CandidateExperienceService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
