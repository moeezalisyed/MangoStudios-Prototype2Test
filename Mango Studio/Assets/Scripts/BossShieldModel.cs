using UnityEngine;
using System.Collections;

public class BossShieldModel : MonoBehaviour {

	private BossShield owner;			// Pointer to the parent object.
	public Material mat;

	public void init(BossShield owner) {
		this.owner = owner;

		transform.parent = owner.transform;					// Set the model's parent to the gem.
		transform.localPosition = new Vector3(0,0,0);		// Center the model on the parent.
		name = "Boss Model";									// Name the object.

		mat = GetComponent<Renderer>().material;		
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. // Get the material component of this quad object.
		mat.mainTexture = Resources.Load<Texture2D>("Textures/Boss2shield");	// Set the texture.  Must be in Resources folder.
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
