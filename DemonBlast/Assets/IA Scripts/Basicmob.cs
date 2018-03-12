using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basicmob : MonoBehaviour {

    public Object target;
    private GameObject targetGameObject;
    public Vector3 A;
    public Vector3 B;
    private Vector3 rpos;
    public float ChasingSpeed;
    public float PatrolSpeed;
    public float range;

    // Use this for initialization
    void Start () {
        Object currentTarget = target ?? gameObject;
        Behaviour targetBehaviour = currentTarget as Behaviour;
        targetGameObject = currentTarget as GameObject;
        rpos = Vector3.positiveInfinity;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (IsClose())
        {
            if (Vector3.Equals(rpos,Vector3.positiveInfinity))
                rpos = this.gameObject.transform.position;
            Vector3 dir = (targetGameObject.transform.position - this.gameObject.transform.position);
            dir.Normalize();
            this.gameObject.transform.Translate(dir * Time.deltaTime * ChasingSpeed);
        }
        else
        {
            if(!Vector3.Equals(rpos,Vector3.positiveInfinity))
            {
                Vector3 dir = (rpos - this.gameObject.transform.position);
                dir.Normalize();
                this.gameObject.transform.Translate(dir * Time.deltaTime * ChasingSpeed);
                if (Vector3.Distance(this.gameObject.transform.position, rpos) < 0.1)
                {
                    this.gameObject.transform.position = rpos;
                    rpos = Vector3.positiveInfinity;
                }
            }
            else
            {
                if (Vector3.Distance(this.gameObject.transform.position, B) < 0.1)
                {
                    this.gameObject.transform.position = B;
                    Vector3 C = A;
                    A = B;
                    B = C;
                }
                else
                {
                    Vector3 dir = (B-A);
                    dir.Normalize();
                    this.gameObject.transform.Translate(dir * Time.deltaTime * PatrolSpeed);
                } 
            }
        }
	}

    private bool IsClose()
    {
        return Vector3.Distance(targetGameObject.transform.position,this.gameObject.transform.position) < range;
    }
}
