using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class ExtensionMethod
{
    static float currentRotationX;

    public static void Move(this Rigidbody value, GameObject target, float moveSpeed)
    {
        if (target.GetComponent<Rigidbody>() == null)
            target.AddComponent<Rigidbody>();

        Rigidbody rigid = target.GetComponent<Rigidbody>();

        float dirX = Input.GetAxisRaw("Horizontal");
        float dirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveX = target.transform.right * dirX;
        Vector3 moveZ = target.transform.forward * dirZ;

        Vector3 velocity = (moveX + moveZ).normalized * moveSpeed;
        rigid.MovePosition(target.transform.position + velocity * Time.deltaTime);
    }
    public static void CameraRotation(this Camera value, float sens, float limit = 90f)
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * sens;

        currentRotationX -= cameraRotationX;
        currentRotationX = Mathf.Clamp(currentRotationX, -limit, limit);

        value.transform.localEulerAngles = new Vector3(currentRotationX, 0f, 0f);
    }
    public static void PlayerRotation(this Rigidbody value, GameObject target, float sens)
    {
        if (target.GetComponent<Rigidbody>() == null)
            target.AddComponent<Rigidbody>();

        Rigidbody rigid = target.GetComponent<Rigidbody>();

        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 playerRotationY = new Vector3(0f, yRotation, 0f) * sens;

        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(playerRotationY));
    }
    public static bool SetAnim(this Animator value, string animName, float startTime, float endTime)
    {
        return value.GetCurrentAnimatorStateInfo(0).IsName(animName) &&
               value.GetCurrentAnimatorStateInfo(0).normalizedTime >= startTime &&
               value.GetCurrentAnimatorStateInfo(0).normalizedTime <= endTime;
    }
    public static bool EndAnim(this Animator value, string animName, float endTime = 0.8f)
    {

        return value.GetCurrentAnimatorStateInfo(0).IsName(animName) &&
               value.GetCurrentAnimatorStateInfo(0).normalizedTime >= endTime;
    }
    public static string RemoveClone(this string value)
    {
        int temp = value.IndexOf("(Clone)");
        string strTemp = value.Substring(0, temp);
        return strTemp;
    }
    public static bool CheckScene(this Scene value, string findSceneName)
    {
        if (SceneManager.GetActiveScene().name.IndexOf(findSceneName) != -1)
        {
            return true;
        }
        return false;
    }

}
