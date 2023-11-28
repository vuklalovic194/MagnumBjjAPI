using Magnum_API_web_application;
using Magnum_API_web_application.Data;
using Magnum_API_web_application.Handler;
using Magnum_API_web_application.Models;
using Magnum_API_web_application.Models.DTO;
using Magnum_API_web_application.Queries;
using Magnum_API_web_application.Repository;
using Magnum_API_web_application.Repository.IRepository;
using Magnum_API_web_application.Service;
using Magnum_API_web_application.Service.IServices;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
builder.Services.AddScoped<ICompetitionRepository, CompetitionRepository>();
builder.Services.AddScoped<ICompetitionResultRepository, CompetitionResultRepository>();

//builder.Services.AddScoped<IActiveMemberService, ActiveMemberService>();
builder.Services.AddScoped<IFeeService, FeeService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<ITrainingService, TrainingService>();
builder.Services.AddScoped<IUnpaidMonthService, UnpaidMonthService>();
builder.Services.AddScoped<IRankService, RankService>();

builder.Services.AddAutoMapper(typeof(MapConfig));
builder.Services.AddDbContext<ApplicationDbContext>(option => {
	option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"), builder =>
	{
		builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null); 
	});
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

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

var app = builder.Build();

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("../swagger/v1/swagger.json", "Test API V1");
	c.RoutePrefix = string.Empty; // Set Swagger UI at apps root
});
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.MapGet("api/Members", async (IMemberRepository repo) =>
{
	return Results.Ok(await repo.GetAllAsync());
});

app.MapGet("api/Member/{id:int}", async (int id, IMemberRepository repo) =>
{
	return Results.Ok(await repo.GetByIdAsync(u => u.Id == id));
});

app.MapPut("api/UpdatedMember/{id:int}", async (int id, IMemberRepository repo, [FromBody] MemberDTO dto) =>
{
	Member member = await repo.GetByIdAsync(u => u.Id == id);
	dto.mapMember(dto, member);
	await repo.Update(member);
	await repo.SaveAsync();
	return Results.NoContent();
});

app.MapPost("api/CreatedMember", async (IMemberRepository repo, [FromBody] MemberDTO dto) => 
{
	Member member = new();
	dto.mapMember(dto, member);
	await repo.CreateAsync(member);
	await repo.SaveAsync();
	return Results.Created($"api/CreatedMember/{member.Id}", member);
});

app.MapDelete("api/Member", async (int id, IMemberRepository repo) =>
{
	Member member = await repo.GetByIdAsync(u => u.Id == id);
	await repo.DeleteAsync(member);
	await repo.SaveAsync();
	return Results.NoContent();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
