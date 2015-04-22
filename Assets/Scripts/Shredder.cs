using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {
	/*
	 * DESTROYS THE OBJECTS THAT COLIED WITH THIS GAME OBJECT
	 */
	void OnTriggerEnter2D(Collider2D collider){
		//THIS METHOD CALL WILL DESTROY THE OBJECTS THAT COLLIDE.
		Debug.Log ("laser explotion");
		collider.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Player/laserExplotion.png");
		//Destroy (collider.gameObject);
	}
}
