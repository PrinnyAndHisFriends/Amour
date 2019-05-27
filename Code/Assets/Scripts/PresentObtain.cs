using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentObtain : MonoBehaviour {

	bool fly = false;
	Vector3 tarPos;
	Vector3 srcPos;
	float timer=0f;
	float flyTime = 1f;
	// Use this for initialization
	void Start () {
		Debug.Log ("ui start");
		this.GetComponent<Animation> ().Play ("presentFly");
	}

	public void flyStart(Vector3 tar){
		tarPos = tar;
		srcPos = this.transform.position;
		fly = true;
	}

	// Update is called once per frame
	void Update () {
		if (fly) {
			this.transform.position = Vector3.Lerp (srcPos, tarPos, timer / flyTime);
			timer += Time.deltaTime;
			if (timer > flyTime) {
				fly = false;
			}
		}

		
	}
}
