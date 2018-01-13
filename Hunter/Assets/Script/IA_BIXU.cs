using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IA_BIXU : MonoBehaviour
{
    public static AudioSource audioSource;
    public static AudioClip audioClip;

    public static float minDistance;
    public static float velocity = 1;

    // Use this for initialization
    void Start()
    {
        minDistance = 35;
    }

    // Update is called once per frame
    void Update()
    {
        int numRandomX = UnityEngine.Random.Range(5, 15);
        int numRandomZ = UnityEngine.Random.Range(5, 15);

        transform.LookAt(PLAYER.position);

        if (PLAYER.position.x >= transform.position.x && PLAYER.position.z >= transform.position.z)
            transform.Translate(velocity * Time.deltaTime, 0f, velocity * Time.deltaTime);
        else
            transform.Translate(-velocity * Time.deltaTime, 0f, -velocity * Time.deltaTime);

        if (PLAYER.position.x >= transform.position.x)
            transform.Translate(velocity * Time.deltaTime, 0f, 0f);
        else
            transform.Translate(-velocity * Time.deltaTime, 0f, 0f);

        if (PLAYER.position.z >= transform.position.z)
            transform.Translate(0f, 0f, velocity * Time.deltaTime);
        else
            transform.Translate(0f, 0f, velocity * Time.deltaTime);

        float distance_x = PLAYER.position.x - this.transform.position.x;
        float distance_z = PLAYER.position.z - this.transform.position.z;

        if (distance_x < 0)
            distance_x *= -1;

        if (distance_x > minDistance)
            transform.position = new Vector3(PLAYER.position.x - numRandomX, PLAYER.position.y, PLAYER.position.z - numRandomZ);

        if (distance_z < 0)
            distance_z *= -1;

        if (distance_z > minDistance)
            transform.position = new Vector3(PLAYER.position.x - numRandomX, PLAYER.position.y, PLAYER.position.z - numRandomZ);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource = FindObjectsOfType<AudioSource>().Where(c => c.name == "Roar").First();            
            audioSource.PlayOneShot(audioSource.clip);

            DontDestroyOnLoad(gameObject);
            SceneManager.LoadSceneAsync("GameOver");
        }
    }
}
