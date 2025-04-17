using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Config.Token;


/// <summary>
/// Endpoint pour vérifier si un token JWT est valide.
/// </summary>
/// <param> (token) Le token JWT envoyé par l'utilisateur.</param>
/// <returns>Retourne une réponse d'authentification en fonction de la validité du token.</returns>
/// <code>var bearerToken = Request.Headers["Authorization"].ToString();</code> 
/// <code>if (!Validate.Token(bearerToken)) return Unauthorized(new { message = "Accès refusé, Token invalide" });</code> 


public static class Validate
{
    public static bool AdminToken(string token)
    {
        // Instanciation de la classe TokenHandler pour pouvoir utiliser ses méthodes via -> checkToken
        var checkToken = new JwtSecurityTokenHandler();
        // Récuperation de la secret key dans le .env
        var getSecretKey = DotNetEnv.Env.GetString("JWT_SECRET_KEY");
        Console.WriteLine(getSecretKey);
        // Encodage de la secret key en binnaire pour pouvoir travailler avec
        var secretKey = Encoding.UTF8.GetBytes(getSecretKey);
        
        // Définition de parametres de validation du token
        var parameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };

        try
        {
            // Récuperer les information du paylaod et vérifier le token
            var paylaod = checkToken.ValidateToken(token, parameters, out _);
            
            // Checker si le user est admin (admin : true)
            var isAdminClaim = paylaod.Claims.FirstOrDefault(c => c.Type == "admin")?.Value;
            return isAdminClaim == "true";
        }
        catch
        {
            // Si le token n'est pas valide retourner false
            return false;
        }
    }
    
    public static bool Token(string token)
    {
        // Instanciation de la classe TokenHandler pour pouvoir utiliser ses méthodes via -> checkToken
        var checkToken = new JwtSecurityTokenHandler();
        // Récuperation de la secret key dans le .env
        var getSecretKey = DotNetEnv.Env.GetString("JWT_SECRET_KEY");
        // Encodage de la secret key en binnaire pour pouvoir travailler avec
        var secretKey = Encoding.UTF8.GetBytes(getSecretKey);
        
        // Définition de parametres de validation du token
        var parameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
        };

        try
        {
            // Récuperer les information du paylaod et vérifier le token
            checkToken.ValidateToken(token, parameters, out _);
            // Retourner vrai si le token est valide
            return true;
        }
        catch
        {
            return false;
        }
    }
}