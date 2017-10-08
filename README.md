# WatchDog

An extendable, simple and fluent file system watcher for .NET

## How to use WatchDog

The `PollingWatcher` is the default file system watcher in WatchDog. It is a polling watcher that polls the directory every 100ms by default.

## Basic Setup

Create a new `WatchDogHost` and add a `PollingWatcher`

```csharp
WatchDogHost h = new WatchDogHost();
h.AddWatcher(new PollingWatcher(@"", 500));
h.Start();
```

Register a handler method for handeling changes that are detected
by WatchDog

```csharp
WatchDogHost h = new WatchDogHost();
h.AddWatcher(new PollingWatcher(@"", 500)
    .UseChangeHandler(ChangeEventHandler));
h.Start();
```

## Change Event Handler

Using the change handler method to do something

```csharp
void ChangeEventHandler(IChangeSet changeSet)
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

## Custom Watchers

The easiest way to create a custom watcher is to take advantage of the default functionality by inheriting from `AbstractWatcher`

```csharp
    public class MyCustomWatcher : AbstractWatcher
    {
        public string Path { get; }
        public Action<IChangeSet> OnChange { get; }
        public bool Start() { }
        public bool Stop() { }
    }
```

If you would like to build your own watcher from the groud up then you can simply implement `IWatcher`. This will still allow you to add it to your `WatchDogHost` object.

```csharp
    public class MyCustomWatcher : IWatcher
    {
        public string Path { get; }
        public Action<IChangeSet> OnChange { get; }
        public bool Start() { }
        public bool Stop() { }
    }
```
