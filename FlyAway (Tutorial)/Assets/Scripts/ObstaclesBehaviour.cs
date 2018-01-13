using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesBehaviour : MonoBehaviour
{
    private GameController gameController;
    public float speed;

    private bool passed;

	void Start ()
    {
        gameController = FindObjectOfType<GameController>() as GameController;
    }

    private void OnEnable()
    {
        passed = false;
    }

    void Update ()
    {
        if(gameController.GetCurrentState() != GameStates.INGAME)
        {
            return;
        }

        transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;

        if(transform.position.x < -15)
        {
            gameObject.SetActive(false);
        }

        if(transform.position.x < gameController.flappy.position.x && !passed)
        {
            passed = true;
            gameController.AddScore();
        }
	}
}
