# WatchDog
An extendable, simple and fluent file system watcher for .NET

## How to use WatchDog
Watcher is the default file system watcher in WatchDog. It is a polling watcher that polls the directory every 100ms by default.

### Basic Setup
```csharp
using WatchDog;

Watcher watcher = new Watcher()
{
    FilePath = @"C:\Temp\MyDirectory", //notice no slash at the end
    PollInterval = 100
};

watcher.OnChange += W_OnChange;

WatchDogHost host = new WatchDogHost().AddWatcher(watcher).Start();
```
#### Change Event Handler
```csharp
void W_OnChange(IChangeSet changeSet)
{
    if (changeSet.HasChanges())
    {
        foreach (ChangeItem c in changeSet.ChangeItems)
        {
          //do something   
        }
    }
}
```
## Extensibility
### Basic Watcher
```csharp
public class MyCustomWatcher : AbstractWatcher
{

}
```
### Polling Watcher
```csharp
public class MyCustomPollingWatcher : AbstractWatcher, IPollable
{
        public Timer Timer { get; set; }
        public double PollInterval { get; set; }

        public Watcher()
        {
            PollInterval = 100;
            Timer = new Timer(PollInterval);
        }

        public Watcher(double interval)
        {
            PollInterval = interval;
            Timer = new Timer(interval);
        }

        public void Tick(object sender, ElapsedEventArgs e)
        {
            if (!IsProcessing)
            {
                IsProcessing = true;

                Processor.Run();

                IsProcessing = false;
            }
        }

        public override bool Start()
        {
            Timer = new Timer(PollInterval);
            Timer.Elapsed += Tick;
            Timer.Start();

            IsRunning = true;

            return true;
        }

        public override bool Stop()
        {
            Timer.Stop();
            Timer.Elapsed -= Tick;

            IsRunning = false;

            return true;
        }
}
```
