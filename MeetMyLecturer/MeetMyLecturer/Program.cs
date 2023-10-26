using BAL.DAOs.Implementations;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Authentications;
using BAL.Profiles;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region JWT 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Meet My Lecturer Application API",
        Description = "JWT Authentication API"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtAuth:Key"])),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});
#endregion

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/account/google-login";
})
.AddGoogle(options =>
{
    options.ClientId = "33202222454-jvmu4vr5h7vmoc5eh86stie3e7ocbeav.apps.googleusercontent.com";
    options.ClientSecret = "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";
});

//add CORS
builder.Services.AddCors(cors => cors.AddPolicy(
                            name: "WebPolicy",
                            build =>
                            {
                                build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                            }
                        ));

builder.Services.Configure<JwtAuth>(builder.Configuration.GetSection("JwtAuth"));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<ISlotRepository, SlotRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();

builder.Services.AddScoped<IAccountDAO, AccountDAO>();
builder.Services.AddScoped<IBookingDAO, BookingDAO>();
builder.Services.AddScoped<IFeedbackDAO, FeedbackDAO>();
builder.Services.AddScoped<INotificationDAO, NotificationDAO>();
builder.Services.AddScoped<IRequestDAO, RequestDAO>();
builder.Services.AddScoped<ISlotDAO, SlotDAO>();
builder.Services.AddScoped<ISubjectDAO, SubjectDAO>();

builder.Services.AddAutoMapper(typeof(AccountProfile),
                               typeof(BookingProfile),
                               typeof(FeedbackProfile),
                               typeof(NotificationProfile),
                               typeof(RequestProfile),
                               typeof(SlotProfile),
                               typeof(SubjectProfile));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseCors("WebPolicy");
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
