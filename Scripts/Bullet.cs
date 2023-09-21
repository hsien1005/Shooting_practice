using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rigid;
    // Start is called before the first frame update
    void Start()
    {
        this._rigid = GetComponent<Rigidbody>();
        this._rigid.velocity = transform.forward * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
