using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fondu : MonoBehaviour
{
    private Image image;

    void Start()
    {
        image = GetComponent<Image>();
        DemarreFonduAuJeu();
    }

    public void DemarreFonduAuJeu()
    {
        StartCoroutine(FonduAuJeu());
    }

    IEnumerator FonduAuJeu()
    {
        Color col = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        image.color = col;
        for (float a = 1.0f; a > 0.0f; a -= 0.05f)
        {
            col.a = a;
            image.color = col;
            yield return new WaitForSeconds(.02f);
        }
        col.a = 0.0f;
        image.color = col;
        Destroy(image);
    }
}