using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.IdentityModel.Tokens;
using BFPR4B.Web._keenthemes;
using BFPR4B.Web._keenthemes.libs;
using System.Text;
using BFPR4B.Web.Utility;
using BFPR4B.Web.Services.IServices.Auth;
using BFPR4B.Web.Services.Services.Auth;
using BFPR4B.Web.Services.IServices.Course;
using BFPR4B.Web.Services.Services.Course;
using BFPR4B.Web.Services.IServices.Eligibility;
using BFPR4B.Web.Services.Services.Eligibility;
using BFPR4B.Web.Services.IServices.Barangay;
using BFPR4B.Web.Services.Services.Location;
using BFPR4B.Web.Services.IServices.Location;
using BFPR4B.Web.Services.IServices.Office;
using BFPR4B.Web.Services.Services.Office;
using BFPR4B.Web.Services.IServices.Rank;
using BFPR4B.Web.Services.Services.Rank;
using BFPR4B.Web.Services.IServices.Religion;
using BFPR4B.Web.Services.Services.Religion;
using BFPR4B.Web.Services.IServices.Station;
using BFPR4B.Web.Services.Services.Station;
using BFPR4B.Web.Services.IServices.Training;
using BFPR4B.Web.Services.Services.Training;
using BFPR4B.Web.Services.IServices.User;
using BFPR4B.Web.Services.Services.User;
using BFPR4B.Web.Services.IServices.Framework;
using BFPR4B.Web.Services.Services.Framework;
using BFPR4B.Web.Services.IServices.Dashboard;
using BFPR4B.Web.Services.Services.Dashboard;
using BFPR4B.Web.Services.IServices.ModuleAccess;
using BFPR4B.Web.Services.Services.ModuleAccess;
using BFPR4B.Web.Services.IServices.Application;
using BFPR4B.Web.Services.Services.Application;
using BFPR4B.Web.Services.IServices.GAD;
using BFPR4B.Web.Services.Services.GAD;
using BFPR4B.Web.Services.IServices.Member;
using BFPR4B.Web.Services.Services.Member;


var builder = WebApplication.CreateBuilder(args);

//// Add logging configuration
//builder.ConfigureLogging((hostingContext, logging) =>
//{
//	logging.AddConsole(); // You can add other logging providers as needed
//	logging.AddDebug();
//	// Configure log levels and other settings as needed
//});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IKTTheme, KTTheme>();
builder.Services.AddScoped<AccessTokenHelper>();        // use for signin only
builder.Services.AddScoped<AccessTokenValidator>();  // use for entire controller
builder.Services.AddScoped<AccessTokenAuthorizationFilter>();
//builder.Services.AddScoped<IAuthorizationFilter, AccessTokenAuthorizationFilter>();

builder.Services.AddSingleton<IKTBootstrapBase, KTBootstrapBase>();

builder.Services.AddHttpClient<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddHttpClient<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseService, CourseService>();

builder.Services.AddHttpClient<IDashboardService, DashboardService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

builder.Services.AddHttpClient<IEligibilityService, EligibilityService>();
builder.Services.AddScoped<IEligibilityService, EligibilityService>();

builder.Services.AddHttpClient<ISystemService, SystemService>();
builder.Services.AddScoped<ISystemService, SystemService>();

builder.Services.AddHttpClient<IEventService, EventService>();
builder.Services.AddScoped<IEventService, EventService>();

builder.Services.AddHttpClient<IResourceService, ResourceService>();
builder.Services.AddScoped<IResourceService, ResourceService>();

builder.Services.AddHttpClient<IBarangayService, BarangayService>();
builder.Services.AddScoped<IBarangayService, BarangayService>();

builder.Services.AddHttpClient<ICityService, CityService>();
builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddHttpClient<IProvinceService, ProvinceService>();
builder.Services.AddScoped<IProvinceService, ProvinceService>();

builder.Services.AddHttpClient<IRegionService, RegionService>();
builder.Services.AddScoped<IRegionService, RegionService>();

builder.Services.AddHttpClient<IMemberDependentService, MemberDependentService>();
builder.Services.AddScoped<IMemberDependentService, MemberDependentService>();

builder.Services.AddHttpClient<IMemberEducationService, MemberEducationService>();
builder.Services.AddScoped<IMemberEducationService, MemberEducationService>();

builder.Services.AddHttpClient<IMemberEligibilityService, MemberEligibilityService>();
builder.Services.AddScoped<IMemberEligibilityService, MemberEligibilityService>();

builder.Services.AddHttpClient<IMemberLeaveRecordService, MemberLeaveRecordService>();
builder.Services.AddScoped<IMemberLeaveRecordService, MemberLeaveRecordService>();

builder.Services.AddHttpClient<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddHttpClient<IMemberServiceRecordService, MemberServiceRecordService>();
builder.Services.AddScoped<IMemberServiceRecordService, MemberServiceRecordService>();

builder.Services.AddHttpClient<IMemberTrainingService, MemberTrainingService>();
builder.Services.AddScoped<IMemberTrainingService, MemberTrainingService>();

