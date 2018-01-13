using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeysCount : MonoBehaviour
{

    public static int KeyQuantity = 0;
    UnityEngine.UI.Text text;
    public static UnityEngine.UI.Text interaction;
    public static UnityEngine.UI.Text interactionInfo;

    void Start ()
    {
        text = FindObjectsOfType<UnityEngine.UI.Text>().Where(c => c.name == "lblKey").First();
        interaction = FindObjectsOfType<UnityEngine.UI.Text>().Where(c => c.name == "lblInteract").First();
        interactionInfo = FindObjectsOfType<UnityEngine.UI.Text>().Where(c => c.name == "lblInteractInfo").First();

        interaction.gameObject.SetActive(false);
        interactionInfo.gameObject.SetActive(false);
    }
	
	void Update ()
    {
        text.text = string.Concat(KeyQuantity, "/5");
    }

    public static void EnableFinishInteraction(bool active)
    {
        interaction.gameObject.SetActive(active);
    }

    public static void validateFinish()
    {
        if (KeyQuantity < 5)
        {
            interactionInfo.gameObject.SetActive(true);

        }
        else
        {
            Cursor.visible = true;
            SceneManager.UnloadSceneAsync("Scene");
            SceneManager.LoadSceneAsync("GameOver");
        }
    }
}
