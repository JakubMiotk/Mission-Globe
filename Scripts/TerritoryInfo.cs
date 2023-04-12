using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TerritoryInfo : MonoBehaviour
{
    public GameObject menu;

    public void UpdateTerritoryData(string countryName, string capitalName, string areaValue, string populationvalue,
        string officialLanguages, string dominantReligion, string currencyValue, string overlodName, Sprite flag, GameObject menutemp)
    {
        GameObject[] textsList = GameObject.FindGameObjectsWithTag("Text");
        GameObject flagObject = GameObject.FindGameObjectWithTag("Flag");
        textsList[0].GetComponent<TextMeshProUGUI>().text = countryName;
        textsList[1].GetComponent<TextMeshProUGUI>().text = capitalName;
        textsList[2].GetComponent<TextMeshProUGUI>().text = areaValue;
        textsList[3].GetComponent<TextMeshProUGUI>().text = populationvalue;
        textsList[4].GetComponent<TextMeshProUGUI>().text = officialLanguages;
        textsList[5].GetComponent<TextMeshProUGUI>().text = dominantReligion;
        textsList[6].GetComponent<TextMeshProUGUI>().text = currencyValue;
        textsList[7].GetComponent<TextMeshProUGUI>().text = overlodName;
        flagObject.GetComponent<Image>().sprite = flag;
        menu = menutemp;
        menu.GetComponent<Resume>().territoryInfoMenu = gameObject;
    }
}
