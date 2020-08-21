using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //public float speed;
    private Vector2 direction;
    public float kTime = 1;
    private Animator animator;
    public Rigidbody2D rigidbody;
    public Player player;
    //public float Speed = 10;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        TakeInput();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        
        Move();
    }

    private void Move()
    {

        rigidbody.MovePosition((Vector2)transform.position + (direction.normalized * player.Speed * Time.fixedDeltaTime));

        //transform.Translate(direction * player.Speed * Time.deltaTime * kTime);

        if (direction.x != 0 || direction.y != 0)
        {
            if (direction.x != 0 && direction.y != 0)
                SetAnimatorMovement(new Vector2(direction.x, 0));
            else 
            {
                gameObject.transform.parent.GetComponent<WorldState>().LogicGeneration(gameObject.transform.position);
                SetAnimatorMovement(direction);
            }
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    private void TakeInput()
    {
        direction = Vector2.zero;
        if (!animator.GetBool("isAttack")) 
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                direction += Vector2.up;
                direction= direction.normalized;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                direction += Vector2.down;
                direction = direction.normalized;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                direction += Vector2.left;
                direction = direction.normalized;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                direction += Vector2.right;
                direction = direction.normalized;
            } 
        }
    }

    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);

        
    }

}
