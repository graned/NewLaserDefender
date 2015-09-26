using UnityEngine;
using System.Collections;

public class EnemyColliderController : MonoBehaviour {
	private EnemySpaceShip enemySpaceShip;
	public float health = 150f;

	void Start(){
		enemySpaceShip = this.transform.GetComponentInParent<EnemySpaceShip> ();
	}
	void OnTriggerEnter2D(Collider2D collider){
		LaserController laser = collider.gameObject.GetComponent<LaserController> ();
		if (laser) {
			laser.hit();
			health -= laser.getLaserDamage();
			if(health <= 0){
				enemySpaceShip.destroyEnemySpaceShip();
			}
		}	
	}

}
