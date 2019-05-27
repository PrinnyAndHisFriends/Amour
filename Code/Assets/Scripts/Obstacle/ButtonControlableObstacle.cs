using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControlableObstacle : Obstacle
{
    public ControlableObstacle obj;

    protected override void OnPlayerEnter2D(PlayerBehavior pb)
    {
        obj.Execute();
    }

    protected override void OnPlayerExit2D(PlayerBehavior pb)
    {
        //obj.Cancel();
    }
}
