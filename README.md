# Observable Models

This package delivers a system for creating observable model objects. These take the form of scriptable objects containing a readable and editable value of a defined type and a way to subscribe to any change applied to it.

## Usage

A model object is a scriptable object asset and, as such, can be created through the asset menu (*Assets -> Create -> Observable Models*).

Each asset carries a value which can be read and modified through the `Value` property attached to it. In other words, you can access that value through `myObject.Value` in code and then read it or modify it.

Model objects are Observable, which mean any of your scripts can subscribe to it. It follows the [Observer design pattern](https://docs.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern) as implemented in the .NET framework: they derive from the [IObservable](https://docs.microsoft.com/en-us/dotnet/api/system.iobservable-1?view=net-6.0) interface.
As such, any of your scripts can subscribe to any observable model by implementing the [IObserver](https://docs.microsoft.com/en-us/dotnet/api/system.iobserver-1?view=net-6.0) interface, with the right type as a generic value (i.e if you try to subscribe to an `IntObservableModel`, your script must implement `IObserver<int>`).
Only the `OnNext` method is really used as of version 1.0.0, the model can't really stop send notifications or produce an error so far.

If you do subscribe one of your scripts to an observable model object, it will recieve a notification for every value change through the `OnNext` method.

## Installation

### Git

If you have access to this package through a git repository, you can add the package to the package manager (+ -> Add package from Git URL…). You can also add it directly to your project's `manifest.json` dependencies:

```json
    "fr.sticmac.eventsystem": "<git url>"
```

### Local download

If you got this package through direct downloading, you can directly add it through the package manager as local package (+ -> Add package from disk…). 

Adding the package folder directly inside the Assets folder is also possible. You may need to import the different Assembly Definitions inside your project. You may find more information in the [corresponding article about Assembly Definitions](https://docs.unity3d.com/2019.4/Documentation/Manual/ScriptCompilationAssemblyDefinitionFiles.html).
