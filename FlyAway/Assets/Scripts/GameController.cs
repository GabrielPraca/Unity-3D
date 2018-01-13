using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameStates
{
    START,
    WAITGAME,
    MAINMENU,
    TUTORIAL,
    INGAME,
    GAMEOVER,
    RANKING
}

public class GameController : MonoBehaviour
{
    public Transform flappy;

    private Vector3 startPositionFlappy;
    private GameStates currentState = GameStates.START;

    public Text score;
    public Text shadowScore;

    private int scoreCount = 0;
    public float timeToRestart;
    private float currentTimeToRestart;

    private GameOverController gameOverController;

    public GameObject mainMenu;
    public GameObject tutorial;

    public GameObject globalSound;

    private bool canPlay;
    private float currentTimeToPlayAgain;

    void Start()
    {
        startPositionFlappy = flappy.position;

        gameOverController = FindObjectOfType<GameOverController>();
    }


    void Update()
    {
        switch (currentState)
        {
            case GameStates.START:
                {
                    scoreCount = 0;
                    score.enabled = false;
                    shadowScore.enabled = false;
                    flappy.position = startPositionFlappy;
                    currentState = GameStates.MAINMENU;
                    mainMenu.SetActive(true);
                    tutorial.SetActive(false);

                    flappy.gameObject.SetActive(false);
                }
                break;
            case GameStates.WAITGAME:
                {
                    flappy.position = startPositionFlappy;
                }
                break;
            case GameStates.MAINMENU:
                {
                    flappy.position = startPositionFlappy;
                }
                break;
            case GameStates.TUTORIAL:
                {
                    currentTimeToRestart += Time.deltaTime;

                    if (currentTimeToRestart > 0.2)
                    {
                        currentTimeToRestart = 0;
                        canPlay = true;
                    }
                    flappy.position = startPositionFlappy;
                }
                break;
            case GameStates.INGAME:
                {
                    score.enabled = true;
                    shadowScore.enabled = true;

                    score.text = scoreCount.ToString();
                    shadowScore.text = scoreCount.ToString();
                }
                break;
            case GameStates.GAMEOVER:
                {
                    currentTimeToRestart += Time.deltaTime;
                    if (currentTimeToRestart > timeToRestart)
                    {
                        currentTimeToRestart = 0;

                        currentState = GameStates.RANKING;

                        score.enabled = false;
                        shadowScore.enabled = false;
                        score.text = scoreCount.ToString();
                        shadowScore.text = scoreCount.ToString();
                        ResetGame();

                        gameOverController.SetGameOver(scoreCount);
                        canPlay = false;
                    }
                }
                break;
            case GameStates.RANKING:
                {
                    flappy.position = startPositionFlappy;

                    score.enabled = false;
                    shadowScore.enabled = false;

                    gameOverController.fadeObject.GetComponent<Animator>().SetBool("StartFade", false);
                    gameOverController.fadeObject.SetActive(false);
                }
                break;
            default:
                {
                    break;
                }
        }
    }

    public void StartGame()
    {
        currentState = GameStates.INGAME;

        scoreCount = 0;
        score.enabled = true;
        shadowScore.enabled = true;

        gameOverController.HideGameOver();
        tutorial.SetActive(false);
    }

    public GameStates GetCurrentState()
    {
        return currentState;
    }

    public void CallGameOver()
    {
        SoundController.PlaySound(soundsGame.hit);
        gameOverController.ShowFade();
        currentState = GameStates.GAMEOVER;
    }

    public void ResetGame()
    {
        flappy.position = startPositionFlappy;
        flappy.GetComponent<PlayerBehaviour>().RestartRotation();

        ObstaclesBehaviour[] pipes = FindObjectsOfType(typeof(ObstaclesBehaviour)) as ObstaclesBehaviour[];

        foreach (ObstaclesBehaviour ob in pipes)
        {
            ob.gameObject.SetActive(false);
        }
    }

    public void AddScore()
    {
        scoreCount++;
        SoundController.PlaySound(soundsGame.point);
    }

    public void CallTutorial()
    {
        currentState = GameStates.TUTORIAL;

        mainMenu.SetActive(false);
        tutorial.SetActive(true);
        flappy.gameObject.SetActive(true);

        gameOverController.HideGameOver();

        SoundController.PlaySound(soundsGame.menu);
    }

    public void CallMainMenu()
    {
        currentState = GameStates.MAINMENU;

        mainMenu.SetActive(true);
        flappy.gameObject.SetActive(false);

        gameOverController.HideGameOver();

        SoundController.PlaySound(soundsGame.menu);
    }

    public void EnableDisableSound()
    {
        globalSound.gameObject.GetComponent<AudioSource>().enabled = !globalSound.gameObject.GetComponent<AudioSource>().enabled;
    }

    public bool CanPlay()
    {
        return canPlay;
    }
}
