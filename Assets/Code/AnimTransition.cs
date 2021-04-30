using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTransition : MonoBehaviour
{
    private Animator anin;
    public bool activador = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        anin = GetComponent <Animator>();
    }

    // Update is called once per frame
    public void ActivarTransition()
    {
        anin.SetBool("Set transition", activador);
        Debug.Log("activador true");
    }
    public void DesctivarTransition()
    {
        anin.SetBool("Set transition", !activador);
        Debug.Log("activador false");
    }
}
