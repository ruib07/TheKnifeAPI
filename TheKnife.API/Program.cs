using TheKnife.API.Configurations.Documentation;
using TheKnife.Services;
using TheKnife.API.Configurations.Persistance;
using TheKnife.API.Configurations.Security;
using TheKnife.Services.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddCustomApiDocumentation();
builder.Services.AddCustomApiSecurity(configuration);
builder.Services.AddCustomServiceDependencyRegister(configuration);
builder.Services.AddCustomDatabaseConfiguration(configuration);

builder.Services.AddScoped<IRegisterUsersService, RegisterUsersService>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IRestaurantRegistrationsService, RestaurantRegistrationsService>();
builder.Services.AddScoped<IRestaurantResponsiblesService, RestaurantResponsiblesService>();
builder.Services.AddScoped<IRestaurantsService, RestaurantsService>();
builder.Services.AddScoped<IReservationsService, ReservationsService>();
builder.Services.AddScoped<ICommentsService, CommentsService>();
builder.Services.AddScoped<IContactsService, ContactsService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminAndResponsible", policy =>
        policy.RequireRole("Admin", "Responsible"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminAndUser", policy =>
        policy.RequireRole("Admin", "User"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
         builder =>
         {
             builder.WithOrigins("http://localhost:4200")
                 .AllowAnyHeader()
                 .AllowAnyMethod();
         });
});

var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseCustomApiDocumentation();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
