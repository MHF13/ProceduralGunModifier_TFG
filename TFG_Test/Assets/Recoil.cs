using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Recoil : MonoBehaviour
{


    /*void Update()
    {
        
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);

        currentRotation = Vector3.Slerp(currentRotation,targetRotation,snappiness * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void RecoilFire()
    {
        Debug.Log("Recoil");
        targetRotation += new Vector3(-recoilX,Random.Range(-recoilY,recoilY),0);
    }*/

    // ---- Recoil ----
    [Range(0, 7f)]
    [SerializeField] private float recoilX, recoilY;

    public float CurrentRecoilX, CurrentRecoilY;

    [SerializeField] private float snappiness;
    [SerializeField] private float returnSpeed;

    public void RecoilFire()
    {
        /*CurrentRecoilX = ((Random.value - .5f)/2) * recoilX;
        CurrentRecoilX = ((Random.value - .5f)/2) * recoilY;

        transform.localRotation = Mathf.Abs(CurrentRecoilY);*/
    }

}
