using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Resume : MonoBehaviour
{
    public GameObject camera;
    public GameObject guessManager;
    public GameObject gameEndedMenu;
    public GameObject goBackButton;
    public GameObject countryInfoMenu = null;
    public GameObject territoryInfoMenu = null;
    public bool spectatorModeEnabled;
    public void onInstance(GameObject cameraTemp, GameObject guessManagerTemp, GameObject gameEndedMenuTemp, GameObject goBackButtontemp)
    {
        gameEndedMenu = gameEndedMenuTemp;
        camera = cameraTemp;
        guessManager = guessManagerTemp;
        goBackButton = goBackButtontemp;
    }
    public void HideThisObject(string GM)
    {
        camera.GetComponent<CameraMovement>().enabled = true;
        if(GM == "Countries")guessManager.GetComponent<GMCountriesPicking>().enabled = false;
        else if(GM == "Capitals") guessManager.GetComponent<GMCapitalsPicking>().enabled = false;
        else if(GM == "CountriesTyping") guessManager.GetComponent<GMCountriesTyping>().enabled = false;
        gameEndedMenu.SetActive(false);
        goBackButton.SetActive(true);
        spectatorModeEnabled = true;
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowThisObject()
    {
        camera.GetComponent<CameraMovement>().enabled = false;
        gameEndedMenu.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        gameEndedMenu.SetActive(true);
        spectatorModeEnabled = false;
        goBackButton.SetActive(false);
        if(countryInfoMenu != null) countryInfoMenu.SetActive(false);
        if(territoryInfoMenu != null) territoryInfoMenu.SetActive(false);
    }
}
