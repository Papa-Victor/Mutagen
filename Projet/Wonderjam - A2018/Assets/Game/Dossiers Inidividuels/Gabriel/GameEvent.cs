using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

//  système de wave par map, 1 map, X ennemy, max Y qui apparaisse en même temps
//  quand X mort, map fini

public class GameEvent : MonoBehaviour {

    public int m_nbEnnemyTotal = 30;
    public int m_maxEnnemiWave = 5;

    public GameObject m_CoinPrefab;

    //  liste des ennemis à spawner
    public List<GameObject> m_lstEnnemy;

    public SceneInfo CurrentLevel;
    public SceneInfo m_NextScene;

    public bool malusApplied = false;

    [SerializeField]
    private SceneInfo endGameScreenSceneInfo;
    [SerializeField]
    private SceneInfo mainMenuSceneInfo;
    [SerializeField]
    private SceneInfo shopSceneInfo;

    //  liste des spawner à ennemi
    GameObject[] m_lstSpawner;

    int m_currentNbEnnemis = 0;
    int m_totalSpawned = 0;
    int m_killCount = 0;

    float m_timeBetweenEnnemy = 4.0f;
    float m_currentTimer = 0.0f;

    private bool canUpdate = false;


    private void Start()
    {
        GameManager.OnGameReady += Init;
        GameManager.OnGameStart += AllowUpdate;
    }

    public void Init()
    {
        Debug.Log("Init");
        //  Affiche le début de la partie "battle begin" du UI ou le faire dans le spawner
        GameObject[] tblHud = GameObject.FindGameObjectsWithTag("HUD");
        if (tblHud.Length > 0)
            tblHud[0].GetComponent<hudManager>().setEnnemyCount(m_nbEnnemyTotal, false);

        m_lstSpawner = GameObject.FindGameObjectsWithTag("Spawner");

        GameManager.OnGameEnd += GameOver;
    }

    public void AllowUpdate()
    {
        Debug.Log("allowUpdate");
        canUpdate = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (canUpdate)
        {
            if (m_currentNbEnnemis < m_maxEnnemiWave && m_totalSpawned < m_nbEnnemyTotal)
            {
                m_currentTimer += Time.deltaTime;

                if (m_currentTimer > m_timeBetweenEnnemy)
                {
                    m_currentNbEnnemis++;
                    m_totalSpawned++;
                    m_currentTimer = 0.0f;

                    // spawn un ennemis
                    int spawnerIndex = Random.Range(0, m_lstSpawner.Length);
                    int ennemyIndex = Random.Range(0, m_lstEnnemy.Count);

                    Instantiate(m_lstEnnemy[ennemyIndex], m_lstSpawner[spawnerIndex].transform.position, m_lstSpawner[spawnerIndex].transform.rotation);
                }
            }

            if (Input.GetKeyDown("x"))
            {
                GameObject[] tblG = GameObject.FindGameObjectsWithTag("Enemy");
                if (tblG.Length > 0)
                {
                    tblG[0].Destroy();
                    SetKillEnnemy();
                    Debug.Log("montre Mouru");
                }
                else
                    Debug.Log("Pas de monstre");
            }
            else if (Input.GetKeyDown("z"))
            {
                RoundWin();
            }
            else if (Input.GetKeyDown("q"))
            {
                MortPlayer();
            }
        }
	}

    private void GameOver()
    {
        canUpdate = false;
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Scenes.Load(endGameScreenSceneInfo, (endGameScreenScene) =>
        {
            endGameScreenScene.FindRootObject<EndGameScript>().Init(() =>
            {
                Scenes.Load(mainMenuSceneInfo);
            });
        });
    }

    //  Quand vous tuer un ennemi, appeler ici pour monter le killcount et diminuer
    //  le CurrentNbEnnemy
    public void SetKillEnnemy()
    {
        m_currentNbEnnemis--;
        m_killCount++;

        GameObject[] tblHud = GameObject.FindGameObjectsWithTag("HUD");
        if (tblHud.Length > 0)
            tblHud[0].GetComponent<hudManager>().setEnnemyCount(m_nbEnnemyTotal - m_killCount, true);

        //  le joueur à tuer tous les ennemis
        if (m_killCount == m_nbEnnemyTotal)
        {
            Sequence seq = DOTween.Sequence();
            GameObject[] tblPlayer = GameObject.FindGameObjectsWithTag("Player");
            
            
            this.DelayedCall(() =>
            {
                ((GetComponent<SpriteRenderer>()).DOFade(255.0f, 4.0f)).SetEase(Ease.InQuad);
            }, 2.0f);

            this.DelayedCall(() => { Debug.Log("Load la gestion");
                if (tblPlayer.Length > 0)
                    tblPlayer[0].GetComponent<PlayerController>().SetStunned(10.0f);
            }, 3.0f);

            Debug.Log("vous avez gagné GG WP");
            RoundWin();
            //  Condition de fin, tous les ennemis sont mort
        }
    }

    //  quand un ennemis meurt, s'il drop un coin, il appelle la fonction en passant ça position
    public void DropCoin(Vector3 pos)
    {
        if(m_CoinPrefab)
            Instantiate(m_CoinPrefab, pos, Quaternion.identity);
    }



    //  quand le player meurt, retour à l'écran d'accueille
    public void MortPlayer()
    {
        GameOver();
    }

    private void RoundWin()
    {
        if (m_NextScene == null)
            GameOver();
        else
        {
            this.DelayedCall(() => 
            {
                Time.timeScale = 0;
                Scenes.Load(shopSceneInfo, (shopScene) =>
                {
                    shopScene.FindRootObject<ShopManager>().Init(() =>
                    {
                        Time.timeScale = 1;
                        ClearLevel();
                        Scenes.Load(m_NextScene, (nextScene) =>
                        {
                            nextScene.GetRootGameObjects()[0].GetComponentInChildren<GameEvent>().Init();
                            this.DelayedCall(() =>
                            {
                                nextScene.GetRootGameObjects()[0].GetComponentInChildren<GameEvent>().AllowUpdate();
                            }, 0.1f);
                            Scenes.UnloadAsync(CurrentLevel);
                            Scenes.UnloadAsync("UICombat");
                        });
                    });
                });
            }, 2f);
        }
        
    }

    private void ClearLevel()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
