using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Boss1 : MonoBehaviour, IAlive
{

    //public Object target;
    //private GameObject targetGameObject;
    public Material mat1;
    public Material mat2;
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
    static Dictionary<int,Vector3> tpos = new Dictionary<int, Vector3>()
    {
        {1,new Vector3(3.5F,2F,-3.5F)},
        {2,new Vector3(66.5F,2F,-39F)},
        {3,new Vector3(-29.5F,2F,-64F)},
        {4,new Vector3(-41F,2F,24F)},
        {5,new Vector3(29F,2F,61.5F)}
    };

    // Use this for initialization
    void Start()
    {
        /*Object currentTarget = target ?? gameObject;
        Behaviour targetBehaviour = currentTarget as Behaviour;
        targetGameObject = currentTarget as GameObject;*/
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
                Instantiate(attack, Vector3.right * Random.Range(-70, 87) + Vector3.forward * Random.Range(-85, 83),
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
            this.GetComponent<MeshRenderer>().material = mat1;
            Invoke("EndVulnerable",10);
        }
    }

    public void EndVulnerable()
    {
        isVulnerable = false;
        int a = Random.Range(1, 5);
        this.transform.position = tpos[a];
        this.GetComponent<MeshRenderer>().material = mat2;
    }
    
    public void TakeDamage(int amount)
    {
        if (isVulnerable)
            health -= amount;
        if (health > 0)
            Destroy(this);
    }
}