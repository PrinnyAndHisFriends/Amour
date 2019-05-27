using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensBehavior : MonoBehaviour {

	public int lensType = 0; // 0:red 1:blue

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void viewUpdate(){
		if (lensType == 0)
			this.GetComponent<SpriteRenderer> ().color = new Color (1f, 0.4f, 0.4f, 1f);
		else
			this.GetComponent<SpriteRenderer>().color = new Color(0.4f,0.4f,1f,1f);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			PlayerBehavior pb = other.GetComponent<PlayerBehavior> ();
			if (pb.shieldType == lensType) {
				//todo: pass
			} else {
				pb.getHit ();
			}
		}
	}

}
