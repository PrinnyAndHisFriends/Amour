using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperBehavior : MonoBehaviour {
	float timer = 0f;
	int stage = 0;
	GameObject helper;
	// Use this for initialization
	void Start () {
		helper = GameObject.Find ("freshmanHelper");
	}

	void stage1(){
		helper.GetComponent<Animation> ().Play ("helperIn");
	}

	void stage2(){
		helper.GetComponent<Animation> ().Play ("helper");
	}

	void stage3(){
		helper.GetComponent<Animation> ().Play ("helperOut");
	}


	// Update is called once per frame
	void Update () {
		if (stage == 0 && timer > 0f) {
			stage1 ();
			stage++;
		}
		if (stage == 1 && timer > 5f) {
			stage2 ();
			stage++;
		}
		if (stage == 2 && timer > 10f) {
			stage3 ();
			stage++;
			Destroy (this);
		}
		timer += Time.deltaTime;
		
	}
}
