using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalObstacle : Obstacle {

    protected int blockType = 2; // 0:red 1:blue 2:blank
    protected int prizePoint = 0;

    protected override void OnPlayerEnter2D(PlayerBehavior pb)
    {
        Destroy(gameObject);
        pb.getHit();
    }

    protected override void OnFireballEnter2D(FireballBehavior fb)
    {
        fb.Hit();
    }
}
