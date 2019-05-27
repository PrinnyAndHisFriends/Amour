using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        OnStart();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "fireball")
        {
            OnFireballEnter2D(collision.GetComponent<FireballBehavior>());
        }
        else if (collision.tag == "Player")
        {
            OnPlayerEnter2D(collision.GetComponent<PlayerBehavior>());
        }
        else if (collision.tag == "destroyer")
        {
            OnDestroyerEnter2D();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "fireball")
        {
            OnFireballExit2D(collision.GetComponent<FireballBehavior>());
        }
        else if (collision.tag == "Player")
        {
            OnPlayerExit2D(collision.GetComponent<PlayerBehavior>());
        }
    }
    protected virtual void OnStart()
    {

    }

    protected virtual void OnFireballEnter2D(FireballBehavior fb)
    {
    }

    protected virtual void OnPlayerEnter2D(PlayerBehavior pb)
    {
    }

    protected virtual void OnDestroyerEnter2D()
    {
        Destroy(this.gameObject);
    }

    protected virtual void OnFireballExit2D(FireballBehavior fb)
    {

    }
    protected virtual void OnPlayerExit2D(PlayerBehavior pb)
    {

    }

    public void OnObsAddToPos(Vector3 pos)
    {
        //todo
        Debug.LogError("OnObsAddToPos");
    }
}
