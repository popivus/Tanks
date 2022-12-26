using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float moveSpeed = 5f, hp = 100;
    private Rigidbody2D rb;
    public Rigidbody2D rbModel;
    Vector2 moveDirection, mousePos;
    public Camera cam;
    public GameObject tankSprite;

    public static MoveController instanse;

    public void Awake()
    {
        instanse = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (!UIController.instanse.paused)
        {
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            moveDirection.y = Input.GetAxisRaw("Vertical");



            var newRotation = tankSprite.transform.rotation;
            if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") == 0) newRotation = Quaternion.Euler(0, 0, 0);
            else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") > 0) newRotation = Quaternion.Euler(0, 0, -90);
            else if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") > 0) newRotation = Quaternion.Euler(0, 0, -45);
            else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") > 0) newRotation =Quaternion.Euler(0, 0, -135);
            else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") == 0) newRotation = Quaternion.Euler(0, 0, -180);
            else if (Input.GetAxisRaw("Vertical") < 0 && Input.GetAxisRaw("Horizontal") < 0) newRotation = Quaternion.Euler(0, 0, 135);
            else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") < 0) newRotation = Quaternion.Euler(0, 0, 90);
            else if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") < 0) newRotation = Quaternion.Euler(0, 0, 45);
            tankSprite.transform.rotation = newRotation;

            Debug.Log(Input.GetAxisRaw("Horizontal").ToString() + " " + Input.GetAxisRaw("Vertical").ToString());

            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        rbModel.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        
        

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rbModel.rotation = angle;
    }

    public void Damage(int points)
    {
        hp -= points;
        if (hp <= 0)
        {
            AudioManager.instanse.PlaySound(2);
            UIController.instanse.DeathUI();
        }
    }

    public void BoostUp()
    {
        moveSpeed = 8.5f;
        StartCoroutine(BoostTimer());
    }

    private IEnumerator BoostTimer()
    {
        yield return new WaitForSeconds(10f);
        moveSpeed = 5f;
    }
}
