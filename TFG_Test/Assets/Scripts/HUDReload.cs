using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDReload : MonoBehaviour
{
    [SerializeField] Image arc;
    [SerializeField] Image guide;
    private GameManager gameManager;

    [Range(0, 1)]
    public float ArcLength;

    [Range(0, 1)]
    public float progression;

    public int maxRangeTime = 5;
    private float fill, endProgresion, ReloadTime, actualTime;

    private bool Reloading;
    // Start is called before the first frame update
    void Start()
    {
        Reloading = false;
        actualTime = 0;
        this.gameObject.SetActive(false);
    }
    public void SetGameManager(GameManager GM) { gameManager = GM; }

    public void SetNewTime(float time)
    {

        // ArcLength cambia en funcion timepo de recarga
        ReloadTime = time;
        ArcLength = ReloadTime / maxRangeTime;
        if (ArcLength >= 1) ArcLength = 1;

        // Cambio de valores para que se vea correctamente
        arc.transform.rotation = Quaternion.Euler(0, 0, (90 - (180 * ArcLength)));
        arc.fillAmount = (-(180 - (180 * ArcLength)) / 180) + 1;
        fill = arc.fillAmount;
        endProgresion = 360 * fill;
        actualTime = 0;
    }

public void StartReloading()
{
    this.gameObject.SetActive(true);
    actualTime = 0;
    Reloading = true;
}
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Reloading)
        {
            // progression varia en funcion del timepo transcurrido
            actualTime += Time.deltaTime;

            progression = (actualTime / ReloadTime);

            guide.transform.localRotation = Quaternion.Euler(0, 0, progression * endProgresion);

            if (progression >= 1)
            {
                Reloading = false;
                gameManager.EndReloading();
                this.gameObject.SetActive(false);
            }
        }
    }
}
