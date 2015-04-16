using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour{
	private MovementController movController;
	public float laserSpeed = 15f;
	
	public void Start(){
		movController = new MovementController (this.gameObject);
		movController.MovementSpeed = this.laserSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.defineWorldBounds (Camera.main);
	}	

	void Update(){
		fireLaser ();
	}
	
	// FIRES THE LASER!
	public void fireLaser(){
		Debug.Log("FIRE THE \"LASER\"");
		movController.moveObject (0f, laserSpeed);
	}


}
