using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCTRL : MonoBehaviour {

    public static MainCTRL instance;

    public int life;
	public float speed;
	public int lovePower = 0;

	[SerializeField]bool ableMove = true;
	[SerializeField]PlayerBehavior playerL;
	[SerializeField]PlayerBehavior playerR;

	float fireTimer = 0f;
	float fireCDTime = 0.5f;

	int maxLovePower = 5;
	int steps = 0;

	Image imgLoveBar;
	[SerializeField]List<Image> listLife = new List<Image> (3);

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		imgLoveBar = GameObject.Find ("imgLoveBar").GetComponent<Image> ();
		/*
		listLife [0] = GameObject.Find ("heart1").GetComponent<Image> ();
		listLife [1] = GameObject.Find ("heart2").GetComponent<Image> ();
		listLife [2] = GameObject.Find ("heart3").GetComponent<Image> ();
		*/
	}

	public void lifeChange(int value){
		life += value;
		if (life > 3)
			life = 3;
		if (life < 0)
			life = 0;
		if(value<0)
			listLife [life].GetComponent<Animation> ().Play ("heartLost");
		else
			listLife [life-1].GetComponent<Animation> ().Play ("heartRecover");
		if (life <= 0) {
			SceneManager.LoadScene ("gameover");
		}
	}

	public int[] randomMap(){
		int[] code;
		if (steps % 50 != 0 && steps % 5 != 0) {
			steps++;
			return new int[6]{ 0, 0, 0, 0, 0, 0 };
		}
		if (steps % 100 == 0 && steps != 0) {
			code = new int[6]{ 0, 0, 9, 0, 0, 0 };
		} else if (steps % 100 == 50) {
			code = new int[6]{ 0, 0, 0, 0, 0, 9 };
		} else {
			int[] codeLeft;
			int[] codeRight;

			code = new int[6];
			float a = Random.Range (0f, 1f);
			if (a < 0.05f) { // trigger & breakable
				codeLeft = randomOneSide (5);
				codeRight = randomOneSide (8);

			} else if (a < 0.1f) {
				codeLeft = randomOneSide (8);
				codeRight = randomOneSide (5);
			} else {
				codeLeft = randomOneSide ();
				codeRight = randomOneSide ();
			}
			//imposible recorrect
			int leftCheck = impossibleCheck(codeLeft);
			if (leftCheck == 3) {
				codeLeft [0] = 0;
			}
			int rightCheck = impossibleCheck (codeRight);
			if (rightCheck == 3) {
				codeRight [0] = 0;
			}
			if (leftCheck == rightCheck && leftCheck != 0) {
				int b = Random.Range (0, 2);
				if (b == 0)
					codeLeft [0] = 3 - leftCheck;
				else
					codeRight [0] = 3 - rightCheck; 
			}

			//concat
			code [0] = codeLeft [0];
			code [1] = codeLeft [1];
			code [2] = codeLeft [2];
			code [3] = codeRight [0];
			code [4] = codeRight [1];
			code [5] = codeRight [2];
		}
		steps++;
		return code;
	}

	int impossibleCheck(int[] code){
		int count3 = 0;
		int count1 = 0;
		int count2 = 0;
		for (int i = 0; i < 3; i++) {
			if (code [i] == 3) {
				count3++;
			} else if (code [i] == 1 || code [i] == 6) {
				count1++;
			} else if (code [i] == 2 || code [i] == 7) {
				count2++;
			}
		}
		if (count3 == 3)
			return 3;//all is 3
		if (count3 + count2 == 3)
			return 2;
		if (count3 + count1 == 3)
			return 1;
		return 0;
	}

	int[] randomOneSide(int need = 0){
		float a = Random.Range (0f, 1f);
		int[] code = new int[3];
		if (need == 5) {
			code [0] = 5;
			code [1] = randomNormalItem ();
			code [2] = randomNormalItem ();
		} else if (need == 8) {
			code [0] = 8;
			code [1] = randomNormalItem ();
			code [2] = randomNormalItem ();
		} else if (a < 0.1f) {
			code [0] = 4;
			code [1] = 0;
			code [2] = 0;
		} else {
			code [0] = randomNormalItem ();
			code [1] = randomNormalItem ();
			code [2] = randomNormalItem ();
		}

		return code;
	}

	int randomNormalItem(){
		float a = Random.Range (0f, 1f);
		if (a < 0.2f) {
			return 0;
		} else if (a < 0.6f) {
			float b = Random.Range (0f, 1f);
			if (b < 0.4f)
				return 1;
			else if (b < 0.5f)
				return 6;
			else if (b < 0.9f)
				return 2;
			else
				return 7;
		} else {
			return 3;
		}
	}

	void LeftCTRL(){
		if (Input.GetKeyDown (KeyCode.A)) {
			playerL.move (true);
		} else if (Input.GetKeyDown (KeyCode.D)) {
			playerL.move (false);
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			playerL.shieldSwitch ();
			playerR.shieldSwitch ();
		}
	}

	void RightCTRL(){
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			playerR.move (true);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			playerR.move (false);
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (fireTimer <= 0) {
				playerL.fire ();
				playerR.fire ();
				fireTimer = fireCDTime;
			}
		}
	}

	void AutoMove(){
		this.transform.Translate (new Vector3 (0, speed * Time.deltaTime, 0));
	}

	void UIUpdate(){
		float tarLoveBar = (float)lovePower / maxLovePower;
		if (Mathf.Abs (imgLoveBar.fillAmount - tarLoveBar) > 0.01f) {
			if (imgLoveBar.fillAmount > tarLoveBar) {
				imgLoveBar.fillAmount -= Time.deltaTime*0.6f;
			} else {
				imgLoveBar.fillAmount += Time.deltaTime*0.2f;
			}
		} else {
			imgLoveBar.fillAmount = tarLoveBar;
		}
		/*
		if (life < 3)
			listLife [2].fillAmount = 0f;
		else
			listLife [2].fillAmount = 1f;
		if (life < 2)
			listLife [1].fillAmount = 0f;
		else
			listLife [1].fillAmount = 1f;
		if (life < 1)
			listLife [0].fillAmount = 0f;
		else
			listLife [0].fillAmount = 1f;
			*/
	}

	// Update is called once per frame
	void Update () {
		/*
		int[] result = randomMap();
		int result_ = result [0] * 100000 + result [1] * 10000 + result [2] * 1000 + result [3] * 100 + result [4] * 10 + result [5];
		Debug.Log(result_);
		*/

		if (!ableMove)
			return;
		LeftCTRL ();
		RightCTRL ();
		AutoMove ();
		UIUpdate ();
		if (fireTimer > 0)
			fireTimer -= Time.deltaTime;
		if (lovePower >= maxLovePower) {
			if (life < 3) {
				lifeChange (1);
				lovePower -= maxLovePower;
			}
		}

		if(Input.GetKeyDown(KeyCode.L)){
			SceneManager.LoadScene ("gameFinished");
		}
	}
}
