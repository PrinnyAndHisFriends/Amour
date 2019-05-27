using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour {
	
	public int blockType = 0; // 0:red 1:blue 2:blank

	// Use this for initialization
	void Start ()
    {
        if (blockType == 0)
            this.GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f, 1f);
        else if (blockType == 1)
            this.GetComponent<SpriteRenderer>().color = new Color(0.4f, 0.4f, 1f, 1f);
        else if (blockType == 2)
            this.GetComponent<SpriteRenderer>().color = new Color(0.6f, 0.4f, 0f, 1f);
    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "fireball") {
			FireballBehavior fb = other.GetComponent<FireballBehavior> ();
			if (fb.type == this.blockType) {
				fb.Hit ();
				//todo: block break
				this.GetComponent<BoxCollider2D>().enabled = false;
				Destroy(this.gameObject,0.5f);
				this.GetComponent<Animation> ().Play ("blockBreak");
			} else {
				fb.Hit ();
			}
		}
		if (other.tag == "Player") {
			PlayerBehavior pb = other.GetComponent<PlayerBehavior> ();
			pb.getHit ();
		}
	}
}
