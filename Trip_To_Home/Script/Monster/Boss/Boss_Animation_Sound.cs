using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Boss_Animation_Sound : MonoBehaviour
{
    public AudioClip[] clips;

    public void Boss_4_Attack_1() 
    {
        GameManager.instance.SFX_Play("Boss_Attack_1", clips[0], 1);
    }
}
