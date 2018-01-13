using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOffset : MonoBehaviour
{
    private GameController gameController;

    public Material currentMaterial;
    public float speed;
    private float offset;

    void Start()
    {
        gameController = FindObjectOfType<GameController>() as GameController;
    }


    void Update()
    {
        if (gameController.GetCurrentState() != GameStates.INGAME &&
            gameController.GetCurrentState() != GameStates.MAINMENU &&
            gameController.GetCurrentState() != GameStates.TUTORIAL)
        {
            return;
        }

        offset += 0.001f;

        currentMaterial.SetTextureOffset("_MainTex", new Vector2(offset * speed, 0));
    }
}
