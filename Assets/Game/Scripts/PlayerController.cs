using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    private void Awake()
    {
        instance = this;
    }

    public float moveSpeed;

    public Animator animator;
    
    public float pickupRange = 1.5f;

    private SpriteRenderer sr;

    //public Weapon activeWeapon;

    public List<Weapon> unassignedWeapons;
    public List<Weapon> assignedWeapons;

    public int maxWeapons = 3;


    // Start is called before the first frame update
    void Start()
    {
        sr = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        
        AddWeapon(Random.Range(0, unassignedWeapons.Count));
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

    //按索引取出武器
    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);

            unassignedWeapons[weaponNumber].gameObject.SetActive(true);
            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    //直接接收武器对象
    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);

        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}
