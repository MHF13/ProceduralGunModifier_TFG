using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;


public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject PlayerCapsule;

    private StarterAssetsInputs _inputs;
    private FirstPersonController fpc;
    private GameManager gameManager;


    // No hes necesario el; serialize por see busca en el start, es para que el gizmo funcione
#pragma warning disable CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva
    [SerializeField]
    private Camera camera;
#pragma warning restore CS0108 // El miembro oculta el miembro heredado. Falta una contraseña nueva

    public GunType type;

    [Range(0, 100)]
    public int[] BS;

    //Base Stats
    private float damage0 = 10f;
    private float dispersion0 = 2f;
    private float recoil0 = 1f;
    private float reload0 = 1.5f;
    private float fireRate0 = 3f;
    private float maxAmmo0 = 12f;
    private int bulletXShoot0 = 1;


    
    [Header("Stats")]
    public float damage;
    public float dispersion;
    public float recoil;
    public float reload;
    public float fireRate;
    public float maxAmmo;
    [Space]
    // TODO: Quitarl el Serial cuando se arregle la dispersion
    [SerializeField]
    private int bulletXShoot;

    [Header("variables")]
    public bool auto = false;
    public int ammo;
    public bool reloading;
    //Todo: cambiar a privado
    public bool pause;

    // Start is called before the first frame update
    void Awake()
    {
        camera = Camera.main.GetComponent<Camera>();
        _inputs = PlayerCapsule.GetComponent<StarterAssetsInputs>();
        reloading = false;
        pause = false;
        fpc = PlayerCapsule.GetComponent<FirstPersonController>();
    }

    public void SetGameManager(GameManager _GM) { gameManager = _GM; }

    float waitTillNextFire;
    // Update is called once per frame
    void Update()
    {
        if (_inputs.pause)
        {
            _inputs.pause = false;
            pause = !pause;
            _inputs.SetCursorState(!pause);
            gameManager.SetActivePauseMenu(pause);
        }

        if (pause){ return; }

        if (_inputs.shoot && !reloading)
        {
            if (!auto)_inputs.shoot = false;
            ShootMethod();
        }
        if (_inputs.reload && !reloading && ammo < maxAmmo+1)
        {
            Reload();
        }
        waitTillNextFire -= Time.deltaTime;

    }
    // ---------------
    #region Stats
    public void SetNewBS(int[] newBS, GunType gunType)
    {

        type = gunType;
        if (BS.Length == 0) BS = new int[newBS.Length];

        for (int i = 0; i < newBS.Length; i++)
        {
            BS[i] = newBS[i];
        }
        NewStats();
    }
    void NewStats()
    {
        switch (type)
        {
            case GunType.Pistol:
                PistolStats();
                break;
            case GunType.ShootGun:
                ShootGunStats();
                break;
            default:
                break;
        }
        gameManager.SetReloadTime(reload);
    }
    void PistolStats()
    {
        // Mathf.Exp() = EXP
        // Mathf.Sqrt() = RAIZ

        float[] P1 = { 0.05f, 0.05f, 0.01f, 0.01f, -0.48f, -0.15f };
        float[] P2 = { -0.02f, 0.01f };
        float[] P3 = { 0.01f, 0.01f, 0.1f, 0.22f };


        // Damage
        damage = (100 - Mathf.Exp(-BS[0] * P1[0]) * 100) + damage0;

        // Dispersion
        dispersion = BS[0] * P1[1] + BS[1] * P2[0] + BS[2] * P3[0] + dispersion0;

        // Recoil
        recoil = (Mathf.Exp(BS[0] * P1[2]) - Mathf.Exp(BS[1] * P2[1])) * 2 + recoil0;
        if (recoil < 0.5f) recoil = 0.5f;

        // Reload
        reload = BS[0] * P1[3] + BS[2] * P3[1] + reload0;

        // FireRate
        fireRate = -Mathf.Sqrt(-(BS[0] + 1) * P1[4]) * Mathf.Sqrt(((BS[2] * P3[2]))) - (-fireRate0 - (BS[2] / 5));
        if (fireRate < 3) fireRate = 3;

        // MaxAmmo
        maxAmmo = (int)(BS[0] * P1[5] + BS[2] * P3[3] + maxAmmo0);
        if (maxAmmo < 3) maxAmmo = 3;
        ammo = (int)maxAmmo;

        // when shoot you shoot x bullets
        bulletXShoot = bulletXShoot0;
    }
    void ShootGunStats()
    {
        //Create stats for other gun type
    }
    #endregion
    // ---------------
    #region Reload
