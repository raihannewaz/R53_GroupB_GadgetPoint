using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using R53_GroupB_GadgetPoint.Context;
using R53_GroupB_GadgetPoint.DAL.Interface;
using R53_GroupB_GadgetPoint.DAL.Interfaces;
using R53_GroupB_GadgetPoint.DAL.JWTService;
using R53_GroupB_GadgetPoint.DAL.Repositories;
using R53_GroupB_GadgetPoint.HelperAutoMapper;
using R53_GroupB_GadgetPoint.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<StoreContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));


builder.Services.AddAutoMapper(typeof(MappingProfile));



var usrIden= builder.Services.AddIdentityCore<AppUser>();
usrIden = new IdentityBuilder(usrIden.UserType, usrIden.Services);
usrIden.AddEntityFrameworkStores<StoreContext>();
usrIden.AddSignInManager<SignInManager<AppUser>>();



var config = builder.Configuration;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
            ValidIssuer = config["Token:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false
        };
    });

builder.Services.AddScoped(typeof(IProductRepository), typeof(ProductRepository));
builder.Services.AddScoped(typeof(ISubCategoryRepository), typeof(SubCategoryRepository));
builder.Services.AddScoped(typeof(IBrandRepository), typeof(BrandRepository));
builder.Services.AddScoped(typeof(ICategoryRepository), typeof(CategoryRepository));
builder.Services.AddScoped(typeof(ISupplierRepository), typeof(SupplierRepository));
builder.Services.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));
builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
//builder.Services.AddScoped(typeof(IDeliveryMethodRepository), typeof(DeliveryMethodRepository));
builder.Services.AddScoped(typeof(ITokenService), typeof(TokenService));
builder.Services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddScoped(typeof(IGenericCrud<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));









builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },new string[] {}
        }
    });

});







var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<StoreContext>();
    var usrMgr = services.GetRequiredService<UserManager<AppUser>>();
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();

    // Seed data
    await StoreContextSeed.SeedAsync(dbContext, loggerFactory);
    await StoreContextSeed.SeedUserAsync(usrMgr);
}


app.UseCors(b=>b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImage")),
    RequestPath = "/ProductImage"
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();