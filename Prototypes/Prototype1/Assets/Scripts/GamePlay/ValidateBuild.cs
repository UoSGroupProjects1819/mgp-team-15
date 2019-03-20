using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateBuild : MonoBehaviour
{
    public Collider2D myCollider;
    public Rigidbody2D myRigid;
    public Collider2D other;

    private void OnTriggerStay2D(Collider2D collider)
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
        if (other != null)
        {
            myCollider.enabled = false;
            other.enabled = true;
        }
    }
}
