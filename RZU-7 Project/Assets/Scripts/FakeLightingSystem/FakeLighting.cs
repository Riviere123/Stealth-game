using System.Collections;
using UnityEngine;

/// <summary>
/// Attach this to any object that will create lighting that the AI or player should be aware of.
/// </summary>
/// <param name="startAlpha">The alpha can be changed in the inspector after run time. But this will be set to the sprites alpha value at Start.</param>
/// <param name="flickerAlphaDifference">The range of difference the alpha will flicker to.</param>
/// <param name="flickerSpeed">The frequency the alpha flickers.</param>
/// <param name="flickerSpeedVariation">The range of variation on the flicker speed.</param>
/// <param name="flickerAlphaSpeed">The speed in which the alpha changes to the new value.</param>
/// <param name="timeToGrow">The time it takes the gameobject to get to the target size.</param>
/// <param name="scaleSizeDifference">The possible variance in scale when changed from the startScale.</param>
/// <param name="scaleSizeSpeed">The time between changing sizes.</param>
/// <param name="sizeScaleSpeedVariation">The variance ontop of scaleSizeSpeed to change between changing size.</param>
/// <param name="independentXYGrow">Allows the gameobject's X and Y to scale independantly creating oblong lighting.</param>
/// <param name="sync">Syncs the growing and flickering effect. THIS OPTION CAN NOT BE EDITED AFTER RUN TIME OR IT WILL NO LONGER GROW THE LIGHT.</param>
/// <param name="sr">Sprite renderer reference</param>
/// <param name="cc2d">Collider 2D reference</param>
/// <param name="startScale">The starting scale of the object. This can be changed after runtime in the inspector but is set at start to equal the localscale of the object.</param>
/// <param name="targetScale">this is the size we want the object to grow or shrink to.</param>
/// <param name="targetAlpha">this is the alpha we want to get to.</param>
public class FakeLighting : MonoBehaviour
{
    [SerializeField]
    [Range(0,1)]
    float startAlpha = 0; 
    [SerializeField]
    [Range(0, 1)]
    float flickerAlphaDifference = 0;
    [SerializeField]
    [Range(0, 1)]
    float flickerSpeed = 0;
    [SerializeField]
    [Range(0, 1)]
    float flickerSpeedVariation = 0;
    [SerializeField]
    [Range(1, 25)]
    float flickerAlphaSpeed = 0;
    [SerializeField]
    [Range(1, 25)]
    float timeToGrow = 0;
    [SerializeField]
    [Range(0, 1)]
    float scaleSizeDifference = 0;
    [SerializeField]
    [Range(0, 1)]
    float scaleSizeSpeed = 0;
    [SerializeField]
    [Range(0, 1)]
    float sizeScaleSpeedVariation = 0;

    [SerializeField]
    bool independentXYGrow;
    [SerializeField]
    bool sync;

    SpriteRenderer sr;
    CapsuleCollider2D cc2d;

    Vector2 startScale;
    Vector3 targetScale;
    float targetAlpha;

    private void Start()
    {
        startScale = transform.localScale;
        sr = GetComponent<SpriteRenderer>();
        cc2d = GetComponent<CapsuleCollider2D>();

        StartCoroutine(Flicker());
        StartCoroutine(ScaleLight());
    }


    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * timeToGrow);
        sr.color = new Color(sr.color[0], sr.color[1], sr.color[2], Mathf.Lerp(sr.color[3], targetAlpha, Time.deltaTime * flickerAlphaSpeed)); 
    }

    /// <summary>
    /// Flickers the light based on the classes variables.
    /// </summary>
    /// <returns>Wait for flickertime</returns>
    IEnumerator Flicker()
    {  
        targetAlpha = Mathf.Clamp(Random.Range(-flickerAlphaDifference, flickerAlphaDifference) + startAlpha, 0, 1);
        

        Mathf.Clamp(Random.Range(-flickerSpeedVariation, flickerSpeedVariation) + flickerSpeed,0,5);
        float flickerTime = Random.Range(-flickerSpeedVariation, flickerSpeedVariation) + flickerSpeed;
        yield return new WaitForSeconds(flickerTime);
        StartCoroutine(Flicker());
        
        if (sync)
        {
            StartCoroutine(ScaleLight());
        }
    }
    /// <summary>
    /// Scales the light based on the classes variables.
    /// </summary>
    /// <returns>Wait for scaletime unless Synced</returns>
    IEnumerator ScaleLight()
    {
        float x = Mathf.Clamp(Random.Range(-scaleSizeDifference, scaleSizeDifference) + startScale.x, .1f, Mathf.Infinity);
        float y = Mathf.Clamp(Random.Range(-scaleSizeDifference, scaleSizeDifference) + startScale.y, .1f, Mathf.Infinity);

        if (x > y)
        {
            cc2d.direction = CapsuleDirection2D.Horizontal;
        }
        else
        {
            cc2d.direction = CapsuleDirection2D.Vertical;
        }
        
        if (independentXYGrow)
        {
            targetScale = new Vector2(x, y);
        }
        else
        {
            targetScale = new Vector2(x,x);
        }
 
        if (!sync)
        {
            float scaleTime = Mathf.Clamp(Random.Range(-sizeScaleSpeedVariation, sizeScaleSpeedVariation) + scaleSizeSpeed, .01f, Mathf.Infinity);
            yield return new WaitForSeconds(scaleTime);
            StartCoroutine(ScaleLight());
        }
    }

    /// <summary>
    /// if the collision object is in the trigger collider and if the colliding object has the fakelighting detection script, set it to visible.
    /// </summary>
    /// <param name="collision">The colliding object</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Status>())
        {
            if (!collision.GetComponent<Status>().statusEffects.Contains(new Illuminated("Illuminated")))
            {
                Illuminated illuminated = new Illuminated("Illuminated");
                collision.GetComponent<Status>().statusEffects.Add(illuminated);
            }               
        }
    }

    /// <summary>
    /// On collision exit, if the colliding object has the fakelighting detection script, set visible to false.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Status>())
        {
            if (collision.GetComponent<Status>().statusEffects.Contains(new Illuminated("Illuminated")))
            {
                collision.GetComponent<Status>().statusEffects.Remove(new Illuminated("Illuminated"));
            }
        }
    }
}
