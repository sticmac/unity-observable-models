# Observable Models

This package delivers a system for creating observable model objects. These take the form of scriptable objects containing a readable and editable value of a defined type and a way to subscribe to any change applied to it.

## Usage

A model object is a scriptable object asset and, as such, can be created through the asset menu (*Assets -> Create -> Observable Models*).

Each asset carries a value which can be read and modified through the `Value` property attached to it. In other words, you can access that value through `myObject.Value` in code and then read it or modify it.

Model objects have a public `OnValueChanged` event property you can subscribe your scripts to so they can be warned when the model's value is updated. This property is stored as a C# generic `Action` delegate and can be used as such.
It is encouraged to [read the documentation about it](https://learn.microsoft.com/en-us/dotnet/api/system.action-1?view=net-7.0) if you haven't already.

## Installation

### Git

If you have access to this package through a git repository, you can add the package to the package manager (+ -> Add package from Git URL…). You can also add it directly to your project's `manifest.json` dependencies:

```json
    "fr.sticmac.eventsystem": "https://github.com/sticmac/unity-observable-models.git#upm"
```

In both cases, remember to point to the release `upm` branch by adding a `#upm` at the end of the URL, so that you can download the package and not the development project.

### Local download

If you got this package through direct downloading, you can directly add it through the package manager as local package (+ -> Add package from disk…). 

Adding the package folder directly inside the Assets folder is also possible. You may need to import the different Assembly Definitions inside your project. You may find more information in the [corresponding article about Assembly Definitions](https://docs.unity3d.com/2019.4/Documentation/Manual/ScriptCompilationAssemblyDefinitionFiles.html).
