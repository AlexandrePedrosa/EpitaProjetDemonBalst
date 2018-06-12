using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss1 : MonoBehaviour, IAlive
{

    public Object target;
    private GameObject targetGameObject;
    public GameObject attack;
    public int AttackDelay;
    private float dtime;
    private float vtime;
    public int health;
    private bool isVulnerable;
    public int HP
    {
        get{return health;}
    }

    // Use this for initialization
    void Start()
    {
        Object currentTarget = target ?? gameObject;
        Behaviour targetBehaviour = currentTarget as Behaviour;
        targetGameObject = currentTarget as GameObject;
        dtime = AttackDelay;
        isVulnerable = false;
    }

    // Update is called once per frame
	void Update ()
    {
        if (dtime > AttackDelay)
        {
            dtime = 0;
            for (int i = 0; i < 5; i++)
            {
                Instantiate(attack, Vector3.right * Random.Range(-200, 200) + Vector3.forward * Random.Range(-200, 200),
                    Quaternion.identity);
            }
        }
        dtime += Time.deltaTime;
    }
    
    public void MakeVulnerable()
    {
        if (!isVulnerable)
        {
            isVulnerable = true;
            Invoke("EndVulnerable",10);
        }
    }

    public void EndVulnerable()
    {
        isVulnerable = false;
        
    }
    
    public void TakeDamage(int amount)
    {
        if (isVulnerable)
            health -= amount;
        if (health > 0)
            Destroy(this);
    }
}