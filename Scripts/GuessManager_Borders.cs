using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GuessManager_Borders : MonoBehaviour
{
    public JSONReader jSONReader;
    public Sprite[] countryBorders;
    public List<float> countryScalesx = new List<float>();
    public List<float> countryScalesy = new List<float>();
    public Canvas gameEndedCanvas;
    public TextMeshProUGUI resultText;
    public GameObject image;
    [System.Serializable]
    public class Countries
    {
        public Sprite countryBorder;
        public float scalex;
        public float scaley;
    }
    [System.Serializable]
    public class CountriesList
    {
        public List<Countries> countries;
    }
    public CountriesList countriesList = new();
    public GameObject inputText;
    public GameObject borderImage;
    public Countries borderToGuess;
    public bool borderGuessed;
    public bool resize = false;
    public int index;
    public AudioSource goodGuessSound;
    public AudioSource clickSound;
    public AudioSource gameEndedSound;
    public AudioSource music;
    private string gameTag;
    private string countryName;

    public void Awake()
    {   //scale volume with settings
        goodGuessSound.volume = PlayerPrefs.GetFloat("volume");
        clickSound.volume = PlayerPrefs.GetFloat("volume");
        gameEndedSound.volume = PlayerPrefs.GetFloat("volume");
        music.volume = PlayerPrefs.GetFloat("music");
    }

    public void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        jSONReader.LoadCountryScaleList();
        gameTag = Menu.gameMode;
        borderGuessed = false;
        //find correct gamemode, load bordrs for this gamemode and their scales from JSON
        GameMode("NACountries");
        GameMode("SACountries");
        GameMode("AFCountries");
        GameMode("ASIACountries");
        GameMode("EUCountries");
        GameMode("OCCountries");
        GameMode("NATerritories");
        GameMode("SATerritories");
        GameMode("AFTerritories");
        GameMode("ASIATerritories");
        GameMode("EUTerritories");
        GameMode("OCTerritories");
        for (int i = 0; i < countryBorders.Length; i++)
        {   
            //make list of sprites and their scales
            Countries tempCountry = new();
            tempCountry.countryBorder = countryBorders[i];
            tempCountry.scalex = countryScalesx[i];
            tempCountry.scaley = countryScalesy[i];
            countriesList.countries.Add(tempCountry);
        }
        Shuffle(countriesList);
        //start
        borderImage.GetComponent<Image>().sprite = countriesList.countries[0].countryBorder;
        borderToGuess = countriesList.countries[0];
        borderImage.GetComponent<RectTransform>().localScale = new Vector3(borderToGuess.scalex, borderToGuess.scaley, 0);
    }

    public void Update()
    {
        //checking a user's guess correctness about country
        if (Normalize(inputText.GetComponent<TMP_InputField>().text) == borderToGuess.countryBorder.name.ToLowerInvariant().Replace('_', ' ') && borderGuessed == false)
        {
            inputText.GetComponent<TMP_InputField>().text = "";
            StartCoroutine(GoodGuessCoroutine(borderImage.GetComponent<Image>(), Color.green, 1f, 0.5f));
        }
    }
    void Shuffle(CountriesList inputlist)
    {

        int n = inputlist.countries.Count;
        for (int i = 0; i < n; i++)
        {
            Countries temp = inputlist.countries[i];
            int rand = Random.Range(i, n);
            inputlist.countries[i] = inputlist.countries[rand];
            inputlist.countries[rand] = temp;

        }
    }
    string Normalize(string textToNormalize)
    {
        string normalizedText;
        normalizedText = textToNormalize.ToLowerInvariant().Replace('¹', 'a').Replace('æ', 'c').Replace('ê', 'e').Replace('Ÿ', 'z').Replace('¿', 'z').Trim(' ');
        return normalizedText;
    }
    IEnumerator GoodGuessCoroutine(Image image, Color color, float duration, float delay)
    {
        //animated color change
        goodGuessSound.Play();
        Color originColor = image.color;
        image.color = color;
        borderGuessed = true;
        yield return new WaitForSeconds(delay);
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            image.color = Color.Lerp(color, originColor, t);

            yield return null;
        }
        image.color = originColor;
        //removing country from list and going to next one
        if (countriesList.countries.Count > 1)
        {
            if (countriesList.countries.Count - 1 > index)
            {
                countriesList.countries.RemoveAt(index);
                borderToGuess = countriesList.countries[index];
            }
            else
            {
                countriesList.countries.RemoveAt(index);
                index = 0;
                borderToGuess = countriesList.countries[index];
            }
            borderImage.GetComponent<Image>().sprite = borderToGuess.countryBorder;
            borderImage.GetComponent<RectTransform>().localScale = new Vector3(borderToGuess.scalex, borderToGuess.scaley, 0);
            borderGuessed = false;
        }
        else
        { //if it's last countr - end game
            countriesList.countries.RemoveAt(0);
            GameEnded();
        }
    }
    public void NextCountry()
    {
        if (index != countriesList.countries.Count - 1)
        {
            index++;
            borderToGuess = countriesList.countries[index];
        }
        else
        {
            borderToGuess = countriesList.countries[0];
            index = 0;
        }
        clickSound.Play();
        borderImage.GetComponent<Image>().sprite = borderToGuess.countryBorder;
        borderImage.GetComponent<RectTransform>().localScale = new Vector3(borderToGuess.scalex, borderToGuess.scaley, 0);
    }

    public void PreviousCountry()
    {
        if (index > 0)
        {
            index = countriesList.countries.IndexOf(borderToGuess) - 1;
            borderToGuess = countriesList.countries[index];
        }

        else
        {
            index = countriesList.countries.Count - 1;
            borderToGuess = countriesList.countries[index];
        }
        clickSound.Play();
        borderImage.GetComponent<Image>().sprite = borderToGuess.countryBorder;
        borderImage.GetComponent<RectTransform>().localScale = new Vector3(borderToGuess.scalex, borderToGuess.scaley, 0);
    }
    void LoadBorders(string path)
    { 
        object[] loadedBorders = Resources.LoadAll(path, typeof(Sprite));
        countryBorders = new Sprite[loadedBorders.Length];
        for (int x = 0; x < loadedBorders.Length; x++)
        {
            countryBorders[x] = (Sprite)loadedBorders[x];
        }
    }
    public void GameEnded()
    {
        gameEndedSound.Play();
        //Change canvas
        GameObject[] canvasList = GameObject.FindGameObjectsWithTag("Canvas");
        canvasList[0].SetActive(false);
        canvasList[1].SetActive(false);
        canvasList[2].SetActive(false);
        gameEndedCanvas.gameObject.SetActive(true);
        //check result, if it's the best score - replace the old one
        int result = countryBorders.Length - countriesList.countries.Count;
        resultText.text = string.Format("{0}/{1}", result, countryBorders.Length);
        int prevresult = PlayerPrefs.GetInt(Menu.gameMode);
        if (prevresult <= result)
        {
            PlayerPrefs.SetInt(Menu.gameMode, result);
        }
    }
    void GameMode(string mode)
    {
        if (gameTag == mode + "Borders")
        {
            LoadBorders(string.Format("Images/{0}", mode));
            for (int i = 0; i < countryBorders.Length; i++)
            {
                countryName = countryBorders[i].texture.name;
                Debug.Log(countryName);
                if (mode == "NACountries" || mode == "SACountries" || mode == "NATerritories" || mode == "SATerritories")
                {
                    resize = true;
                    countryScalesx.Add(JSONReader.SearchForCountryScale(jSONReader.countryScaleList, countryName).scalex -0.5f);
                    countryScalesy.Add(JSONReader.SearchForCountryScale(jSONReader.countryScaleList, countryName).scalex * 2 -1f);
                }
                else
                {
                    countryScalesx.Add(JSONReader.SearchForCountryScale(jSONReader.countryScaleList, countryName).scalex);
                    countryScalesy.Add(JSONReader.SearchForCountryScale(jSONReader.countryScaleList, countryName).scalex);
                }
            }
        }
        
    }
}
