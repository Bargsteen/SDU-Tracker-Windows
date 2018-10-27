using System;
using System.Collections.Generic;
using TrackerLib;
using System.Linq;

namespace TrackerConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Logging.SetupLogging();
            
            Logging.LogInfo("hey from main");
            //CheckPersistence();
            CheckOnSuccessOrError();
            Console.WriteLine("Done.");
        }

        private static void CheckOnSuccessOrError() 
        {
            
            
            var credentials = new Credentials("***REMOVED***", "***REMOVED***");
            
            // for (int i = 0; i < 5; i++)
            // {
            //     var appUsage = new AppUsage(i + "***REMOVED***:KasperWindows", "WindowsPC", DateTimeOffset.UtcNow, 1, "VSCode", 10000);
            //     DeviceUsage deviceUsage 
            //     = new DeviceUsage(i + "***REMOVED***:KasperWindows", "WindowsPC", DateTimeOffset.UtcNow, 1, EventType.Started);

            //     Persistence.SaveUsage(appUsage);
            //     Persistence.SaveUsage(deviceUsage);
            //     System.Threading.Thread.Sleep(500);
            // }
            
            //SendOrSaveHandler.SendSomeUsagesFromPersistence(credentials, limitOfEach: 10);
            Logging.LogInfo("INFO HERE");
            Logging.LogError("error uh uh");

            // foreach (var appU in Persistence.FetchAppUsages().as)
            // {
            //     Console.WriteLine(appU.TimeStamp);
            // }

            //Requests.SendUsageAsync(appUsage, credentials, () => Console.WriteLine("Success"), () => Console.WriteLine("Error.."));
        }

        // private static void CheckPersistence()
        // {
        //     DeviceUsage deviceUsage 
        //         = new DeviceUsage("***REMOVED***:KasperWindows", "WindowsPC", DateTimeOffset.UtcNow, 1, EventType.Started);

        //     var appUsage = new AppUsage("***REMOVED***:KasperWindows", "WindowsPC", DateTimeOffset.UtcNow, 1, "VSCode", 10000);

        //     var credentials = new Credentials("***REMOVED***", "***REMOVED***");
        //     Console.WriteLine(Persistence.RealmFileLocation);
        //     Persistence.SaveUsage(deviceUsage);
        //     Persistence.SaveUsage(appUsage);

        //     var fetchedAppUsages = Persistence.FetchAppUsages();
        //     var fetchedDeviceUsages = Persistence.FetchDeviceUsages();

        //     foreach (var appU in fetchedAppUsages)
        //     {

        //         Console.WriteLine(appU.UsageIdentifier);
        //         Requests.SendUsageAsync(appU, credentials);
        //         Persistence.DeleteUsage(appU);
        //     }

        //     foreach (var devU in fetchedDeviceUsages)
        //     {
        //         Console.WriteLine(devU.UsageIdentifier);
        //         Requests.SendUsageAsync(devU, credentials);
        //         Persistence.DeleteUsage(devU);
        //     }

        //     var fetchedDeletedAppUsages = Persistence.FetchAppUsages();
        //     var fetchedDeletedDeviceUsages = Persistence.FetchDeviceUsages();
        //     Console.WriteLine("Deleted App:");
        //     foreach (var appU in fetchedDeletedAppUsages)
        //     {
        //         Console.WriteLine(appU);
        //     }
        //     Console.WriteLine("Delete Device: ");
        //     foreach (var devU in fetchedDeletedDeviceUsages)
        //     {
        //         Console.WriteLine(devU);
        //     }
        // }
    }
}
