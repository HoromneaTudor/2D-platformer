using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private Collision coll;
    //private GameMaster gm;
    private AnimationScript anim;
    public float death_height;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collision>();
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        anim = GetComponentInChildren<AnimationScript>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = GameMaster.lastCheckPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)||transform.position.y<death_height||coll.onSpykeWall)
        {
            StartCoroutine(Death());
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //mari probleme la dash (nu imi mai gaseste rigid bodiul din cauza ca a fost distrus)
        }
    }
    IEnumerator Death()
    {
        if (rb.velocity.x > 0)
            rb.velocity = new Vector2(-6, 20);
        else if (rb.velocity.x < 0)
            rb.velocity = new Vector2(6, 20);
        else
            rb.velocity = new Vector2(0, 20);
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.position = GameMaster.lastCheckPos;
        anim.SetTrigger("Dead");
        //yield return new WaitForSeconds(4f);
    }
}