builder.Services.AddHttpClient<IModuleAccessService, ModuleAccessService>();
builder.Services.AddScoped<IModuleAccessService, ModuleAccessService>();

builder.Services.AddHttpClient<IOfficeService, OfficeService>();
builder.Services.AddScoped<IOfficeService, OfficeService>();

builder.Services.AddHttpClient<IRankService, RankService>();
builder.Services.AddScoped<IRankService, RankService>();

builder.Services.AddHttpClient<IReligionService, ReligionService>();
builder.Services.AddScoped<IReligionService, ReligionService>();

builder.Services.AddHttpClient<IStationService, StationService>();
builder.Services.AddScoped<IStationService, StationService>();

builder.Services.AddHttpClient<ITrainingService, TrainingService>();
builder.Services.AddScoped<ITrainingService, TrainingService>();

builder.Services.AddHttpClient<IUserService, UserService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddSingleton<IAuthorizationHandler, ValidAccessTokenHandler>();

builder.Services.AddDistributedMemoryCache();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//}).AddCookie(options =>
//{
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
//    options.LoginPath = "/signin";
//    options.LogoutPath = "/logout";
//    //options.AccessDeniedPath = "/accessdenied";
//    options.SlidingExpiration = true;
//});

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
	options.Cookie.Name = "AuthTokenCookie";
	options.Cookie.HttpOnly = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
	options.LoginPath = "/signin";
	options.LogoutPath = "/logout";
	options.AccessDeniedPath = "/accessdenied";
	options.SlidingExpiration = true;
	options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
	options.Events = new CookieAuthenticationEvents
	{
		OnRedirectToLogout = context =>
		{
			// Clear other cookies or perform any additional actions before logout.
			// You can access HttpContext to clear cookies if needed.

			context.Response.Cookies.Delete("Access_Token"); // Replace with the name of other cookies you want to clear
			context.Response.Cookies.Delete("AuthTokenCookie"); // Replace with the name of other cookies you want to clear

			return Task.CompletedTask;
		}
	};
}).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["APISettings:Issuer"], // Replace with your issuer
		ValidateAudience = true,
		ValidAudience = builder.Configuration["APISettings:Audience"], // Replace with your audience
		ValidateLifetime = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["APISettings:SecretKey"])) // Replace with your key
	};
});

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(1); // Adjust as needed
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.WithOrigins("https://localhost:7060") // Allow requests from this origin
			   .AllowAnyHeader()
			   .AllowAnyMethod();
	});
});

builder.Services.AddHsts(options =>
{
	options.IncludeSubDomains = true;
	options.Preload = true;
	options.MaxAge = TimeSpan.FromMinutes(5);
	options.ExcludedHosts.Add("region4b.bfp.gov.ph");
	options.ExcludedHosts.Add("https://localhost:7060");
});

builder.Services.AddDataProtection()
	.SetApplicationName("BFPR4B.Web")
	.PersistKeysToFileSystem(new DirectoryInfo(@"c:\keys"))
	.SetDefaultKeyLifetime(TimeSpan.FromDays(14));



//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("ValidAccessToken", policy =>
//    {
//        policy.RequireAuthenticatedUser(); // Require authenticated users
//        policy.AddRequirements(new ValidAccessTokenRequirement());
//    });
//});

//builder.Services.AddSingleton<IAuthorizationHandler, ValidAccessTokenHandler>();

IConfiguration themeConfiguration = new ConfigurationBuilder()
							.AddJsonFile("_keenthemes/config/themesettings.json")
							.Build();

IConfiguration iconsConfiguration = new ConfigurationBuilder()
							.AddJsonFile("_keenthemes/config/icons.json")
							.Build();

KTThemeSettings.init(themeConfiguration);
KTIconsSettings.init(iconsConfiguration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/error");
	var options = new RewriteOptions().AddRedirectToHttps();
	app.UseRewriter(options);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

// Place the middleware here
app.Use(async (context, next) => {

	await next(); // Invoke the next middleware

	//if (!context.User.Identity.IsAuthenticated)
	//{
	//    if (!context.Request.Path.StartsWithSegments("/Auth/signin"))
	//    {
	//        // Redirect to the login page if not authenticated and not already on the login page
	//        context.Response.Redirect("/Auth/signin");
	//        return;
	//    }
	//}

	if (context.Response.StatusCode == 404)
	{
		context.Request.Path = "/notfound";
		await next(); // Invoke the next middleware if the response status code is 404
	}

	if (context.Response.StatusCode == 401)
	{
		context.Request.Path = "/accessdenied";
		await next(); // Invoke the next middleware if the response status code is 404
	}
});

//app.UseMiddleware<ValidAccessTokenRequirement>();

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors();
app.UseAuthentication();

//app.UseMiddleware<TokenValidationMiddleware>();

app.UseAuthorization();


//app.UseMiddleware<ValidAccessTokenHandler>();

app.UseThemeMiddleware();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();




