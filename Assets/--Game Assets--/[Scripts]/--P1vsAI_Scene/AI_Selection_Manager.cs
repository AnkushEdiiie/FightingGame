using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AI_Selection_Manager : MonoBehaviour
{
    private void Start()
    {
        SelectedSide(0);
    }
    public void SelectedSide(int index)
    {
        if(index == 0)
        {
            GameManager_Old.instance.uiController.PlayerASelection();
        }
        else if(index == 1)
        {
            GameManager_Old.instance.uiController.PlayerBSelection();
        }
    }
}