using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLineObstacle : NormalObstacle {

    protected override void OnDestroyerEnter2D()
    {
        UIObstacleSystem.instance.GenNextLine();
        base.OnDestroyerEnter2D();
    }
}


