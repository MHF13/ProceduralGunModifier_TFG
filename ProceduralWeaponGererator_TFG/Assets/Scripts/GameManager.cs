using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private HUDReload hud;

    [SerializeField]
    private Gun theGun;

    [SerializeField]
    private FirstPersonController firstPerson;

    [SerializeField]
    private PauseManager pauseManager;

    private bool pause;

    // Start is called before the first frame update
    private void Awake()
    {
        hud.SetGameManager(this);
        theGun.SetGameManager(this);
        //pauseManager.SetGameManager(this);
        firstPerson.SetGameManager(this);
    }

    void Start()
    {
        pause = false;
        /*
        if (!hud)
        {
            Debug.Log("No hay hud puesto"); 
            hud = GameObject.FindGameObjectWithTag("HUDReload").GetComponent<HUDReload>();
        }
        //hud.SetGameManager(this);
        if (!theGun)
        {
            Debug.Log("No hay hud puesto");
            theGun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
        }
        Debug.Log("GameManager");
        //theGun.SetGameManager(this);*/
    }

    public void SetActivePauseMenu(bool state)
    {
        pause = state;
        pauseManager.gameObject.SetActive(pause);
        firstPerson.setPause(pause);

        hud.transform.parent.gameObject.SetActive(!pause);

        if (pause == true)
            pauseManager.GetBSValues();
            
    }

    public void SetNewStats(float[] stats)
    {
        pauseManager.SetStats(stats);
    }

    public HUDReload GetHUD() { return hud; }

    public void SetReloadTime(float reloadTime) { hud.SetNewTime(reloadTime); }
    public void StartReloading() { hud.StartReloading(); }
    public void EndReloading() { theGun.EndReload(); }

}
