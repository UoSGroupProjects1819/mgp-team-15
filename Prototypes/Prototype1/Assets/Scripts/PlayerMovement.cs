using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform footPosition;
    public string LastName = "";
    private ConstantForce2D myForce;

    private void Start()
    {
        myForce = GetComponent<ConstantForce2D>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.transform.tag == "Slope")
        {
            if (LastName == collision.transform.parent.name) { return; }

            LastName = collision.transform.parent.name;

            if (collision.transform.position.y > footPosition.position.y)
            {
                //slide up
                myForce.relativeForce = new Vector2(myForce.relativeForce.x, myForce.relativeForce.x);
            }
            else
            {
                //slide down
                myForce.relativeForce = new Vector2(myForce.relativeForce.x, -myForce.relativeForce.x);
            }
        }
        else if (collision.transform.tag == "Flip")
        {
            if (LastName == collision.transform.parent.name) { return; }

            LastName = collision.transform.parent.name;
            Debug.Log("Flip down");
            float yDif = transform.position.y - collision.transform.position.y;

            transform.position = new Vector3(transform.position.x, collision.transform.position.y - yDif - 0.5f, transform.position.z);
            GetComponent<Rigidbody2D>().gravityScale = -GetComponent<Rigidbody2D>().gravityScale;
            transform.Rotate(Vector2.left * 180);
        }
        else if (collision.transform.tag == "Deflective")
        {
            myForce.relativeForce = -myForce.relativeForce;
        }

        else if (collision.transform.tag == "Bounce")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up*15, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Slope")
        {
            if (LastName == collision.transform.parent.name)
            {
                myForce.relativeForce = new Vector2(myForce.relativeForce.x, 0);
                LastName = "";
            }
        }
       /* else if (collision.transform.tag == "Flip")
        {
            if (LastName == collision.transform.parent.name)
            {
                float yDif = transform.position.y - collision.transform.position.y;

                transform.position = new Vector3(transform.position.x, collision.transform.position.y - yDif + 0.5f, transform.position.z);
                GetComponent<Rigidbody2D>().gravityScale = -GetComponent<Rigidbody2D>().gravityScale;
                transform.Rotate(Vector2.left * 180);

                LastName = "";
            }
        }*/
    }
}
