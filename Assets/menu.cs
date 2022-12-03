using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    // Start is called before the first frame update
    public string premierNiveau;
    public Button boutonDebuter;
    public Button boutonQuitter;

    public void LorsDebuter()
    {
        SceneManager.LoadScene(premierNiveau);
    }

    public void LorsQuitter()
    {
        Application.Quit();
        Debug.Log("Bye!");
    }
}
