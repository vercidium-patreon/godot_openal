# OpenAL Plugin for Godot

This plugin provides custom nodes for using OpenAL Soft directly in Godot, bypassing Godot's built-in audio system.

## Dependencies

- This project uses the [openal_soft_bindings](https://github.com/vercidium-patreon/openal_soft_bindings) repo to invoke OpenAL Soft functions via C#. You can add this to your C# project as a [NuGet package](https://www.nuget.org/packages/openal_soft_bindings).
- The OpenAL DLL `soft_oal.dll` must be in the build directory at runtime. It can be obtained from the [OpenAL Soft](https://github.com/kcat/openal-soft/releases/tag/1.24.3) repository.

## Project Structure

```
addons/godot_openal/
├── nodes/
│   ├── ALManager.cs      # Node that manages OpenAL device + context
│   └── ALSource3D.cs     # 3D audio source node
├── plugin.cfg           # Plugin configuration
├── plugin.gd            # Plugin registration script
```

## Custom Nodes

### ALSource3D

A 3D audio source node that uses OpenAL for playback. Replace `AudioStreamPlayer3D` with this.

**Properties:**

- `Volume` (0.0 - 1.0): Audio volume
- `Pitch` (0.0 - 2.0): Playback pitch
- `MaxDistance`: Maximum audible distance
- `ReferenceDistance`: Distance at which volume is unchanged
- `Looping`: Whether audio should loop
- `Stream`: AudioStream to play

**Methods:**

- `Play()`: Start playback
- `Stop()`: Stop playback
- `Pause()`: Pause playback
- `IsPlaying()`: Check if currently playing

### ALManager

Node that manages the OpenAL context

## Setup Instructions

### 1. Enable the Plugin

1. Clone this project and place it in the addons/godot_openal folder
2. Open your Godot project
3. Go to `Project > Project Settings > Plugins`
4. Enable "godot_openal"

<img width="1132" height="258" alt="image" src="https://github.com/user-attachments/assets/94ca386d-60d5-4b3b-a1e3-fd281c09ba97" />


## Integration Notes

- The nodes automatically update their 3D positions every frame
- The manager handles OpenAL context creation/destruction
