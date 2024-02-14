namespace Web_II_Labs.Middleware
{
    public class AddToHeaderMiddleware: IMiddleware
    {
              
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers.Add("X-Custom-Header", "Testing the addition of stuff to the header response");
            await next(context);
        }
    }
}
