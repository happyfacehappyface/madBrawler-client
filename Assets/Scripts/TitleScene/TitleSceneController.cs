using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneController : MonoBehaviour
{
    [SerializeField] private CharacterSelectHandler _characterSelectHandler;


    private void ManualStart()
    {
        _characterSelectHandler.ManualStart();
    }

    private void ManualUpdate()
    {
        _characterSelectHandler.ManualUpdate();
    }

    protected void Start()
    {
        ManualStart();
    }

    protected void Update()
    {
        ManualUpdate();
    }
}
