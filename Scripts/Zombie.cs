using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    public Transform target;
    public List<Rigidbody> rigidList;//各個部分的分支(頭 小腿)
    public Animator anim;//希望程式運作時每一次生成的殭屍起始動作都不一樣
    private NavMeshAgent _nma;
    // Start is called before the first frame update
    void Start()
    {
        this._nma = this.GetComponent<NavMeshAgent>();
        this.anim.PlayInFixedTime("Run", 0, Random.Range(0.0f,1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if(this._nma.enabled)
        {
            this._nma.SetDestination(this.target.position);
        }
    }
    public void kill()
    {
        this._nma.enabled = false;
        this.anim.enabled = false;
        foreach(Rigidbody r in this.rigidList)
        {
            r.isKinematic = false;//把剛體射程false 會變成一坨爛泥
        }
    }
}
