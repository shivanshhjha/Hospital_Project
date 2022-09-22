// WebApplication: The Web ENvironment that is common for
// Razor Views, MVC, and APIs
// Build and Initialize the HTTP Pipeline for
// 1. Registeing all Dependencies as Service
// 2. Middlewares
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Dal.Contract;
using Application.Entities;
using HospitalApp.Repositories;
using Application.Data.DataAccess;
using HospitalApp.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("HospitalAppIdentityDbContextConnection");

builder.Services.AddDbContext<HospitalAppIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
  //  .AddEntityFrameworkStores<HospitalAppIdentityDbContext>();


// Read the Connection String from appSettings.json

/*var connectionString = builder.Configuration.GetConnectionString("mbaspnetcore6IdentityDbContextConnection");
// Register the DbContext class for Identity into the DI Container
builder.Services.AddDbContext<HospitalAppIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));*/
// Configure the Identity Service for Authentication in the DI Container
// AddDefaultIdentity(): Method for Registering 'User-BAsed' Authentication for
// Current App in DI
// SignIn.RequireConfirmedAccount = true: Means that the UserName (Email) MUST
// be verified
// AddEntityFrameworkStores<mbaspnetcore6IdentityDbContext>(): This will
// COnfigire the SQL Server Database for Storing Users, Roles information
// AddDefaultUI(): This will make sure that the Default UI for Register and Login is added for the Applciation
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<mbaspnetcore6IdentityDbContext>()
//    .AddDefaultUI(); // Add this method explicitly
/// The DI of for the Identity Service for User and Role Manager
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<HospitalAppIdentityDbContext>().AddDefaultTokenProviders()
    .AddDefaultUI(); // Add this method explicitly
// The builder is a WebApplicationBuilder class
// THis is used to Provide the 'Dependency Container'
// This provides the ApplicationBuilder
// Add services to the container.
// The 'Services' is of the type 'IServiceCollection' which is DI Container
// to register all Services

builder.Services.AddAuthorization(options =>
{
    // policy, an instace of AuthorizationPolicyBuilder class
    // Used to create policies based on Roles
    options.AddPolicy("Doctor", policy =>
    {
        policy.RequireRole("Administrator", "Manager", "IPD_Patient");
    });
    options.AddPolicy("Order", policy =>
    {
        policy.RequireRole("Administrator", "Manager");
    });
    options.AddPolicy("Role", policy =>
    {
        policy.RequireRole("Administrator");
    });
});


// The DI For the MVC COntroller and View
// Registration of the Data Access Lyer in The DI Container Provided
// by ASP.NET Core
// Registered the DepartmentDataAcess
// The class which will be injected using IDataAccess<Department,int>
// will be actually injected usign an Instance of DepartmentDataAccess class
// because the DI Container is already registered using
// the DepartmentDataAccess
builder.Services.AddScoped<IDataAccess<Doctor, int>, DoctorDataAccess>();
builder.Services.AddScoped<IDataAccess<IPD_Patient, int>, IPD_PatientDataAccess>();
builder.Services.AddScoped<IDataAccess<Charges, int>, ChargesDataAccess>();
builder.Services.AddScoped<IDataAccess<Address, int>, AddressDataAccess>();
builder.Services.AddScoped<IDataAccess<Patient, int>, PatientDataAccess>();
// Also Register all Repositoiry services in DI Container
builder.Services.AddScoped<IServiceRepository<Doctor, int>, DoctorRepository>();
builder.Services.AddScoped<IServiceRepository<IPD_Patient, int>, IPD_PatientRepository>();
builder.Services.AddScoped<IServiceRepository<Charges, int>, ChargesRepository>();
builder.Services.AddScoped<IServiceRepository<Address, int>, AddressRepository>();
builder.Services.AddScoped<IServiceRepository<Patient, int>, PatientRepository>();
// Adding Session Service
// Tell Server that Session will be stored
// in the Memory of the Hosting Env.
builder.Services.AddDistributedMemoryCache();
// The Session Condiguration
// The Session will be closed if
// After the First Request to Server
// the Server does not receive any request for next 20 mins
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});
// Service registered for Request Processing for MVC and API Controller and MVC Views
// Lets Configure the Action Filter for the MVC COntrollers
builder.Services.AddControllersWithViews(options =>
{
    // options.Filters.Add(new LogFilterAttribute());
    // options.Filters.Add(typeof(LogFilterAttribute));
    // Register the Custom Exception Filter
    // This will Resolve the IModelMetadataProvider
    //options.Filters.Add(typeof(AppExceptionFilterAttribute));
});
// Application Builder for Registering Middlewares for
// HTTP Pipelie
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod();
                      });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for Doctorion scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// Redirect HTTP Request to HTTPS while Doctorion
app.UseHttpsRedirection();
// Read all Static files for Download/Upload Operations
app.UseStaticFiles();
// Contains a Route Table
app.UseRouting();
// Middleware for Authentication
app.UseAuthentication();
// Middleware for Authorization
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
// COnfigure the Session Middleware
// THis will use Configuration for session
// from Session Service from DI COntainer
app.UseSession();
// For Role Based Security
app.UseAuthorization();
// Pass the Request to MVC Controllers
// That matches with the ControllerName in Route Table
// Default is HomeController class and its method is Index
// The 'id' is an Optional Parameter, used in case of Edit and Delete
// Or Whichever an action method that accepts an 'id' parameter
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// Acept Request for Razor Views those are used for Identity
app.MapRazorPages();
app.Run();