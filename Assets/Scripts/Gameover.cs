using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameover : MonoBehaviour {
    public GameObject part1, part2;
    public AudioClip gameover;
    public AudioSource asource;
    public SpriteRenderer pause;
    public Image img;
    public Texture2D t2d;
    public Sprite spr;
    public bool yn = false;
    public int counter;
    [Range(0, 1)]
    public float volume;
    private void Start()
    {
    }
    void Update () {

        if (Input.GetKeyDown(KeyCode.P) && yn == false)
        {
            if (global.isPaused)
            { 
                foreach (AudioSource asource in FindObjectsOfType<AudioSource>())
                { asource.UnPause(); }
                img.color = new Color(0f, 0f, 0f, 0f);
                pause.gameObject.GetComponent<Animator>().Play("pause fade out");
                t2d = null;
                spr = null;
                img.sprite = spr;
            }
            else
            {   foreach (AudioSource asource in FindObjectsOfType<AudioSource>())
                { asource.Pause(); }
                StartCoroutine(TakeSs());
            }
            global.isPaused = !global.isPaused;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        { SceneManager.LoadScene(0); }
        if (yn == true)
        {
            counter += 1;
            if (counter == 51)
            {   Instantiate(part2, new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
        
        }
        asource = GetComponent<AudioSource>();
        if (GameObject.Find("Player") == null && yn == false)
        { Instantiate(part1,new Vector3(0f,0f,0f),Quaternion.identity);
            asource.PlayOneShot(gameover,volume);
            yn = true;
        }
        if (yn == true && counter > 2.5f/Time.fixedDeltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(1);
        }

    }
    private void LateUpdate()
    {
        if (t2d !=null)
        {
            spr = Sprite.Create(t2d, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0, 0));
            img.sprite = spr;
            pause.gameObject.GetComponent<Animator>().Play("pause fade in");
            img.color = new Color(255, 255, 255, 1f);
        }
    }
    IEnumerator TakeSs()
    {
        yield return new WaitForEndOfFrame();
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();
        t2d = tex;
    }
}
