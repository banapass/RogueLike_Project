using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamageTextConroller : MonoBehaviour
{
    [SerializeField] float textSpeed;
    [SerializeField] float alphaSpeed;
    private TextMeshProUGUI targetText;
    public int damage;
    Color textColor;
    // Start is called before the first frame update
    void Start()
    {
        targetText = GetComponent<TextMeshProUGUI>();
        textColor = targetText.color;
        targetText.text = damage.ToString();
        StartCoroutine(TakeDamageAction());
    }

    private IEnumerator TakeDamageAction()
    {
        while (true)
        {
            transform.position += Vector3.up * textSpeed * Time.deltaTime;
            textColor.a -= alphaSpeed * Time.deltaTime;
            targetText.color = textColor;

            if (targetText.color.a <= 0)
            {
                Destroy(gameObject);
                break;
            }
            yield return null;

        }
    }
}
