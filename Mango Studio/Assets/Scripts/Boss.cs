using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

	public int bossType;

	//Boss1 things
	public BossModel model1;
	public float speed;
	public GameManager m;
	private float bulletCooldown;
	private float beamCooldown;
	private float bladeCooldown;
	private float bladeDuration;
	private bool usingBlades;
	private bool charge;
	private bool flicker;
	private float charging;
	public float chargeSpeed;
	private float chargecd;
	public int bossHealth;
	private bool slow;
	private BossBlades blade;
	private float targetx;
	private float targety;
	private float chargeMultiplier = 2f;
	public float xpos;
	public float ypos;

	// sfx
	public AudioClip bossDead;
	public AudioClip bossHit;
	public AudioClip bossHitX;


	//Boss 2 things
	public Boss2Model model2;
	private float attackCD;
	private float recharging;
	public int boss2Health;
	private Player target;
	public bool shieldDead;
	public float shieldcharge;

	// Use this for initialization
	public void init (GameManager owner, int type) {
		bossType = type;
		this.name = "Boss";
		m = owner;

		if (bossType == 1) {
			speed = m.bossSpeed;
			chargeSpeed = m.bossSpeed * this.chargeMultiplier;
			this.bossHealth = 3;
			var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
			model1 = modelObject.AddComponent<BossModel> ();						// Add a marbleModel script to control visuals of the gem.
			model1.init (this);
			this.bossHit = m.bossHit;
			this.bossHitX = m.bossHitX;
			this.bossDead = m.bossDead;
			BoxCollider2D bossbody = gameObject.AddComponent<BoxCollider2D> ();
			Rigidbody2D bossRbody = gameObject.AddComponent<Rigidbody2D> ();
			bossRbody.gravityScale = 0;
			bossbody.isTrigger = true;
			transform.localScale = new Vector3 (2f, 2f, 1);
		} 
		else if (bossType == 2) {
			speed = 1.8f;
			this.speed = m.bossSpeed;
			this.charge = false;
			this.flicker = false;
			chargeSpeed = m.bossSpeed * this.chargeMultiplier;
			this.bossHealth = 100;
			var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
			model2 = modelObject.AddComponent<Boss2Model>();						// Add a marbleModel script to control visuals of the gem.
			model2.init(this);

			BoxCollider2D bossbody = gameObject.AddComponent<BoxCollider2D> ();
			Rigidbody2D bossRbody = gameObject.AddComponent<Rigidbody2D> ();
			bossRbody.gravityScale = 0;
			bossbody.isTrigger = true;

			transform.localScale = new Vector3 (3f, 3f, 1);

			GameObject shieldObject = new GameObject();		
			BossShield shield = shieldObject.AddComponent<BossShield>();
			shield.init (this);
			shield.transform.position = new Vector3(this.transform.position.x+3.5f,this.transform.position.y+2.7f,0);

			shieldDead = false;
			this.bossHit = m.bossHit;
			this.bossHitX = m.bossHitX;
			this.bossDead = m.bossDead;
		}
	}

	/*public void updatePositions(float posx, float posy){
		targetx = posx;
		targety = posy;
	}*/

	IEnumerator flickerRoutine (){
		while (charge) {
			print ("started flickeritng");
			this.model2.mat.mainTexture = Resources.Load<Texture2D>("Textures/boss2d0c");	
			yield return new WaitForSeconds (0.03f);
			this.model2.mat.mainTexture = Resources.Load<Texture2D>("Textures/boss2d0");	
			yield return new WaitForSeconds (0.05f);
		}

	}

	// Update is called once per frame
	void Update () {
		xpos = transform.position.x;
		ypos = transform.position.y;
		targetx = m.GetTargetX ();
		targety = m.GetTargetY ();

		if (bossType == 1) {
			if (!usingBlades) {
				if ((targety - this.transform.position.y) <= 0 && !charge) {
					float angle = Mathf.Rad2Deg * Mathf.Acos (Mathf.Abs (targety - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow ((targetx - this.transform.position.x), 2) + Mathf.Pow ((targety - this.transform.position.y), 2)));
					float sign = (targetx - this.transform.position.x) / Mathf.Abs (targetx - this.transform.position.x);
					transform.eulerAngles = new Vector3 (0, 0, 180 + (sign * angle));
				} else if ((targety - this.transform.position.y) > 0 && !charge) {
					float angle = Mathf.Rad2Deg * Mathf.Acos (Mathf.Abs (targety - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow ((targetx - this.transform.position.x), 2) + Mathf.Pow ((targety - this.transform.position.y), 2)));
					float sign = (targetx - this.transform.position.x) / Mathf.Abs (targetx - this.transform.position.x);
					transform.eulerAngles = new Vector3 (0, 0, 0 + (sign * angle * -1));
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
				if ((Mathf.Sqrt (Mathf.Pow ((targetx - this.transform.position.x), 2) + Mathf.Pow ((targety - this.transform.position.y), 2))) >= 3.5f) {
					slow = true;
					int x = Random.Range (0, 600);
					if (x == 0) {
						if (chargecd <= 0) {
							if (!charge) {
								charge = true;




								charging = 1.3f;
							}
						}
					}
					if ((Mathf.Sqrt (Mathf.Pow ((targetx - this.transform.position.x), 2) + Mathf.Pow ((targety - this.transform.position.y), 2))) >= 5) {
						slow = false;
						if (beamCooldown <= 0) {
							FireBeam ();
							beamCooldown = 1;
						}
						beamCooldown = beamCooldown - Time.deltaTime;
					} else if (bulletCooldown <= 0) {
						FireBullet ();
						bulletCooldown = .6f;
					} else {
						chargecd = chargecd - Time.deltaTime;
						chargecd = chargecd - Time.deltaTime;
						bulletCooldown = bulletCooldown - Time.deltaTime;
					}

				} else if ((Mathf.Sqrt (Mathf.Pow ((targetx - this.transform.position.x), 2) + Mathf.Pow ((targety - this.transform.position.y), 2))) < 3) { 
					if (bladeCooldown <= 0) {
						SpawnBlades ();
						usingBlades = true;
						bladeDuration = 1f;
						bladeCooldown = 1.5f;
					} else if (!usingBlades) {
						bladeCooldown = bladeCooldown - Time.deltaTime;
					}
				}
			}

			if (usingBlades) {
				bladeDuration = bladeDuration - Time.deltaTime;

				if (bladeDuration <= 0) {
					usingBlades = false;
					blade.Retract ();

				}
			}

			if (this.bossHealth <= 0) {
				//Destroy (this.gameObject);
				if (this.bossType == 1) {
					m.PlayEffect (bossDead);
				}

			}
		} 
		else if (bossType == 2) {
			if ((targety - this.transform.position.y) <= 0 && !charge) {
				float angle = Mathf.Rad2Deg * Mathf.Acos (Mathf.Abs (targety - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow ((targetx - this.transform.position.x), 2) + Mathf.Pow ((targety - this.transform.position.y), 2)));
				float sign = (targetx - this.transform.position.x) / Mathf.Abs (targetx - this.transform.position.x);
				transform.eulerAngles = new Vector3 (0, 0, 180 + (sign * angle));
			} else if ((targety - this.transform.position.y) > 0 && !charge) {
				float angle = Mathf.Rad2Deg * Mathf.Acos (Mathf.Abs (targety - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow ((targetx - this.transform.position.x), 2) + Mathf.Pow ((targety - this.transform.position.y), 2)));
				float sign = (targetx - this.transform.position.x) / Mathf.Abs (targetx - this.transform.position.x);
				transform.eulerAngles = new Vector3 (0, 0, 0 + (sign * angle * -1));
			}
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
				int x = Random.Range (0, 5);
				if (!charge) {
					if (attackCD <= 0) {
						attackCD = 2f;
						if (x < 4) {
							FireTracer ();

						} else {
							charge = true;
							StartCoroutine (flickerRoutine ());
							recharging = 1.5f;


						}
					}
				} else {
					recharging = recharging - Time.deltaTime;
					if (recharging <= 0) {
						charge = false;
						StopCoroutine (flickerRoutine ());
						this.model2.mat.mainTexture = Resources.Load<Texture2D>("Textures/boss2d0");	
						if (x < 2) {
							FireAOE ();
						} else {
							FireBurst ();
						}

					}
				}

				attackCD = attackCD - Time.deltaTime;

			}
		}

	}

	public void setSpeeds(){
		this.speed = m.bossSpeed;
		this.chargeSpeed = m.bossSpeed * this.chargeMultiplier;
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

	void FireTracer(){ 						//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject bulletObject = new GameObject();		
		TracerBullet bullet = bulletObject.AddComponent<TracerBullet>();
		bullet.init (m.GetTarget(),this);
		bullet.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
		bullet.transform.rotation = new Quaternion(this.transform.rotation .x,this.transform.rotation.y,this.transform.rotation.z,this.transform.rotation.w);
	}

	void FireAOE(){
		GameObject bulletObject = new GameObject();		
		AOE bullet = bulletObject.AddComponent<AOE>();
		bullet.init (m.GetTarget());
		bullet.transform.position = new Vector3(targetx,targety,0);
	}

	void FireBurst(){
		for(int x = 0; x <= 360; x = x +10){
			GameObject bulletObject = new GameObject();		
			BossBullet bullet = bulletObject.AddComponent<BossBullet>();
			bullet.init (this);
			bullet.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
			bullet.transform.eulerAngles = new Vector3(this.transform.position.x,this.transform.position.y,x);
		}
	}


	public void dealDamage(int damage){
		this.bossHealth -= damage;

		if (bossType == 1) {
			if (this.bossHealth > 90 && this.bossHealth <= 100) {
				model1.changeTexture (0);
			} else if (this.bossHealth > 80 && this.bossHealth <= 90) {
				model1.changeTexture (1);
			} else if (this.bossHealth > 70 && this.bossHealth <= 80) {
				model1.changeTexture (2);
			} else if (this.bossHealth > 60 && this.bossHealth <= 70) {
				model1.changeTexture (3);
			} else if (this.bossHealth > 50 && this.bossHealth <= 60) {
				model1.changeTexture (4);
			} else if (this.bossHealth > 40 && this.bossHealth <= 50) {
				model1.changeTexture (5);
			} else if (this.bossHealth > 30 && this.bossHealth <= 40) {
				model1.changeTexture (6);
			} else if (this.bossHealth > 20 && this.bossHealth <= 30) {
				model1.changeTexture (7);
			} else if (this.bossHealth > 10 && this.bossHealth <= 20) {
				model1.changeTexture (8);
			} else {
				//model.changeTexture (9);
			}
			//print ("bosshealth: " + bossHealth);
		}
		if (bossType == 2) {
			if (this.bossHealth > 90 && this.bossHealth <= 100) {
				model2.changeTexture (0);
			} else if (this.bossHealth > 80 && this.bossHealth <= 90) {
				model2.changeTexture (1);
			} else if (this.bossHealth > 70 && this.bossHealth <= 80) {
				model2.changeTexture (2);
			} else if (this.bossHealth > 60 && this.bossHealth <= 70) {
				model2.changeTexture (3);
			} else if (this.bossHealth > 50 && this.bossHealth <= 60) {
				model2.changeTexture (4);
			} else if (this.bossHealth > 40 && this.bossHealth <= 50) {
				model2.changeTexture (5);
			} else if (this.bossHealth > 30 && this.bossHealth <= 40) {
				model2.changeTexture (6);
			} else if (this.bossHealth > 20 && this.bossHealth <= 30) {
				model2.changeTexture (7);
			} else if (this.bossHealth > 10 && this.bossHealth <= 20) {
				model2.changeTexture (8);
			} else {
				//model.changeTexture (9);
			}
		
		
		
		
		}
	}

	void OnGUI(){
		GUI.Box(new Rect (190, 28, 200, 33), m.bossText);
		if (this.bossHealth > 30) {			
			GUI.color = Color.green;
		} else {
			GUI.color = Color.red;
		}
		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
		GUI.skin.box.fontSize = 25;
		string s = "";

//		for (int i = 0; i < this.bossHealth / 10; i++) {
//		
//			s += "I";
//
//		}
		int index =  this.bossHealth/10;
		if(index > 0){
		//GUI.Box(new Rect (250, 28, 200, 33), m.bossText);
		GUI.Box(new Rect (220, 55, 200, 50), Resources.Load<Texture>("Textures/bar"+index));
		}
		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);

		//GUI.Box(new Rect(targetPos.x-40, Screen.height-targetPos.y-80, 100, 25), s);

		GUI.color = Color.white;
		GUI.skin.box.fontSize = 12;
		GUI.skin.box.alignment = TextAnchor.MiddleCenter;

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Bullet") {
			this.dealDamage (2);
			//if (this.bossType == 1) {
				m.PlayEffect (bossHit);
			//}
		} else if (other.name == "SpecialBullet") {
			print("Did special damage");
			this.dealDamage (4);
		//	if (this.bossType == 1) {
				m.PlayEffect (bossHitX);
		//	}
		}
	}

	public void giveFullHealth(){
		this.bossHealth = 100;
		this.dealDamage (0);
	}
}