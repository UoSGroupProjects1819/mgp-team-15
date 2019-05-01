using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform footPosition;
    public string LastName = "";//last name of object it hit
    private ConstantForce2D myForce;
    public Sprite Stand, Walk1, Walk2,Jump,Hurt;
    public Sprite Spring1, Spring2, Spring3;
    private Rigidbody2D MyRigid;
    private SpriteRenderer myRend;
    SpriteRenderer r;
    public int VerticalSpeed,HorizontalSpeed,FlyingSpeed;

    private void Start()
    {
        MyRigid = GetComponent<Rigidbody2D>();
        myRend = GetComponent<SpriteRenderer>();
        myForce = GetComponent<ConstantForce2D>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Slope")
        {
            if (LastName == collision.transform.parent.name) { return; }

            LastName = collision.transform.parent.name;

            if (collision.transform.position.y > footPosition.position.y)
            {
                //slide up
                Debug.Log("UP");
                myForce.relativeForce = new Vector2(HorizontalSpeed, VerticalSpeed);
            }
            else
            {
                //slide down
                Debug.Log("DOWN");
                myForce.relativeForce = new Vector2(HorizontalSpeed, -VerticalSpeed);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
         if (collision.transform.tag == "Flip")
        {
            if (LastName == collision.transform.parent.name) { return; }

            //set name of flip it it
            LastName = collision.transform.parent.name;
            float yDif = transform.position.y - collision.transform.position.y;

            //move the player to the other side of the flip and invert gravity
            transform.position = new Vector3(transform.position.x, collision.transform.position.y - yDif - 0.5f, transform.position.z);
            MyRigid.gravityScale = -GetComponent<Rigidbody2D>().gravityScale;
            transform.Rotate(Vector2.left * 180);
        }
        else if (collision.transform.tag == "Deflective")
        {
            Debug.Log("Deflect");
            myForce.relativeForce = -myForce.relativeForce;
            MyRigid.velocity = Vector2.zero;
        }
        else if (collision.transform.tag == "Bounce")
        {
            MyRigid.velocity = new Vector2(MyRigid.velocity.x,0);
            MyRigid.AddForce(transform.TransformDirection(Vector3.up)*15, ForceMode2D.Impulse);
            r = collision.gameObject.GetComponent<SpriteFind>().r;
            StartCoroutine("Spring", collision.transform);
            FindObjectOfType<AudioManager>().Play("BounceStraight"); // jump sound effect
        }
    }

    float timer = 0.00f;
    bool walkToggle = true;
    public bool dead = false;

    private void Update()
    {
        if (dead)
        {
            myRend.sprite = Hurt;
            return;
        }

        if (MyRigid.velocity.x > 0 || MyRigid.velocity.x < 0)
        {
            walkToggle = true;
            //walk
        }
        else
        {
            myRend.sprite = Stand;
            walkToggle = false;
            //Stand
        }
        
        if (walkToggle)
        {
            timer += Time.deltaTime;
            if (timer > 0.10f)
            {
                if (myRend.sprite == Walk1)
                {
                    myRend.sprite = Walk2;
                }
                else
                {
                    myRend.sprite = Walk1;
                }
                timer = 0.00f;
            }
        }

        //jump
        if ((MyRigid.velocity.y > 2 || MyRigid.velocity.y < -1) && myForce.relativeForce.y == -1)
        {
            myRend.sprite = Jump;
            if (MyRigid.velocity.x > FlyingSpeed)
            {
                MyRigid.velocity = new Vector2(FlyingSpeed, MyRigid.velocity.y);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Slope")
        {
            if (LastName == collision.transform.parent.name)
            {
                myForce.relativeForce = new Vector2(HorizontalSpeed, -1);
                LastName = "";
            }
        }
    }

    private IEnumerator Spring(Transform springer)
    {
        Debug.Log(springer.name);

        r.sprite = Spring2;
        yield return new WaitForSeconds(0.1f);
        r.sprite = Spring3;
        yield return new WaitForSeconds(0.2f);
        r.sprite = Spring2;
        yield return new WaitForSeconds(0.1f);
        r.sprite = Spring1;
    }
}
