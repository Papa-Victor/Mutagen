using UnityEngine;
using CCC.DesignPattern;
using System.Collections.Generic;
using System;

public delegate void SimpleEvent();

public partial class GameManager : PublicSingleton<GameManager> {

    [System.NonSerialized, HideInInspector]
    public GameState gameState = GameState.NotReady;
    static private event SimpleEvent onGameReady;
    static private event SimpleEvent onGameStart;
    static private event SimpleEvent onGameEnd;

    public enum GameState { NotReady, Ready, Started, Over}

    [HideInInspector]
    public Locker gameRunning = new Locker();

    static public event SimpleEvent OnGameReady
    {
        add
        {
            if (instance != null && instance.gameState >= GameState.Ready)
                value();
            else
                onGameReady += value;
        }
        remove { onGameReady -= value; }
    }

    static public event SimpleEvent OnGameStart
    {
        add
        {
            if (instance != null && instance.gameState >= GameState.Started)
                value();
            else
                onGameStart += value;
        }
        remove { onGameStart -= value; }
    }

    static public event SimpleEvent OnGameEnd
    {
        add
        {
            if (instance != null && instance.gameState >= GameState.Over)
                value();
            else
                onGameEnd += value;
        }
        remove { onGameEnd -= value; }
    }

    [SerializeField]
    private SceneInfo shopSceneInfo;

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onGameReady = null;
        onGameStart = null;
    }

    void Update()
    {
            
    }

    private void GameRunning_onLockStateChange(bool state)
    {
        if (state)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    protected override void Awake()
    {
        base.Awake();
        gameRunning.onLockStateChange += GameRunning_onLockStateChange;
        PersistentLoader.LoadIfNotLoaded(InitGame);
    }

    void FetchAllReferences(Action onComplete)
    {
        // C'est ici qu'on peut aller chercher tous les références.


        onComplete();
    }

    
    public SceneInfo gameSceneInfo;

    public SceneInfo CurrentLevelSceneInfo;

    [SerializeField]
    private SceneInfo UISceneInfo;

    public void Init(SceneInfo currentSceneInfo)
    {

    }

    void InitGame()
    {
        FetchAllReferences(() =>
        {
            Scenes.Load(CurrentLevelSceneInfo, (currentLevelScene) =>
            {
                Scenes.Load(shopSceneInfo, (shopScene) =>
                {
                    shopScene.FindRootObject<ShopManager>().Init(() =>
                    {
                        Scenes.Load(UISceneInfo, (UIScene) => 
                        {
                            UIScene.FindRootObject<hudManager>().Init(() =>
                            {
                                ReadyGame();
                                this.DelayedCall(() =>
                                {
                                    StartGame();
                                }, 0.1f);
                            });
                            
                        });

                        
                    });
                });
            });

           
            
        });
    }

    void ReadyGame()
    {
        if (gameState >= GameState.Ready)
            return;

        gameState = GameState.Ready;

        LoadingScreen.OnNewSetupComplete();

        if (onGameReady != null)
        {
            onGameReady();
            onGameReady = null;
        }
    }
    void StartGame()
    {
        if (gameState >= GameState.Started)
            return;

        gameState = GameState.Started;

        // Init Game Start Events
        if (onGameStart != null)
        {
            onGameStart();
            onGameStart = null;
        }
    }

    public void EndGame()
    {
        // End Game Screen
        if (gameState >= GameState.Over)
            return;

        gameState = GameState.NotReady;
        

        if (onGameEnd != null)
        {
            onGameEnd();
            onGameEnd = null;
            Awake();
        }
    }
}
