using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform targetTf;
    [SerializeField] float followSpeed = 10;
    [SerializeField] float clampAngle = 70;

    private float rotX;
    private float rotY;

    public float sens;
    public Transform cam;
    public Vector3 dirNormalized;
    public Vector3 finalDir;
    public float minDistance;
    public float maxDistance;
    private float currentDistnace;
    public float finalDistance;
    public float smooth = 10;
    // Start is called before the first frame update
    void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNormalized = cam.localPosition.normalized;
        finalDistance = cam.localPosition.magnitude;
        currentDistnace = finalDistance;
    }

    // Update is called once per frame
    void Update()
    {
        CameraRotation();
    }
    void LateUpdate()
    {
        FollowCamera();
    }
    // 카메라 회전
    void CameraRotation()
    {
        rotX -= Input.GetAxis("Mouse Y") * sens * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * sens * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle); // 위아래 회전 제한

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0); // Vector3을 쿼터니언값으로 변환
        transform.rotation = rot; // 변환한 값을 적용
    }
    void FollowCamera()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTf.position, followSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNormalized * currentDistnace);

        currentDistnace -= Input.GetAxis("Mouse ScrollWheel");
        currentDistnace = Mathf.Clamp(currentDistnace, minDistance, maxDistance);

        RaycastHit hit;

        #region 위치 체크용 디버그
        //Debug.Log("finalDir/ " + finalDir);
        // Debug.Log(cam.localPosition + " " + cam.position);
        // Debug.Log("finalDir/"+finalDir);
        // Debug.Log("local" + dirNormalized * currentDistnace);
        // Debug.Log("World" + transform.TransformPoint(dirNormalized * currentDistnace));
        // Debug.Log("CamLocalPostionMag" + cam.localPosition.magnitude);
        // Debug.Log("CamLocalPostion" + cam.localPosition);
        // Debug.Log("CamLocalPostionNormalized" + dirNormalized);
        //Debug.Log(dirNormalized * currentDistnace);

        Debug.DrawLine(transform.position, finalDir, Color.red);
        #endregion

        if (Physics.Linecast(transform.position, finalDir, out hit))
        {
            // hit한 거리를 min과 max값으로 제한을 건뒤 거리 변수에 담음
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }

        else
            finalDistance = currentDistnace; // 사이에 아무런 물체가 없을 시엔 원래 위치로 돌아감

        cam.localPosition = Vector3.Lerp(cam.localPosition, dirNormalized * finalDistance, Time.deltaTime * smooth);
    }
}

