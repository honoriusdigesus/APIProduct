using APIProduct.Data.Context;
using APIProduct.Domain.Exceptions;
using APIProduct.Domain.Mappers;
using APIProduct.Domain.UseCases;
using APIProduct.Presenter.Mappers;
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


    //Injects mappers
    builder.Services.AddScoped<RoleMapperPresenter>();
    builder.Services.AddScoped<RoleMapperDomain>();


    //Injects utils

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


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
