using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StayWithObject : MonoBehaviour
{
    public Transform TransformToStayWith;
    public bool StayWith = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StayWith) {
            if(TransformToStayWith != null) {
                transform.position = TransformToStayWith.position;
            }
        }
    }
}
