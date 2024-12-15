using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MongMong_Trace : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Monster2"))
        {
            if (gameObject.transform.parent.gameObject.GetComponent<MongMong>().target_select == false && collision.gameObject.GetComponent<Monster2>().infinity_monster == false)
            {
                gameObject.transform.parent.gameObject.GetComponent<MongMong>().target = collision.gameObject;
            }
        }
        else if (collision.gameObject.CompareTag("Summon")) 
        {
            if (gameObject.transform.parent.gameObject.GetComponent<MongMong>().target_select == false && collision.gameObject.GetComponent<Stage_3_Tentacle>().origin.GetComponent<Monster2>().infinity_monster == false)
            {
                gameObject.transform.parent.gameObject.GetComponent<MongMong>().target = collision.gameObject;
            }
        }
    }
}
