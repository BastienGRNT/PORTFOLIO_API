using Npgsql;
using static API.Config.EnvVariable;

namespace API.Config.Sql;

public static class ConnectSql
{
    public static NpgsqlConnection Run()
    {
        Console.WriteLine($"Tentative de connexion à la base de données avec l'URL : {bddUrl}");
        
        // Connexion à la BDD grace à l'URL
        var connexion = new NpgsqlConnection(bddUrl);
        connexion.Open();
        
        Console.WriteLine("Connexion réussie à la base de données !");
        
        // Renvoie de la connexion
        return connexion;
    }
}