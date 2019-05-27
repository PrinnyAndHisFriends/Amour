using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehavior : MonoBehaviour {

	public float speed = 8f;
	public int type = 0;
	public float timer = 1.0f;

	bool onFly = true;

	// Use this for initialization
	void Start () {
		
	}

	public void Hit(){
		//onFly = false;
		//todo : destroy this
		Destroy(this.gameObject,0.5f);
		this.GetComponent<Animation> ().Play ("fireHit");
		speed = speed / 2f;
		this.tag = "Untagged";
		onFly = false;
	}

	public void FadeOut(){
		//onFly = false;
		//todo : destroy this
		Destroy(this.gameObject,0.5f);
		this.GetComponent<Animation> ().Play ("fireFade");
		speed = speed / 2f;
		this.tag = "Untagged";
		onFly = false;
	}

	void viewUpdate(){
		if (type == 0)
			this.GetComponentInChildren<SpriteRenderer> ().color = new Color (1f, 0.4f, 0.4f, 1f);
		else
			this.GetComponentInChildren<SpriteRenderer> ().color = new Color(0.4f,0.4f,1f,1f);
	}

	// Update is called once per frame
	void Update () {
		viewUpdate ();
		if (true) {
			this.transform.Translate (0, speed * Time.deltaTime, 0);
		}
		timer -= Time.deltaTime;
		if (timer < 0 && onFly) {
			FadeOut ();
		}
	}
}
