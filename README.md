# FasterDevSpeed

A mod for the game Rimworld. Increases the max speed of the game in dev mode. Increased the number of ticks by 3x for dev speed.

## Requirements for Visual Studio
- Harmony 2.0.0.8 - [https://github.com/pardeike/HarmonyRimWorld]
- .NET Framework 4.7.2

## Refernces in Visual Studio
Add the following references to your Class Library (<b>.NET Framework</b>) project:
- Assembly-CSharp.dll
- Unity-Engine.dll
- 0Harmony.dll

### Note - Set references for <b>0Harmony.dll</b> in VS-project only! No need to include it in the Mod/Assemblies folder when installing into Rimworld, as the About/about.xml will handle dependency injection.

## Modding Tutorials
```url
https://rimworldwiki.com/wiki/Modding_Tutorials
```

## Authors
- sparr - initial work - [https://github.com/sparr]
- sarabrajsingh - rimworld 1.1 integration - [https://github.com/sarabrajsingh]