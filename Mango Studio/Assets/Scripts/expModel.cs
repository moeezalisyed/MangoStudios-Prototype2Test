using UnityEngine;
using System.Collections;

public class expModel : MonoBehaviour {

	private explosion owner;
	public Material mat;

	// Use this for initialization
	public void init (explosion owner) {
	
		//beign
		this.owner = owner;

		transform.parent = owner.transform;					// Set the model's parent to the gem.
		transform.localPosition = new Vector3(0,0,0);		// Center the model on the parent.
		//name = "Bullet Model";									// Name the object.

		mat = GetComponent<Renderer>().material;		
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. // Get the material component of this quad object.

		int type = owner.type;

		if (type == 1) {
			//  when a player his hit
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/playerhit");
		} else if (type == 2) {
			//  when boss is hit
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/bosshit");
			transform.localScale = new Vector3 (5f, 5f, 1);
		} else if (type == 3) {
			// when boss is hit by special bullet
			// yellow
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/bosshitsp");
			transform.localScale = new Vector3 (5f, 5f, 1);
		} else if (type == 4) {
			// when player dies
			//reddish
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/playerdead");
			transform.localScale = new Vector3 (3f, 3f, 1);
		} else if (type == 5) {
			// when boss dies
			//whitish
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/bossdead");
			transform.localScale = new Vector3 (4f, 4f, 1);
		}





//		if (this.owner.name == "Bullet") {
//			mat.mainTexture = Resources.Load<Texture2D> ("Textures/playerbullet");	// Set the texture.  Must be in Resources folder.
//			//mat.color = new Color(1,1,1);
//		} else if (this.owner.name == "SpecialBullet") {
//			mat.mainTexture = Resources.Load<Texture2D> ("Textures/specialBullet");	// Set the texture.  Must be in Resources folder.
//			//mat.color = new Color(1,0,0);
//		}



		//this.transform.rotation = new Quaternion(owner.transform.rotation .x,owner.transform.rotation.y,owner.transform.rotation.z,owner.transform.rotation.w);

		//end


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
