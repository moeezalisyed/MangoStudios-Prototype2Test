using UnityEngine;
using System.Collections;

public class TracerBullet : MonoBehaviour {

	private Player t;
	private Boss m;
	private float speed;
	private TracerBulletModel model;
	private float clock;

	// Use this for initialization
	public void init (Player target, Boss owner) {
		this.name = "Tracer Bullet";
		t = target;
		m = owner;
		speed = m.speed;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<TracerBulletModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		playerbody.isTrigger = true;
		transform.localScale = new Vector3 (.35f, .35f, 1);
		clock = 0;


	}
	
	// Update is called once per frame
	void Update () {
		clock = clock + Time.deltaTime;
		if (clock <= 2) {
				if ((t.getY () - this.transform.position.y <= 0)) {
					float angle = Mathf.Rad2Deg * Mathf.Acos (Mathf.Abs (t.getY () - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow ((t.getX () - this.transform.position.x), 2) + Mathf.Pow ((t.getY () - this.transform.position.y), 2)));
					float sign = (t.getX () - this.transform.position.x) / Mathf.Abs (t.getX () - this.transform.position.x);
					transform.localEulerAngles  = new Vector3 (0, 0, 180 + (sign * (angle)));
				} else if ((t.getY () - this.transform.position.y > 0)) {
					float angle = Mathf.Rad2Deg * Mathf.Acos (Mathf.Abs (t.getY () - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow ((t.getX () - this.transform.position.x), 2) + Mathf.Pow ((t.getY () - this.transform.position.y), 2)));
					float sign = (t.getX () - this.transform.position.x) / Mathf.Abs (t.getX () - this.transform.position.x);
					transform.eulerAngles = new Vector3 (0, 0, 0 + (sign * (angle) * -1));
				} 
		}
		
		transform.Translate (Vector3.up * Time.deltaTime * speed);

		if (this.transform.position.x > 7 || this.transform.position.x < -7 || this.transform.position.y > 5 || this.transform.position.y < -5) {
			Destroy (this.gameObject);
		}
	}
}
