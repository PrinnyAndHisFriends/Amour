using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour {
	public int shieldType; // 0:red 1:blue
	public int line;//0,1,2

	GameObject shield;
	MainCTRL mainCTRL;
	[SerializeField]float lineOffset = 1f;
	[SerializeField]float recoverTime = 1.5f;
	[SerializeField]GameObject fireball;
	float recoverTimer = 0f;

	// Use this for initialization
	void Start () {
		shield = this.transform.Find ("ball").Find ("shield").gameObject;
		mainCTRL = GameObject.Find ("mainController").GetComponent<MainCTRL> ();
	}

	public void getHit(){
		if (recoverTimer > 0)
			return;
		mainCTRL.lifeChange (-1);
		recoverTimer = recoverTime;
	}

	public void move(bool toLeft){
		if (toLeft && line > 0) {
			line--;
			this.transform.Translate (new Vector3 (-lineOffset, 0, 0));
		} else if (!toLeft && line < 2) {
			line++;
			this.transform.Translate (new Vector3 (+lineOffset, 0, 0));
		}
	}

	public void fire(){
		//todo: fire a missile
		fireball.transform.position = this.transform.position;
		fireball.GetComponent<FireballBehavior> ().type = shieldType;
		GameObject.Instantiate (fireball);
	}

	public void shieldSwitch(){
		shieldType = 1 - shieldType;
		//update color

	}

	//private
	void viewUpdate(){
		if (shieldType == 0)
			shield.GetComponent<SpriteRenderer> ().color = new Color (1f, 0.345f, 0.584f, 1f);
		else
			shield.GetComponent<SpriteRenderer>().color = new Color(0.35f,0.89f,1f,1f);
	}

	// Update is called once per frame
	void Update () {
		viewUpdate ();
		if (recoverTimer > 0)
			recoverTimer -= Time.deltaTime;
	}
}
