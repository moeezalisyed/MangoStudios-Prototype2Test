using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

	public BossModel model;
	public float speed;
	public GameManager m;
	private float bulletCooldown;
	private float beamCooldown;
	private float bladeCooldown;
	private float bladeDuration;
	private bool usingBlades;
	private bool charge;
	private float charging;
	public float chargeSpeed;
	private float chargecd;
	public int bossHealth;
	private bool slow;
	private BossBlades blade;

	// sfx
	public AudioClip bossDead;
	public AudioClip bossHit;
	public AudioClip bossHitX;	

	// Use this for initialization
	public void init (GameManager owner) {
		this.name = "Boss";

		m = owner;
		speed = m.bossSpeed;
		chargeSpeed = m.bossSpeed*2;
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

		transform.localScale = new Vector3 (1.2f, 1.2f, 1);
	}
	
	// Update is called once per frame
	void Update () {
		//model.changeTexture (this.bossHealth/10);
		float playerx = m.currentplayer.transform.position.x;
		float playery = m.currentplayer.transform.position.y;
		if (!usingBlades) {
			if ((playery - this.transform.position.y) <= 0 && !charge) {
				float angle = Mathf.Rad2Deg * Mathf.Acos (Mathf.Abs (playery - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow ((playerx - this.transform.position.x), 2) + Mathf.Pow ((playery - this.transform.position.y), 2)));
				float sign = (playerx - this.transform.position.x) / Mathf.Abs (playerx - this.transform.position.x);
				transform.eulerAngles = new Vector3 (0, 0, 180 + (sign * angle));
				if (slow) {
					transform.Translate (Vector3.up * speed * Time.deltaTime * .5f);
				} else {
					transform.Translate (Vector3.up * speed * Time.deltaTime);
				}
			} else if ((playery - this.transform.position.y) > 0 && !charge) {
				float angle = Mathf.Rad2Deg * Mathf.Acos (Mathf.Abs (playery - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow ((playerx - this.transform.position.x), 2) + Mathf.Pow ((playery - this.transform.position.y), 2)));
				float sign = (playerx - this.transform.position.x) / Mathf.Abs (playerx - this.transform.position.x);
				transform.eulerAngles = new Vector3 (0, 0, 0 + (sign * angle * -1));
				if (slow) {
					transform.Translate (Vector3.up * speed * Time.deltaTime * .5f);
				} else {
					transform.Translate (Vector3.up * speed * Time.deltaTime);
				}
			} else {
				transform.Translate (Vector3.up * chargeSpeed * Time.deltaTime);
				charging = charging - Time.deltaTime;

				if (charging <= 0) {
					charge = false;
					chargecd = 1;
				}
			}
		}
			
		if (!usingBlades) {
			if ((Mathf.Sqrt (Mathf.Pow ((playerx - this.transform.position.x), 2) + Mathf.Pow ((playery - this.transform.position.y), 2))) >= 3) {
				slow = true;
				int x = Random.Range (0, 70);
				if ((Mathf.Sqrt (Mathf.Pow ((playerx - this.transform.position.x), 2) + Mathf.Pow ((playery - this.transform.position.y), 2))) >= 5) {
					slow = false;
					if (beamCooldown <= 0) {
						FireBeam ();
						beamCooldown = 1;
					}
					beamCooldown = beamCooldown - Time.deltaTime;
				} else if (x == 3) {
					if (chargecd <= 0) {
						if (!charge) {
							charge = true;
							charging = .5f;
						}
					}
				} else if (bulletCooldown <= 0) {
					FireBullet ();
					bulletCooldown = .6f;
				} else {
					chargecd = chargecd - Time.deltaTime;
					chargecd = chargecd - Time.deltaTime;
					bulletCooldown = bulletCooldown - Time.deltaTime;
				}

			} else if ((Mathf.Sqrt (Mathf.Pow ((playerx - this.transform.position.x), 2) + Mathf.Pow ((playery - this.transform.position.y), 2))) < 3) { 
				if (bladeCooldown <= 0) {
					SpawnBlades ();
					usingBlades = true;
					bladeDuration = 1f;
					bladeCooldown = 1.8f;
				} else if (!usingBlades) {
					bladeCooldown = bladeCooldown - Time.deltaTime;
				}
			}
		}

		if (usingBlades) {
			bladeDuration = bladeDuration - Time.deltaTime;
		}

		if (bladeDuration <= 0) {
			blade.Retract();
			usingBlades = false;
		}

		if (this.bossHealth <= 0) {
			Destroy (this.gameObject);
			m.PlayEffect (bossDead);
		}

	}

	public void setSpeeds(){
		this.speed = m.bossSpeed;
		this.chargeSpeed = m.bossSpeed * 2;
	}

	void FireBullet(){ 						//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject bulletObject = new GameObject();		
		BossBullet bullet = bulletObject.AddComponent<BossBullet>();
		bullet.init (this);
		bullet.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
		bullet.transform.rotation = new Quaternion(this.transform.rotation .x,this.transform.rotation.y,this.transform.rotation.z,this.transform.rotation.w);
	}

	void FireBeam(){ 						//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject beamObject = new GameObject();		
		BossBeam beam = beamObject.AddComponent<BossBeam>();
		beam.init (this);
		beam.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);

	}

	void SpawnBlades(){
		GameObject bladeObject = new GameObject();		
		blade = bladeObject.AddComponent<BossBlades>();
		blade.init (this);
	}



	public void dealDamage(int damage){
		this.bossHealth -= damage;


		if (this.bossHealth > 90 && this.bossHealth <= 100) {
			model.changeTexture (0);
		} else if (this.bossHealth > 80 && this.bossHealth <= 90 ) {
			model.changeTexture (1);
		} else if (this.bossHealth > 70&& this.bossHealth <= 80) {
			model.changeTexture (2);
		} else if (this.bossHealth > 60&& this.bossHealth <= 70) {
			model.changeTexture (3);
		} else if (this.bossHealth > 50&& this.bossHealth <= 60) {
			model.changeTexture (4);
		} else if (this.bossHealth > 40&& this.bossHealth <= 50) {
			model.changeTexture (5);
		} else if (this.bossHealth > 30&& this.bossHealth <= 40) {
			model.changeTexture (6);
		} else if (this.bossHealth > 20&& this.bossHealth <= 30) {
			model.changeTexture (7);
		} else if (this.bossHealth > 10&& this.bossHealth <= 20) {
			model.changeTexture (8);
		} else {
			//model.changeTexture (9);
		}
		print ("bosshealth: " + bossHealth);
	}

	void OnGUI(){
		if (this.bossHealth > 30) {			
			GUI.color = Color.green;
		} else {
			GUI.color = Color.red;
		}
		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
		GUI.skin.box.fontSize = 25;
		string s = "";

		for (int i = 0; i < this.bossHealth / 10; i++) {
		
			s += "I";

		}

		GUI.Box(new Rect (250, 5, 200, 100), "Boss: \n" + s);

		GUI.color = Color.white;
		GUI.skin.box.fontSize = 12;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Bullet") {
			this.dealDamage (2);
			m.PlayEffect (bossHit);
		} else if (other.name == "SpecialBullet") {
			print("Did special damage");
			this.dealDamage (5);
			m.PlayEffect (bossHitX);
		}
	}

	public void giveFullHealth(){
		this.bossHealth = 100;
	}
}