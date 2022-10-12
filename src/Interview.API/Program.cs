using Interview.Auth.API;
using Interview.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidateModelStateFilter));
    options.Filters.Add(typeof(GlobalExceptionFilter));
}).AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opts.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(o => { o.SuppressModelStateInvalidFilter = true; });
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<RestaurantQueryValidator>();
builder.Services.AddDistributedMemoryCache();
builder.Services
    .AddRepositories()
    .AddBusinessServices()
    .AddUnitOfWork()
    .AddDatabase(builder.Configuration);
// global cors policy

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
    app.Services.SeedData();

app.Run();
