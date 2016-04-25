using UnityEngine;
using System.Collections;

public class Boss2 : MonoBehaviour {

	public Boss2Model model;
	public GameManager m;
	private float bulletCooldown;
	private float AOECooldown;
	private float beamCooldown;
	private float charging;
	public int bossHealth;
	private bool slow;
	private Player target;
	private float targetx;
	private float targety;
	public float xpos;
	public float ypos;
	public bool shieldDead;
	public float shieldcharge;

	public void init (GameManager owner) {
		this.name = "Boss2";

		m = owner;
		this.bossHealth = 100;
		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<Boss2Model>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);
		BoxCollider2D bossbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D bossRbody = gameObject.AddComponent<Rigidbody2D> ();
		bossRbody.gravityScale = 0;
		bossbody.isTrigger = true;

		transform.localScale = new Vector3 (2f, 2f, 1);

		GameObject shieldObject = new GameObject();		
		BossShield shield = shieldObject.AddComponent<BossShield>();
		shield.init (this);
		shield.transform.position = new Vector3(this.transform.position.x-.5f,this.transform.position.y+.5f,0);

		shieldDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		xpos = transform.position.x;
		ypos = transform.position.y;

		if (shieldDead) {
			shieldcharge = shieldcharge - Time.deltaTime;
			if (shieldcharge <= 0) {
				shieldDead = false;
				GameObject shieldObject = new GameObject();		
				BossShield shield = shieldObject.AddComponent<BossShield>();
				shield.init (this);
				shield.transform.position = new Vector3(this.transform.position.x-.5f,this.transform.position.y+.5f,0);
			}
		}
		else{
			if (bulletCooldown <= 0) {
				FireTracer ();
				bulletCooldown = .6f;
			} else {
				bulletCooldown = bulletCooldown - Time.deltaTime;
			}
		}
	}

	void FireTracer(){ 						//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject bulletObject = new GameObject();		
		TracerBullet bullet = bulletObject.AddComponent<TracerBullet>();
		bullet.init (m.GetTarget());
		bullet.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
	}
}
