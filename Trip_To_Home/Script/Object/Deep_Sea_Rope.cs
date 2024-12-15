using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deep_Sea_Rope : MonoBehaviour
{
    public GameObject chain_Prefab;
    public int chain_cnt;
    public Rigidbody2D point_Rigid;
    FixedJoint2D ex_Joint;

    void Start()
    {
        for(int i = 0; i < chain_cnt; i++) 
        {
            FixedJoint2D current_Joint = Instantiate(chain_Prefab, transform).GetComponent<FixedJoint2D>();
            current_Joint.transform.localPosition = new Vector3(0, (i + 1) * -0.95f, 0);
            if (i == 0)
            {
                current_Joint.connectedBody = point_Rigid;
            }
            else
            {
                current_Joint.connectedBody = ex_Joint.GetComponent<Rigidbody2D>();
            }

            ex_Joint= current_Joint;

            if (i == chain_cnt - 1) 
            {
                current_Joint.GetComponent<Rigidbody2D>().mass = 10;
            }
        }
    }
}
