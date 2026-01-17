namespace godot_openal;

public unsafe partial class ALManager
{
    public virtual bool TryCreateSource(string name, bool spatialised, out ALSource source)
    {
        if (!GlobalSoundStorage.TryGetValue(name, out var sound))
        {
            // Trying to play a sound we never loaded from disk
            GD.PushWarning($"[OpenAL] Unable to play sound {name} as it does not exist.");
            source = null;
            return false;
        }

        return sound.TryCreateSource(spatialised, out source);
    }
}