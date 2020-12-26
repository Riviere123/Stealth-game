using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeLighting : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    float startAlpha;
    [SerializeField]
    [Range(0, 1)]
    float flickerAlphaDifference;
    [SerializeField]
    [Range(0, 1)]
    float flickerSpeed;
    [SerializeField]
    [Range(0, 1)]
    float flickerSpeedVariation;

    [SerializeField]
    Vector2 startScale;
    [SerializeField]
    [Range(0, 1)]
    float scaleSizeDifference;
    [SerializeField]
    [Range(0, 1)]
    float scaleSizeSpeed;
    [SerializeField]
    [Range(0, 1)]
    float sizeScaleSpeedVariation;

    [SerializeField]
    bool independantXYGrow;
    [SerializeField]
    bool sync;

    SpriteRenderer sr;
    CapsuleCollider2D cc2d;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cc2d = GetComponent<CapsuleCollider2D>();

        StartCoroutine(Flicker());
        StartCoroutine(ScaleLight());
    }

    IEnumerator Flicker()
    {
        float flickerAmmount = Random.Range(-flickerAlphaDifference, flickerAlphaDifference) + startAlpha;
        sr.color = new Color(sr.color[0], sr.color[1], sr.color[2], flickerAmmount);

        float flickerTime = Random.Range(-flickerSpeedVariation, flickerSpeedVariation) + flickerSpeed;
        yield return new WaitForSeconds(flickerTime);
        StartCoroutine(Flicker());
        
        if (sync)
        {
            StartCoroutine(ScaleLight());
        }
    }

    IEnumerator ScaleLight()
    {
        float x = Random.Range(-scaleSizeDifference, scaleSizeDifference) + startScale.x;
        float y = Random.Range(-scaleSizeDifference, scaleSizeDifference) + startScale.y;

        if(x > y)
        {
            cc2d.direction = CapsuleDirection2D.Horizontal;
        }
        else
        {
            cc2d.direction = CapsuleDirection2D.Vertical;
        }
        if (independantXYGrow)
        {
            transform.localScale = new Vector2(x, y);
        }
        else
        {
            transform.localScale = new Vector2(x,x);
        }
 
        if (!sync)
        {
            float scaleTime = Random.Range(-sizeScaleSpeedVariation, sizeScaleSpeedVariation) + scaleSizeSpeed;
            yield return new WaitForSeconds(scaleTime);
            StartCoroutine(ScaleLight());
        }
    }
}
