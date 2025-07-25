using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    private TimeSpan _currentTime;
    private Character _leftPlayerCharacter;
    public Character LeftPlayerCharacter => _leftPlayerCharacter;

    private Character _rightPlayerCharacter;
    public Character RightPlayerCharacter => _rightPlayerCharacter;


    

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private ProjectileHandler _projectileHandler;
    [SerializeField] private GameUIDrawer _gameUIDrawer;

    public ProjectileHandler ProjectileHandler => _projectileHandler;


    private void ManualStart()
    {
        Instance = this;

        GameObject leftPlayer = Instantiate(_playerPrefab);
        GameObject rightPlayer = Instantiate(_playerPrefab);

        _leftPlayerCharacter = leftPlayer.GetComponent<CharacterSungjun>();
        _leftPlayerCharacter.ManualStart(Team.Left);
        _rightPlayerCharacter = rightPlayer.GetComponent<CharacterSungjun>();
        _rightPlayerCharacter.ManualStart(Team.Right);

        _currentTime = TimeSpan.Zero;

        _projectileHandler.ManualStart();
    }

    private void ManualUpdate()
    {
        _currentTime += TimeSpan.FromSeconds(Time.deltaTime);
        _projectileHandler.ManualUpdate();

        HandleInput();
        _gameUIDrawer.ManualUpdate();
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




public enum Team
{
    Left,
    Right
}

