using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnIF : MonoBehaviour
{
    protected Block StandBlock;
    public Transform tf;
    public Vector3 OldPos;
    public Vector3 Size;      //大きさ（ワールド）
    public virtual void HitUnder(Block block) { }
    public virtual void HitTop(Block block) { }
    public virtual void HitRight(Block block) { }
    public virtual void HitLeft(Block block) { }
    public virtual void NonHitUnder() { }
    public virtual void Umore() { }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