private void Reload()
{
    reloading = true;
    gameManager.StartReloading();
}
public void EndReload()
{
    reloading = false;
    ammo = (int)maxAmmo;
    if (_inputs.reload) ammo += 1;
    _inputs.reload = false;
}
    #endregion
    // ---------------
    #region Shoot
    private void ShootMethod()
    {
        if (waitTillNextFire <= 0)
        {
            if (ammo > 0)
            {
                for (int i = 0; i < bulletXShoot; i++)
                {
                    Bullet();
                }

                // Recoil
                fpc.SetCamRecoil(recoil);

                ammo--;
                waitTillNextFire = 1/fireRate;
            }
            else
            {
                Reload();
                _inputs.shoot = false;
            }
        }
    }

    private RaycastHit hit;
    private float maxDis = 1000000;
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private BulletHollePooling bulletHollePooling;
    private void Bullet()
    {
        Vector3 direction = Desviation();
        if (Physics.Raycast(Camera.main.transform.position, direction, out hit, maxDis))
        {

                if (hit.transform.CompareTag("Dummie"))
                {
                    bulletHollePooling.RequesHole(hit, BulletHitParticle.ParticleType.Damage,damage);

                    /*DestroyAfterTimeParticle holle = Instantiate(bulletHole, hit.point + hit.normal * 0.1f, Quaternion.LookRotation(hit.normal)).GetComponent<DestroyAfterTimeParticle>();
                    holle.Efect(DestroyAfterTimeParticle.ParticleType.Damage, damage);*/
                }
                else
                {
                    bulletHollePooling.RequesHole(hit, BulletHitParticle.ParticleType.None);
                    //Instantiate(bulletHole, hit.point + hit.normal * 0.1f, Quaternion.LookRotation(hit.normal));
                }
                
        }
        else
        {
            // Fail shoot
        }
    }
private Vector3 Desviation()
{
    float desviation = dispersion / 10;
    //
    //using Random = UnityEngine.Random;
    Vector3 direction = camera.transform.forward; // your initial aim.
    Vector3 spread = Vector3.zero;
    spread += camera.transform.up * Random.Range(-desviation, desviation); // add random up or down (because random can get negative too)
    spread += camera.transform.right * Random.Range(-desviation, desviation); // add random left or right

    // Using random up and right values will lead to a square spray pattern. If we normalize this vector, we'll get the spread direction, but as a circle.
    // Since the radius is always 1 then (after normalization), we need another random call. 
    return direction += spread.normalized * Random.Range(0, desviation/100);
}
    #endregion


    // -------Gizmos--------

    //Gizmos 
    //Para ver la dispersion que puede tener el arma sin ejecutar
    private void OnDrawGizmosSelected()
    {
        // the bullet here
        for (int i = 0; i < bulletXShoot; i++)
        {
            Vector3 direction = Desviation();
            //--
            //
            if (Physics.Raycast(camera.transform.position, direction, out hit, maxDis))
            {
                Debug.DrawLine(camera.transform.position, hit.point, Color.green, 1f);
            }
            else
            {
                Debug.DrawLine(camera.transform.position, camera.transform.position + direction * maxDis, Color.red, 1f);
            }
            //--
        }
    }
}


