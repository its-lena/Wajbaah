using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Wajbah_API.Data;
using Wajbah_API.Repository.IRepository;
using Wajbah_API.Repository;
using Wajbah_API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000",
                                              "http://www.contoso.com").AllowAnyHeader()
                                                  .AllowAnyMethod();
                      });
});
// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
//Rate Limit implementaon
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


builder.Services.AddTransient<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserChefRepository, UserChefRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IExtraMenuItemRepository, ExtraMenuItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IChefRepository, ChefRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IItemRateRepository, ItemRateRepository>();
builder.Services.AddAutoMapper(typeof(MapperConfig));

//Authentication
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x => {
		x.RequireHttpsMetadata = false;
		x.SaveToken = true;
		x.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});
//var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
//builder.Services.AddAuthentication(x =>
//{
//	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//	.AddJwtBearer(x => {
//		x.RequireHttpsMetadata = false;
//		x.SaveToken = true;
//		x.TokenValidationParameters = new TokenValidationParameters
//		{
//			ValidateIssuerSigningKey = true,
//			IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
//			ValidateIssuer = false,
//			ValidateAudience = false
//		};
//	});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = 
		"JWT Authorization header using the Bearer scheme. \r\n\r\n" +
		"Enter 'Bearer' [space] and then your token in the input below \r\n\r\n" +
		"Example: \"Bearer 12345abcdef \"",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Scheme = "Bearer"
	});
	options.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1.0",
		Title = "WajbahAPI V1",
		Description = "API for Wajbah System",
		TermsOfService = new Uri("https://example.com/terms"),
		License=new OpenApiLicense
		{
			Name="Example License",
			Url=new Uri("https://example.com/License")
		}

	});
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
if (app.Environment.IsDevelopment())
{

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic_VillaV1");
    }
        );

}
else
{
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Magic_VillaV1");
        options.RoutePrefix = "";
    }
        );
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
