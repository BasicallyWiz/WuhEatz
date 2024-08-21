using MongoDB.Driver;

namespace WuhEatz.Services
{
  public class MongoService
  {
    public static MongoService? instance;
    public bool isEnabled = true;
    string connectionString = "";
    public MongoClient? client;

    public MongoService()
    {
      if (!File.Exists("MongoDB.token"))
      {
        File.WriteAllText("MongoDB.token", "mongodb:yourmongoserver:yourmongoport");
        isEnabled = false;
        return;
      }

#if DEBUG
      connectionString = File.ReadAllLines("MongoDB.token")[1];
#else
      connectionString = File.ReadAllLines("MongoDB.token")[0];
#endif

      if (connectionString == "mongodb:yourmongoserver:yourmongoport")
      {
        Console.WriteLine("You must set your MongoDB connection string to a valid MongoDB server.");
        isEnabled = false;
        return;
      }

      client = new MongoClient(connectionString);
    }

    public IMongoDatabase GetDatabase(string DBName) => client!.GetDatabase(DBName);

  }
}
