using UnityEngine;
using System.Collections;

public class EnemyColliderController : MonoBehaviour {
	private EnemySpaceShip enemySpaceShip;
	private EnemySpawner enemySpawner;
	//THIS SHOULD BE MODIFIED BASED ON THE ENEMY'S LEVEL
	public float health = 150f;


	void Start(){
		enemySpawner = this.transform.GetComponentInParent<EnemySpawner> ();
		enemySpaceShip = this.transform.GetComponentInParent<EnemySpaceShip> ();
		enemySpaceShip.Health = this.health;
	}
	void OnTriggerEnter2D(Collider2D collider){
		LaserController laser = collider.gameObject.GetComponent<LaserController> ();

		if (laser){
			//Debug.Log(laser.LaserOrign);
			if(laser.LaserOrign.Equals("PLAYER_LASER")){
				//THIS DESTROYS THE LASER THAT JUST HITTED THE SPACE SHIP
				laser.hit();
				//REDUCES THE HEALTH OF THE SPACESHIP
				enemySpaceShip.Health -= laser.getLaserDamage();
				if(enemySpaceShip.Health <= 0){
					//IF THE HEALTH IS 0 OR LESS THAN CERO THEN DESTROY THE ENEMYSPACESHIP OBJECT

					enemySpawner.enemyGotDestroyNotifier ();
					//enemySpawner.nextFreePosition (enemySpaceShip);
					enemySpaceShip.destroyEnemySpaceShip();
				}
			}
		}	
	}

}
