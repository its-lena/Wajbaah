using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Wajbah_API.Data;
using Wajbah_API.Models;
using Wajbah_API.Repository.IRepository;
using Wajbah_API.Repository;
using Wajbah_API.Services;
using Wajbah_API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));

//resolving the categoryService dependency
builder.Services.AddTransient<IConversationService, ConversationService>();
//builder.Services.AddSingleton<MongoDBService>();
//resolving the IMessageService dependency
builder.Services.AddTransient<IMessageService, MessageService>();

builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IExtraMenuItemRepository, ExtraMenuItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IChefRepository, ChefRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
