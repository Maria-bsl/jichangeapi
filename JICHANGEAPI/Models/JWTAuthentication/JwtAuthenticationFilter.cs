using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http;

namespace JichangeApi.Models.JWTAuthentication
{
    public class JwtAuthenticationFilter : Attribute, IAuthenticationFilter
    {
        private const string Secret = "UHJTFPTUJKUY787FVGHMJYAERvlkuytxaysom="; // Replace with your actual secret

        public bool AllowMultiple => false;

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
           
            var controllerContext = context.ActionContext.ControllerContext;
            var descriptor = context.ActionContext.ActionDescriptor;

            // Check if the action or controller has the [AllowAnonymous] attribute
            if (descriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                controllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return Task.CompletedTask; // Skip authentication
            }


            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing or invalid Authorization header", request);
                return Task.CompletedTask;
            }

            var token = authorization.Parameter;
            if (string.IsNullOrEmpty(token))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing token", request);
                return Task.CompletedTask;
            }

            ClaimsPrincipal principal = null;
            try
            {
                principal = ValidateToken(token);
            }
            catch (SecurityTokenException)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);
                return Task.CompletedTask;
            }

            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);
            }

            context.Principal = principal;
            return Task.CompletedTask;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
            return principal;
        }
    }

    public class AuthenticationFailureResult : IHttpActionResult
    {
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase { get; }

        public HttpRequestMessage Request { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = Request,
                ReasonPhrase = ReasonPhrase
            };
            return response;
        }
    }


}