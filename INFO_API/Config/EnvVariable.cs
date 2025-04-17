namespace API.Config;

public class EnvVariable
{
    // Récupère les valeurs des variables d'environnement depuis le fichier .env
    public static string environment { get; } = DotNetEnv.Env.GetString("ENVIRONMENT");
    public static int portHttp { get; } = DotNetEnv.Env.GetInt("HTTP_PORT");
    public static int portHttps { get; } = DotNetEnv.Env.GetInt("HTTPS_PORT");
    public static string pathCertPem { get; } = DotNetEnv.Env.GetString("PATH_CERT_PEM");
    public static string pathKeyPem { get; } = DotNetEnv.Env.GetString("PATH_KEY_PEM");
    public static string bddUrl { get; } = DotNetEnv.Env.GetString("DATABASE_URL");
}

public class CheckVariable
{
    public static void Run()
    {
        // Vérifie si le fichier .env existe
        if (!File.Exists("./.env"))
        {
            throw new InvalidOperationException("Le fichier .env n'existe pas !!!");
        }
        
        // Parcourt toutes les propriétés statiques de la classe EnvVariable
        foreach (var prop in typeof(EnvVariable).GetProperties())
        {
            var value = prop.GetValue(null);

            // Vérifie si la valeur de la propriété est invalide (chaîne vide ou entier égal à 0)
            if (!((value is string str && string.IsNullOrWhiteSpace(str)) || (value is 0))) continue;
            
            Console.WriteLine($"Variable d'environnement manquante ou invalide : {prop.Name}");
        }

        Console.WriteLine("Les variables d'environnement sont valides !!!");
    }
}

