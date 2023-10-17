using Magnum_web_application;
using Magnum_web_application.Data;
using Magnum_web_application.Models;
using Magnum_web_application.Repository;
using Magnum_web_application.Repository.IRepository;
using Magnum_web_application.Service;
using Magnum_web_application.Service.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IActiveMemberRepository, ActiveMemberRepository>();
builder.Services.AddScoped<IFeeRepository, FeeRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<ITrainingSessionRepository, TrainingSessionRepository>();
builder.Services.AddScoped<IUnpaidMonthRepository, UnpaidMonthRepository>();
builder.Services.AddScoped<IRankRepository, RankRepository>();

builder.Services.AddScoped<IActiveMemberService, ActiveMemberService>();
builder.Services.AddScoped<IFeeService, FeeService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ITrainingService, TrainingService>();
builder.Services.AddScoped<IUnpaidMonthService, UnpaidMonthService>();
builder.Services.AddScoped<IRankService, RankService>();

builder.Services.AddAutoMapper(typeof(MapConfig));
builder.Services.AddDbContext<ApplicationDbContext>(option => {
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(x => {
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

builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description =
		"JWT Authorization header using the Bearer scheme. \r\n\r\n" +
		"Enter 'Bearer' [space] and then token in the next input bellow. \r\n\r\n" +
		"Example: \"Bearer 12345abcdef\"",
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
								Type= ReferenceType.SecurityScheme,
								Id =  "Bearer"
							},
				Scheme = "oauth2",
				Name = "Bearer",
				In = ParameterLocation.Header
			},
			new List<string>()
		}
	});
});


var app = builder.Build();
app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwaggerUI(options => {
		options.SwaggerEndpoint("/swagger/v1/swagger.json", "MagnumApi");
	});
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
