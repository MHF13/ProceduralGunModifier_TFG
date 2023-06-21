using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private HUDReload hud;

    [SerializeField]
    private Gun theGun;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if (!hud)
        {
            Debug.Log("No hay hud puesto"); 
            hud = GameObject.FindGameObjectWithTag("HUDReload").GetComponent<HUDReload>();
        }
            hud.SetGameManager(this);
        if (!theGun)
        {
            Debug.Log("No hay hud puesto");
            theGun = GameObject.FindGameObjectWithTag("Gun").GetComponent<Gun>();
        }
            theGun.SetGameManager(this);

    }

    public HUDReload GetHUD() { return hud; }

    public void SetReloadTiem(float reloadTime) { hud.SetNewTime(reloadTime); }
    public void StartReloading() { hud.StartReloading(); }
    public void EndReloading() { theGun.EndReload(); }

    // Update is called once per frame
    void Update()
    {
        
    }


}
