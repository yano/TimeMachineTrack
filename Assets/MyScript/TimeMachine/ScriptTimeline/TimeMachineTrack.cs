using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(1.0f, 0.0f, 0.0f)]
[TrackClipType(typeof(TimeMachineClip))]
public class TimeMachineTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
		var scriptPlayable = ScriptPlayable<TimeMachineMixerBehaviour>.Create(graph, inputCount);

		TimeMachineMixerBehaviour b = scriptPlayable.GetBehaviour();
		b.markerClips = new System.Collections.Generic.Dictionary<int, double>();

        int phase_Marker = 0;
        int phase_JumpToMarker = 0;

		//This foreach will rename clips based on what they do, and collect the markers and put them into a dictionary
		//Since this happens when you enter Preview or Play mode, the object holding the Timeline must be enabled or you won't see any change in names
		foreach (var c in GetClips())
		{
			TimeMachineClip clip = (TimeMachineClip)c.asset;
			string clipName = c.displayName;

			switch(clip.action)
			{
				case TimeMachineBehaviour.TimeMachineAction.Marker:
                    clipName = "● " + phase_Marker.ToString(); //clip.marker.ToString();
                    clip.phase = phase_Marker;

					//Insert the marker clip into the Dictionary of markers
					if(!b.markerClips.ContainsKey(clip.phase)) //happens when you duplicate a clip and it has the same markerLabel
					{
						b.markerClips.Add(phase_Marker, (double)c.start);
					}

                    phase_Marker++;
                    break;

				case TimeMachineBehaviour.TimeMachineAction.JumpToMarker:
                    clipName = "↩︎ " + phase_JumpToMarker.ToString(); //clip.marker.ToString();
                    clip.phase = phase_JumpToMarker;
                    phase_JumpToMarker++;
					break;
			}

			c.displayName = clipName;
		}

        return scriptPlayable;
    }
}
