using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.EventSystems;

public class CountryButton : MonoBehaviour
{
    private Collider2D collider1;
    public GMCountriesPicking guessmanager;
    private SpriteRenderer SpriteRend;
    private CountryButton countryButton;
    private GameObject CountryInfoMenu;
    private GameObject TerritoryInfoMenu;
    private bool countryGuessed;
    private Vector2 cameraPos1;
    private Vector2 cameraPos2;
    private float cameraSize1;
    private string gameModeTag;

    [SerializeField]
    public Sprite countryFlag;

    private JSONReader.CountryInfo countryInfo;
    private JSONReader.TerritoryInfo territoryInfo;
    void Start()
    {
        GetGameMode();
        CountryInfoMenu = guessmanager.CountryInfoMenu;
        collider1 = GetComponent<Collider2D>();
        SpriteRend = GetComponent<SpriteRenderer>();
        countryButton = GetComponent<CountryButton>();
        countryGuessed = false;
    }
    private void Update()
    {
        if (!GetComponentInParent<GMCountriesPicking>().gameEnded && !EventSystem.current.IsPointerOverGameObject(0)) CheckForTouch();
        else if(guessmanager.gameEndedMenuclone.GetComponent<Resume>().spectatorModeEnabled && !EventSystem.current.IsPointerOverGameObject(0)) CheckForTouchForInfo();
    }

    bool CheckForTouch() 
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            cameraPos1 = Camera.main.transform.position;
            cameraSize1 = Camera.main.orthographicSize;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            cameraPos2 = Camera.main.transform.position;
            var wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            var touchPosition = new Vector2(wp.x, wp.y);

            if (collider1 == Physics2D.OverlapPoint(touchPosition) && cameraPos1 == cameraPos2 && cameraSize1 == Camera.main.orthographicSize)
            {
                if (name == guessmanager.CountryToGuess && countryGuessed == false && gameObject.tag == gameModeTag)
                {
                        GoodGuess();
                }
                else if (name != guessmanager.CountryToGuess && countryGuessed == false)
                {
                    guessmanager.badGuessSound.Play();
                    StartCoroutine(ChangeColor(SpriteRend, Color.red, 1f, 0.5f));
                    guessmanager.Lifes--;
                }
            }
        }
        return false;
    }
    IEnumerator ChangeColor(SpriteRenderer sr, Color color, float duration, float delay)
    {
        Color originColor = sr.color;
        sr.color = color;
        countryGuessed = true;
        yield return new WaitForSeconds(delay);
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            sr.color = Color.Lerp(color, originColor, t);

            yield return null;
        }
        countryGuessed = false;
        sr.color = originColor;
    }
    public void GoodGuess()
    {
        if (guessmanager.CountryNamesList.Count > 1)
        {
            guessmanager.goodGuessSound.Play();
            SpriteRend.color = Color.green;
            List<string> tempNamesList = new List<string>();
            for (int i = 0; i < guessmanager.CountryNamesList.Count; i++)
            {
                if (guessmanager.CountryNamesList[i] != name)
                {
                    tempNamesList.Add(guessmanager.CountryNamesList[i]);
                }
            }

            guessmanager.CountryNamesList = tempNamesList;
            if (guessmanager.index <= tempNamesList.Count - 1)
                guessmanager.CountryToGuess = guessmanager.CountryNamesList[guessmanager.index];
            else
                guessmanager.CountryToGuess = tempNamesList[0];
            countryGuessed = true;
        }
        else
        {
            SpriteRend.color = Color.green;
            List<string> tempNamesList = new List<string>();
            guessmanager.CountryNamesList = tempNamesList;
            guessmanager.GameEnded();
            countryGuessed = true;
        }

    }
    bool CheckForTouchForInfo()
    {   
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
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

                var wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                var touchPosition = new Vector2(wp.x, wp.y);
                if (collider1 == Physics2D.OverlapPoint(touchPosition) && gameObject.tag.Contains("Countries"))
                {
                    if (guessmanager.TerritoryInfoMenu.activeSelf == true) guessmanager.TerritoryInfoMenu.SetActive(false);
                    guessmanager.clickSound.Play();
                    guessmanager.CountryInfoMenu.SetActive(true);
                    guessmanager.CountryInfoMenu.GetComponent<CountryInfo>().UpdateCountryData(countryInfo.name, countryInfo.capital, countryInfo.area,
                    countryInfo.population, countryInfo.languages, countryInfo.religion,
                    countryInfo.currency, countryFlag, guessmanager.gameEndedMenuclone);   
                }
                else if (collider1 == Physics2D.OverlapPoint(touchPosition) && gameObject.tag.Contains("Territories"))
                {

                    if (guessmanager.CountryInfoMenu.activeSelf == true) guessmanager.CountryInfoMenu.SetActive(false);
                    guessmanager.clickSound.Play();
                    guessmanager.TerritoryInfoMenu.SetActive(true);
                    guessmanager.TerritoryInfoMenu.GetComponent<TerritoryInfo>().UpdateTerritoryData(territoryInfo.name, territoryInfo.capital, territoryInfo.area,
                    territoryInfo.population, territoryInfo.languages, territoryInfo.religion,
                    territoryInfo.currency, territoryInfo.overlord, countryFlag, guessmanager.gameEndedMenuclone);
              
                }
            }
        return false;
    }
    void GetGameMode()
    {
        if (Menu.gameMode.Contains("Picking"))
        {
            gameModeTag = Menu.gameMode.Replace("Picking", "");
        }
        if (Menu.gameMode.Contains("Typing"))
        {
            gameModeTag = Menu.gameMode.Replace("Typing", "");
        }
    }
}
