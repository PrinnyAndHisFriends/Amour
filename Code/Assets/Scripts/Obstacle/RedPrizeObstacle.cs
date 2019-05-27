using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPrizeObstacle : NormalObstacle
{
    // Use this for initialization
    protected override void OnStart()
    {
        blockType = 0;
        prizePoint = 1;
    }

    protected override void OnFireballEnter2D(FireballBehavior fb)
    {
        if (fb.type == this.blockType)
        {
            fb.Hit();
            //todo: block break
            Destroy(this.gameObject);
            MainCTRL.instance.lovePower += this.prizePoint;
        }
        else
        {
            fb.Hit();
        }
    }
}


