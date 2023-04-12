using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountryTyping : MonoBehaviour
{
    public GMCountriesTyping gameManager;
    public GameObject inputField;
    public Sprite countryFlag;
    private string[] names = null;
    private bool countryGuessed;
    private string gameModeTag;
    private JSONReader.CountryInfo countryInfo;
    private JSONReader.TerritoryInfo territoryInfo;
    private Collider2D collider;
    private void Start()
    {
        GetGameMode();
        countryGuessed = false;
        if(name.Contains('/')) names = name.Split('/');
        collider = GetComponent<Collider2D>();

    }
    void Update()
    {
        if (!GetComponentInParent<GMCountriesTyping>().gameEnded) CheckForTouch();
        else if (gameManager.gameEndedMenuclone.GetComponent<Resume>().spectatorModeEnabled) CheckForTouchForInfo();
        
    }
    bool CheckForTouch()
    {
        string guess = NormalizeText(inputField.GetComponent<TMP_InputField>().text);
        if (names == null && tag == gameModeTag)
        {
            if ((countryGuessed == false) && (guess == NormalizeName(name)))
            {
                gameManager.goodGuessSound.Play();
                inputField.GetComponent<TMP_InputField>().text = "";
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                countryGuessed = true;
                gameManager.CountryNamesList.RemoveAt(gameManager.CountryNamesList.IndexOf(gameObject));
                gameManager.guessedNumber.text = string.Format("{0}/{1}", gameManager.numberOfCountries - gameManager.CountryNamesList.Count, gameManager.numberOfCountries);
            }
        }
        else if (names != null && names.Length == 2 && tag == gameModeTag)
        {
            if ((countryGuessed == false) && guess == NormalizeName(names[0]) || (guess == NormalizeName(names[1])))
            {
                gameManager.goodGuessSound.Play();
                inputField.GetComponent<TMP_InputField>().text = "";
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                countryGuessed = true;
                gameManager.CountryNamesList.RemoveAt(gameManager.CountryNamesList.IndexOf(gameObject));
                gameManager.guessedNumber.text = string.Format("{0}/{1}", gameManager.numberOfCountries - gameManager.CountryNamesList.Count, gameManager.numberOfCountries);
            }
        }
        else if (names != null && names.Length == 3 && tag == gameModeTag)
        {
            if ((countryGuessed == false) && guess == NormalizeName(names[0]) || guess == NormalizeName(names[1]) || inputField.GetComponent<TMP_InputField>().text == NormalizeName(names[2]))
            {
                gameManager.goodGuessSound.Play();
                inputField.GetComponent<TMP_InputField>().text = "";
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                countryGuessed = true;
                gameManager.CountryNamesList.RemoveAt(gameManager.CountryNamesList.IndexOf(gameObject));
                gameManager.guessedNumber.text = string.Format("{0}/{1}", gameManager.numberOfCountries - gameManager.CountryNamesList.Count, gameManager.numberOfCountries);
            }
        } return false;
    }
    bool CheckForTouchForInfo()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Menu.gameMode.Contains("Capitals"))
            {
                if (gameObject.tag.Contains("Countries"))
                {
                    JSONReader.CountryInfoList countryInfoList = GameObject.Find("CountryInfoReader").GetComponent<JSONReader>().countryInfoList;
                    countryInfo = JSONReader.SearchforCountryInfoCapital(countryInfoList, name.ToString().Replace("_", " "));
                }
                else if (gameObject.tag.Contains("Territories"))
                {
                    JSONReader.TerritoryInfoList territoryInfoList = GameObject.Find("CountryInfoReader").GetComponent<JSONReader>().territoryInfoList;
                    territoryInfo = JSONReader.SearchforTerritoryInfoCapital(territoryInfoList, name.ToString().Replace("_", " "));
                }
            }
            else
            {
                if (gameObject.tag.Contains("Countries"))
                {
                    JSONReader.CountryInfoList countryInfoList = GameObject.Find("CountryInfoReader").GetComponent<JSONReader>().countryInfoList;
                    countryInfo = JSONReader.SearchforCountryInfo(countryInfoList, name.ToString().Replace("_", " "));
                }
                else if (gameObject.tag.Contains("Territories"))
                {
                    JSONReader.TerritoryInfoList territoryInfoList = GameObject.Find("CountryInfoReader").GetComponent<JSONReader>().territoryInfoList;
                    territoryInfo = JSONReader.SearchforTerritoryInfo(territoryInfoList, name.ToString().Replace("_", " "));
                }
            }
            var wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            var touchPosition = new Vector2(wp.x, wp.y);
            if (collider == Physics2D.OverlapPoint(touchPosition) && gameObject.tag.Contains("Countries"))
            {
                if (gameManager.TerritoryInfoMenu.activeSelf == true) gameManager.TerritoryInfoMenu.SetActive(false);
                gameManager.clickSound.Play();
                gameManager.CountryInfoMenu.SetActive(true);
                gameManager.CountryInfoMenu.GetComponent<CountryInfo>().UpdateCountryData(countryInfo.name, countryInfo.capital, countryInfo.area,
                countryInfo.population, countryInfo.languages, countryInfo.religion,
                countryInfo.currency, countryFlag, gameManager.gameEndedMenuclone);
            }
            else if (collider == Physics2D.OverlapPoint(touchPosition) && gameObject.tag.Contains("Territories"))
            {

                if (gameManager.CountryInfoMenu.activeSelf == true) gameManager.CountryInfoMenu.SetActive(false);
                gameManager.clickSound.Play();
                gameManager.TerritoryInfoMenu.SetActive(true);
                gameManager.TerritoryInfoMenu.GetComponent<TerritoryInfo>().UpdateTerritoryData(territoryInfo.name, territoryInfo.capital, territoryInfo.area,
                territoryInfo.population, territoryInfo.languages, territoryInfo.religion,
                territoryInfo.currency, territoryInfo.overlord, countryFlag, gameManager.gameEndedMenuclone);

            }
        }
        return false;
    }
    string NormalizeName(string textToNormalize)
    {
        string normalizedText;
        normalizedText = textToNormalize.ToLowerInvariant().Replace('¹', 'a').Replace('æ', 'c').Replace('ê', 'e').Replace('Ÿ', 'z').Replace('¿', 'z').Replace('_', ' ');
        return normalizedText;
    }
    string NormalizeText(string textToNormalize)
    {
        string normalizedText;
        normalizedText = textToNormalize.ToLowerInvariant().Replace('¹', 'a').Replace('æ', 'c').Replace('ê', 'e').Replace('Ÿ', 'z').Replace('¿', 'z').Trim(' ');
        return normalizedText;
    }
    void GetGameMode()
    {
        if (Menu.gameMode.Contains("Capitals"))
        {
            gameModeTag = Menu.gameMode.Replace("CapitalsTyping", "");
        }
        else gameModeTag = Menu.gameMode.Replace("Typing", "");

    }
}
