using System.Collections;
using UnityEngine;

public class SoundRingVisual : MonoBehaviour
{
    [SerializeField]
    GameObject soundRingPrefab;
    [Range(.1f,10)]
    [SerializeField]
    float maxSize = 3;
    [Range(.01f,3)]
    [SerializeField]
    float duration = 5;
    [SerializeField]
    Color color;

    [SerializeField]
    bool testBool = false;
    private void Update()
    {
        if(testBool == true)
        {
            CreateSound(transform.position, maxSize, duration, color);
            testBool = false;
        }
    }
    public void CreateSound(Vector2 position, float maxSize, float duration, Color color)
    {
        GameObject soundRing = Instantiate(soundRingPrefab);
        SpriteRenderer SR = soundRing.GetComponent<SpriteRenderer>();
        soundRing.transform.localScale = new Vector2(0, 0);
        soundRing.transform.position = position;
        SR.color = color;

        StartCoroutine(GrowRing(soundRing, duration, maxSize));
    }
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
