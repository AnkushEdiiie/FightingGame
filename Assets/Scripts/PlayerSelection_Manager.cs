using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelection_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        if(!PlayerPrefs.HasKey("Selected_Character_Player_1"))
        {
            PlayerPrefs.SetInt("Selected_Character_Player_1", 0);
        }
        if (!PlayerPrefs.HasKey("Selected_Character_Player_2"))
        {
            PlayerPrefs.SetInt("Selected_Character_Player_2", 0);
        }

        SceneManager.LoadScene("MainGamePlay");
    }

}
