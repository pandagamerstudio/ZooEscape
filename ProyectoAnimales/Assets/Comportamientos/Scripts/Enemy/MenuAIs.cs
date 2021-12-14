using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAIs : MonoBehaviour
{
    public GameObject menuPrincipalScreen, opcionesScreen, creditosScreen, controlesScreen, levelsScreen;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("music") == false)
        {
            PlayerPrefs.SetFloat("music", 0.5f);
        }
        if (PlayerPrefs.HasKey("sfx") == false)
        {
            PlayerPrefs.SetFloat("sfx", 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetScreen(GameObject screen)
    {
        menuPrincipalScreen.SetActive(false);
        creditosScreen.SetActive(false);
        opcionesScreen.SetActive(false);
        controlesScreen.SetActive(false);
        levelsScreen.SetActive(false);

        screen.SetActive(true);
    }


    public void ChangeScene( string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
