using Interview.API.Extensions;
using Interview.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidateModelStateFilter));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<ApiBehaviorOptions>(o => { o.SuppressModelStateInvalidFilter = true; });
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantQueryValidator>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
