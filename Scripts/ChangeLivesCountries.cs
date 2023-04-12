using UnityEngine;
using TMPro;

public class ChangeLivesCountries : MonoBehaviour
{
    public GMCountriesPicking guessmanager;
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = guessmanager.Lifes.ToString();
    }
}
