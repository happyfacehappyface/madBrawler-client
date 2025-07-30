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

    private CharacterHolder _leftCharacterHolder;
    private CharacterHolder _rightCharacterHolder;
    


    [SerializeField] private GameObject _characterPrefab;
    [SerializeField] private ProjectileHandler _projectileHandler;
    [SerializeField] private ControllableHandler _controllableHandler;
    [SerializeField] private WallHandler _wallHandler;
    [SerializeField] private GameUIDrawer _gameUIDrawer;

    private CharacterEffectFactory _characterEffectFactory;

    public ProjectileHandler ProjectileHandler => _projectileHandler;
    public ControllableHandler ControllableHandler => _controllableHandler;

    public CharacterEffectFactory CharacterEffectFactory => _characterEffectFactory;




    private void ManualStart()
    {
        Instance = this;

        GameObject leftPlayer = Instantiate(_characterPrefab);
        GameObject rightPlayer = Instantiate(_characterPrefab);

        _leftCharacterHolder = leftPlayer.GetComponent<CharacterHolder>();
        _rightCharacterHolder = rightPlayer.GetComponent<CharacterHolder>();

        _leftCharacterHolder.ManualStart(CharacterType.Jaehyeon);
        _rightCharacterHolder.ManualStart(CharacterType.Sungjun);

        _leftPlayerCharacter = leftPlayer.GetComponent<Character>();
        _leftPlayerCharacter.ManualStart(Team.Left, _leftCharacterHolder);
        _rightPlayerCharacter = rightPlayer.GetComponent<Character>();
        _rightPlayerCharacter.ManualStart(Team.Right, _rightCharacterHolder);

        _currentTime = TimeSpan.Zero;
        _leftTime = TimeSpan.Zero;
        _rightTime = TimeSpan.Zero;

        _characterEffectFactory = new CharacterEffectFactory();

        _projectileHandler.ManualStart();
        _controllableHandler.ManualStart();
        _wallHandler.ManualStart();

        _gameUIDrawer.ManualStart();
    }

    private void ManualUpdate()
    {
        _currentTime += TimeSpan.FromSeconds(Time.deltaTime);
        _leftTime += TimeSpan.FromSeconds(Time.deltaTime);
        _rightTime += TimeSpan.FromSeconds(Time.deltaTime);

        _leftPlayerCharacter.ManualUpdate();
        _rightPlayerCharacter.ManualUpdate();

        _projectileHandler.ManualUpdate();
        _wallHandler.ManualUpdate();

        HandleInput();
        _gameUIDrawer.ManualUpdate();
    }

    private void HandleInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.A))
            {
                _leftPlayerCharacter.OnPressDirection(Direction.UpLeft);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _leftPlayerCharacter.OnPressDirection(Direction.UpRight);
            }
            else
            {
                _leftPlayerCharacter.OnPressDirection(Direction.Up);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
            {
                _leftPlayerCharacter.OnPressDirection(Direction.DownLeft);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _leftPlayerCharacter.OnPressDirection(Direction.DownRight);
            }
            else
            {
                _leftPlayerCharacter.OnPressDirection(Direction.Down);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                _leftPlayerCharacter.OnPressDirection(Direction.Left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _leftPlayerCharacter.OnPressDirection(Direction.Right);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rightPlayerCharacter.OnPressDirection(Direction.UpLeft);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _rightPlayerCharacter.OnPressDirection(Direction.UpRight);
            }
            else
            {
                _rightPlayerCharacter.OnPressDirection(Direction.Up);
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rightPlayerCharacter.OnPressDirection(Direction.DownLeft);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _rightPlayerCharacter.OnPressDirection(Direction.DownRight);
            }
            else
            {
                _rightPlayerCharacter.OnPressDirection(Direction.Down);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _rightPlayerCharacter.OnPressDirection(Direction.Left);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _rightPlayerCharacter.OnPressDirection(Direction.Right);
            }
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

    public Transform GetPlayerTransform(Team team)
    {
        if (team == Team.Left)
        {
            return _leftPlayerCharacter.GetPlayerTransform();
        }
        else
        {
            return _rightPlayerCharacter.GetPlayerTransform();
        }
    }

    public Transform GetPlayerSpinnedTransform(Team team)
    {
        if (team == Team.Left)
        {
            return _leftPlayerCharacter.GetSpinnedTransform();
        }
        else
        {
            return _rightPlayerCharacter.GetSpinnedTransform();
        }
    }

    public Transform GetPlayerNotSpinnedTransform(Team team)
    {
        if (team == Team.Left)
        {
            return _leftPlayerCharacter.GetNotSpinnedTransform();
        }
        else
        {
            return _rightPlayerCharacter.GetNotSpinnedTransform();
        }
    }

    public Character GetPlayerCharacter(Team team)
    {
        if (team == Team.Left)
        {
            return _leftPlayerCharacter;
        }
        else
        {
            return _rightPlayerCharacter;
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
