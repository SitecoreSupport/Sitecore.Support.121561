namespace Sitecore.Support.Analytics.Data.MongoDb.ProcessingPool
{
  using System;
  using System.Linq;
  using Sitecore.Analytics.Data.DataAccess.MongoDb;
  using Sitecore.Analytics.Processing.ProcessingPool;

  public class MongoDbProcessingPool : Sitecore.Analytics.Data.MongoDb.ProcessingPool.MongoDbProcessingPool
  {
    const string StackTrace = "StackTrace";
    const string UtcNow = "UtcNow";    

    public MongoDbProcessingPool(MongoDbCollection pool) : base(pool)
    {
    }

    public MongoDbProcessingPool(string connectionStringName) : base(connectionStringName)
    {
    }

    public bool Debug { get; [UsedImplicitly] set; }

    public override bool Add(ProcessingPoolItem workItem, SchedulingOptions options = null)
    {
      if (Debug)
      {
        // remove unncessary lines from stacktrace, skipping first 2 Environment.StackTrace calls and last System.Threading.ExecutionContext.Run
        var lines = Environment.StackTrace.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        var stackTrace = string.Join(Environment.NewLine, lines.Skip(2).TakeWhile(line => !line.Contains("System.Threading.ExecutionContext.Run")));

        // add normalized stacktrace to properties
        var properties = workItem.Properties;
        if (properties.ContainsKey(StackTrace))
        {
          properties[StackTrace] = stackTrace;
        }
        else
        {
          properties.Add(StackTrace, stackTrace);
        }

        // add UTC timestamp                      
        var utcNow = DateTime.UtcNow.ToString();
        if (properties.ContainsKey(UtcNow))
        {
          properties[UtcNow] = utcNow;
        }
        else
        {
          properties.Add(UtcNow, utcNow);
        }
      }

      return base.Add(workItem, options);
    }
  }
}