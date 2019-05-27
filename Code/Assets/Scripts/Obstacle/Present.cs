using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Present : Obstacle {
    public bool firstGet = false;
    public bool isInLeft = false;
    public int number;
    SpriteRenderer mr;

    private void Awake()
    {
        mr = GetComponent<SpriteRenderer>();
        mr.sprite = PresentSystem.instance.GetSprite();
        number = PresentSystem.instance.GetNumber();
		transform.localScale *= 2f;
    }

    protected override void OnFireballEnter2D(FireballBehavior fb)
    {
        Destroy(this.gameObject);
    }

    protected override void OnPlayerEnter2D(PlayerBehavior pb)
    {
        if (!firstGet)
        {
            firstGet = true;
            if (!UIObstacleSystem.instance.AddPresentToQueue(this))
            {
                Debug.Log("ERROR");
                Destroy(this.gameObject);
            }
        }
        else
        {
            PresentSystem.instance.PlayerGetPresent(this);
            Destroy(this.gameObject);
        }
    }

    protected override void OnDestroyerEnter2D()
    {
        Destroy(this.gameObject);
    }

    public void OnGetPresent(Vector3 uiPoint)
    {
		//Vector3 tarPoint = Camera.main.WorldToScreenPoint (uiPoint);
		//print (tarPoint);
		Transform canvas = GameObject.Find ("Canvas").transform;
		GameObject uiPresent = GameObject.Instantiate( Resources.Load ("Prefab/uiPresent"),this.transform.position,Quaternion.identity,canvas) as GameObject;
		uiPresent.GetComponent<Image> ().sprite = mr.sprite;

		uiPresent.GetComponent<PresentObtain> ().flyStart (uiPoint+new Vector3(0,3f,0));
		//print (uiPresent.transform.position);
		//uiPresent.GetComponent<RectTransform> ().position = tarPoint;
		//GameObject.Instantiate (uiPresent);
        Debug.LogError("OnGetPresent" + uiPoint);
    }

    IEnumerator MoveTo(Vector3 uiPoint, int time)
    {
        float nowTime = 0f;
        while (nowTime < time)
        {
            nowTime += Time.fixedDeltaTime;
            transform.Translate(uiPoint * nowTime / time);
            yield return new WaitForFixedUpdate();
        }

    }
}
