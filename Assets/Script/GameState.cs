using UnityEngine;
using UnityEngine.SceneManagement;

public class FGameState
{
    public enum EState { Main, Jogo, GameOver, Going };
    private EState _currentEState = EState.Main;
    private FState _currentState = null;

    public bool bOnMenu = true;
    public bool bIsDeath = false;
    public bool bIsPlaying = false;
    public bool bIsExiting = false;


    //private FState[] states = new FMenu[5];

    public FGameState()
    {
        _currentEState = EState.Main;
        _currentState = new FMenu(this);
    }

    public void OnUpdate()
    {
        //EState frameState = states[(int)_currentEState].OnUpdate();

        EState frameState = _currentState.OnUpdate();
        if (frameState != _currentEState)
        {
            _currentState.OnEnd();
            // Troca de estado
            _currentEState = frameState;

            switch (_currentEState)
            {
                case EState.Main:
                    _currentState = new FMenu(this);
                    break;
                case EState.Jogo:
                    _currentState = new FGame(this);
                    break;
                case EState.GameOver:
                    _currentState = new FGameOver(this);
                    break;
                case EState.Going:
                    _currentState = new FGameExiting(this);
                    break;
                default:
                    break;
            }
            _currentState.OnBegin();
        }
    }
}

public abstract class FState
{
    protected FGameState.EState _nextState;
    protected FGameState _gameState;

    public FState(FGameState inGameState) { _gameState = inGameState; }

    public abstract void OnBegin();
    public abstract FGameState.EState OnUpdate();
    public abstract void OnEnd();
}

public class FMenu : FState
{

    public FMenu(FGameState inGameState) : base(inGameState) { }

    public override void OnBegin()
    {
        GameManager.GetInstance().SetPlaying(false);
        GameManager.GetInstance().SetExit(false);
        GameManager.GetInstance().SetDeath(false);
        _nextState = FGameState.EState.Main;
        SceneManager.LoadScene("Main");
        //_deltaTime = 0.0f;
    }

    public override FGameState.EState OnUpdate()
    {
        if (_gameState.bOnMenu)
            _nextState = FGameState.EState.Main;
        if (_gameState.bIsPlaying)
            _nextState = FGameState.EState.Jogo;
        if (_gameState.bIsExiting)
            _nextState = FGameState.EState.Going;
        // Defino qual estado estou...
        Debug.Log("Estou no Menu");
        return _nextState;
    }

    public override void OnEnd()
    {
        GameManager.GetInstance().SetMenu(true);
        GameManager.GetInstance().SetPlaying(false);
        GameManager.GetInstance().SetExit(false);
        GameManager.GetInstance().SetDeath(false);
    }
}

public class FGame : FState
{
    public FGame(FGameState inGameState) : base(inGameState) { }
    public override void OnBegin()
    {
        _nextState = FGameState.EState.Jogo;
        SceneManager.LoadScene("Game");
    }
    public override FGameState.EState OnUpdate()
    {
        if (_gameState.bIsDeath)
            _nextState = FGameState.EState.GameOver;
        Debug.Log("Estou no Jogo");
        return _nextState;
    }

    public override void OnEnd()
    {
        //GameManager.GetInstance().SetDeath(false);
    }
}

public class FGameOver : FState
{
    private float _targetTime = 4.0f;
    private float _deltaTime = 0.0f;
    public FGameOver(FGameState inGameState) : base(inGameState) { }
    public override void OnBegin()
    {
        _deltaTime = 0.0f;
        _nextState = FGameState.EState.GameOver;
        SceneManager.LoadScene("GameOver");
    }
    public override FGameState.EState OnUpdate()
    {
        _deltaTime += Time.deltaTime;
        if (_deltaTime >= _targetTime)
            _nextState = FGameState.EState.Main;
        Debug.Log("Estou no GameOver");
        return _nextState;
    }

    public override void OnEnd()
    {

    }
}

public class FGameExiting : FState
{
    public FGameExiting(FGameState inGameState) : base(inGameState) { }
    public override void OnBegin()
    {
        Application.Quit();
    }
    public override FGameState.EState OnUpdate()
    {
        return 0;
    }
    public override void OnEnd()
    {

    }
}