using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{

    public Text score;
    public Text bestScore;

    public Renderer[] medals;

    public GameObject content;
    public GameObject title;
    public GameObject newBestScore;

    public GameObject fadeObject;

    void Start ()
    {
        HideGameOver();
	}
	
	void Update ()
    {
		
	}

    public void SetGameOver(int scoreInGame)
    {
        if(scoreInGame > PlayerPrefs.GetInt("BestScore"))
        {
            newBestScore.SetActive(true);
            PlayerPrefs.SetInt("BestScore", scoreInGame);
        }
        else
        {
            newBestScore.SetActive(false);
        }

        score.text = scoreInGame.ToString();
        bestScore.text = PlayerPrefs.GetInt("BestScore").ToString();

        if(scoreInGame >= 10 && scoreInGame < 25)
        {
            medals[0].enabled = true;
        }

        if (scoreInGame >= 25 && scoreInGame < 35)
        {
            medals[1].enabled = true;
        }

        if (scoreInGame >= 35 && scoreInGame < 50)
        {
            medals[2].enabled = true;
        }

        if (scoreInGame >= 50)
        {
            medals[3].enabled = true;
        }

        content.SetActive(true);
        title.SetActive(true);

        content.GetComponent<Animator>().SetBool("CallGameOver", true);
        title.GetComponent<Animator>().SetBool("CallGameOver", true);
        
        SoundController.PlaySound(soundsGame.die);
    }

    public void HideGameOver()
    {
        content.GetComponent<Animator>().SetBool("CallGameOver", false);
        title.GetComponent<Animator>().SetBool("CallGameOver", false);

        content.SetActive(false);
        title.SetActive(false);

        foreach (Renderer medal in medals)
        {
            medal.enabled = false;
        }

        if(fadeObject.activeSelf)
        {
            fadeObject.GetComponent<Animator>().SetBool("StartFade", false);
        }

        fadeObject.SetActive(false);
    }

    public void ShowFade()
    {
        fadeObject.SetActive(true);
        fadeObject.GetComponent<Animator>().SetBool("StartFade", true);
    }
}
