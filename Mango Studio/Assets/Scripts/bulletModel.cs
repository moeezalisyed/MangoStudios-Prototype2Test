using UnityEngine;
using System.Collections;

public class BulletModel : MonoBehaviour {

	private Bullet owner;			// Pointer to the parent object.
	public Material mat;


	public void init(Bullet owner) {
		this.owner = owner;

		transform.parent = owner.transform;					// Set the model's parent to the gem.
		transform.localPosition = new Vector3(0,0,0);		// Center the model on the parent.
		name = "Bullet Model";									// Name the object.

		mat = GetComponent<Renderer>().material;		
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. // Get the material component of this quad object.
		mat.mainTexture = Resources.Load<Texture2D>("Textures/bullet2");	// Set the texture.  Must be in Resources folder.
		mat.color = new Color(1,1,1);

		this.transform.rotation = new Quaternion(owner.transform.rotation .x,owner.transform.rotation.y,owner.transform.rotation.z,owner.transform.rotation.w);
	}
}
