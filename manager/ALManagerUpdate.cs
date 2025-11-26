using OpenAL.managed;

namespace OpenALAudio;

public unsafe partial class ALManager
{
    public void SetListenerGain(float gain) => AL.Listenerf(AL.AL_GAIN, gain);
    public void SetHRTFEnabled(bool enabled) => RecreateDevice();
    public void SetSampleRate(int sampleRate) => RecreateDevice();
    public void SetDistanceModel(ALDistanceModel model) => AL.DistanceModel((int)model);
    public void SetMetersPerUnit(float metersPerUnit) => AL.Listenerf(AL.AL_METERS_PER_UNIT, metersPerUnit);
    public void SetSpeedOfSound(float speedOfSound) => AL.SpeedOfSound(speedOfSound);

    void SetMicrophoneEnabled(bool enabled)
    {
        if (enabled)
            InitialiseCaptureDevice();
        else
            CloseCaptureDevice();
    }

    public void UpdateListener(Vector3 position, float pitch, float yaw)
    {
        var cameraVel = Vector3.Zero;
        var orientation = Helper.PitchYawToVector3(pitch, yaw);

        // Up vector MUST be perpendicular to look direction, else spatialisation gets distorted
        var up = Helper.PitchYawToVector3(pitch + MathF.PI / 2, yaw);

        AL.Listenerfv(AL.AL_POSITION, [position.X, position.Y, position.Z]);
        AL.Listenerfv(AL.AL_VELOCITY, [cameraVel.X, cameraVel.Y, cameraVel.Z]);
        AL.Listenerfv(AL.AL_ORIENTATION, [orientation.X, orientation.Y, orientation.Z, up.X, up.Y, up.Z]);
    }

    void DisposeFinishedSources()
    {
        for (int i = ActiveSources.Count - 1; i >= 0; i--)
        {
            var s = ActiveSources[i];

            if (s.Finished())
            {
                s.Dispose();
                ActiveSources.RemoveAt(i);
            }
        }
    }
}