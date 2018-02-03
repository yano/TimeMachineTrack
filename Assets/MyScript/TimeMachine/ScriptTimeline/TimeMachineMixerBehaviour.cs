using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeMachineMixerBehaviour : PlayableBehaviour
{
    public Dictionary<int, double> markerClips;

    public override void OnPlayableCreate(Playable playable)
    {
        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            ScriptPlayable<TimeMachineBehaviour> inputPlayable = (ScriptPlayable<TimeMachineBehaviour>)playable.GetInput(i);
            TimeMachineBehaviour input = inputPlayable.GetBehaviour();
        }
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        // Trackêî
        int inputCount = playable.GetInputCount();

        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);

            if (inputWeight > 0f)
            {
                ScriptPlayable<TimeMachineBehaviour> inputPlayable = (ScriptPlayable<TimeMachineBehaviour>)playable.GetInput(i);
                TimeMachineBehaviour input = inputPlayable.GetBehaviour();

                //if (!input.clipExecuted)
                //{
                if (input.clip.action >= TimeMachineBehaviour.TimeMachineAction.JumpToMarker)
                {
                    if (input.clip.phase >= SingletonTimeMachine.Instance.phase)
                        (playable.GetGraph().GetResolver() as PlayableDirector).time = markerClips[input.clip.phase];
                }
                //}
            }
        }
    }
}
