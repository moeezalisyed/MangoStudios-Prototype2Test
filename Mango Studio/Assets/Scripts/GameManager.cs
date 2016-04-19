using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int boardWidth, boardHeight; // board size init is in Unity editor
    private GameObject playerFolder;// folders for object organization

    private List<Player> players; // list of all placed players
	public Player currentplayer;
	public List<Player> shadowPlayers = new List<Player>();
    // Beat tracking
    private float clock;
    private float startTime;
    private float BEAT = .5f;
    private int numBeats = 0;
    int playerbeaten = 0;
    int playernum = 0;
	private int playertype;
	public Text HealthText;
	private List<Vector3> shadow;
	private int shadowiterator;
	private Boolean startitr;
	public Boss THEBOSS;
	public Boolean gameover = false;
	public Boolean gamewon = false;


    // Level number

    private int level = 99;


    //button locations
    float trayx = 0;
    float traywidth = 0;
    float trayspace = 0;

    // Sound stuff
    public AudioSource music;
    public AudioSource sfx;

    // Music clips
    private AudioClip idle;
    private AudioClip gametrack;
    private AudioClip winmusic;

    // Sound effect clips
    private AudioClip bossDead;
    private AudioClip bossHit;
    private AudioClip bossHitX;
    public AudioClip abilityon;

    // Use this for initialization
    void Start()
	{
		//set up folder for enemies
		playerFolder = new GameObject ();
		playerFolder.name = "Enemies";
		players = new List<Player> ();
		playertype = 2;
		//addPlayer(0, 1, 0, 0);
		//addPlayer(playertype, 1, -4, -4);
		addPlayer(playertype, 1, -4, -4);
		currentplayer = players [0];
		currentplayer.setCD (0.5f);
		//setHealthText ();
		clock = 0f;
		shadow = new List<Vector3> ();
		shadowiterator = 0;
		startitr = false;

		GameObject bossObject = new GameObject();
		Boss boss = bossObject.AddComponent<Boss>();
		boss.init (this);
		THEBOSS = boss;

		// setting up music
        SoundSetUp();

	}
        
    // Update is called once per frame
    void Update()
    {




		if (this.gamewon == false && THEBOSS.bossHealth <= 0) {
			this.gamewon = true;
		}
		clock += Time.deltaTime;
		currentplayer.model.shadowDirection.Add (currentplayer.direction);
	//	shadow.Add (currentplayer.model.transform.localPosition);
		if (Input.GetKey (KeyCode.RightArrow) ) {
			if (currentplayer.playerType != 2) {
				currentplayer.direction = 3;
				currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
				currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
//				if (currentplayer.transform.position.x > Screen.width) {
//					print ("x width");
//					Vector3 xvec = currentplayer.transform.position;
//					xvec.x = 0;
//					currentplayer.transform.position = xvec;
//				}
			} else{
				if (!currentplayer.usingability) {
					currentplayer.direction = 3;
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
					currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
//					if (currentplayer.transform.position.x > Screen.width) {
//						print ("x width");
//						Vector3 xvec = currentplayer.transform.position;
//						xvec.x = 0;
//						currentplayer.transform.position = xvec;
//					}
				
				
				}
			
			
			}
		} 
		if (Input.GetKey (KeyCode.UpArrow) ) {
			


			//above
			if (currentplayer.playerType != 2) {
				currentplayer.direction = 0;
				currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
				currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
//				if (currentplayer.transform.position.y < 0) {
//					print ("y 0");
//					Vector3 xvec = currentplayer.transform.position;
//					xvec.y = Screen.height;
//					currentplayer.transform.position = xvec;
//				}
			} else {
				if (!currentplayer.usingability) {
					currentplayer.direction = 0;
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
					currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
//					if (currentplayer.transform.position.y < 0) {
//						print ("y 0");
//						Vector3 xvec = currentplayer.transform.position;
//						xvec.y = Screen.height;
//						currentplayer.transform.position = xvec;
//					}

				}


			}
			//below

		}
		if (Input.GetKey (KeyCode.LeftArrow) ){
			
			//above
			if (currentplayer.playerType != 2) {
				currentplayer.direction = 1;
				currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
				currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
//				if (currentplayer.transform.position.x < 0) {
//					print ("x 0");
//					Vector3 xvec = currentplayer.transform.position;
//					xvec.x = Screen.width;
//					currentplayer.transform.position = xvec;
//				}


			} else {
				if (!currentplayer.usingability) {
					currentplayer.direction = 1;
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
					currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
//					if (currentplayer.transform.position.x < 0) {
//						print ("x 0");
//						Vector3 xvec = currentplayer.transform.position;
//						xvec.x = Screen.width;
//						currentplayer.transform.position = xvec;
//					}
				}


			}
			//below
		}
		if (Input.GetKey (KeyCode.DownArrow) ) {
			

			//bove
			if (currentplayer.playerType != 2) {
				currentplayer.direction = 2;
				currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
				currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
//				if (currentplayer.transform.position.y > Screen.height) {
//					print ("y height");
//					Vector3 xvec = currentplayer.transform.position;
//					xvec.y = 0;
//					currentplayer.transform.position = xvec;
//				}

			} else {
				if (!currentplayer.usingability) {
					currentplayer.direction = 2;
					currentplayer.transform.eulerAngles = new Vector3 (0, 0, currentplayer.direction * 90);
					currentplayer.transform.Translate (Vector3.up * 4 * Time.deltaTime);
//					if (currentplayer.transform.position.y > Screen.height) {
//						print ("y height");
//						Vector3 xvec = currentplayer.transform.position;
//						xvec.y = 0;
//						currentplayer.transform.position = xvec;
//					}
				}


			}
			//below

		}


		if (Input.GetKeyDown (KeyCode.Space)) {
			//The next line is just for testing texture 
			//this.THEBOSS.dealDamage (5);
			currentplayer.shoot();
		}
		if (Input.GetKeyDown (KeyCode.Z)) {
			currentplayer.useAbility ();
			PlayEffect(abilityon);
		}

		//setHealthText ();
		if (currentplayer.getHealth () <= 0) {
			Vector3 p = THEBOSS.transform.position;
			float q = p.z;
			THEBOSS.transform.position = new Vector3 (0, 0, q);
			currentplayer.destroy();

			//this.THEBOSS.model.transform.position.y = 0;
			players.Remove(currentplayer);
			shadowPlayers.Add (currentplayer);
			//startitr = true;

			if ( shadowPlayers.Count <= 5) {
				playertype++;
			} else if (shadowPlayers.Count > 5) {
				this.gameOver();
			
			}
			playertype = playertype % 3 ;
			playertype++;
			addPlayer (playertype, 1, -4, -4);
			currentplayer = players [0];
			if (playertype == 3) {
				currentplayer.setCD (.5f);
			}
		}
//		if (startitr){
//			
//
//
//			print ("we got here!" + shadow[0]);
//
//			enemies [1].model.transform.localPosition = shadow [shadowiterator];
//			shadowiterator++;
//			}
    }

	public void addPlayer(int playerType, int initHealth, int x, int y)
	{
		GameObject playerObject = new GameObject();
		Player player = playerObject.AddComponent<Player>();
		player.transform.parent = playerFolder.transform;
		player.transform.position = new Vector3(x, y, 0);
		player.init(playerType, this);
		players.Add(player);
		playernum++;
		player.name = "Player " + players.Count;
	}

	void setHealthText (){
		HealthText.text = "Health: "+currentplayer.getHealth();
	}

	public void gameOver(){
		foreach (Player x in shadowPlayers) {
			Destroy (x.gameObject);
		}
		foreach (Player x in this.players) {
			Destroy (x.gameObject);
		}
		Destroy (THEBOSS.gameObject);
		this.gameover = true;

	}

    private void SoundSetUp()
    {
        // music
/*        idle = Resources.Load<AudioClip>("Music/title song");
        gametrack = Resources.Load<AudioClip>("Music/Main song loop");
        winmusic = Resources.Load<AudioClip>("Music/You Win Song");*/

        // sfx
        bossDead = Resources.Load<AudioClip>("Music/explosion");
        bossHit = Resources.Load<AudioClip>("Music/shoot");
        bossHitX = Resources.Load<AudioClip>("Music/shootX");  //Special bullet when ability is on
        abilityon = Resources.Load<AudioClip>("Music/abilityon");

    }
        // Music section
    public void PlayEffect(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
    }

    public void PlayMusic(AudioClip clip)
    {
        this.music.loop = true;
        this.music.clip = clip;
        this.music.Play();
    }


	void OnGUI(){
		if (this.gameover) {


			GUI.skin.box.alignment = TextAnchor.MiddleLeft;
			GUI.skin.box.fontSize = 25;
			string s = "GAME OVER!";


			GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 50, 200, 100), s);
			s = null;
			GUI.color = Color.white;
			GUI.skin.box.fontSize = 12;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		
		}

		if(this.gamewon){

			foreach (Player x in this.players) {
				Destroy (x.gameObject);
			}
			this.players.Clear ();

			foreach (Player x in this.shadowPlayers) {
				Destroy (x.gameObject);
			}

			this.shadowPlayers.Clear ();

			GUI.skin.box.alignment = TextAnchor.MiddleLeft;
			GUI.skin.box.fontSize = 25;
			string s = "GAME WON!";


			GUI.Box (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 50, 200, 100), s);
			s = null;
			GUI.color = Color.white;
			GUI.skin.box.fontSize = 12;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;



		}
			if (this.currentplayer.model.healthval > 3) {			
				GUI.color = Color.green;
			} else {
				GUI.color = Color.red;
			}
			GUI.skin.box.alignment = TextAnchor.MiddleLeft;
			GUI.skin.box.fontSize = 22;
			 String ss = "";

			for (int i = 0; i < this.currentplayer.model.healthval; i++) {

				ss += "I";

			}

			GUI.Box(new Rect (10, 150, 200, 100), "Player: \n " + ss);

			GUI.color = Color.white;
			GUI.skin.box.fontSize = 12;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		


	}





}
