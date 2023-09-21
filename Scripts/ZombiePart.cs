using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePart : MonoBehaviour
{
    public Zombie zombie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Bullet")
        {
            zombie.kill();
            this.GetComponent<Rigidbody>().velocity = -other.impulse;//往反方向動所以要加上負號
        }    
    }
}
