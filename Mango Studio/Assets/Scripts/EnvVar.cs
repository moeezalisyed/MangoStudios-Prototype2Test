using UnityEngine;
using System.Collections;

public class EnvVar : MonoBehaviour {

	private EnvVarModel model;
	private GameManager owner;

	private int health;
	

	// Use this for initialization
	public void init (GameManager m) {
		owner = m;
		this.name = "EnvVar";
		this.health = 5;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.

		model = modelObject.AddComponent<EnvVarModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D envbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D envrbody = gameObject.AddComponent<Rigidbody2D> ();
		envbody.isTrigger = true;
		envrbody.gravityScale = 0;
		model.offset ();
		//transform.localScale = new Vector3 (.35f, .35f, 1);
		//transform.localPosition -= new Vector3(-2f, -2f, 0);
		//this.owner.m.envFolder.Add (this.model.gameObject);

	}

	// Update is called once per frame
	void Update () {
		
	}

	void doDamage(int x){
		this.health -= x;
		if (health <= 0) {
			this.killThisEnv ();
		}
	}

	void killThisEnv(){
		//Kill this and spawn a new one somewher else
		this.owner.spawnNewEnv();
		Destroy(this.gameObject);
	}


	void OnTriggerEnter2D(Collider2D other){
		//print ("entered collider in boss bullet");
		if (other.tag == "Player" || other.tag == "inviscircle") {
			//Destroy (this.gameObject);
		} else if (other.tag == "Bullet" || other.tag == "SpecialBullet") {
			// When hit by a bullet
		} else if (other.tag == "BossBUllet") {
			// When hit by a bossBullet
			Destroy(other.gameObject);
			this.doDamage (1);

		} else if (other.tag == "BossBeam") {
			// When hit by a BossBeam
			Destroy(other.gameObject);
			this.doDamage (2);
		}
	}
}
