# OpenAL Plugin for Godot

This plugin provides custom nodes for using OpenAL Soft directly in Godot, bypassing Godot's built-in audio system.

## Project Structure

```
addons/godot_openal/
├── nodes/
│   ├── ALSource3D.cs    # 3D audio source node
│   └── ALManager.cs     # Node that manages OpenAL device + context
├── plugin.cfg                     # Plugin configuration
├── plugin.gd                      # Plugin registration script
```

## Custom Nodes

### ALSource3D

A 3D audio source node that uses OpenAL for playback. Replace `AudioStreamPlayer3D` with this.

**Properties:**

- `Volume` (0.0 - 1.0): Audio volume
- `Pitch` (0.5 - 2.0): Playback pitch
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

1. Open your Godot project
2. Go to `Project > Project Settings > Plugins`
3. Enable "OpenAL Audio"

### 2. Create an ALManager
In your main scene, add an `ALManager` node:

![Scene tree with ALManager child nodes](docs/al_manager_node.png)

The `ALManager` node overrides Godot's inbuilt audio system, and has settings for controlling volume, enabling HRTF, output/input device, etc.

### 2. Play a Sound

Create an `ALSource3D` node and set its `Sound Name` to the path of the sound in the `res://audio` folder. To play the sound, invoke `.Play()` on the node via GDScript or C#.

If your sound files live in a different folder, you can set a custom path using the `audio/openal_sound_folder.custom` setting:

![Project Settings > Audio > OpenAL sound folder setting](docs/custom_audio_folder.png)