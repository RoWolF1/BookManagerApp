using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BookManagerApp.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var method = context.Request.Method;
            var path = context.Request.Path.Value ?? string.Empty;

            // Kiểm tra nếu là file tĩnh (có chứa dấu chấm extension như .css, .js, .ico...) thì BỎ QUA HOÀN TOÀN
            bool isStaticFile = Path.HasExtension(path);

            // Chỉ log khi KHÔNG PHẢI file tĩnh
            bool shouldLog = !isStaticFile;

            if (shouldLog)
            {
                var timeWithMs = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                Console.WriteLine($"[{timeWithMs}] Method: {method} - Path: {path}");
            }

            // Chặn ID không hợp lệ (0 hoặc -1)
            if (path.Equals("/Book/Detail/0", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/Book/Detail/-1", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/Books/Details/0", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/Books/Details/-1", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = 400; // Bad Request
                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync("Book id khong hop le");

                if (shouldLog)
                {
                    Console.WriteLine($"Status Code: {context.Response.StatusCode}");
                }
                return;
            }

            await _next(context);

            if (shouldLog)
            {
                Console.WriteLine($"Status Code: {context.Response.StatusCode}");
            }
        }
    }
}