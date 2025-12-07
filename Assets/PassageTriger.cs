using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageTriger : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        var ai = collision.transform.root.GetComponent<AIStickman>();

        if (ai)
        {
            var aiX = Mathf.Abs(ai.transform.GetChild(0).position.x);
            var mineX = Mathf.Abs(transform.position.x);

            if(mineX > aiX)
                ai.SetLayer();
        }
    }
}
