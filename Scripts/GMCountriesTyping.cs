using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GMCountriesTyping : MonoBehaviour
{
    public List<GameObject> CountryNamesList;
    public Timer timer;
    public TextMeshProUGUI guessedNumber;
    public CameraMovement cameraMovement;
    public GameObject gameEndedMenuclone;
    public GameObject goBackButton;
    public GameObject CountryInfoMenu;
    public GameObject TerritoryInfoMenu;
    public GameObject NACountries;
    public GameObject NATerritories;
    public GameObject SACountries;
    public GameObject SATerritories;
    public GameObject AFCountries;
    public GameObject AFTerritories;
    public GameObject ASIACountries;
    public GameObject ASIATerritories;
    public GameObject EUCountries;
    public GameObject EUTerritories;
    public GameObject OCCountries;
    public GameObject OCTerritories;
    public GameObject tempCanvas;
    public bool gameEnded = false;
    public int numberOfCountries;
    public AudioSource goodGuessSound;
    public AudioSource badGuessSound;
    public AudioSource clickSound;
    public AudioSource gameEndedSound;
    public AudioSource music;
    private string gameMode = Menu.gameMode;
    public void Awake()
    {
        //scale volume with settings
        goodGuessSound.volume = PlayerPrefs.GetFloat("volume");
        badGuessSound.volume = PlayerPrefs.GetFloat("volume");
        clickSound.volume = PlayerPrefs.GetFloat("volume");
        gameEndedSound.volume = PlayerPrefs.GetFloat("volume");
        music.volume = PlayerPrefs.GetFloat("music");
    }
    public void Start()
    {
        //find correct gamemode, enable compatible country-buttons holder, move camera to it's position in scene and limit it
        GameMode(NACountries, NATerritories, "NACountries", -21, -1, -12, -30, 21, -8);
        GameMode(NACountries, NATerritories, "NATerritories", -21, -1, -12, -30, 21, -8);
        GameMode(SACountries, SATerritories, "SACountries", -11, -12, -5, -16, -5.5f, -22.5f);
        GameMode(SACountries, SATerritories, "SATerritories", -11, -12, -5, -16, -5.5f, -22.5f);
        GameMode(AFCountries, AFTerritories, "AFCountries", 4, -7, 13, -4, 0, -17);
        GameMode(AFCountries, AFTerritories, "AFTerritories", 4, -7, 13, -4, 0, -17);
        GameMode(ASIACountries, ASIATerritories, "ASIACountries", 16, -2, 40, 2, 23, -17);
        GameMode(ASIACountries, ASIATerritories, "ASIATerritories", 16, -2, 40, 2, 23, -17);
        GameMode(EUCountries, EUTerritories, "EUCountries", 3, 2, 6, -5, 20, -6f);
        GameMode(EUCountries, EUTerritories, "EUTerritories", 3, 2, 6, -5, 20, -6f);
        GameMode(OCCountries, OCTerritories, "OCCountries", 32, -12, 55, 23, -2.5f, -24);
        GameMode(OCCountries, OCTerritories, "OCTerritories", 32, -12, 55, 23, -2.5f, -24);

        numberOfCountries = CountryNamesList.Count;
        goBackButton.SetActive(false);
        guessedNumber.text = string.Format("0/{0}", numberOfCountries);
    }
    private void Update()
    {
        if (CountryNamesList.Count == 0) GameEnded();
    }

    public void GameEnded()
    {
        //pause current game state and change canvas
        gameEndedSound.Play();
        gameEnded = true;
        timer.enabled = false;
        Camera.main.GetComponent<CameraMovement>().enabled = false;
        tempCanvas.SetActive(false);
        gameEndedMenuclone.SetActive(true);
        TextMeshProUGUI[] textlist = gameEndedMenuclone.GetComponentsInChildren<TextMeshProUGUI>();
        int result = numberOfCountries - CountryNamesList.Count;
        textlist[2].text = string.Format("{0}/{1}", result, numberOfCountries);
        textlist[3].text = timer.currentTime;
        gameEndedMenuclone.GetComponent<Resume>().onInstance(Camera.main.gameObject, gameObject, gameEndedMenuclone, goBackButton);
        goBackButton.GetComponent<Button>().onClick.AddListener(delegate
        {
            gameEndedMenuclone.GetComponent<Resume>().ShowThisObject();
            CountryInfoMenu.SetActive(false);
            TerritoryInfoMenu.SetActive(false);
        });
        enabled = false;
        //check result, if it's the best score - replace the old one
        int prevresult = PlayerPrefs.GetInt(Menu.gameMode);
        if (prevresult <= result)
        {
            PlayerPrefs.SetInt(Menu.gameMode, result);
        }
    }
    public void GameMode(GameObject cHolder, GameObject tHolder, string mode, int x, int y, float maxX, float minX, float maxY, float minY)
    {
        if (gameMode == mode + "Typing" || gameMode == mode + "CapitalsTyping")
        {
            cHolder.SetActive(true);
            tHolder.SetActive(true);
            Camera.main.transform.position = new Vector3(x, y, -10);
            cameraMovement.maxX = maxX;
            cameraMovement.minX = minX;
            cameraMovement.maxY = maxY;
            cameraMovement.minY = minY;
            GameObject[] CountryNamesListtemp = GameObject.FindGameObjectsWithTag(mode);
            for (int i = 0; i < CountryNamesListtemp.Length; i++)
            {
                CountryNamesList.Add(CountryNamesListtemp[i]);
            }
        }
    }
}



