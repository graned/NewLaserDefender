using UnityEngine;
using System.Collections;

public class EnemySpaceShip : MonoBehaviour {	
	private GameObject enemyPrefab;
	private float width, height;
	private MovementController movController;
	public float movementSpeed;
	private float radius;
	private string spaceShipName;

	/**
	 * *********************************************************************
	 * PROPERTIES
	 */ 
	public MovementController MovController {
		get {
			return movController;
		}
		set {
			movController = value;
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

	public GameObject EnemyPrefab {
		get {
			return enemyPrefab;
		}
		set {
			enemyPrefab = value;
		}
	}
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
		//MOVEMENT CONTROLLER
		if (enemyPrefab == null) {
			enemyPrefab = this.gameObject;//GameObject.Instantiate(Resources.LoadAssetAtPath("./Entities/Enemies/Enemy", typeof(GameObject)) ) as GameObject;
		}
		movController = new MovementController (enemyPrefab);
		movController.MovementSpeed = this.movementSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.defineWorldBounds (Camera.main);
		//Debug.Log (spriteToLoad.Length);
		//enemyPrefab.GetComponent<SpriteRenderer> ().sprite = spriteToLoad[Random.Range(0,4)];

		//TODO AGREGATE THE COLLITION THAT CORRESPONDS TO THE FIGURE! THIS MIGHT HAVE TO BE DONE IN ENEMY SPAWNER CLASS

	}

	//CREATES A NEW ENEMY
	/*public static GameObject createNewEnemy(Transform t){
		return Instantiate (this.enemyPrefab, t.position, Quaternion.identity) as GameObject;
	}*/
	//THIS METHOD CHANGES THE ENEMY SPRITE WHEN SPAWING!
	private void changeSprite(GameObject enemySpaceShip, Sprite sprite){
		enemySpaceShip.GetComponent<SpriteRenderer> ().sprite = sprite;
	}

	//TODO DETECT COLLISION OF LASER!

	// Update is called once per frame
	private float changeMovementOrientation(float movementOrientation){
		return movementOrientation * -1;
	}
	void Update () {
		if (Mathf.Round(this.transform.position.x) <= Mathf.Round(movController.CameraMinBoundX) &&
		    movController.MovementSpeed < 0){
		    	//Debug.Log("movController.CameraMinBoundX:" +Mathf.Round(movController.CameraMinBoundX));
        		//Debug.Log("movController.CameraMaxBoundX:" +Mathf.Round(movController.CameraMaxBoundX));
			    //Debug.Log("this.transform.position.x:" +Mathf.Round(this.transform.position.x));
				movController.MovementSpeed = changeMovementOrientation(movController.MovementSpeed);
		}else if(Mathf.Round(this.transform.position.x) >= Mathf.Round(movController.CameraMaxBoundX) 
		         && movController.MovementSpeed >= 0){
			movController.MovementSpeed = changeMovementOrientation(movController.MovementSpeed);

		}

		//movBoundriesX -= this.transform.position.x;
		/*Debug.Log ("movBoundriesX: " + movBoundriesX);
		Debug.Log("movController.CameraMinBoundX:" + Mathf.Round(movController.CameraMinBoundX));
		Debug.Log("movController.CameraMaxBoundX:" +Mathf.Round(movController.CameraMaxBoundX));
		Debug.Log("this.transform.position.x:" +Mathf.Round(this.transform.position.x));*/
		movController.moveObject (movController.MovementSpeed, 0f);
	}
}
