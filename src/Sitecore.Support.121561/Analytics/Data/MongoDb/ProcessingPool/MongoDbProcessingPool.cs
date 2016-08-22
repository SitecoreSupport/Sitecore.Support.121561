namespace Sitecore.Support.Analytics.Data.MongoDb.ProcessingPool
{
  using System;
  using System.Linq;
  using Sitecore.Analytics.Data.DataAccess.MongoDb;
  using Sitecore.Analytics.Processing.ProcessingPool;

  public class MongoDbProcessingPool : Sitecore.Analytics.Data.MongoDb.ProcessingPool.MongoDbProcessingPool
  {
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
        // remove unncessary lines from stacktrace
        var lines = Environment.StackTrace.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        var stackTrace = string.Join("\r\n", lines.Skip(2).TakeWhile(line => !line.Contains("System.Threading.ExecutionContext.Run")));

        // add normalized stacktrace to properties
        workItem.Properties.Add("StackTrace", stackTrace);
      }

      return base.Add(workItem, options);
    }
  }
}