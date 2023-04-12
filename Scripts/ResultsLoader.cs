using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ResultsLoader : MonoBehaviour
{
    public GameObject NACountries;
    public GameObject SACountries;
    public GameObject AFCountries;
    public GameObject ASIACountries;
    public GameObject EUCountries;
    public GameObject OCCountries;
    public GameObject NATerritories;
    public GameObject SATerritories;
    public GameObject AFTerritories;
    public GameObject ASIATerritories;
    public GameObject EUTerritories;
    public GameObject OCTerritories;
    public GameObject BorderImage;
    public GuessManager_Borders gmBorders;
    public GameObject image;
    private TextMeshProUGUI countryName;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        //Find current gamemode and enable compatible list
        GameMode(NACountries);
        GameMode(SACountries);
        GameMode(AFCountries);
        GameMode(EUCountries);
        GameMode(ASIACountries);
        GameMode(OCCountries);
        GameMode(NATerritories);
        GameMode(SATerritories);
        GameMode(AFTerritories);
        GameMode(ASIATerritories);
        GameMode(EUTerritories);
        GameMode(OCTerritories);
        FindNotGuessed();
    }
    void FindNotGuessed()
    {
        //foreach not guessed object finds correct TMPro and change it's color 
        for (int i = 0; i < gmBorders.countriesList.countries.Count; i++)
        {
            string name = gmBorders.countriesList.countries[i].countryBorder.texture.name;
            GameObject country = GameObject.Find(name);
            countryName = country.GetComponent<TextMeshProUGUI>();
            countryName.color = Color.red;
        }
    }
    public void LoadBorder(GameObject holder)
    {
        gmBorders.clickSound.Play();
        BorderImage.SetActive(true);
        for (int i =0 ; i<gmBorders.countryBorders.Length ; i++)
        {
            if (gmBorders.countryBorders[i].texture.name == holder.name) 
            {
                //Scaling images base on CountryScales JSON
                image.GetComponent<Image>().sprite = gmBorders.countryBorders[i];
                if (gmBorders.resize)
                {
                    image.GetComponent<RectTransform>().localScale = new Vector3(gmBorders.countryScalesx[i], gmBorders.countryScalesy[i] /2 +0.5f, 0);
                }            
                else image.GetComponent<RectTransform>().localScale = new Vector3(gmBorders.countryScalesx[i], gmBorders.countryScalesy[i], 0);
            }
        }    
    }
    public void CloseBorderImage()
    {
        gmBorders.clickSound.Play();
        BorderImage.SetActive(false);
    }
    void GameMode(GameObject cHolder)
    {
        if(Menu.gameMode == cHolder.name + "Borders")
        {
            cHolder.SetActive(true);
        }
    }
}
