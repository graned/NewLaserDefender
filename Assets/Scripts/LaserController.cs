using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour{
	private MovementController movController;
	public float laserSpeed ;
	
	public void Start(){
		movController = new MovementController (this.gameObject);
		movController.MovementSpeed = this.laserSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.defineWorldBounds (Camera.main);
		fireLaser ();
	}	

	void Update(){
		//fireLaser ();
	}
	
	// FIRES THE LASER!
	public void fireLaser(){
		Debug.Log("FIRE THE \"LASER\"");
		//movController.moveObject (0f, laserSpeed);
		this.rigidbody2D.velocity = new Vector3(0f,laserSpeed,0f);
	}
	/*
	 * THIS METHOD CAN BE USED TO CHANGE THE SPRITES TO THE EXPLITION
	 */
	void OnTriggerEnter2D(Collider2D collider){

	}

}
