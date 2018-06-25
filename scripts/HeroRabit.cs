using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabit : MonoBehaviour {

    public static HeroRabit lastRabit = null;

    public float speed = 1;
    Rigidbody2D myBody = null;
    Transform heroParent = null;

    bool isGrounded = false;
    bool JumpActive = false;
    public bool isBig = false;
    float JumpTime = 0f;
    public float MaxJumpTime = 2f;
    public float JumpSpeed = 2f;

    static void SetNewParent(Transform obj, Transform new_parent)
    {
        if (obj.transform.parent != new_parent)
        {
            //Засікаємо позицію у Глобальних координатах
            Vector3 pos = obj.transform.position;
            //Встановлюємо нового батька
            obj.transform.parent = new_parent;
            //Після зміни батька координати кролика зміняться
            //Оскільки вони тепер відносно іншого об’єкта
            //повертаємо кролика в ті самі глобальні координати
            obj.transform.position = pos;
        }
    }

    void Awake()
    {
        lastRabit = this;
    }

    // Use this for initialization
    void Start () {
        myBody = this.GetComponent<Rigidbody2D>();
        //Зберегти стандартний батьківський GameObject
        this.heroParent = this.transform.parent;
        //Зберігаємо позицію кролика на початку
        LevelController.current.setStartPosition(transform.position);
    }
	
	// Update is called once per frame
	void Update () {
        float value = Input.GetAxis("Horizontal");
        Animator animator = GetComponent<Animator>();
        if (Mathf.Abs(value) > 0)
        {
            Vector2 vel = myBody.velocity;
            vel.x = value * speed;
            myBody.velocity = vel;
        }
        if (Mathf.Abs(value) > 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }
        
        if (this.isGrounded)
        {
            animator.SetBool("jump", false);
        }
        else
        {
            animator.SetBool("jump", true);
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (value < 0)
        {
            sr.flipX = true;
        }
        else if (value > 0)
        {
            sr.flipX = false;
        }

        Vector3 from = transform.position + Vector3.up;
        Vector3 to = transform.position + Vector3.down;
        int layer_id = 1 << LayerMask.NameToLayer("Ground");
        //Перевіряємо чи проходить лінія через Collider з шаром Ground
        RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
        if (hit)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (hit)
        {
            //Перевіряємо чи ми опинились на платформі
            if (hit.transform != null
            && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                //Приліпаємо до платформи
                SetNewParent(this.transform, hit.transform);
            }
        }
        else
        {
            //Ми в повітрі відліпаємо під платформи
            SetNewParent(this.transform, this.heroParent);
        }
        //Намалювати лінію (для розробника)
        Debug.DrawLine(from, to, Color.red);

        //Якщо кнопка тільки що натислась
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            this.JumpActive = true;
        }
        if (this.JumpActive)
        {
            //Якщо кнопку ще тримають
            if (Input.GetKey(KeyCode.Space))
            {
                this.JumpTime += Time.deltaTime;
                if (this.JumpTime < this.MaxJumpTime)
                {
                    Vector2 vel = myBody.velocity;
                    vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                    myBody.velocity = vel;
                }
            }
            else
            {
                this.JumpActive = false;
                this.JumpTime = 0;
            }
        }
    }

    public void Big()
    {
        myBody.transform.localScale = new Vector3(14f, 14f, 14f);
        isBig = true;
    }

    public void Small()
    {
        myBody.transform.localScale = new Vector3(9f, 9f, 9f);
        isBig = false;
    }
}