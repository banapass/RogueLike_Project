using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator playerAnim;
    // 카메라 관련
    [SerializeField] private Transform characterPos;
    // [SerializeField] private Transform cameraArm;
    // [SerializeField] private Transform cameraTf;
    // [SerializeField] private float scroll;
    // [SerializeField] private float scrollSpeed;
    // [SerializeField] private float zoomMin, zoomMax;
    // [SerializeField] private float moveSpeed; // 캐릭터 움직임 속도
    // [SerializeField] private float sens = 2f; // 마우스 움직임 속도
    //float currentRotationY;
    public bool isAttack;


    // 카메라 줌인 아웃
    // private void MouseScroll()
    // {
    //     //Vector3 scroll =  (cameraTf.position - cameraArm.position).normalized;
    //     scroll += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
    //     scroll = Mathf.Clamp(scroll, zoomMin, zoomMax);
    //     cameraTf.localPosition = new Vector3(0f, 0f, scroll);
    // }
    // 카메라 좌우상하 움직임
    // private void LookAround()
    // {

    //     float rotationX = Input.GetAxis("Mouse X");
    //     float rotationY = Input.GetAxis("Mouse Y");
    //     float cameraRotationX = rotationY * sens;

    //     Vector3 camAngle = cameraArm.rotation.eulerAngles;
    //     Vector3 playerRotationY = new Vector3(0f, rotationX, 0f);

    //     Rigidbody rigid = GetComponent<Rigidbody>();

    //     currentRotationY -= cameraRotationX;
    //     currentRotationY = Mathf.Clamp(currentRotationY, -80f, 75f);

    //     rigid.MoveRotation(rigid.rotation * Quaternion.Euler(playerRotationY));
    //     cameraArm.rotation = Quaternion.Euler(currentRotationY, camAngle.y + rotationX, camAngle.z);
    // }
}
