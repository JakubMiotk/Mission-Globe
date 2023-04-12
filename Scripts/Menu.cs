using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public static string gameMode;

    public void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Application.targetFrameRate = 30;
    }
    public void SceneChange(int sceneid)
    { 
        SceneManager.LoadScene(sceneid);
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void CanvasChange(Canvas newCanvas)
    { 
        newCanvas.gameObject.SetActive(true);
        GameObject oldCanvas = GameObject.FindGameObjectWithTag("Canvas");
        oldCanvas.SetActive(false);
    }
    public void ChooseGameMode(string chosenGameMode)
    {
        gameMode = chosenGameMode;
    }
}
