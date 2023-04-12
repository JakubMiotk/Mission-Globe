using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public string numberOfCountries;
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = string.Format("{0}/{1}", PlayerPrefs.GetInt(name).ToString(), numberOfCountries);
    }

}
