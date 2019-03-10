using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class ScoreHelper : MonoBehaviour {


	public int Score;
    private int _currentScore;


    // Use this for initialization
    private void Start () {
	
	}

    // Update is called once per frame
    private void Update () {
	

		if (PhotonNetwork.LocalPlayer !=null && Score != _currentScore)
		{
			_currentScore = Score;
			PhotonNetwork.LocalPlayer.SetScore(Score);
		}

	}
}
