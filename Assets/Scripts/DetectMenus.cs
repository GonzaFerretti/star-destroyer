using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum swipeDirection
{
    None = 0,
    Up = 1,
    Down = 2,
    Left = 4,
    Right = 8,
}
public class swipeController
{
    public swipeDirection Direction;
    private bool initialSet = false;
    public float delta, initialPosition;
    private void Update()
    {
        

    }
}
public class DetectMenus : MonoBehaviour {
    [Header("Swipe")]
    public swipeDirection Direction;
    private bool initialSet = false;
    public Vector3 delta, initialPosition;
    public float ThX, ThY;

    public GameObject start, options, exit, volume, mute, bar;
    public SpriteRenderer SRstart, SRopt, SRexit, SRvol, SRmute, SRbarvol;
    public Sprite defstart, defopt, defexit, defvol,defmute, hstart, hopt, hexit, hvol, hmute;
    public Transform volumeBar;
    public int menu = 1;
    public bool optionsOn = false, isaudioon = true, ingame = false;
    public AudioSource asource;
    public AudioClip change, startSound, select;
    [Range(0, 1)]
    public float volumelev;
    public float globalvol = 1f;

    public void SwipeDetect()
    {
        Direction = swipeDirection.None;
        if (Input.GetMouseButtonDown(0) && initialSet == false)
        {
            initialPosition = Input.mousePosition;
            initialSet = true;
        }
        if (initialSet == true)
        {
            delta = initialPosition - Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (Mathf.Abs(delta.x) > ThX)
                { Direction |= (delta.x > 0) ? swipeDirection.Left : swipeDirection.Right; }
            }
            else
            {
                if (Mathf.Abs(delta.y) > ThY)
                { Direction |= (delta.y > 0) ? swipeDirection.Down : swipeDirection.Up; }
            }
            initialSet = false;
            delta = new Vector3(0, 0, 0);
        }
    }
	void Start () {
        start = GameObject.Find("star");
        options = GameObject.Find("option");
        exit = GameObject.Find("exit");
        mute = GameObject.Find("mute");
        bar = GameObject.Find("volslider");
        volume = GameObject.Find("volume");
        SRstart = start.GetComponent<SpriteRenderer>();
        SRopt = options.GetComponent<SpriteRenderer>();
        SRexit = exit.GetComponent<SpriteRenderer>();
        SRbarvol = bar.GetComponent<SpriteRenderer>();
        SRmute = mute.GetComponent<SpriteRenderer>();
        SRvol = volume.GetComponent<SpriteRenderer>();
        asource = GetComponent<AudioSource>();
        volumeBar = bar.GetComponent<Transform>();
        SRstart.sprite = hstart;
        SRbarvol.enabled = false;
        SRvol.enabled = false;
        SRmute.enabled = false;
        menu = 1;
        Application.targetFrameRate = 60;
        /*
        global.rescalingFactor = (Screen.currentResolution.height * 0.94f) / 900f;
        Screen.SetResolution(Mathf.RoundToInt(600 * global.rescalingFactor), Mathf.RoundToInt(900 * global.rescalingFactor), false);*/
        AudioListener.volume = globalvol;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(global.rescalingFactor);
        if (SceneManager.GetActiveScene().name == "menu" && ingame == true)
            { ingame = false;
            optionsOn = false;
            SRstart.enabled = true;
            SRopt.enabled = true;
            SRexit.enabled = true;
            SRvol.enabled = false;
            SRmute.enabled = false;
            SRbarvol.enabled = false;
            SRstart.sprite = hstart;
            SRopt.sprite = defopt;
            SRexit.sprite = defexit;
            menu = 1;
            if (FindObjectsOfType(GetType()).Length > 1)
            {

                Destroy(GameObject.Find("Menu"));
            }
        }
        if (ingame == false)
        {
            SwipeDetect();
            if (optionsOn == false)
            {
                if (Input.GetKeyDown("s") || Input.GetKeyDown("down") || Direction == swipeDirection.Down)
                {
                    menu += 1;

                    if (menu != 4)
                    {
                        asource.PlayOneShot(change, volumelev);
                    }
                    menu = Mathf.Clamp(menu, 1, 3);

                }
                if (Input.GetKeyDown("w") || Input.GetKeyDown("up") || Direction == swipeDirection.Up)
                {
                    menu -= 1;

                    if (menu != 0)
                    {
                        asource.PlayOneShot(change, volumelev);
                    }
                    menu = Mathf.Clamp(menu, 1, 3);


                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    switch (isaudioon)
                    {
                        case true:
                            {
                                SRmute.sprite = hmute;
                                isaudioon = false;
                                globalvol = 0f;

                            }
                            break;
                        case false:
                            {

                                SRmute.sprite = defmute;
                                isaudioon = true;
                                if (volumeBar.localScale.y - 0.076625f <= 0)
                                {
                                    volumeBar.localScale = new Vector3(volumeBar.localScale.x, Mathf.Clamp(volumeBar.localScale.y + 0.076625f, 0, 0.76625f), 0f);
                                    asource.PlayOneShot(change, volumelev * volumeBar.localScale.y / 613 * 800);
                                }
                                globalvol = Mathf.Round((volumeBar.localScale.y / 613 * 800) * 1000000) / 1000000;
                            }
                            break;

                    }
                }
                if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
                {
                    volumeBar.localScale = new Vector3(volumeBar.localScale.x, Mathf.Clamp(volumeBar.localScale.y - 0.076625f, 0, 0.76625f), 0f);

                    if (volumeBar.localScale.y - 0.076625f < 0)
                    {
                        SRmute.sprite = hmute;
                        isaudioon = false;

                        globalvol = 0f;
                    }
                    if (volumeBar.localScale.y > 0 && volumeBar.localScale.y < 0.76625f)
                    {
                        asource.PlayOneShot(change, volumelev * volumeBar.localScale.y / 613 * 800);
                    }

                    globalvol = Mathf.Round((volumeBar.localScale.y / 613 * 800) * 1000000) / 1000000;

                }
                if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
                {

                    if (volumeBar.localScale.y > 0 && volumeBar.localScale.y <= 0.76625f && volumeBar.localScale.y < 0.76625f)
                    {
                        asource.PlayOneShot(change, volumelev * volumeBar.localScale.y / 613 * 800);
                    }
                    volumeBar.localScale = new Vector3(volumeBar.localScale.x, Mathf.Clamp(volumeBar.localScale.y + 0.076625f, 0, 0.76625f), 0f);
                    if (globalvol <  0.2)
                    {
                        
                        SRmute.sprite = defmute;
                        isaudioon = true;
                    }

                    globalvol = Mathf.Round((volumeBar.localScale.y / 613 * 800) * 1000000) / 1000000;

                }
                if (isaudioon == true)
                { AudioListener.volume = globalvol; }
                else AudioListener.volume = 0f;
                if (Input.GetKeyDown(KeyCode.Escape) || Direction == swipeDirection.Right)
                {
                    asource.PlayOneShot(select, 0.7f);
                    optionsOn = false;
                    SRstart.enabled = true;
                    SRopt.enabled = true;
                    SRexit.enabled = true;
                    SRvol.enabled = false;
                    SRmute.enabled = false;
                    SRbarvol.enabled = false;
                }
            }
            if (optionsOn == false)
            {
                switch (menu)
                {
                    case 1:
                        {
                            if (optionsOn == false)
                            {
                                SRstart.sprite = hstart;
                                SRopt.sprite = defopt;
                                SRexit.sprite = defexit;
                                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Direction == swipeDirection.Left)
                                {
                                    asource.PlayOneShot(startSound, 0.7f);
                                    optionsOn = false;
                                    SRstart.enabled = false;
                                    SRopt.enabled = false;
                                    SRexit.enabled = false;
                                    SRvol.enabled = false;
                                    SRmute.enabled = false;
                                    SRbarvol.enabled = false;
                                    ingame = true;
                                    gameObject.name = "MainMenu";
                                    GameObject.DontDestroyOnLoad(gameObject);
                                    global.isPaused = false;
                                    SceneManager.LoadScene("main");
                                }

                            }
                            break;



                        }
                    case 2:
                        {
                            if (optionsOn == false)
                            {
                                SRstart.sprite = defstart;
                                SRopt.sprite = hopt;
                                SRexit.sprite = defexit;
                                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Direction == swipeDirection.Left)
                                {
                                    asource.PlayOneShot(select, 0.7f);
                                    optionsOn = true;
                                    SRstart.enabled = false;
                                    SRopt.enabled = false;
                                    SRexit.enabled = false;
                                    SRvol.enabled = true;
                                    SRmute.enabled = true;
                                    SRbarvol.enabled = true;
                                }
                            }
                            break;

                        }
                    case 3:
                        {
                            if (optionsOn == false)
                            {
                                SRstart.sprite = defstart;
                                SRopt.sprite = defopt;
                                SRexit.sprite = hexit;
                                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Direction == swipeDirection.Left)
                                {
                                    asource.PlayOneShot(select, 0.7f);
                                    Application.Quit();
                                }
                            }
                            break;

                        }
                }
            }
        }
    }
}
