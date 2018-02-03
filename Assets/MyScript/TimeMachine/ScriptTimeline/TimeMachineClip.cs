using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TimeMachineClip : PlayableAsset, ITimelineClipAsset
{
	public TimeMachineBehaviour.TimeMachineAction action;

    [HideInInspector]
    public int phase = -1;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        TimeMachineBehaviour template = new TimeMachineBehaviour();
        template.clip = this;

        var playable = ScriptPlayable<TimeMachineBehaviour>.Create(graph, template);

        TimeMachineBehaviour clone = playable.GetBehaviour();
        clone.clip = this;

        return playable;
    }
}
