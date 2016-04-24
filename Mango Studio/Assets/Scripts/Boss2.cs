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

	public void init (GameManager owner) {
		this.name = "Boss";

		m = owner;
		speed = m.bossSpeed;
		chargeSpeed = m.bossSpeed*4;
		this.bossHealth = 100;
		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<BossModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);
		this.bossHit = m.bossHit;
		this.bossHitX = m.bossHitX;
		this.bossDead = m.bossDead;
		BoxCollider2D bossbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D bossRbody = gameObject.AddComponent<Rigidbody2D> ();
		bossRbody.gravityScale = 0;
		bossbody.isTrigger = true;
		target = m.currentplayer;

		transform.localScale = new Vector3 (1.2f, 1.2f, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
