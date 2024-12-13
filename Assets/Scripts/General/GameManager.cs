using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Events
    public static event Action onGameReady;
    #endregion
    #region Variables
    private static GameManager _i;
    public static GameManager i { get { return _i; } }
    [SerializeField] private Transform sysMessagePoint;
    [SerializeField] private Transform playerSpawnPoint, sunSpawnPoint;
    [SerializeField] private LevelStatsSO levelStatsSO;
    [SerializeField] private CameraController _cameraController;
    private GameObject playerGO, sunGO;
    private bool isPaused;

    #endregion
    
    #region Initialize
    private void Awake() 
    {
        _i = this;  
        SetupObjectPools();  
        Initialize();
    }

    private void Initialize() 
    {
        SpawnPlayerObject();
    }

    private void SpawnPlayerObject()
    {
        playerGO = Instantiate(GameAssets.i.pfPlayerObject, playerSpawnPoint);
        playerGO.transform.parent = null;
        playerGO.GetComponent<IHandler>().Initialize();
        playerGO.GetComponent<ISizeHandler>().Initialize();
        SpawnSun();
    }

    private void SpawnSun()
    {
        sunGO = Instantiate(GameAssets.i.pfSunObject, sunSpawnPoint);
        sunGO.transform.parent = null;
        sunGO.GetComponent<ISunController>().Initialize();
        onGameReady?.Invoke();
    }

    public void SetupObjectPools()
    {
        //Do the below for all objects that will need pooled for use
        //ObjectPooler.SetupPool(OBJECT, SIZE, "NAME") == Object is pulled from GameAssets, Setup object with a SO that contains size and name
        
        //The below is placed in location where object is needed from pool
        //==============================
        //PREFAB_SCRIPT instance = ObjectPooler.DequeueObject<PREFAB_SCRIPT>("NAME");
        //instance.gameobject.SetActive(true);
        //instance.Initialize();
        //==============================
    }
    #endregion

    public void PauseGame(){if(isPaused) return; else isPaused = true;}
    public void UnPauseGame(){if(isPaused) isPaused = false; else return;}
    
    public Transform GetSysMessagePoint(){ return sysMessagePoint;}
    public GameObject GetPlayerGO() { return playerGO; }
    public LevelStatsSO GetLevelStats(){return levelStatsSO;}
    public bool GetIsPaused() { return isPaused; }

}
