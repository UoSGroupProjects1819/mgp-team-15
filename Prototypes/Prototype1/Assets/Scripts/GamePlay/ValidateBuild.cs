using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateBuild : MonoBehaviour
{
    public Collider2D myCollider;
    public Rigidbody2D myRigid;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Can't build here
        Builder.ValidateBuildCount = false;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //Can build here
        Builder.ValidateBuildCount = true;
    }

    //Turns on validation
    public void ActivateValidator()
    {
        myCollider.isTrigger = true;
        myRigid.WakeUp();
    }

    //Turns it off
    public void DisableValidator()
    {
        myCollider.isTrigger = false;
        myRigid.Sleep();
    }
}
