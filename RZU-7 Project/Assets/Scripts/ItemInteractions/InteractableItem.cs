using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Governs behavior for items the player can interact with
/// </summary>
/// <param name="spriteRenderer">The sprite renderer for the item</param>
/// <param name="itemCollider">The item's 2D interaction trigger collider</param>
/// <param name="sprites">The sprites that denote the interaction taking place</param>
/// <param name="timeDelta">The amount of time to wait before rendering the next sprite</param>
/// <param name="triggeredSequence">Denotes whether or not the sequence has been triggered</param>
/// <param name="isOn">Denotes whether or not the item is in the "On" state</param>
public class InteractableItem : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    Collider2D itemCollider;
    [SerializeField]
    Interaction interaction;
    [SerializeField]
    List<Sprite> sprites;
    [SerializeField]
    float timeDelta = InteractionConstants.defaultSpriteRenderingTimeDelta;
    bool triggeredSequence;
    bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (itemCollider == null)
        {
            itemCollider = GetComponent<Collider2D>();
        }
    }

    /// <summary>
    /// sets the SpriteRenderer sprite to each sprite in the list on a timer interval.
    /// </summary>
    /// <returns>
    /// IEnumerator object. This is a coroutine so...magic.
    /// </returns>
    IEnumerator RunSpriteSequence()
    {
        foreach (Sprite sprite in sprites)
        {
            spriteRenderer.sprite = sprite;

            yield return new WaitForSeconds(timeDelta);
        }

        sprites.Reverse();
        triggeredSequence = false;

        yield return null;
    }

    /// <summary>
    /// Triggers interaction sequence when the player interacts with the item
    /// </summary>
    /// <param name="collision">The player's interaction collider</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!triggeredSequence && collision.gameObject.CompareTag(InteractionConstants.interactTag))
        {
            StartCoroutine(RunSpriteSequence());
            triggeredSequence = true;
            isOn = !isOn;
            interaction.OnInteract(isOn);
        }
    }
}
