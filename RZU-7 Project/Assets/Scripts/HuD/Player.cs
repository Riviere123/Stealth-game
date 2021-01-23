using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// This would be inside the players controller
/// </summary>
/// <param name"hud">Reference to the hud script</param>
/// <param name"playerMaster">Reference to the PlayerMasterScript</param>
public class Player : MonoBehaviour
{
    Rigidbody2D rb2d;
    GameMaster gameMaster;
    HuD hud;
    PlayerMaster playerMaster;
    SoundRingVisual soundRing;
    ActorMovement actorMovement;
    ActorAnimations actorAnimations;

    bool dead;
    bool soundtick;

    [SerializeField]
    float timeBetweenSound;
    [SerializeField]
    float ringSpeed;
    private void Start()
    {
        try
        {
            gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
            hud = gameMaster.GetHud();
            playerMaster = gameMaster.GetPlayerMaster();
            soundRing = GetComponent<SoundRingVisual>();
            actorMovement = GetComponent<ActorMovement>();
            rb2d = GetComponent<Rigidbody2D>();
            actorAnimations = GetComponent<ActorAnimations>();
        }
        catch
        {
            Debug.LogError($"Hud has no HuD Tag or is not in the scene.");
        }
    }
    /// <summary>
    /// sound code Wasn't a fan of this.
    /// </summary>
    /// <param name="collision"></param>
    private void Update()
    {
        //////////THIS IS FOR TESTING
        ///
        if (Input.GetKeyDown(KeyCode.Z))
        {
            gameMaster.CompleteLevel();
        }
        if (rb2d.velocity.magnitude > 0.25f)
        {
            if (actorMovement.currentMoveState == MovementConstants.ActorMovementStates.RUN)
            {
                if (!soundtick)
                {
                    soundRing.CreateSound(transform.position, 6f, ringSpeed, Color.red - new Color(0, 0, 0, .75f));
                    StartCoroutine(SoundTick(timeBetweenSound / 2));
                }
            }
            if (actorMovement.currentMoveState == MovementConstants.ActorMovementStates.WALK)
            {
                if (!soundtick)
                {
                    soundRing.CreateSound(transform.position, 4f, ringSpeed, Color.yellow - new Color(0,0,0,.75f));
                    StartCoroutine(SoundTick(timeBetweenSound));
                }
            }
            if (actorMovement.currentMoveState == MovementConstants.ActorMovementStates.CROUCH)
            {
                if (!soundtick)
                {
                    soundRing.CreateSound(transform.position, 2f, ringSpeed, Color.green - new Color(0, 0, 0, .75f));
                    StartCoroutine(SoundTick(timeBetweenSound));
                }
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Key")
        {
            if (!playerMaster.GetKeys().Contains(collision.gameObject))
            {
                GameObject key = collision.gameObject;
                playerMaster.AddKey(key);
                key.GetComponent<SpriteRenderer>().enabled = false;
                key.GetComponent<Collider2D>().enabled = false;
                hud.DisplayKeys();
            }
        }

        if (collision.tag == "Valuable")
        {
            playerMaster.AddGold(collision.GetComponent<Valuable>().value);
            Destroy(collision.GetComponent<SpriteRenderer>());
            Destroy(collision.GetComponent<Collider2D>());
        }

        if (collision.tag == "SecretItem")
        {
            playerMaster.AddSecretItem(collision.gameObject);
            collision.GetComponent<SpriteRenderer>().enabled = false;
            collision.GetComponent<Collider2D>().enabled = false;
            hud.DisplaySecretItems();
        }
        if(collision.tag == "EnemyAttack")
        {
            actorAnimations.SetIsDeadStatus(true);
        }
        if(collision.tag == "CompleteZone")
        {
            gameMaster.CompleteLevel();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            Door door = collision.gameObject.GetComponent<Door>();
            if (playerMaster.GetKeys().Contains(door.Key))
            {
                Destroy(collision.gameObject);
                gameMaster.GetComponent<Grid>().CreateGrid();
            }
            else
            {
                Debug.Log("I do not have the key!");
            }
        }
    }
    IEnumerator SoundTick(float time)
    {
        soundtick = true;
        yield return new WaitForSeconds(time);
        soundtick = false;
    }
}
