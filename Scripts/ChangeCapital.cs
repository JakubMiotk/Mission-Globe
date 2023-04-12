using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeCapital : MonoBehaviour
{
    public GMCapitalsPicking guessmanager;
    void Update()
    {
        string countryname;
        string temp = guessmanager.CountryToGuess;
        countryname = temp.Replace('_', ' ');
        GetComponent<TextMeshProUGUI>().text = countryname;
    }
}
