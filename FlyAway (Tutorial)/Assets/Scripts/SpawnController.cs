using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private GameController gameController;

    public float maxHeight;
    public float minHeight;

    public float rateSpawn;
    private float currentRateSpawn;

    public GameObject pipesPrefab;

    public int maxSpawnPipes;

    public List<GameObject> pipes;

    void Start ()
    {
        for (int i = 0; i < maxSpawnPipes; i++)
        {
            GameObject tempObj = Instantiate(pipesPrefab) as GameObject;
            pipes.Add(tempObj);
            tempObj.SetActive(false);
        }

        currentRateSpawn = rateSpawn;
        gameController = FindObjectOfType<GameController>() as GameController;
    }
	
	void Update ()
    {
        if (gameController.GetCurrentState() != GameStates.INGAME)
        {
            return;
        }

        currentRateSpawn += Time.deltaTime;

        if(currentRateSpawn > rateSpawn)
        {
            currentRateSpawn = 0;
            Spawn();
        }
	}

    private void Spawn()
    {
        float randHeight = Random.Range(minHeight, maxHeight);
        GameObject tempObj = null;

        for (int i = 0; i < maxSpawnPipes; i++)
        {
            if(pipes[i].activeSelf == false)
            {
                tempObj = pipes[i];
                break;
            }
        }

        if (tempObj != null)
        {
            tempObj.transform.position = new Vector3(transform.position.x, randHeight, transform.position.z);
            tempObj.SetActive(true);
        }
    }
}
