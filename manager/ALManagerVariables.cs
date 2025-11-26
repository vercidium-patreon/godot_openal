using OpenAL.managed;

namespace OpenALAudio;

public unsafe partial class ALManager
{
    // AL resources
    ALDevice ALDevice;
    ALContext ALContext;
    ALCaptureDevice ALCaptureDevice;

    // TODO - support for multiple reverb zones
    public ALReverbEffect listenerReverbEffect;

    // Device cache
    List<string> OutputDeviceList = [];
    List<string> InputDeviceList = [];

    // Intermediate listener data
    public Vector3 listenerPosition;
    public Vector3 listenerVelocity;
    public Vector3 listenerOrientation;
    public Vector3 listenerUp;

    // Keep track of all currently playing sources, so we can destroy them when they've finished playing
    List<ALSource> ActiveSources = [];
}
