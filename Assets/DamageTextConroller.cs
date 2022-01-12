using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageTextConroller : MonoBehaviour
{
    [SerializeField] float textSpeed;
    [SerializeField] float alphaSpeed;
    public TextMeshProUGUI targetText;
    private IEnumerator takeDamageAction;
    public int damage;
    Color textColor;
    // Start is called before the first frame update
    private void Start()
    {


        //targetText.text = damage.ToString();

    }
    private void OnEnable()
    {
        if (targetText == null)
            targetText = GetComponent<TextMeshProUGUI>();
        if (textColor == null)
            textColor = targetText.color;
        StartCoroutine(TakeDamageAction());

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
