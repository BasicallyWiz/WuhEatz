using MongoDB.Bson;
using WuhEatz.Shared.DenpaDB.Contexts;
using WuhEatz.Shared.DenpaDB.Models;
using Log = WuhEatz.Shared.DenpaDB.Models.Log;

namespace WuhEatz.Services
{
  public class WuhLogger
  {
    public string LogPath;

    public WuhLogger(string LogPath)
    {
      this.LogPath = LogPath;

      if (Directory.Exists(LogPath)) Directory.CreateDirectory(LogPath);
    }

    public Task LogDebug(string Data)
    {
      if (Environment.GetEnvironmentVariable("DEBUG") is not null)
      {
        Console.WriteLine("[DEBUG] " + Data);
        FileStream fs = new FileStream(LogPath + $"/{DateTime.Today}.log", FileMode.Append);
      }
      LoggingContext ctx = LoggingContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));

      ctx.Logs.Add(new Log
      {
        _id = ObjectId.GenerateNewId(),
        Data = Data,
        Type = "DEBUG"
      });
      ctx.SaveChanges();
      return Task.CompletedTask;
    }
    public Task LogDebug(string Data, ObjectId? UserId)
    {
      if (Environment.GetEnvironmentVariable("DEBUG") is not null)
      {
        Console.WriteLine("[DEBUG] " + Data);
        FileStream fs = new FileStream(LogPath + $"/{DateTime.Today}.log", FileMode.Append);
      }
      LoggingContext ctx = LoggingContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));

      ctx.Logs.Add(new Log
      {
        _id = ObjectId.GenerateNewId(),
        Data = Data,
        Type = "DEBUG",
        LoggedUser_id = UserId
      });
      ctx.SaveChanges();
      return Task.CompletedTask;
    }
    public Task LogInfo(string Data)
    {
      Console.WriteLine("[INFO]  " + Data);
      LoggingContext ctx = LoggingContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));

      ctx.Logs.Add(new Log
      {
        _id = ObjectId.GenerateNewId(),
        Data = Data,
        Type = "Info"
      });
      ctx.SaveChanges();

      return Task.CompletedTask;
    }
    public Task LogInfo(string Data, ObjectId? UserId)
    {
      Console.WriteLine("[INFO]  " + Data);
      LoggingContext ctx = LoggingContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));

      ctx.Logs.Add(new Log
      {
        _id = ObjectId.GenerateNewId(),
        Data = Data,
        Type = "Info",
        LoggedUser_id = UserId
      });
      ctx.SaveChanges();

      return Task.CompletedTask;
    }
    public Task LogWarn(string Data)
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.Write("[WARN]  ");
      Console.ResetColor();
      Console.WriteLine(Data);

      LoggingContext ctx = LoggingContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));

      ctx.Logs.Add(new Log
      {
        _id = ObjectId.GenerateNewId(),
        Data = Data,
        Type = "Warn"
      });
      ctx.SaveChanges();

      return Task.CompletedTask;
    }
    public Task LogWarn(string Data, ObjectId? UserId)
    {
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.Write("[WARN]  ");
      Console.ResetColor();
      Console.WriteLine(Data);
      LoggingContext ctx = LoggingContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));
      ctx.Logs.Add(new Log
      {
        _id = ObjectId.GenerateNewId(),
        Data = Data,
        Type = "Warn",
        LoggedUser_id = UserId
      });
      ctx.SaveChanges();
      return Task.CompletedTask;
    }
    public Task LogError(string Data)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write("[ERROR] ");
      Console.ResetColor();
      Console.WriteLine(Data);

      LoggingContext ctx = LoggingContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));

      ctx.Logs.Add(new Log
      {
        _id = ObjectId.GenerateNewId(),
        Data = Data,
        Type = "Warn"
      });
      ctx.SaveChanges();

      return Task.CompletedTask;
    }
    public Task LogError(string Data, ObjectId? UserId)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write("[ERROR] ");
      Console.ResetColor();
      Console.WriteLine(Data);
      LoggingContext ctx = LoggingContext.Create(MongoService.instance!.GetDatabase("DenpaDB"));
      ctx.Logs.Add(new Log
      {
        _id = ObjectId.GenerateNewId(),
        Data = Data,
        Type = "Error",
        LoggedUser_id = UserId
      });
      ctx.SaveChanges();
      return Task.CompletedTask;
    }
  }
}
