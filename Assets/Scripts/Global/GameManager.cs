using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    public List<NamedGameObject> gameModes;

    private bool GameModeSetupDone = false;
    private bool GameModeTearDownDone = false;
    private bool UnloadedGameMode = false;
    private bool LoadedGameMode = false;
    private bool isSwapingGameModes = false;

    public override void Awake()
    {
        base.Awake();
        
        if(gameModes.Count == 0)
        {
            Debug.LogWarning("The current GameManager has no assigned GameModes");
            return;
        }

        StartCoroutine(LoadGameMode(gameModes[0]));
    }

    public void SwitchToGameMode(string gameModeName)
    {
        NamedGameObject newGameMode = Utils.FindNamedGameObjectByName(gameModeName, ref gameModes);
        
        if(newGameMode.name == "")
        {
            Debug.LogError("Attempted to load GameMode " + gameModeName + " but it does not exist on the GameManager");
            return;
        }
        
        StartCoroutine(SwapGameModes(newGameMode));
    }
    
    public void OnGameModeSetupFinish() => GameModeSetupDone = true;
    public void OnGameModeTearDownFinish() => GameModeTearDownDone = true;

    private IEnumerator SwapGameModes(NamedGameObject newGameMode)
    {
        Debug.Log("Starting SwapGameModes");
        
        isSwapingGameModes = true;

        StartCoroutine(UnloadCurrentGameMode());
        yield return new WaitUntil(() => UnloadedGameMode = true);
        UnloadedGameMode = false;

        StartCoroutine(LoadGameMode(newGameMode));
        yield return new WaitUntil(() => LoadedGameMode = true);
        LoadedGameMode = false;

        isSwapingGameModes = false;

        Debug.Log("Ending SwapGameModes");
    }

    private IEnumerator UnloadCurrentGameMode()
    {
        Debug.Log("Starting UnloadCurrentGameMode");
        
        UnloadedGameMode = false;

        GameMode.Instance.TearDown();

        yield return new WaitUntil(() => GameModeTearDownDone == true);
        GameModeTearDownDone = false;

        Destroy(GameMode.Instance.gameObject);

        yield return null;
        UnloadedGameMode = true;

        Debug.Log("Ending UnloadCurrentGameMode");
    }

    private IEnumerator LoadGameMode(NamedGameObject newGameMode)
    {
        Debug.Log("Starting LoadGameMode");
        LoadedGameMode = false;

        yield return null;

        Debug.Log("Trying to instatiate " + newGameMode.obj);
        Instantiate(newGameMode.obj);

        // yield return new WaitUntil(() => !(GameObject.FindObjectOfType<GameMode> == null));

        GameMode gameMode = GameMode.Instance;
        GameMode.Instance.OnSetupEnd.AddListener(OnGameModeSetupFinish);
        GameMode.Instance.OnTearDownEnd.AddListener(OnGameModeTearDownFinish);
        GameMode.Instance.Setup();
        yield return new WaitUntil(() => GameModeSetupDone == true);
        GameModeSetupDone = false;

        LoadedGameMode = true;
        Debug.Log("Ending LoadGameMode");
    }
}