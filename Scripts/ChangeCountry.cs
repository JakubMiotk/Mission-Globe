using UnityEngine;
using TMPro;

public class ChangeCountry : MonoBehaviour
{
    public GMCountriesPicking guessmanager;
    void Update()
    {
        string countryname;
        string temp = guessmanager.CountryToGuess;
        countryname = temp.Replace('_',' ');
        GetComponent<TextMeshProUGUI>().text = countryname;
    }
}
