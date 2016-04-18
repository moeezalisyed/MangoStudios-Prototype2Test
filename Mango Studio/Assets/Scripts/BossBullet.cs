using UnityEngine;
using System.Collections;

public class BossBullet : MonoBehaviour {
	
	private BossBulletModel model;
	private float speed;

	// Use this for initialization
	void Start () {
		this.name = "BossBullet";
		speed = 6;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<BossBulletModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		playerbody.isTrigger = true;
		transform.localScale = new Vector3 (.5f, .5f, 1);
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.up * Time.deltaTime * speed);

		if (this.transform.position.x > 7 || this.transform.position.x < -7 || this.transform.position.y > 5 || this.transform.position.y < -5) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		print ("entered collider in boss bullet");
		if (other.tag == "Player") {
			Destroy (this.gameObject);
		}
	}
}
