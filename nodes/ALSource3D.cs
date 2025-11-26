using OpenAL.managed;

namespace OpenALAudio;

[Tool]
public partial class ALSource3D : Node3D
{
    List<ALSource> sources = [];
    public ALFilter filter;

    public void UpdateFilter(float gain, float gainHF)
    {
        filter?.SetGain(gain, gainHF);
    }

    public void Play()
    {
        if (!ALManager.instance.TryCreateSource(SoundName, true, out var source))
            return;

        // Set initial properties
        source.SetRelative(Relative);
        source.SetMaxDistance(MaxDistance);
        source.SetReferenceDistance(ReferenceDistance);
        source.SetGain(Volume);
        source.SetPitch(Pitch);
        source.SetLooping(Looping);

        filter ??= new(1, 1);
        source.SetFilter(ALManager.instance.listenerReverbEffect, filter, filter);

        source.Play();
        sources.Add(source);
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
