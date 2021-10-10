using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject BuildUpVFX;
    public AudioClip BuildUpSFX;
    public GameObject ExplosionVFX;
    public AudioClip explosionSFX;
    public Text restartText;
    private bool restartable;
    private bool explosionSequence;

    public Player player1;
    public Player player2;
    [HideInInspector] public List<Player> players;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        players = new List<Player>();
        players.Add(player1); players.Add(player2);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && restartable)
        {
            StopAllCoroutines();
            explosionSequence = false;
            restartText.enabled = false;

            FindObjectOfType<Grid>().RestartMap();

            foreach (Player player in players)
            {
                if (player.cargoes.Count > 0)
                {
                    for (int i = player.cargoes.Count - 1; i >= 0; --i)
                    {
                        Destroy(player.cargoes[i].gameObject);
                    }
                    player.cargoes.Clear();
                }
                player.Destroyed = false;
                Color color = player.GetComponentInChildren<SpriteRenderer>().color;
                color.a = 1;
                player.GetComponentInChildren<SpriteRenderer>().color = color;
                player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                player.transform.localEulerAngles = Vector3.zero;
            }

            player1.SetPosition(new Vector3(-11.5f, .5f, 0), 270);
            player2.SetPosition(new Vector3(11.5f, -.5f, 0), 90);
        }

        if (!explosionSequence)
        {
            player1.UpdatePlayer(GetPlayer1Input());
            player2.UpdatePlayer(GetPlayer2Input());
        }

    }


    public void HandleDestroyPlayer(Player player)
    {
        if (player.Destroyed) return;
        player.Destroyed = true;
        StartCoroutine(DestroySpaceshipSequence(player));
    }

    public IEnumerator DestroySpaceshipSequence(Player player)
    {
        explosionSequence = true;
        Destroy(Instantiate(BuildUpVFX, player.transform.position, Quaternion.identity), 1f);
        GetComponent<CameraShake>().shakeDuration = .2f;
        GetComponent<AudioSource>().PlayOneShot(BuildUpSFX, .9f);
        yield return new WaitForSeconds(1f);
        Destroy(Instantiate(ExplosionVFX, player.transform.position, Quaternion.identity), 5);
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Color color = player.GetComponentInChildren<SpriteRenderer>().color;
        color.a = 0;
        player.GetComponentInChildren<SpriteRenderer>().color = color;
        GetComponent<AudioSource>().PlayOneShot(explosionSFX, .75f);

        foreach (Cargo c in player.cargoes)
        {
            c.GetComponent<Collider2D>().enabled = false;
        }
        GetComponent<CameraShake>().shakeDuration = .5f;
        yield return new WaitForSeconds(.1f);
        foreach (Cargo c in player.cargoes)
        {
            GameObject g = Instantiate(ExplosionVFX, c.transform.position, Quaternion.identity);
            c.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            c.GetComponent<Collider2D>().enabled = false;
            Destroy(g, 5f);
            GetComponent<AudioSource>().PlayOneShot(explosionSFX, .75f);
            GetComponent<CameraShake>().shakeDuration += .1f;
            yield return new WaitForSeconds(.1f);
        }

        for (int i = player.cargoes.Count - 1; i >= 0; --i)
        {
            Destroy(player.cargoes[i].gameObject);
        }
        player.cargoes.Clear();

        restartable = true;
        explosionSequence = false;
        yield return new WaitForSeconds(1);
        restartText.enabled = true;
    }

    public Vector2 GetPlayer1Input()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.W)) input.y = 1;
        else if (Input.GetKeyDown(KeyCode.S)) input.y = -1;
        else if (Input.GetKeyDown(KeyCode.D)) input.x = 1;
        else if (Input.GetKeyDown(KeyCode.A)) input.x = -1;

        return input;
    }

    public Vector2 GetPlayer2Input()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow)) input.y = 1;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) input.y = -1;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) input.x = 1;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) input.x = -1;

        return input;
    }


    public void Explode(Vector3 position)
    {
        Destroy(Instantiate(ExplosionVFX, position, Quaternion.identity), 5);
        GetComponent<AudioSource>().PlayOneShot(explosionSFX);
    }
}
