using UnityEngine;
using System.Collections;

public class EnemySpaceShip : MonoBehaviour {	
	private float width, height;
	private MovementController movController;
	public float movementSpeed;
	private float radius;
	private string spaceShipName;
	private GameObject enemyCollider;
	private int enemyType;
	private Transform parentTransform;
	private GameObject laserObject;
	private float health;
	public float shotsPerSecond;//TODO: change to private when animation testing is done
	private Animator animator;
	private int enemyValue;
	public AudioClip laserAudioClip;
	/**
	 * *********************************************************************
	 * PROPERTIES
	 */ 
	public Transform ParentTransform {
		get {
			return parentTransform;
		}
		set {
			parentTransform = value;
		}
	}

	public int EnemyValue {
		get {
			return enemyValue;
		}
		set {
			enemyValue = value;
		}
	}

	public AudioClip LaserAudioClip {
		get {
			return laserAudioClip;
		}
		set {
			laserAudioClip = value;
		}
	}

	public float ShotsPerSecond {
		get {
			return shotsPerSecond;
		}
		set {
			shotsPerSecond = value;
		}
	}

	public float Health {
		get {
			return health;
		}
		set {
			health = value;
		}
	}

	public MovementController MovController {
		get {
			return movController;
		}
		set {
			movController = value;
		}
	}

	public int EnemyType {
		get {
			return enemyType;
		}
		set {
			enemyType = value;
		}
	}

	public string SpaceShipName {
		get {
			return spaceShipName;
		}
		set {
			spaceShipName = value;
		}
	}

	public float Radius {
		get {
			return radius;
		}
		set {
			radius = value;
		}
	}

	public float MovementSpeed {
		get {
			return movementSpeed;
		}
		set {
			movementSpeed = value;
		}
	}

	public float Height {
		get {
			return height;
		}
		set {
			height = value;
		}
	}

	public float Width {
		get {
			return width;
		}
		set {
			width = value;
		}
	}
	/**
	 * PROPERTIES END
	 * *****************************************************************************
	 */

	//THIS METHOD DRAWS A SPHERE IN A POSITION
	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (transform.position, radius);
	}
	// Use this for initialization
	void Start () {
		//MOVEMENT CONTROLLER
		movController = new MovementController (this.gameObject);
		movController.MovementSpeed = this.movementSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.defineWorldBounds (Camera.main);
		animator = this.gameObject.GetComponent<Animator> ();

		//adds a collider to the current spaceship game object created
		addCustomCollider (enemyType);
		if (enemyValue == 0) {
			throw new UnityException ("Enemy must contain a value");
		}

		laserAudioClip = Resources.Load<AudioClip> ("Sounds/sfx_laser2");
	}

	private void addCustomCollider(int enemyType){
		//DEFAULT VALUE
		if (enemyType == 0) {
			enemyType = 1;
		}
		//THIS LINE DINAMICALLY LOADS A PREFAB NAMED ENEMYCOLLIDER[NUMBER],WHERE THE NUMBER IS THE ID FOR THE DIFFERENT ENEMY SPACESHIP SHAPES
		enemyCollider = Instantiate (Resources.Load ("Enemy/EnemyColliders/enemyCollider"+enemyType),this.transform.position, Quaternion.identity) as GameObject;
		//THE RECENTLY CREATED COLLIDER WILL HAVE AS PARENT THE ENEMY SPACESHIP OBJECT
		enemyCollider.transform.parent = this.transform;
	}
		
	
	// Update is called once per frame
	private float changeMovementOrientation(float movementOrientation){
		return movementOrientation * -1;
	}
	void Update () {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
			animator.applyRootMotion=true;

			movController.ObjectToMoveCurrentPosition = this.transform.position;

			if (Mathf.Round(this.transform.position.x) <= Mathf.Round(movController.CameraMinBoundX) &&
				movController.MovementSpeed < 0){
				movController.MovementSpeed = changeMovementOrientation(movController.MovementSpeed);
			}else if(Mathf.Round(this.transform.position.x) >= Mathf.Round(movController.CameraMaxBoundX) 
				&& movController.MovementSpeed >= 0){
				movController.MovementSpeed = changeMovementOrientation(movController.MovementSpeed);

			}
			movController.moveObject (movController.MovementSpeed, 0f);
			fireLaser ();
		}
	}

	public void fireLaser(){
		float fireProbability = Time.deltaTime * shotsPerSecond;
		if (Random.value < fireProbability) {
			spawnLaser (transform);
			AudioSource.PlayClipAtPoint (LaserAudioClip, transform.position);
		}
	}
	public void destroyEnemySpaceShip(){
		Destroy (gameObject);
		//TODO: propagate a notification that the spaceship has been destroyed
	}


	//PROPOSED: CREATE A MASTER CLASS CALLED SPACESHIP AND MAKE ENEMY AND PLAYER INHERIT FROM IT
	/*
	 * THIS METHOD SPAWNS LASER BEAM
	 */
	private void spawnLaser(Transform transParent){

		//this line creates a new isntance of the game object enemyPrefab, which is initialized from the UI
		Vector3 position = transParent.transform.position;
		position.z = 1;
		//adds laser object to the spaceship
		laserObject = Instantiate (Resources.Load ("Enemy/Prefab/enemyLaserBeam"),position, Quaternion.identity) as GameObject;
		LaserController laser = laserObject.GetComponent<LaserController> ();
		laser.laserSpeed = laser.laserSpeed * -1;
		laser.LaserOrign = "ENEMY_LASER";
	}
}
