using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] Transform cameraArm;
    [SerializeField] Transform characterPos;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private BoxCollider weaponCol;
    public bool isAttack;
    public bool isDodge;
    // Start is called before the first frame update
    void Start()
    {
        playerAnim = characterPos.gameObject.GetComponent<Animator>();
        // StartCoroutine(CheckCo());
    }

    bool isFirst = true;
    // Update is called once per frame
    void Update()
    {
        Move();
        Dodge();
        Attack();


        // if (Input.GetMouseButtonDown(0))
        // {
        //     playerAnim.SetTrigger("Combo1");
        // }

        //if(playerAnim.GetCurrentAnimatorStateInfo(0).IsName("GreatSword_Attack")&&
        //   playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0)
        //{
        //    if(isFirst)
        //    {
        //        Debug.Log("aaa");
        //        isFirst = false;
        //    }

        //}
        //if (EndAnim("GreatSword_Attack",1))
        //{
        //    isFirst = true;
        //}
    }
    #region 애니메이션 구간 확인용
    public bool EndAnim(string animName, float time = 0.99f)
    {
        return playerAnim.GetCurrentAnimatorStateInfo(0).IsName(animName) &&
               playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= time;
    }
    // 공격 가능한 구간 설정시 사용
    public bool SetAnim(string animname, float startTime, float endTime)
    {
        return playerAnim.GetCurrentAnimatorStateInfo(0).IsName(animname) &&
               playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= startTime &&
               playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime <= endTime;
    }
    #endregion
    // 캐릭터 움직임
    private void Move()
    {

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMove = moveInput.magnitude != 0; // 움직이는지 확인
        playerAnim.SetBool("isMove", isMove); // 움짐임에 따라 애니메이션 실행

        if (isMove /*startTime*/)
        {
            moveSpeed = walkSpeed;
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized; // 앞뒤
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized; // 좌우
            Vector3 moveDir = (lookForward * moveInput.y) + (lookRight * moveInput.x);

            characterPos.forward = moveDir;
            if (!isAttack)
                transform.position += moveDir * Time.deltaTime * moveSpeed;
        }


        Debug.DrawRay(cameraArm.position, new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized, Color.red);
    }

    #region 공격관련
    private void Attack()
    {


        if (!isAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                playerAnim.SetTrigger("Attack");

            }

        }
        for (int i = 1; i < 4; i++)
        {

            if (SetAnim("GreatSword_Attack" + i, 0.3f, 0.7f) && !isDodge)
            {
                if (Input.GetMouseButton(0)) // 입력가능 구간안에서 마우스 클릭시 콤보어택
                    playerAnim.SetTrigger("Combo");
            }

        }


    }
    public void SetAttack(int boolCheck)
    {
        bool temp = Convert.ToBoolean(boolCheck);
        isAttack = temp;
        weaponCol.enabled = temp;
    }
    #endregion

    // 회피기 (구르기)
    private void Dodge()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isDodge = true;
            SetAttack(0);
            playerAnim.SetTrigger("Dodge");
            // this.tag = "Invincibility";

        }
        if (EndAnim("Dodge", 0.7f))
        {
            isDodge = false;
            // this.tag = "Player";
            SetAttack(0);
        }

    }
    private IEnumerator CheckCo()
    {
        while (true)
        {
            Debug.Log(isAttack);
            yield return null;
        }
    }
}
