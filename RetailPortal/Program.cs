using RetailPortal.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<MemberDetailsRepository>(sp =>
    new MemberDetailsRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ProductDetailsRepository>(sp =>
    new ProductDetailsRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PolicyDetailsRepository>(sp =>
    new PolicyDetailsRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<SponsorDetailsRepository>(sp =>
    new SponsorDetailsRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<PaymentRepository>(sp =>
    new PaymentRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add other services like controllers, authentication, etc.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
