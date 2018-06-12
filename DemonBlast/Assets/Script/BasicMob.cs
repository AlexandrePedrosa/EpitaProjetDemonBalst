using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMob : MonoBehaviour {

    public Object target;
    private GameObject targetGameObject;
    public Vector3 A;
    public Vector3 B;
    private Vector3 rpos;
    public float ChasingSpeed;
    public float PatrolSpeed;
    public float DetectRange;
    public float AttackRange;
    private GameObject attack;
    public int AttackDelay;
    public int damage;
    private float dtime;

    // Use this for initialization
    void Start()
    {
        Object currentTarget = target ?? gameObject;
        Behaviour targetBehaviour = currentTarget as Behaviour;
        targetGameObject = currentTarget as GameObject;
        rpos = Vector3.positiveInfinity;
        /*attack = GameObject.CreatePrimitive(PrimitiveType.Cube);
        attack.GetComponent<BoxCollider>().isTrigger = true;
        attack.transform.localScale = Vector3.one;
        attack.GetComponent<MeshRenderer>().enabled = false;
        attack.AddComponent<DealDamage>();
        attack.GetComponent<DealDamage>().damage = damage;*/
        dtime = AttackDelay;

    }

    // Update is called once per frame
	void Update ()
    {
        if (CanAttack())
        {
            gameObject.transform.LookAt(targetGameObject.transform);
            // Attack
            if (dtime > AttackDelay)
            {
                dtime = 0;
                //var t = Instantiate(attack, this.transform.position + transform.forward * 2, this.transform.rotation, gameObject.transform);
                //Destroy(t);
                targetGameObject.GetComponent<IAlive>().TakeDamage(damage);
            }
            dtime += Time.deltaTime;
            //
        }
        else
        {
            if (IsClose())
            {
                if (Vector3.Equals(rpos, Vector3.positiveInfinity))
                    rpos = this.gameObject.transform.position;
                gameObject.transform.LookAt(targetGameObject.transform);
                this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * ChasingSpeed);
            }
            else
            {
                if (!Vector3.Equals(rpos, Vector3.positiveInfinity))
                {
                    gameObject.transform.LookAt(rpos);
                    this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * ChasingSpeed);
                    dtime = AttackDelay;
                    if (Vector3.Distance(this.gameObject.transform.position, rpos) < 0.1)
                    {
                        this.gameObject.transform.position = rpos;
                        rpos = Vector3.positiveInfinity;
                    }
                }
                else
                {
                    gameObject.transform.LookAt(B);
                    if (Vector3.Distance(this.gameObject.transform.position, B) < 0.1)
                    {
                        this.gameObject.transform.position = B;
                        Vector3 C = A;
                        A = B;
                        B = C;
                    }
                    else
                    {
                        this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * PatrolSpeed);
                    }
                }
            }
        }
    }

    private bool IsClose()
    {
        return Vector3.Distance(targetGameObject.transform.position,this.gameObject.transform.position) < DetectRange;
    }
    
    private bool CanAttack()
    {
        return Vector3.Distance(targetGameObject.transform.position,this.gameObject.transform.position) < AttackRange;
    }
}