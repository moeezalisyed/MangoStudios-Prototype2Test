using UnityEngine;
using System.Collections;

public class AOE : MonoBehaviour {
	
	private Player t;
	private AOEModel model;
	private float clock;


	// Use thi s for initialization
	public void init (Player target) {
		this.name = "AOE";
		t = target;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<AOEModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		playerbody.isTrigger = true;
		transform.localScale = new Vector3 (1.8f, 1.8f, 1);
	}
	
	// Update is called once per frame
	void Update () {
		clock = clock + Time.deltaTime;
		if (clock >= 2) {
			Destroy (this.gameObject);
		}
	}
}
