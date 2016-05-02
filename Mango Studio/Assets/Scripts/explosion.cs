using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {
	public int type;
	private expModel model;
	private float totalTime;
	private float timer;



	// Use this for initialization
	public void init (Vector3 pos, int ptype) {
		type = ptype;
		transform.position = pos;

		if (type == 1) {
			//  when a player his hit
			totalTime = 0.1f;
		} else if (type == 2) {
			//  when boss is hit
			totalTime = 0.1f;
		} else if (type == 3) {
			// when boss is hit by special bullet
			// yellow
			totalTime = 1f;
		} else if (type == 4) {
			// when player dies
			//reddish
			totalTime = 3.0f;
			timer = 0;
		} else if (type == 5) {
			// when boss dies
			//whitish
			timer = 0;
			totalTime = 5.0f;
		}

	
		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<expModel>();					// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		StartCoroutine (timerT ());
	
	}


	
	// Update is called once per frame
	void Update () {
		if (type == 1) {
			//  when a player his hit
			// Don't really need to change it
		} else if (type == 2) {
			//  when boss is hit
			// Don't really need to change it
			this.model.transform.localScale -= new Vector3(0.3f, 0.3f, 1);
		} else if (type == 3) {
			// when boss is hit by special bullet
			// yellow
			// Don't really need to change it
			this.model.transform.localScale -= new Vector3(0.15f, 0.15f, 1);
		} else if (type == 4) {
			// when player dies
			//reddish
			if (timer <= totalTime * 0.67f) {
				transform.localScale += new Vector3 (0.099f, 0.099f, 1f);
			} else {
				transform.localScale -= new Vector3 (0.007f, 0.007f, 1f);
			}
			timer++;
		} else if (type == 5) {
			// when boss dies
			//whitish
			if (timer <= totalTime * 0.67f) {
				transform.localScale += new Vector3 (0.4f, 0.4f, 1f);
			} else {
				transform.localScale -= new Vector3 (0.0085f, 0.0085f, 1f);
			}
			timer++;
		}



	
	}

	IEnumerator timerT (){
		yield return new WaitForSeconds (this.totalTime);
		this.destroy ();
	}

	void destroy(){
		Destroy (this.model.gameObject);
		Destroy (this.gameObject);
	}

}
