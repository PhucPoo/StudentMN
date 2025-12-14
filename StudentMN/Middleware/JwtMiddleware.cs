//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Text.Json;

//namespace StudentMN.Middleware
//{
//    public class JwtMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly IConfiguration _configuration;

//        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
//        {
//            _next = next;
//            _configuration = configuration;
//        }

//        private async Task<bool> AttachUserToContext(HttpContext context, string token, bool ignoreLifetime = false)
//        {
//            try
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key missing"));

//                tokenHandler.ValidateToken(token, new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ValidateLifetime = !ignoreLifetime,
//                    ClockSkew = TimeSpan.Zero
//                }, out SecurityToken validatedToken);

//                var jwtToken = (JwtSecurityToken)validatedToken;
//                var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
//                context.Items["UserId"] = userId;

//                return true; // token hợp lệ
//            }
//            catch (SecurityTokenExpiredException)
//            {
//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync(JsonSerializer.Serialize(new { success = false, message = "Token hết hạn" }));
//                return false;
//            }
//            catch (SecurityTokenNoExpirationException)
//            {
//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync(JsonSerializer.Serialize(new { success = false, message = "Token thiếu thời hạn (exp)" }));
//                return false;
//            }
//            catch (SecurityTokenInvalidSignatureException)
//            {
//                context.Response.StatusCode = 401;
//                await context.Response.WriteAsync(JsonSerializer.Serialize(new { success = false, message = "Token không hợp lệ" }));
//                return false;
//            }
//            catch (Exception)
//            {
//                context.Response.StatusCode = 500;
//                await context.Response.WriteAsync(JsonSerializer.Serialize(new { success = false, message = "Lỗi xác thực" }));
//                return false;
//            }
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            // Chỉ xử lý access token từ Authorization header
//            var token = context.Request.Headers["Authorization"]
//                .FirstOrDefault()?.Split(" ").Last();

//            if (token != null)
//            {
//                // Access token → validate lifetime
//                bool isValid = await AttachUserToContext(context, token, ignoreLifetime: false);
//                if (!isValid)
//                {
//                    return; // dừng pipeline nếu token không hợp lệ
//                }
//            }

//            await _next(context);
//        }
//    }
//}
