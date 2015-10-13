using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour{
	private MovementController movController;
	public float laserSpeed ;
	public float laserDamage = 100f;
	public string name;

	public string Name {
		get {
			return name;
		}
		set {
			name = value;
		}
	}
	
	public void Start(){
		movController = new MovementController (this.gameObject);
		movController.MovementSpeed = this.laserSpeed;
		movController.ObjectToMoveCurrentPosition = this.transform.position;
		movController.defineWorldBounds (Camera.main);
		fireLaser ();
	}	
	//this method will capture the laser behaviour
	public void hit(){
		Destroy (this.gameObject);
	}
	//return the damage 
	public float getLaserDamage(){
		return laserDamage;
	}
	// FIRES THE LASER!
	public void fireLaser(){
		//Debug.Log("FIRE THE \"LASER\"");
		//movController.moveObject (0f, laserSpeed);
		this.rigidbody2D.velocity = new Vector3(0f,laserSpeed,0f);
	}
	/*
	 * THIS METHOD CAN BE USED TO CHANGE THE SPRITES TO THE EXPLITION
	 */
	void OnTriggerEnter2D(Collider2D collider){
		//Debug.Log ("LASER HIT BITCH!");
		//this.rigidbody2D.velocity = new Vector3(0f,0f,0f);
		//this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> ("Player")[1];	

	}


}
