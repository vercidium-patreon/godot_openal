using Godot;
using OpenAL.managed;

namespace godot_openal;

[Tool]
[GlobalClass]
public partial class ALSource3D : Node3D
{
    protected List<ALSource> sources = [];
    public ALFilter filter;
    public ALReverbEffect effect;

    public static ALFilter silenceFilter = new(0, 0);
    public static ALFilter fullFilter = new(1, 1);


    public void UpdateFilter(float gain, float gainHF, bool fullReverb = false)
    {
        if (filter == null)
            filter = new(gain, gainHF);
        else
            filter.SetGain(gain, gainHF);

        // For reverb in other rooms, we send the sound's clear audio to the reverb effect,
        //  then reduce the reverb effect's gain to make it muffled
        var reverbFilter = fullReverb ? fullFilter : filter;

        foreach (var s in sources)
            s.SetFilter(effect, filter, reverbFilter);

        fullFilter.Delete();
    }

    public virtual bool Play()
    {
        if (ALManager.instance == null)
        {
            GD.PrintErr($"godot_openal: unable to play the {Name} ALSource3D node because the ALManager has not been initialised yet.");
            return false;
        }

        if (!ALManager.instance.TryCreateSource(SoundName, true, out var source))
            return false;

        // Set initial properties
        source.SetRelative(Relative);
        source.SetMaxDistance(MaxDistance);
        source.SetReferenceDistance(ReferenceDistance);
        source.SetGain(Volume);
        source.SetPitch(Pitch);
        source.SetLooping(Looping);

        source.SetFilter(effect, filter, filter);

        source.Play();
        sources.Add(source);
        return true;
    }

    public void Stop()
    {
        foreach (var s in sources)
            s.Stop();
    }

    public bool IsPlaying()
    {
        foreach (var s in sources)
            if (!s.Finished())
                return false;

        return true;
    }

    public void OnDeviceDestroyed()
    {
        foreach (var s in sources)
            s.Dispose();

        sources.Clear();

        // Must delete the filter after the sources
        filter?.Delete();
        filter = null;
    }
}
