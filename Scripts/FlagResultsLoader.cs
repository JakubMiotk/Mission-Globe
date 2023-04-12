using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FlagResultsLoader : MonoBehaviour
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
    public GameObject FlagImage;
    public GMFlags gmFlags;
    public GameObject image;
    private TextMeshProUGUI countryName;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        LoadResults(NACountries);
        LoadResults(SACountries);
        LoadResults(AFCountries);
        LoadResults(ASIACountries);
        LoadResults(EUCountries);
        LoadResults(OCCountries);
        LoadResults(NATerritories);
        LoadResults(SATerritories);
        LoadResults(AFTerritories);
        LoadResults(ASIATerritories);
        LoadResults(EUTerritories);
        LoadResults(OCTerritories);
        FindNotGuessed();
    }
    void FindNotGuessed() 
    {
        for (int i = 0; i < gmFlags.numberOfFlags.Count; i++)
        {
            string name = gmFlags.Flags[i].texture.name.Replace("Flag","");
            GameObject country = GameObject.Find(name).gameObject;
            countryName = country.GetComponent<TextMeshProUGUI>();
            countryName.color = Color.red;
        }
    }
    public void LoadBorder(GameObject holder)
    {
        gmFlags.clickSound.Play();
        FlagImage.SetActive(true);
        for (int i = 0; i < gmFlags.numberOfFlags.Count; i++)
        {
            if (gmFlags.numberOfFlags[i].texture.name.Replace("Flag","") == holder.name)
            {
                image.GetComponent<Image>().sprite = gmFlags.numberOfFlags[i];
            }
        }
    }
    public void CloseBorderImage()
    {
        gmFlags.clickSound.Play();
        FlagImage.SetActive(false);
    }

    void LoadResults(GameObject cHolder)
    {
        string mode = cHolder.name;
        if (Menu.gameMode == string.Format("{0}Flags", mode))
        {
            cHolder.SetActive(true);
        }
    }
}
