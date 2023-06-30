using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHollePooling : MonoBehaviour
{
    [SerializeField] private GameObject BulletHolePrefab;
    [SerializeField] private List<GameObject> List;
    [SerializeField] private int size = 10;

    private int index;

    /*private static BulletHollePooling instance;
    public static BulletHollePooling Instance { get { return instance; } }

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);
    }*/
    private void Start()
    {
        AddHoles(size);
    }

    private void AddHoles(int _size)
    {
        for (int i = 0; i < _size; i++)
        {
            GameObject hole = Instantiate(BulletHolePrefab);
            hole.SetActive(false);
            List.Add(hole);
        }
    }

    public void RequesHole(RaycastHit hit,BulletHitParticle.ParticleType type,float damage = 0)
    {
        for (int i = 0; i < List.Count; i++)
        {
            if (!List[i].activeSelf)
            {
                index = i;
                List[i].GetComponent<BulletHitParticle>().ResetStart(hit, type, damage);
                return;
            }
        }
        if (index+1 == List.Count)
        {
            index=0;
            List[index].SetActive(false);
            List[index].GetComponent<BulletHitParticle>().ResetStart(hit, type, damage);
        }
        else
        {
            index += 1;
            List[index].SetActive(false);
            List[index].GetComponent<BulletHitParticle>().ResetStart(hit, type, damage);
        }
    }
}
