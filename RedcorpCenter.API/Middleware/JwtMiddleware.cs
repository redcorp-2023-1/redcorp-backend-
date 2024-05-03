using RedcorpCenter.Domain;

namespace RedcorpCenter.API.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Autenticación
        /// </summary>
        /// <param name="context"></param>
        /// <param name="tokenDomain"></param>
        /// <param name="employeeDomain"></param>
        public async Task Invoke(HttpContext context, ITokenDomain tokenDomain, IEmployeeDomain employeeDomain)
        {
            //Autenticación

            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var email = tokenDomain.ValidateJwt(token);

            if (email != null)
            {   
                context.Items["User"] = await employeeDomain.GetByEmail(email);
            }

            await _next(context);
        }
    }
}
