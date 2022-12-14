using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fondu : MonoBehaviour
{
    private SpriteRenderer rendu;

    void Start()
    {
        rendu = GetComponent<SpriteRenderer>();
        DemarreFonduAuJeu();
    }

    public void DemarreFonduAuJeu()
    {
        StartCoroutine(FonduAuJeu());
    }

    public void DemarreFonduAuNoirChangerScene(int sceneIndex)
    {
        StartCoroutine(FonduAuNoirChangerScene(sceneIndex));
    }

    IEnumerator FonduAuJeu()
    {
        Color col = new Color(0.0f, 0.0f, 0.0f, 1.0f);
        rendu.color = col;
        for (float a = 1.0f; a > 0.0f; a -= 0.05f)
        {
            col.a = a;
            rendu.color = col;
            yield return new WaitForSeconds(.02f);
        }
        col.a = 0.0f;
        rendu.color = col;
    }

    IEnumerator FonduAuNoirChangerScene(int sceneIndex)
    {
        Color col = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        rendu.color = col;
        for (float a = 0.0f; a < 1.0f; a += 0.05f)
        {
            col.a = a;
            rendu.color = col;
            yield return new WaitForSeconds(.02f);
        }
        col.a = 1.0f;
        rendu.color = col;
        SceneManager.LoadScene(sceneIndex);
    }
}