using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Character leftPlayerCharacter;
    public Character rightPlayerCharacter;


    private void ManualStart()
    {
        leftPlayerCharacter = new CharacterSungjun();
        rightPlayerCharacter = new CharacterSungjun();
    }

    private void ManualUpdate()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            leftPlayerCharacter.OnPressUp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            leftPlayerCharacter.OnPressDown();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            leftPlayerCharacter.OnPressLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            leftPlayerCharacter.OnPressRight();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            rightPlayerCharacter.OnPressUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            rightPlayerCharacter.OnPressDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rightPlayerCharacter.OnPressLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightPlayerCharacter.OnPressRight();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            leftPlayerCharacter.OnUseSkill1();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            leftPlayerCharacter.OnUseSkill2();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            leftPlayerCharacter.OnUseSkill3();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            rightPlayerCharacter.OnUseSkill1();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            rightPlayerCharacter.OnUseSkill2();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            rightPlayerCharacter.OnUseSkill3();
        }

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
