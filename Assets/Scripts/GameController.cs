using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Character _leftPlayerCharacter;
    public Character _rightPlayerCharacter;

    [SerializeField] private GameObject _playerPrefab;


    private void ManualStart()
    {
        GameObject leftPlayer = Instantiate(_playerPrefab);
        GameObject rightPlayer = Instantiate(_playerPrefab);

        _leftPlayerCharacter = new CharacterSungjun();
        _rightPlayerCharacter = new CharacterSungjun();

        _leftPlayerCharacter.SetPlayerObject(leftPlayer);
        _rightPlayerCharacter.SetPlayerObject(rightPlayer);
    }

    private void ManualUpdate()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _leftPlayerCharacter.OnPressUp();
        }
        if (Input.GetKey(KeyCode.S))
        {
            _leftPlayerCharacter.OnPressDown();
        }
        if (Input.GetKey(KeyCode.A))
        {
            _leftPlayerCharacter.OnPressLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            _leftPlayerCharacter.OnPressRight();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _rightPlayerCharacter.OnPressUp();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _rightPlayerCharacter.OnPressDown();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _rightPlayerCharacter.OnPressLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _rightPlayerCharacter.OnPressRight();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            _leftPlayerCharacter.OnUseSkill1();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            _leftPlayerCharacter.OnUseSkill2();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _leftPlayerCharacter.OnUseSkill3();
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            _rightPlayerCharacter.OnUseSkill1();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            _rightPlayerCharacter.OnUseSkill2();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            _rightPlayerCharacter.OnUseSkill3();
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
