global using System;
global using System.Collections.Generic;
global using System.Diagnostics;
global using OpenAL;
global using OpenAL.managed;
global using Godot;

namespace OpenALAudio;

public static class Constants
{
    public static string AudioPath = (string)ProjectSettings.GetSetting("audio/default_path.custom", "res://audio");
}

public static class Helper
{
    public static Vector3 PitchYawToVector3(float pitch, float yaw)
    {
        Vector3 direction = new()
        {
            X = Mathf.Cos(pitch) * Mathf.Sin(yaw),
            Y = -Mathf.Sin(pitch),
            Z = Mathf.Cos(pitch) * Mathf.Cos(yaw)
        };

        return direction.Normalized();
    }
}