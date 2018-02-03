using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonTimeMachine : SingletonForTimeMachine<SingletonTimeMachine>
{
    public int phase = 0;

    void Awake()
    {

    }

}
