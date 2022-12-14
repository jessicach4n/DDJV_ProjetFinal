using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ui : MonoBehaviour
{
    public mouvementRadish radish;
    public TextMeshProUGUI nbMaxPies;
    public TextMeshProUGUI nbLivesText;
    public TextMeshProUGUI nbPiesText;
    // Start is called before the first frame update
    void Start()
    {
        setTexts();
    }

    // Update is called once per frame
    void Update()
    {
        setTexts();
    }

    private void setTexts()
    {
        nbMaxPies.text = "/" + radish.maxPies.ToString();
        nbLivesText.text = radish.nbLives.ToString();
        nbPiesText.text = radish.nbPiesCollected.ToString();
    }
}
