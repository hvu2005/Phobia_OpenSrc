using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DendroSlime : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DetectedPlayer())
        {
            Vector2 targetPosition = new Vector2(_player.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * 2f);
        }
    }
}
