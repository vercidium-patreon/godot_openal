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

### 4. Add Icons (Optional)

Create SVG icons in `addons/godot_openal/icons/`:

- `audio_source_icon.svg`
- `listener_icon.svg`

Or remove the icon parameters from `plugin.gd` if you don't want custom icons.

## Integration Notes

- The nodes automatically update their 3D positions every frame
- The manager handles OpenAL context creation/destruction
