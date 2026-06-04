using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public Animator animator;
    
    public float pickupRange = 1.5f;

    private SpriteRenderer sr;


    // Start is called before the first frame update
    void Start()
    {
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //创建向量存储玩家输入的移动信息
        Vector3 moveInput = new Vector3(0f, 0f, 0f);
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        if(moveInput.x < 0f)
            sr.flipX = false;
        else if(moveInput.x > 0f)
            sr.flipX = true;
       

        //确保各方向移动速度相同
        moveInput.Normalize();

        //改变位置
        this.transform.position += moveInput * moveSpeed * Time.deltaTime;
        
        //动画状态切换
         if(moveInput != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
