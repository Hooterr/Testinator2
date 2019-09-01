using Dna;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Testinator.Web.Database;

namespace Testinator.Web
{
    /// <summary>
    /// The extension methods for working with JWT Bearer tokens
    /// </summary>
    public static class JwtTokenExtensions
    {
        /// <summary>
        /// Generates a JWT Bearer token containing the user's details
        /// </summary>
        /// <param name="user">The users details</param>
        /// <returns>The JWT token</returns>
        public static string GenerateJwtToken(this ApplicationUser user)
        {
            // Prepare all the token's claims
            var claims = new[]
            {
                // Unique ID for this token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),

                // The email using the Identity name so it fills out the HttpContext.User.Identity.Name value
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),

                // Add user Id so that UserManager.GetUserAsync can find the user based on Id
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Create the credentials used to generate the token
            var credentials = new SigningCredentials(
                // Get the secret key from configuration
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(FrameworkDI.Configuration["Jwt:SecretKey"])),
                // Use HS256 algorithm
                SecurityAlgorithms.HmacSha256);

            // Generate the JWT token
            var token = new JwtSecurityToken(
                issuer: FrameworkDI.Configuration["Jwt:Issuer"],
                audience: FrameworkDI.Configuration["Jwt:Audience"],
                claims: claims,
                signingCredentials: credentials,
                // Expire if not used for 3 months
                expires: DateTime.Now.AddMonths(3)
                );

            // Return the generated token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
