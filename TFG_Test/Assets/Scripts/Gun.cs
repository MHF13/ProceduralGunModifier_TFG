using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;



public class Gun : MonoBehaviour
{
    private StarterAssetsInputs _inputs;
    private GunModifier02 GM;
    private FirstPersonController fpc;
    private bool canShoot;

    public GunType type;

    [Range(0, 100)]
    public int[] BS;

    //Base Stats
    private float damage0 = 10f;
    private float dispersion0 = 5f;
    private float recoil0 = 10f;
    private float reload0 = 1.5f;
    private float fireRate0 = 3f;
    private float maxAmmo0 = 12f;
    [Header("Stats")]
    public float damage;
    public float dispersion;
    public float recoil;
    public float reload;
    public float fireRate;
    public float maxAmmo;
    [Space]
    public bool auto;
    public int ammo;



    // Start is called before the first frame update
    void Start()
    {
        _inputs = transform.parent.parent.GetComponent<StarterAssetsInputs>();
        GM = this.GetComponent<GunModifier02>();
        canShoot = true;
        fpc = transform.parent.parent.GetComponent<FirstPersonController>();
    }

    float waitTillNextFire;
    // Update is called once per frame
    void Update()
    {

        if (_inputs.shoot && canShoot)
        {
            if (!auto)
            {
                _inputs.shoot = false;
            }
            ShootMethod();
            //StartCoroutine(Shoot());

        }
        waitTillNextFire -= Time.deltaTime;
    }

    // ---- Stats ----
    public void SetNewBS(int[] newBS)
    {
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
                break;
            default:
                break;
        }

    }

    void PistolStats()
    {
        // Mathf.Exp() = EXP
        // Mathf.Sqrt() = RAIZ

        float[] P1 = { 0.05f, 0.05f, 0.02f, 0.01f, -0.48f, -0.15f };
        float[] P2 = { 0.01f, 0.01f };
        float[] P3 = { -0.01f, 0.01f, 0.1f, 0.22f };


        // Damage
        damage = (100 - Mathf.Exp(-BS[0] * P1[0]) * 100) + damage0;

        // Dispersion
        dispersion = BS[0] * P1[1] + BS[1] * P2[0] + BS[2] * P3[0] + dispersion0;

        // Recoil
        recoil = (Mathf.Exp(BS[0] * P1[2]) - Mathf.Exp(BS[1] * P2[1])) * 2 + recoil0;

        // Reload
        reload = BS[0] * P1[3] + BS[2] * P3[1] + reload0;

        // FireRate
        fireRate = -Mathf.Sqrt(-(BS[0] + 1) * P1[4]) * Mathf.Sqrt(((BS[2] * P3[2]))) - (-fireRate0 - (BS[2] / 5));
        if (fireRate < 3) fireRate = 3;

        // MaxAmmo
        maxAmmo = (int)(BS[0] * P1[5] + BS[2] * P3[3] + maxAmmo0);
        if (maxAmmo < 1) maxAmmo = 1;
        ammo = (int)maxAmmo;
    }
    // ---------------
    // ---- Recoil ----
    [Space]
    [Range(0, 7f)]
    [SerializeField] private float recoilX, recoilY;

    private float CurrentRecoilX, CurrentRecoilY;

    public void RecoilFire()
    {
        CurrentRecoilX = ((Random.value - .5f) / 2) * recoilX;
        CurrentRecoilY = ((Random.value - .5f) / 2) * recoilY;

        fpc.SetNewRot(CurrentRecoilX, CurrentRecoilY, recoil);
    }
    // ---------------

    // ---- Shoot ----
    void ShootMethod()
    {
        //if (waitTillNextFire <= 0 && !reloading)
        if (waitTillNextFire <= 0)
        {
            if (ammo > 0)
            {
                Debug.Log("shoot!");

                // the bullet here
                Bullet();


                RecoilFire();

                ammo--;
                waitTillNextFire = 1/fireRate;

            }
            else
            {
                _inputs.shoot = false;
                ammo = (int)maxAmmo;
            }
        }
    }

    private RaycastHit hit;
    private float maxDistance = 1000000;
    [SerializeField] private GameObject bulletHole;
    void Bullet()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            Instantiate(bulletHole, hit.point + hit.normal * 0.1f, Quaternion.LookRotation(hit.normal));
        }
    }

}
