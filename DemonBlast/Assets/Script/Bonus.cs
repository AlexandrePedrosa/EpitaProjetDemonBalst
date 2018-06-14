using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Bonus : MonoBehaviour
{

    public enum BonusType
    {
        SpeedUp = 0,
        SpeedDown = 1,
        Invisibility = 2,
        JumpBoost = 3,
        
    }
    
    public BonusType type = BonusType.SpeedUp;
    private Collider player;
    
    private void OnTriggerEnter(Collider other)
    {
        if (type == BonusType.SpeedUp)
        {
            var a = other.gameObject.GetComponent<CharacterControllerLogicMulti>();
            //a.moveSpeedMultiplier += 0.2f;
            Invoke("End",5);

        }
        if (type == BonusType.SpeedDown)
        {
            var a = other.gameObject.GetComponent<CharacterControllerLogicMulti>();
            //a.moveSpeedMultiplier -= 0.2f;
            Invoke("End",5);

        }
        if (type == BonusType.Invisibility)
        {
            var a = other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
			if (a != null) 
			{
				a.enabled = false;
				Invoke ("End", 5);
				gameObject.GetComponent<BoxCollider> ().enabled = false;
				gameObject.GetComponent<MeshRenderer> ().enabled = false;
			}

        }
        if (type == BonusType.JumpBoost)
        {
            var a = other.gameObject.GetComponent<CharacterControllerLogicMulti>();
			if (a != null)
			{
				a.jumpDist += 2f;
				a.jumpForce += 2f;
				Invoke ("End", 5);
				gameObject.GetComponent<BoxCollider> ().enabled = false;
				gameObject.GetComponent<MeshRenderer> ().enabled = false;
			}
        }
        player = other;

        

    }

    private void End()
    {
        if (type == BonusType.SpeedUp)
        {
            var a = player.gameObject.GetComponent<CharacterControllerLogicMulti>();
            //a.moveSpeedMultiplier -= 0.2f;

        }
        if (type == BonusType.SpeedDown)
        {
            var a = player.gameObject.GetComponent<CharacterControllerLogicMulti>();
            //a.moveSpeedMultiplier += 0.2f;

        }
        if (type == BonusType.Invisibility)
        {
			var a = player.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
			if (a != null) 
			{
				a.enabled = true;
			}
        }
        if (type == BonusType.JumpBoost)
        {
            var a = player.gameObject.GetComponent<CharacterControllerLogicMulti>();
            a.jumpDist -= 2f;
            a.jumpForce -= 2f;
        }
        Destroy(gameObject);
    }
}