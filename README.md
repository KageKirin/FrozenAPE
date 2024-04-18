# Frozen A-Pose Exporter, a.k.a FrozenAPE 🧊🦍

![Logo showing a frozen ape 🧊🦍](./Documentation~/frozenape_logo.png)

FrozenAPE is a set of Unity-specific functions and Editor Menus
to allow moving a skinned model into a specific pose
and exporting the _frozen_ static mesh of that pose
into a Wavefront OBJ or FBX file.

This rather experimental package can be used to pose any given character model
into an A-Pose (hence this project's name), and export the frozen, static meshes as
OBJ files, to allow further modifications, such as re-skinning, in another DCC tool.

The whole picture is to simplify the workload for Technical Artists when faced to recycling models
to other games/engines that require skinning to a particular rig.
The closest use-case would be to transform a character model to A-Pose, freeze and export as OBJ,
and then reskin the model for e.g. Roblox.

## 🔧 Building and Running

Add the 3 registries as described below,
followed by the dependency on `com.kagekirin.frozenape`.

Then follow the project configuration described by [Cysharp's Csproj Modified](https://github.com/Cysharp/CsprojModifier)
and add (copy) the `LangVersion.props` from this repo to the 'Addition Project Imports'.

### 🔨 Add the Project

Due to some dependencies, this projects requires adding the following registries to Unity:

* GitHub (which hosts this package)
* OpenUPM (which hosts a Unity dependency)
* Unity NuGet (which hosts a C# dependency)

#### Add package to project

Add `com.kagekirin.frozenape` to the `manifest.json` `.dependencies{}`:

```json
"dependencies": {
    "com.kagekirin.frozenape": "0.0.7",
}
```

#### Add GitHub registry

The following registry must be added to Unity's `manifest.json` `.scopedRegistries[]`:

```json
{
    "name": "KageKirin's GitHub",
    "url": "https://npm.pkg.github.com/@kagekirin",
    "scopes": [
        "com.kagekirin"
    ]
}
```

#### Add OpenUPM registry

The following registry must be added to Unity's `manifest.json` `.scopedRegistries[]`:

```json
{
    "name": "OpenUPM",
    "url": "https://package.openupm.com",
    "scopes": [
        "com.cysharp"
    ]
}
```

#### Add Unity NuGet registry

The following registry must be added to Unity's `manifest.json` `.scopedRegistries[]`:

```json
{
    "name": "Unity NuGet",
    "url": "https://unitynuget-registry.azurewebsites.net",
    "scopes": [
        "org.nuget"
    ]
}
```


### ▶ Running and Settings

TODO: elaborate

## 🤝 Collaborate with My Project

Please refer to [COLLABORATION.md](./COLLABORATION.md)
