using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCameraTarget : MonoBehaviour
{

    [SerializeField] Transform playerHip;
    

    // Update is called once per frame
    void Update()
    {
        var pos = new Vector2(playerHip.position.x, transform.position.y);
        var distance = Vector2.Distance(playerHip.position, transform.position);

        transform.position = Vector3.MoveTowards(transform.position, pos, distance * 7 * Time.deltaTime);
        
    }
}
