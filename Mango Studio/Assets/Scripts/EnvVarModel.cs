using UnityEngine;
using System.Collections;

public class EnvVarModel : MonoBehaviour {

	private EnvVar owner;			// Pointer to the parent object.
	public Material mat;

	public void init(EnvVar owner) {
		this.owner = owner;

	//	transform.parent = owner.transform;					// Set the model's parent to the gem.


		//Here we will assign a random position
		System.Random rng = new System.Random ();
		float posx = rng.Next (-8, 8) * 1.0f;
		float posy = rng.Next (-4, 4) * 1.0f;
		transform.position = new Vector3(posx,posy,0);		



		this.name = "EnvVarModel";									// Name the object.

		mat = GetComponent<Renderer>().material;		
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. // Get the material component of this quad object.
		mat.mainTexture = Resources.Load<Texture2D>("Textures/envTex");	// Set the texture.  Must be in Resources folder.
		//mat.color = new Color(1,1,1);
		transform.localScale = new Vector3(0.8f, 0.8f, 1);
		//this.owner.GetComponent<BoxCollider2D>().transform.position = transform.position;
	}

	// Update is called once per frame
	void Update () {

	}

	public void offset(){
		this.owner.GetComponent<BoxCollider2D>().transform.position = transform.position;
	}
}
