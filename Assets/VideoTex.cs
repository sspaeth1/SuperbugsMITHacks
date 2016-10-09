using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Universal Video Texture Lite Ver. 1.1

public class VideoTex : MonoBehaviour
{
    public GameObject mat;
    public float FPS = 30;

    public int firstFrame;
    public int lastFrame;

    public string FileName = "VidTex";
    public string digitsFormat = "0000";

    public enum digitsLocation { Prefix, Postfix };
    public digitsLocation DigitsLocation = digitsLocation.Postfix;

    public float aspectRatio = 1.78f;

    public bool enableAudio = false;

    public bool enableReplay = true;

    public bool showInstructions = true;

    bool audioAttached = false;

    bool firstPlay = true;

    string indexStr = "";

    Texture newTex;
    Texture lastTex;

    float index = 0;

    int intIndex = 0;
    int lastIndex = -1;

    AttachedAudio1 myAudio = new AttachedAudio1(); // Creates an audio class for audio management 


    void Awake()
    {
        // Application.targetFrameRate = 60; (Optional for smoother scrubbing on capable systems)

        audioAttached = GetComponent("AudioSource");

        // Zeros camera range - effectively blackens the screen

        //	GetComponent<Camera>().farClipPlane = 10000;
        //	GetComponent<Camera>().nearClipPlane = 0;
    }

    void Start()
    {
        index = firstFrame;

        if (audioAttached)
        {
            myAudio.AttachedAudio1Source = GetComponent<AudioSource>();
            myAudio.fps = FPS;
            myAudio.frameIndex = firstFrame;
        }

    }


    void Update()
    {
        // Forces audio sync on first play (helpful for some devices)

        if ((firstPlay) && (index < firstFrame + 1) && enableAudio)
        {


            myAudio.frameIndex = index;
            myAudio.Sync();
            myAudio.Play();

        }

        if (Input.GetMouseButtonDown(0) && enableReplay)
        {
            index = firstFrame;
            if (audioAttached && enableAudio)
            {

                myAudio.frameIndex = index;
                myAudio.Sync();
                myAudio.Play();

            }
        }

        index += FPS * Time.deltaTime;

        intIndex = (int)index;

        if (index >= lastFrame)
        {
            // index = lastFrame;
            if (SceneManager.GetActiveScene().name == "SampleScene")
                SceneManager.LoadScene("Level 01 5.x");

        }

        if (intIndex != lastIndex)
        {

            indexStr = string.Format("{0:" + digitsFormat + "}", intIndex);

            if (DigitsLocation == digitsLocation.Postfix)
                newTex = Resources.Load(FileName + indexStr) as Texture;
            else
                newTex = Resources.Load(indexStr + FileName) as Texture;

            lastIndex = intIndex;
        }




    }

    void OnGUI()
    {
        //if (enableReplay && showInstructions)
        //	GUI.Box(new Rect(0, 0, Screen.width, Screen.height),"Click the left mouse button or touch the screen to rewind & replay");
        if (intIndex <= lastFrame)
        {
            MeshRenderer mr = mat.GetComponent<MeshRenderer>();



            mr.material.mainTexture = newTex;


        }
        //	GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height),newTex,ScaleMode.ScaleToFit,true,aspectRatio); // Actual video texture draw

    }
}

// Class for audio management

public class AttachedAudio1
{
    public AudioSource AttachedAudio1Source;

    public float frameIndex = 801;
    public float fps = 0;

    public bool togglePlay = true;

    public void Play()
    {
        if (AttachedAudio1Source)
            if (!AttachedAudio1Source.isPlaying)
                AttachedAudio1Source.Play();
    }

    public void Sync()
    {
        if (AttachedAudio1Source)
            AttachedAudio1Source.time = frameIndex / fps;
    }
}