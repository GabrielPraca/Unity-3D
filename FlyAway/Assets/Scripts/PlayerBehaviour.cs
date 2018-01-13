using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public Transform mesh;
    public float forceFly;
    private Animator animatorPlayer;

    private float currentTimeToAnim;
    private bool inAnim = true;

    private GameController gameController;

    void Start()
    {
        animatorPlayer = mesh.GetComponent<Animator>();
        gameController = FindObjectOfType<GameController>() as GameController;
    }


    void Update()
    {
        animatorPlayer.SetBool("CallFly", inAnim);

        if (Input.GetMouseButtonDown(0) && gameController.GetCurrentState() == GameStates.INGAME &&
            gameController.GetCurrentState() != GameStates.GAMEOVER)
        {
            inAnim = true;

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (mesh.position.y < 4)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * forceFly);
            }

            SoundController.PlaySound(soundsGame.wing);
        }

        else if (Input.GetMouseButtonDown(0) && gameController.GetCurrentState() == GameStates.TUTORIAL)
        {
            if (gameController.CanPlay())
            {
                Restart();
            }
        }

        else if (gameController.GetCurrentState() != GameStates.TUTORIAL)
        {
            inAnim = true;
        }

        if (gameController.GetCurrentState() != GameStates.INGAME &&
            gameController.GetCurrentState() != GameStates.GAMEOVER)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            return;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }

        if (inAnim && gameController.GetCurrentState() != GameStates.TUTORIAL)
        {
            currentTimeToAnim += Time.deltaTime;

            if (currentTimeToAnim > 0.4f)
            {
                currentTimeToAnim = 0;
                inAnim = false;
            }
        }

        if (gameController.GetCurrentState() == GameStates.INGAME)
        {
            //Rotaciona Mesh
            if (GetComponent<Rigidbody2D>().velocity.y < 0)
            {
                mesh.eulerAngles -= new Vector3(0, 0, 2);
                if (mesh.eulerAngles.z < 270 && mesh.eulerAngles.z > 30)
                {
                    mesh.eulerAngles = new Vector3(0, 0, 270);
                }
            }
            else if (GetComponent<Rigidbody2D>().velocity.y > 0)
            {
                mesh.eulerAngles += new Vector3(0, 0, 2);
                if (mesh.eulerAngles.z > 30)
                {
                    mesh.eulerAngles = new Vector3(0, 0, 30);
                }
            }
        }
        else if (gameController.GetCurrentState() == GameStates.GAMEOVER)
        {
            mesh.eulerAngles -= new Vector3(0, 0, 10);
            if (mesh.eulerAngles.z < 270 && mesh.eulerAngles.z > 30)
            {
                mesh.eulerAngles = new Vector3(0, 0, 270);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameController.CallGameOver();
        mesh.eulerAngles = new Vector3(0, 0, 0);
    }

    public void RestartRotation()
    {
        mesh.eulerAngles = new Vector3(0, 0, 0);
    }

    public void Restart()
    {
        gameController.ResetGame();
        gameController.StartGame();
    }
}
