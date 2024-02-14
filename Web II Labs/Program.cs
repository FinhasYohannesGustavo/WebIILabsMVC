using Web_II_Labs.ExtensionMethods;
using Web_II_Labs.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add your custom middleware to the service collection
builder.Services.AddTransient<AddToHeaderMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Use your custom middleware
app.UseMiddleware<AddToHeaderMiddleware>();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


Animal finhas = new Animal("Human");
finhas.Family = "Mammal";
Console.WriteLine(finhas.getFamily());
Console.WriteLine(finhas.getFamily());


app.Run();
