using Assets.Scripts.newScripts.test;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTest : ITest
{
    public int positiveFinal;
    public int negativeFinal;
    int score = 0;
    public bool Evaluate(int newState)
    {
        if(newState==positiveFinal)
        {
            score = 1;
            return true;
        }
        if (newState == negativeFinal)
        {
            score = 0;
            return true;
        }
        return false;
    }

    public float GetCurrentMark()
    {
        return score;
    }

    public void Start()
    {
        
    }
}
