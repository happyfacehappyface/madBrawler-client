using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using UnityEngine.UI;

public class GameUIDrawer : MonoBehaviour
{
    [SerializeField] private Image _leftPlayerHitPointBar;
    [SerializeField] private Image _rightPlayerHitPointBar;



    

    public void ManualUpdate()
    {
        _leftPlayerHitPointBar.fillAmount = GameController.Instance.LeftPlayerCharacter.HitPointRatio();
        _rightPlayerHitPointBar.fillAmount = GameController.Instance.RightPlayerCharacter.HitPointRatio();
    }
}
