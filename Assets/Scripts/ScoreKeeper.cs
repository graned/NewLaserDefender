using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

	private int score;
	private Text scoreText;

	void Start (){
		scoreText = GetComponent<Text> ();
		reset ();
	}

	// KEEPS TRACK OF SCORE
	public void updateScore(int points){
		score += points;
		scoreText.text = score.ToString ();
	}

	//resets the score
	public void reset(){
		this.score = 0;
		scoreText.text = score.ToString ();
	}
}
