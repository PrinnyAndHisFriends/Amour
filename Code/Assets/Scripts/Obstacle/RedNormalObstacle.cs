using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedNormalObstacle : NormalObstacle
{
    // Use this for initialization
    protected override void OnStart()
    {
        blockType = 0;
    }

    protected override void OnFireballEnter2D(FireballBehavior fb)
    {
        if (fb.type == this.blockType)
        {
            fb.Hit();
            //todo: block break
            Destroy(this.gameObject);
        }
        else
        {
            fb.Hit();
        }
    }
}