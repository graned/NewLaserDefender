using UnityEngine;
using System.Collections;

public class EnemySpaceShip : MonoBehaviour {	
	private Sprite[] spriteToLoad;
	private GameObject enemyPrefab;
	private float width, height;
	private MovementController movController;
	public float movementSpeed;
	private float radius;
	private float movBoundriesX;
	private float movBoundriesY;

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

	public float MovBoundriesX {
		get {
			return movBoundriesX;
		}
		set {
			movBoundriesX = value;
		}
	}

	public float MovBoundriesY {
		get {
			return movBoundriesY;
		}
		set {
			movBoundriesY = value;
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

	public Sprite[] SpriteToLoad {
		get {
			return spriteToLoad;
		}
		set {
			spriteToLoad = value;
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
		//THIS LINE LOADS ALL SPRITES LOCATED IN THE RESOURCES/ENEMY FILE
		spriteToLoad = Resources.LoadAll<Sprite> ("Enemy");
		//MOVEMENT CONTROLLER
		movController = new MovementController (enemyPrefab);
		movController.MovementSpeed = this.movementSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.defineWorldBounds (Camera.main);

		movBoundriesX = this.transform.position.x;
		movBoundriesY = this.transform.position.y;
	}

	//THIS METHOD CHANGES THE ENEMY SPRITE WHEN SPAWING!
	public void changeSprite(int levelEnemies){
		this.GetComponent<SpriteRenderer> ().sprite = spriteToLoad[Random.Range(0,levelEnemies)];
	}

	//CREATES A NEW ENEMY
	/*public static GameObject createNewEnemy(Transform t){
		return Instantiate (this.enemyPrefab, t.position, Quaternion.identity) as GameObject;
	}*/

	//TODO DETECT COLLISION OF LASER!

	// Update is called once per frame
	void Update () {
		//Debug.Log ("enemy parent: " +this.transform.parent);

		//Debug.Log("movController.CameraMinBoundX:" +Mathf.Round(movController.CameraMinBoundX));
		//Debug.Log("movController.CameraMaxBoundX:" +Mathf.Round(movController.CameraMaxBoundX));
		if (Mathf.Round(this.transform.position.x) <= Mathf.Round(movController.CameraMinBoundX) &&
		    movController.MovementSpeed < 0){
		    	//Debug.Log("movController.CameraMinBoundX:" +Mathf.Round(movController.CameraMinBoundX));
        		//Debug.Log("movController.CameraMaxBoundX:" +Mathf.Round(movController.CameraMaxBoundX));
			    //Debug.Log("this.transform.position.x:" +Mathf.Round(this.transform.position.x));
				movController.MovementSpeed *= -1;
		}else if(Mathf.Round(this.transform.position.x) >= Mathf.Round(movController.CameraMaxBoundX) 
		         && movController.MovementSpeed >= 0){
			movController.MovementSpeed *= -1;

		}

		//movBoundriesX -= this.transform.position.x;
		/*Debug.Log ("movBoundriesX: " + movBoundriesX);
		Debug.Log("movController.CameraMinBoundX:" + Mathf.Round(movController.CameraMinBoundX));
		Debug.Log("movController.CameraMaxBoundX:" +Mathf.Round(movController.CameraMaxBoundX));
		Debug.Log("this.transform.position.x:" +Mathf.Round(this.transform.position.x));*/
		movController.moveObject (movController.MovementSpeed, 0f);
	}
}
