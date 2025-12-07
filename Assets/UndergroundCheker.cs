using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndergroundCheker : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var d = collision.gameObject.GetComponent<AIDamager>();
        if (d != null)
        {
            d.transform.root.GetComponent<AIStickman>().Damage(int.MaxValue);
            d.transform.root.gameObject.SetActive(false);
        }
    }
    
}
