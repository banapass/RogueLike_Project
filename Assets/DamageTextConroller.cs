using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageTextConroller : MonoBehaviour
{
    [SerializeField] float textSpeed;
    [SerializeField] float alphaSpeed;
    private TextMeshProUGUI targetText;
    private IEnumerator takeDamageAction;
    public int damage;
    Color textColor;
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Test");

        textColor = targetText.color;
        targetText.text = damage.ToString();
        //  StartCoroutine(TakeDamageAction());


    }
    private void OnEnable()
    {
        targetText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TakeDamageAction());

    }
    private void OnDisable()
    {
        //ObjectPools.ReturnParts(this.gameObject, "DamageText");
    }

    private IEnumerator TakeDamageAction()
    {
        textColor = Color.white;
        textColor.a = 1;
        targetText.color = textColor;
        while (true)
        {

            transform.position += Vector3.up * textSpeed * Time.deltaTime;
            textColor.a -= alphaSpeed * Time.deltaTime;
            targetText.color = textColor;

            if (targetText.color.a <= 0)
            {
                ObjectPools.ReturnParts(this.gameObject, name.RemoveClone());
                break;
            }
            yield return null;

        }
    }
}
