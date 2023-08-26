using Microsoft.EntityFrameworkCore;
using Project_Entity.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<StoreContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));
builder.Services.AddScoped(typeof(ISubCategoryRepository), typeof(SubCategoryRepository));
builder.Services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(ISupplierRepository), typeof(SupplierRepository));
builder.Services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));




builder.Services.AddSwaggerGen();

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
