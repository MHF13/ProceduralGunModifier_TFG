using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitParticle : MonoBehaviour
{
    [SerializeField]
    private float timeToDesactive = 0.3f;
    private float CurrentTime;
    [SerializeField]
    private TMPro.TextMeshPro Text;

    [SerializeField]
    private float maxScale;
    [SerializeField]
    private float minScale;
    //[SerializeField]private Vector3Int endPos;
    //private Vector3 startPos;
    public enum ParticleType
    {
        None,
        Damage
    }
    private ParticleType type;


    void Start()
    {
        CurrentTime = timeToDesactive;
        type = ParticleType.None;
    }

    public void ResetStart(RaycastHit hit,ParticleType _type, float damage)
    {
        gameObject.SetActive(true);

        transform.SetPositionAndRotation(hit.point + hit.normal * 0.5f, Quaternion.LookRotation(hit.normal));

        //startPos = hit.point + hit.normal * 0.1f;

        CurrentTime = timeToDesactive;
        Efect(_type, damage);
    }

    void Update()
    {
        switch (type)
        {
            case ParticleType.Damage:

                //Text.transform.rotation.LookAt(Camera.main.transform.position,Vector3.right);

                //transform.rotation = Quaternion.LookRotation(Camera.main.transform.position,Vector3.up);

                //transform.localRotation = Quaternion.LookRotation(Camera.main.transform.position);
                //transform.rotation += Quaternion.Euler(Vector3.up * 180f);*/

                transform.LookAt(Camera.main.transform.position);

                //transform.rotation = Quaternion.LookRotation(Camera.main.transform.position, Vector3.up*180f);

                //Text.fontSize = Mathf.Lerp (maxScale,minScale, timeToDesactive*2);
                //transform.localPosition = Vector3.Lerp(transform.localPosition,endPos, timeToDesactive);

                break;
            default:
                break;
        }

        CurrentTime -= Time.deltaTime;
        if (CurrentTime < 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Efect(ParticleType _type, float damage)
    {
        type = _type;
        switch (type)
        {
            case ParticleType.None:
                this.gameObject.GetComponent<MeshRenderer>().enabled = true;
                Text.gameObject.SetActive(false);

                break;
            case ParticleType.Damage:

                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                Text.gameObject.SetActive(true);

                Text.text = damage.ToString();
                Text.fontSize = maxScale;
                break;
            default:

                break;
        }
    }
}
