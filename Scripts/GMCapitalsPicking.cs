using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GMCapitalsPicking : MonoBehaviour
{
    public List<string> CountryNamesList;
    public string CountryToGuess;
    public int Lifes;
    public Timer timer;
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
    public int index;
    public AudioSource goodGuessSound;
    public AudioSource badGuessSound;
    public AudioSource clickSound;
    public AudioSource gameEndedSound;
    public AudioSource music;
    private int numberOfCountries;
    private string gameMode = Menu.gameMode;
    private GameObject[] CountryList;
    public void Awake()
    {
        goodGuessSound.volume = PlayerPrefs.GetFloat("volume");
        badGuessSound.volume = PlayerPrefs.GetFloat("volume");
        clickSound.volume = PlayerPrefs.GetFloat("volume");
        gameEndedSound.volume = PlayerPrefs.GetFloat("volume");
        music.volume = PlayerPrefs.GetFloat("music");
    }
    public void Start()
    {
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
        CountryToGuess = CountryNamesList[0];
        Lifes = 5;
        goBackButton.SetActive(false);
    }

    List<string> Shuffle (GameObject[] inputlist)
    {
        
        int n = inputlist.Length;
        List<string> nameslist = new List<string>();  
        for(int i = 0; i<n; i++)
        { 
            GameObject temp = inputlist[i];
            int rand = Random.Range(i, n);
            inputlist[i] = inputlist[rand];
            inputlist[rand] = temp;
            nameslist.Add(inputlist[i].name);
        }
        return nameslist;
    }
    private void Update()
    {
        if (Lifes == 0){
            GameEnded();
        }
    }
    public void GameEnded()
    {
        gameEndedSound.Play();
        gameEnded = true;
        timer.enabled = false;
        CountryToGuess = "Good job!";
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
        int prevresult = PlayerPrefs.GetInt(Menu.gameMode);
        if (prevresult <= result)
        {
            PlayerPrefs.SetInt(Menu.gameMode, result);
        }
    }
    public void NextCountry()
    {
        index = CountryNamesList.IndexOf(CountryToGuess);
        if (index != CountryNamesList.Count - 1)
        {
            index++;
            CountryToGuess = CountryNamesList[index];
        }
        else
        {
            CountryToGuess = CountryNamesList[0];
            index = 0;
        }
        clickSound.Play();
    }

    public void PreviousCountry()
    {
        if (index > 0) 
        {
            index = CountryNamesList.IndexOf(CountryToGuess)-1;
            CountryToGuess = CountryNamesList[index];       
        }

        else
        {
            index = CountryNamesList.Count-1;
            CountryToGuess = CountryNamesList[index];
        }
        clickSound.Play();
    }
    void GameMode(GameObject cHolder, GameObject tHolder, string mode, int x, int y, float maxX, float minX, float maxY, float minY)
    {
        if (gameMode == mode + "CapitalsPicking")
        {
            cHolder.SetActive(true);
            tHolder.SetActive(true);
            Camera.main.transform.position = new Vector3(x, y, -10);
            cameraMovement.maxX = maxX;
            cameraMovement.minX = minX;
            cameraMovement.maxY = maxY;
            cameraMovement.minY = minY;
            CountryList = GameObject.FindGameObjectsWithTag(mode);
            CountryNamesList = Shuffle(CountryList);
        }
    }
}


