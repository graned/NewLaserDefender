using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float movementSpeed; //THIS VARIABLE WILL BE CONTROLLED FROM THE EDITOR
	public GameObject spaceShip;
	private MovementController movController;
	public GameObject laserPrefab;
	//public GameObject laserContainer;
	public float repeatRate = 0.2f;
	// Use this for initialization
	void Start () {
		movController = new MovementController (spaceShip);
		movController.ObjectToMoveCurrentPosition = spaceShip.transform.position;
		movController.MovementSpeed = movementSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.defineWorldBounds (Camera.main);
	}
			
	// Update is called once per frame
	void Update () {
		movController.arrowControl ();
		KeyPressed ();
	}	
	private void KeyPressed(){
		if(Input.GetKeyDown(KeyCode.Space)){
			//SPAWN LASER BEAM & FIRE LASER!
			//Debug.Log("FIRE THE \"LASER\"");
			InvokeRepeating("fireLaser",0.0001f,repeatRate);
			//spawnLaser(transform);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke("fireLaser");
		}
	}
	public void fireLaser(){
		spawnLaser(transform);
	}

	/*
	 * THIS METHOD SPAWNS LASER BEAM
	 */
	private void spawnLaser(Transform transParent){
		//foreach(Transform t in transParent){
			//this line creates a new isntance of the game object enemyPrefab, which is initialized from the UI
		Vector3 position = transParent.transform.position;
		position.z -= 5;
		//GameObject laser = Instantiate (laserPrefab, position, Quaternion.identity) as GameObject;
		Instantiate (laserPrefab, position, Quaternion.identity);
		//assignes the new instance created a parent
		//laser.transform.parent = laserContainer.transform;

		//}
	}
	
}
