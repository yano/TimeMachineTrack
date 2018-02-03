using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TimeMachineBehaviour : PlayableBehaviour
{
    public enum TimeMachineAction
    {
        Marker,
        JumpToMarker,
    }

    public TimeMachineClip clip;
}
