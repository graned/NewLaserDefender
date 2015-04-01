using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
	private static int currentLevel = 1;
	public Text levelTextValue;
	public Text levelTextCounter;
	private int seconds;

	private class TimerThread{
		private LevelManager levelManager;

		public TimerThread(LevelManager levelManager){
			this.levelManager = levelManager;
		}

		public void DoWork(){
			while (levelManager.seconds > 0) {
				System.Threading.Thread.Sleep(1000);
				levelManager.seconds--;
			}
		}
	}

	void Start(){
	}

	public void resetLevel(){
		currentLevel = 1;
	}

	public void LoadLevel(string name){
		Application.LoadLevel (name);
	}

	public void Update(){
		//IF LEVEL LOADED IS LEVEL INTRODUCTION THEN CHECK IF THE THREAD FINISHED DEVREMENTING THE COUNTER SO IT AUTOMATICALLY
		//LOADS LEVEL 01
		if (Application.loadedLevel == 1) {
			levelTextCounter.text = seconds.ToString();
			if(seconds == 0){
				//LOADS NEXT LEVEL WHEN THE SECONDS IS EQUIAL TO CERO
				//TODO: LOADS FOR NOW THE WIN SCREEN
				LoadLevel("Win Screen");
			}
		}
	}

	public void OnLevelWasLoaded(int level){
		/*
		 * IF LEVEL LOADED IS EQUAL TO LEVEL INTRODUCTION THEN EXECUTE THE WAIT THREAD
		 */
		if (level == 1) {
			levelTextValue.text = currentLevel.ToString ();
			//WAIT 5 SECONDS TO LOAD THE NEXT LEVEL
			seconds = 5;
			TimerThread tt = new TimerThread(this);
			System.Threading.Thread timerThread = new System.Threading.Thread(tt.DoWork);
			//STARTS THREAD
			timerThread.Start();
		}
	}
	
	public void QuitRequest(){
		//WILL NOT WORK FOR WEB APPLICATIONS
		Application.Quit ();
	}
	
	public void loadNextLevel(int levelIndex){
		//LOADS A LEVEL BASED ON AN INDEX
		Application.LoadLevel (levelIndex);
	}


}
