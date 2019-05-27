using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveableObstacle : NormalObstacle
{
    public float speed;
    public Vector3[] points;
    private int startPoint;
    private int endPoint;

    private void FixedUpdate()
    {
        transform.Translate(Time.fixedDeltaTime * speed, 0, 0);
        if (transform.localPosition.x > points[endPoint].x || transform.localPosition.x < points[startPoint].x)
        {
            speed *= -1;
        }
    }

    protected override void OnStart()
    {
        blockType = 2;
        prizePoint = 1;

        transform.GetChild(0).transform.Translate(-0.5f,0,0);
        transform.GetChild(2).transform.Translate(0.5f,0,0);

        points = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            points[i] = transform.GetChild(i).transform.position;
        }
        switch (Random.Range(0, 3))
        {
            case 0:
                startPoint = 0;
                endPoint = 1;
                break;
            case 1:
                startPoint = 0;
                endPoint = 2;
                break;
            case 2:
                startPoint = 1;
                endPoint = 2;
                break;
        }
    }
    protected override void OnFireballEnter2D(FireballBehavior fb)
    {
        fb.Hit();
    }
}
