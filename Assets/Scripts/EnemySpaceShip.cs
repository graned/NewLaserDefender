using UnityEngine;
using System.Collections;

public class EnemySpaceShip : MonoBehaviour {	
	//private GameObject enemyPrefab;
	private float width, height;
	private MovementController movController;
	public float movementSpeed;
	private float radius;
	private string spaceShipName;
	private GameObject enemyCollider;
	private int enemyType;
	private Transform parentTransform;
	private GameObject laserObject;
//	public float fireRepeatRate;
	private float health;
	public float shotsPerSecond;//TODO: change to private when animation testing is done
	private Animator animator;
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

//	public float FireRepeatRate {
//		get {
//			return fireRepeatRate;
//		}
//		set {
//			fireRepeatRate = value;
//		}
//	}

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
	/*
	public GameObject EnemyPrefab {
		get {
			return enemyPrefab;
		}
		set {
			enemyPrefab = value;
		}
	}*/
	/**
	 * PROPERTIES END
	 * *****************************************************************************
	 */

	//THIS METHOD DRAWS A SPHERE IN A POSITION
	void OnDrawGizmos(){
		//vector3 = transform.position;
		Gizmos.DrawWireSphere (transform.position, radius);
	}
	// Use this for initialization
	void Start () {
		//default values for prefab attribute which helps determing which game object will need to be manipulated by the 
		//movement controller class
		//if (enemyPrefab == null) {
			//enemyPrefab = Instantiate (Resources.Load ("Enemy/Prefab/Enemy")) as GameObject//GameObject.Instantiate(Resources.LoadAssetAtPath("./Entities/Enemies/Enemy", typeof(GameObject)) ) as GameObject;
		//}
		//MOVEMENT CONTROLLER
		movController = new MovementController (this.gameObject);
		movController.MovementSpeed = this.movementSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.defineWorldBounds (Camera.main);
		animator = this.gameObject.GetComponent<Animator> ();
		//adds a collider to the current spaceship game object created
		addCustomCollider (enemyType);



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

	/*//THIS METHOD CHANGES THE ENEMY SPRITE WHEN SPAWING!
	private void changeSprite(Sprite sprite){
		this.GetComponent<SpriteRenderer> ().sprite = sprite;
	}
	*/
	
	// Update is called once per frame
	private float changeMovementOrientation(float movementOrientation){
		return movementOrientation * -1;
	}
	void Update () {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Idle")) {
			animator.applyRootMotion=true;

//			if(!this.transform.position.Equals(this.transform.parent.position)){
//				Debug.Log ("game object x: " + this.transform.parent.position.x);
//				Debug.Log ("animator x: " + animator.transform.position.x);
//
//			}

			//this.transform.position = new Vector2(this.transform.parent.position.x, this.transform.parent.position.y);

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

		//foreach(Transform t in transParent){
		//this line creates a new isntance of the game object enemyPrefab, which is initialized from the UI
		Vector3 position = transParent.transform.position;
		position.z = 1;
		//GameObject laser = Instantiate (laserPrefab, position, Quaternion.identity) as GameObject;
		//NO NEED TO ESTABLISH A PARENT FOR THE BEAM!.
		//adds laser object to the spaceship
		laserObject = Instantiate (Resources.Load ("Enemy/Prefab/enemyLaserBeam"),position, Quaternion.identity) as GameObject;
		LaserController laser = laserObject.GetComponent<LaserController> ();
		laser.laserSpeed = laser.laserSpeed * -1;
		laser.LaserOrign = "ENEMY_LASER";

		//Instantiate (laserObject, position, Quaternion.identity);
		//assignes the new instance created a parent
		//laser.transform.parent = laserContainer.transform;
		
		//}
	}
}
