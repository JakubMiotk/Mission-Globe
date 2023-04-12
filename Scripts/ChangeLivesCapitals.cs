using UnityEngine;
using TMPro;

public class ChangeLivesCapitals : MonoBehaviour
{
    public GMCapitalsPicking guessmanager;
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = guessmanager.Lifes.ToString();
    }
}
