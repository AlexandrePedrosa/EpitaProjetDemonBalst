using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

/*public class Bonus : MonoBehaviour
{

    public enum BonusType
    {
        SpeedUp = 0,    // Just broadcast the action on to the target
        SpeedDown = 1,    // replace target with source
        Invisibility = 2,   // Activate the target GameObject
        JumpBoost = 3,     // Enable a component
        Animate = 4,    // Start animation on target
        Deactivate = 5, // Decativate target GameObject
        Move = 6
    }

    public BonusType type = BonusType.SpeedUp;         // The action to accomplish
    public Object target;                       // The game object to affect. If none, the trigger work on this game object
    public GameObject source;
    public int triggerCount = 1;
    public bool repeatTrigger = false;
    public float x;
    public float y;
    public float z;

    private void OnTriggerEnter(Collider other)
    {
        if (type == BonusType.SpeedUp)
        {
            var a = other.gameObject.GetComponent<SoloThirdCharacter>();
            a.m_MoveSpeedMultiplier += 0.2f;

        }
        if (type == BonusType.SpeedDown)
        {
            var a = other.gameObject.GetComponent<SoloThirdCharacter>();
            a.m_MoveSpeedMultiplier -= 0.2f;

        }
        if (type == BonusType.Invisibility)
        {
            other.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

        }
        if (type == BonusType.JumpBoost)
        {
            var a = other.gameObject.GetComponent<SoloThirdCharacter>();
            a.m_JumpPower += 2f;
        }
        Destroy(gameObject);

    }
}*/