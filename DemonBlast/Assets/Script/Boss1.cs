using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour, IAlive
{

    //public Object target;
    //private GameObject targetGameObject;
	[SerializeField]
	private ParticleSystem particles;

	private ParticleSystem cloud;
    public GameObject attack;
	private Animator animator;
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
		animator = GetComponent<Animator> ();
        dtime = 0;
        isVulnerable = false;

    }

    // Update is called once per frame
	void Update ()
    {
		animator.SetBool ("Attack", false);
        if (dtime > AttackDelay && !isVulnerable)
        {
            dtime = 0;
			animator.SetBool ("Attack", true);
            for (int i = 0; i < 5; i++)
            {
                Instantiate(attack, Vector3.right * Random.Range(-70, 87) + Vector3.forward * Random.Range(-85, 83),
                    Quaternion.identity);
            }
        }
		dtime += Time.deltaTime;
		

		if ( animator.GetCurrentAnimatorStateInfo (0).IsName ("Death"))
		{
			animator.SetBool ("Dead", false);
		}
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("TakeDamage"))
		{
			animator.SetBool ("TakeDamage", false);
		}
    }
    
    public void MakeVulnerable()
    {
        if (!isVulnerable)
        {
            isVulnerable = true;
			animator.SetBool ("Stunned", true);
			Invoke ("EmitPart", 4);
            Invoke("EndVulnerable", 5);

        }
    }

	private void EmitPart()
	{
		particles.Play ();
	}

    public void EndVulnerable()
    {
        isVulnerable = false;
		animator.SetBool ("Stunned", false);
        int a = Random.Range(1, 5);
        this.transform.position = tpos[a];
    }
    
    public void TakeDamage(int amount)
    {
		if (isVulnerable) 
		{
			health -= amount;
			animator.SetBool ("TakeDamage", true);
		}
		if (health < 0)
		{
			animator.SetBool ("Dead", true);
		}
    }
}