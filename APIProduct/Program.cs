using APIProduct.Data.Context;
using APIProduct.Domain.Exceptions;
using APIProduct.Domain.Mappers;
using APIProduct.Domain.UseCases;
using APIProduct.Domain.Utlis;
using APIProduct.Presenter.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options =>
{
    options.Filters.Add<ErrorHandler>(); // Agregar el filtro de excepciones personalizado
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//*************************************************************************************************************************************





// Inject the DbContext
builder.Services.AddDbContext<MyAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Inject the UseCases
try
{
    //Injects use cases
    builder.Services.AddScoped<CreateRoleUseCase>();
    builder.Services.AddScoped<GetAllRolesUseCase>();
    builder.Services.AddScoped<GetRoleByIdUseCase>();
    builder.Services.AddScoped<UpdateRoleUseCase>();
    builder.Services.AddScoped<DeleteRoleByIdUseCase>();

    builder.Services.AddScoped<CreateUserUseCase>();
    builder.Services.AddScoped<GetAllUserUseCase>();
    builder.Services.AddScoped<GetUserByIdentityDocumentUseCase>();
    builder.Services.AddScoped<UpdateUserByIdentityDocumentUseCase>();
    builder.Services.AddScoped<DeleteUserByIdentityDocumentUseCase>();

    builder.Services.AddScoped<LoginUseCase>();


    //Injects mappers
    builder.Services.AddScoped<RoleMapperPresenter>();
    builder.Services.AddScoped<RoleMapperDomain>();
    builder.Services.AddScoped<UserMapperPresenter>();
    builder.Services.AddScoped<UserMapperDomain>();
    builder.Services.AddScoped<LoginMapperPresenter>();


    //Injects utils
    builder.Services.AddScoped<MyValidator>();

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


// Inject the UtilsJwt and configure the authentication
builder.Services.AddSingleton<UtilsJwt>();

builder.Services.AddAuthentication(config => {
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // autenticación predeterminada para la aplicación
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // esquema de desafío predeterminado para la aplicación
}).AddJwtBearer(config => { // agregar autenticación jwt
    config.RequireHttpsMetadata = true; // requiere que las solicitudes se realicen a través de HTTPS
    config.SaveToken = true; // guardar el token
    config.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters // parámetros de validación del token
    {
        ValidateIssuerSigningKey = true, // validar la clave del emisor
        ValidateIssuer = false, // no validar el emisor
        ValidateAudience = false, // no validar la audiencia
        ValidateLifetime = true, // validar el tiempo de vida
        ClockSkew = TimeSpan.Zero, // tiempo de espera
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)), // clave de emisión
    };
});


//*************************************************************************************************************************************


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
