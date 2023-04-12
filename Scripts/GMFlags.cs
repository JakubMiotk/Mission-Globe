using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GMFlags : MonoBehaviour
{
    public List<Sprite> Flags;
    public GameObject FlagImage;
    public Sprite flagToGuess;
    public Canvas gameEndedCanvas;
    public TextMeshProUGUI resultText;
    public GameObject inputText;   
    public List<Sprite> numberOfFlags;
    public AudioSource goodGuessSound;
    public AudioSource clickSound;
    public AudioSource gameEndedSound;
    public AudioSource music;
    private int index;
    private bool flagGuessed;

    public void Awake()
    {

        goodGuessSound.volume = PlayerPrefs.GetFloat("volume");
        clickSound.volume*= PlayerPrefs.GetFloat("volume");
        gameEndedSound.volume = PlayerPrefs.GetFloat("volume");
        music.volume = PlayerPrefs.GetFloat("music");
    }
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        flagGuessed = false;
        GameMode("NACountries");
        GameMode("NATerritories");
        GameMode("SACountries");
        GameMode("SATerritories");
        GameMode("AFCountries");
        GameMode("AFTerritories");
        GameMode("ASIACountries");
        GameMode("ASIATerritories");
        GameMode("EUCountries");
        GameMode("EUTerritories");
        GameMode("OCCountries");
        GameMode("OCTerritories");
        Shuffle(Flags);
        FlagImage.GetComponent<Image>().sprite = Flags[0];
        flagToGuess = Flags[0];
        numberOfFlags = new List<Sprite>(Flags);
    }
    void Update()
    {
        Color transparent = Color.green;
        transparent.a = 0f;
        if (Normalize(inputText.GetComponent<TMP_InputField>().text) == flagToGuess.name.ToLowerInvariant().Replace('_', ' ').Replace("flag", "") && flagGuessed == false)
        {
            inputText.GetComponent<TMP_InputField>().text = "";
            StartCoroutine(GoodGuessCoroutine(FlagImage.GetComponent<Image>(), transparent, 1f, 0.25f));
        }
    }
    void LoadFlags(string path)
    {
        Object[] loadedflags = Resources.LoadAll<Sprite>(path);
        for (int x = 0; x < loadedflags.Length; x++)
        {
            Flags.Add((Sprite)loadedflags[x]);
        }
    }
    void Shuffle(List<Sprite> inputlist)
    {

        int n = inputlist.Count;
        for (int i = 0; i < n; i++)
        {
            Sprite temp = inputlist[i];
            int rand = Random.Range(i, n);
            inputlist[i] = inputlist[rand];
            inputlist[rand] = temp;

        }
    }
    IEnumerator GoodGuessCoroutine(Image image, Color color, float duration, float delay)
    {
        goodGuessSound.Play();
        Color originColor = image.color;
        flagGuessed = true;
        yield return new WaitForSeconds(delay);
        for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
        {
            image.color = Color.Lerp(image.color, color, t);

            yield return null;
        }
        image.color = originColor;
        if (Flags.Count > 1)
        {
            if (Flags.Count - 1 > index)
            {
                Flags.RemoveAt(index);
                flagToGuess = Flags[index];
            }
            else
            {
                Flags.RemoveAt(index);
                index = 0;
                flagToGuess = Flags[index];
            }
            FlagImage.GetComponent<Image>().sprite = flagToGuess;
            flagGuessed = false;
        }
        else
        {
            Flags.RemoveAt(0);
            GameEnded();
        }
    }
    public void NextCountry()
    {
        if (index != Flags.Count - 1)
        {
            clickSound.Play();
            index++;
            flagToGuess = Flags[index];
        }
        else
        {
            clickSound.Play();
            flagToGuess = Flags[0];
            index = 0;
        }
        FlagImage.GetComponent<Image>().sprite = flagToGuess;
    }

    public void PreviousCountry()
    {
        if (index > 0)
        {
            clickSound.Play();
            index = Flags.IndexOf(flagToGuess) - 1;
            flagToGuess = Flags[index];
        }

        else
        {
            clickSound.Play();
            index = Flags.Count - 1;
            flagToGuess = Flags[index];
        }
        FlagImage.GetComponent<Image>().sprite = flagToGuess;
    }
    public void GameEnded()
    {
        gameEndedSound.Play();
        GameObject[] canvasList = GameObject.FindGameObjectsWithTag("Canvas");
        canvasList[0].SetActive(false);
        canvasList[1].SetActive(false);
        gameEndedCanvas.gameObject.SetActive(true);
        int result = numberOfFlags.Count - Flags.Count;
        resultText.text = string.Format("{0}/{1}", result, numberOfFlags.Count);
        int prevresult = PlayerPrefs.GetInt(Menu.gameMode);
        if (prevresult <= result)
        {
            PlayerPrefs.SetInt(Menu.gameMode, result);
        }
    }
    string Normalize(string textToNormalize)
    {
        string normalizedText;
        normalizedText = textToNormalize.ToLowerInvariant().Replace('¹', 'a').Replace('æ', 'c').Replace('ê', 'e').Replace('Ÿ', 'z').Replace('¿', 'z').Trim(' ');
        return normalizedText;
    }
    private void GameMode(string mode)
    {
        if (Menu.gameMode == mode + "Flags")
        {
            LoadFlags(string.Format("Flags/{0}", mode));
        }
    }
}
