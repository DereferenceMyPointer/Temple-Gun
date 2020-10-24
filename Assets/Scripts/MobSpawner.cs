using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject mob;
    public float timeout;
    public float variance;
    public int maxSpawns;
    public List<GameObject> spawns;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        for(int i = 0; i < spawns.Count; i++)
        {
            if(spawns[i] == null)
            {
                spawns.Remove(spawns[i]);
                i--;
            }
        }
        if(spawns.Count < maxSpawns)
        {
            spawns.Add(Instantiate(mob, transform.position, Quaternion.identity));
        }
        yield return new WaitForSeconds(Random.Range(timeout - variance, timeout + variance));
        StartCoroutine(Spawn());
    }

}
