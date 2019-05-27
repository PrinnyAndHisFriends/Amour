using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlableObstacle : NormalObstacle
{
    public void Execute()
    {
        if (gameObject != null)
            Destroy(this.gameObject);
    }

    public void Cancel()
    {
    }

    protected override void OnStart()
    {
        transform.localScale *= 0.9f;
    }

    protected override void OnFireballEnter2D(FireballBehavior fb)
    {
        fb.Hit();
    }
}