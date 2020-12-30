using System.Collections;
using UnityEngine;

/// <summary>
/// Attach this to any gameobject that makes sounds the player or AI should know about.
/// </summary>
public class SoundRingVisual : MonoBehaviour
{
    [SerializeField]
    GameObject soundRingPrefab; //The prefab of the soundring we are instantiating.
    [Range(.1f,10)]
    [SerializeField]
    float maxSize = 3; //the max size of the sound ring.
    [Range(.01f,3)]
    [SerializeField]
    float duration = 5; //the time it takes the soundring to reach max size.
    [SerializeField]
    Color color; //the color we will set the sound ring to.

    [SerializeField]
    bool trigger = false;

    private void Update()
    {
        if(trigger == true)
        {
            CreateSound(transform.position, maxSize, duration, color);
            trigger = false;
        }
    }

    /// <summary>
    /// This spawns the sound ring with the provided details of this class. Add parameters to overide the classes details.
    /// </summary>
    public void CreateSound()
    {
        GameObject soundRing = Instantiate(soundRingPrefab);
        SpriteRenderer SR = soundRing.GetComponent<SpriteRenderer>();

        soundRing.transform.localScale = new Vector2(0, 0);
        soundRing.transform.position = transform.position;
        SR.color = color;

        StartCoroutine(GrowRing(soundRing, duration, maxSize));
    }

    /// <summary>
    /// This spawns a sound ring with the provided details you specify.
    /// </summary>
    /// <param name="position">The position to spawn this ring.</param>
    /// <param name="maxSize">The maximum size the ring will get.</param>
    /// <param name="duration">The time it takes the ring to reach maximum size.</param>
    /// <param name="color">The color of the ring.</param>
    public void CreateSound(Vector2 position, float maxSize, float duration, Color color)
    {
        GameObject soundRing = Instantiate(soundRingPrefab);
        SpriteRenderer SR = soundRing.GetComponent<SpriteRenderer>();
        soundRing.transform.localScale = new Vector2(0, 0);
        soundRing.transform.position = position;
        SR.color = color;

        StartCoroutine(GrowRing(soundRing, duration, maxSize));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ring">The ring gameobject that we want to grow.</param>
    /// <param name="duration">The time before it reaches max size.</param>
    /// <param name="maxSize">The max size the ring will get.</param>
    /// <returns></returns>
    
    IEnumerator GrowRing(GameObject ring, float duration, float maxSize)
    {
        float increment = .01f;

        while(ring.transform.localScale.x < maxSize)
        {
            Vector2 newsize =  new Vector2(ring.transform.lossyScale.x + (increment*maxSize), ring.transform.lossyScale.y + (increment * maxSize));

            yield return new WaitForSeconds(duration*increment);

            if (newsize.x > maxSize)
            {
                ring.transform.localScale = new Vector2(maxSize, maxSize);
            }
            else
            {
                ring.transform.localScale = newsize;
                
            }
        }
        Destroy(ring);
    }
}
