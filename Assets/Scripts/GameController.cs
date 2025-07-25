using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    private TimeSpan _currentTime;
    private TimeSpan _leftTime;
    private TimeSpan _rightTime;

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
        _leftTime = TimeSpan.Zero;
        _rightTime = TimeSpan.Zero;

        _projectileHandler.ManualStart();
    }

    private void ManualUpdate()
    {
        _currentTime += TimeSpan.FromSeconds(Time.deltaTime);
        _leftTime += TimeSpan.FromSeconds(Time.deltaTime);
        _rightTime += TimeSpan.FromSeconds(Time.deltaTime);

        _leftPlayerCharacter.ManualUpdate();
        _rightPlayerCharacter.ManualUpdate();

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _leftPlayerCharacter.OnPressBasicAttack();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _leftPlayerCharacter.OnPressSkill0();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            _leftPlayerCharacter.OnPressSkill1();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            _leftPlayerCharacter.OnPressSkill2();
        }

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            _rightPlayerCharacter.OnPressBasicAttack();
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            _rightPlayerCharacter.OnPressSkill0();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            _rightPlayerCharacter.OnPressSkill1();
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            _rightPlayerCharacter.OnPressSkill2();
        }

    }


    public TimeSpan GetCurrentTime()
    {
        return _currentTime;
    }

    public TimeSpan GetPlayerTime(Team team)
    {
        if (team == Team.Left)
        {
            return _leftTime;
        }
        else
        {
            return _rightTime;
        }
    }

    public TimeSpan GetCurrentDeltaTime()
    {
        return TimeSpan.FromSeconds(Time.deltaTime);
    }

    public TimeSpan GetPlayerDeltaTime(Team team)
    {
        if (team == Team.Left)
        {
            return TimeSpan.FromSeconds(Time.deltaTime);
        }
        else
        {
            return TimeSpan.FromSeconds(Time.deltaTime);
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







namespace System.Runtime.CompilerServices
{
        internal static class IsExternalInit {}
}
