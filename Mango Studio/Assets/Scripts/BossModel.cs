using UnityEngine;
using System.Collections;

public class BossModel : MonoBehaviour {

	private Boss owner;			// Pointer to the parent object.
	public Material mat;

	public void init(Boss owner) {
		this.owner = owner;

		transform.parent = owner.transform;					// Set the model's parent to the gem.
		transform.localPosition = new Vector3(0,0,0);		// Center the model on the parent.
		name = "Boss Model";									// Name the object.

		mat = GetComponent<Renderer>().material;		
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. // Get the material component of this quad object.
		mat.mainTexture = Resources.Load<Texture2D>("Textures/WhiteBox");	// Set the texture.  Must be in Resources folder.
		if (owner.m.bossCurrentLife == 1) {
			mat.color = new Color(1,0,0);
		} else if (owner.m.bossCurrentLife == 2){
			mat.color = new Color(0,1,0);
		} else if(owner.m.bossCurrentLife == 3){
			mat.color = new Color(0,0,1);
		}



	}

	public void changeTexture(int texType){
		mat.mainTexture = Resources.Load<Texture2D>("Textures/Square"+texType);	// Set the texture.  Must be in Resources folder.
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
