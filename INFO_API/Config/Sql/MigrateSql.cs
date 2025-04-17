using Npgsql;

namespace API.Config.Sql;

public class MigrateSql
{
    public static void Run()
    {
        // Connexion à la Bdd
        using var connexion = ConnectSql.Run();
        
            // Pour chaque propriété publique statique avec {get} de la classe TableSql
            foreach (var prop in typeof(TableSql).GetProperties())
            {
                // Si la propritété n'est pas un string on passe à la prochaine itération
                if(prop.GetValue(null) is not string table) continue;
                // On execute la commande stocké dans la propriété prop -> table de la classe TableSql
                using var commande = new NpgsqlCommand(table, connexion);
                commande.ExecuteNonQuery();
                Console.WriteLine($"Succès -> {prop.Name}");
            }
            
        Console.WriteLine("Les tables SQL on bien était créer !!!");
    }
}