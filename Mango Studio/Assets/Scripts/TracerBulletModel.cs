using UnityEngine;
using System.Collections;

public class TracerBulletModel : MonoBehaviour {

	private TracerBullet owner;			// Pointer to the parent object.
	public Material mat;
	private float clock;

	public void init(TracerBullet owner) {
		this.owner = owner;
		clock = 0;

		transform.parent = owner.transform;					// Set the model's parent to the gem.
		transform.localPosition = new Vector3(0,0,0);		// Center the model on the parent.
		name = "TracerBullet Model";									// Name the object.

		mat = GetComponent<Renderer>().material;		
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. // Get the material component of this quad object.
		mat.mainTexture = Resources.Load<Texture2D>("Textures/tracerBullet");	// Set the texture.  Must be in Resources folder.
		//mat.color = new Color(0,1,0);
	}
	
	// Update is called once per frame
	void Update () {
		clock = clock + (Time.deltaTime)*2;
		transform.eulerAngles = new Vector3 (0, 0, clock * 90);
	}
}
