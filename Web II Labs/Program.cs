using Web_II_Labs.ExtensionMethods;
using Web_II_Labs.Middleware;
using static Web_II_Labs.Delegates.CalcDelagate;

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

/*Testing Extension Methods*/
{
    Animal finhas = new Animal("Human");
    finhas.Family = "Mammal";

    Animal Semion = new Animal("Human");
    Semion.Family = "Gustavo Family";

    Console.WriteLine("Type of Animal is: "+finhas.getType());
    Console.WriteLine("The family of the Animal is: "+finhas.getFamily());

    Console.WriteLine("Type of Animal for semi is : " + Semion.getType());
    Console.WriteLine("The family of the Animal for semi is : " + Semion.getFamily());


}

/*Testing Delegate*/
CalcDelegateMethod add = Add;
CalcDelegateMethod subtract = Subtract;
CalcDelegateMethod mul = Multiply;
CalcDelegateMethod div = Divide;

Console.WriteLine("add gives: " + Calc(10, 15, add));
Console.WriteLine("subtract gives: " + Calc(10, 15, subtract));
Console.WriteLine("Multiply gives: " + Calc(10, 15, mul));
Console.WriteLine("Divide gives: " + Calc(10, 15, div));

/*Testing Synchronous programming*/
static void longProcess()
{
    Console.WriteLine("Long process started");
    System.Threading.Thread.Sleep(4000);
    Console.WriteLine("Long process finished");
}

static void shortProcess()
{
    Console.WriteLine("Short process started!");
    Console.WriteLine("Short process finished!");

}
Console.WriteLine("Synchronous programming!");

longProcess();
shortProcess();

static async Task asyncLongProcess()
{
    Console.WriteLine("Long process started");
    /*await Task.Delay(4000);*/
    String message = await Task.Run(()=> Console.ReadLine());
    Console.WriteLine("Long process finished with message: "+message);
}

static void asyncShortProcess()
{
    Console.WriteLine("Short process started!");
    Console.WriteLine("Short process finished!");

}
Console.WriteLine("Asynchronous programming!");

Task s =asyncLongProcess();
asyncShortProcess();



app.Run();
