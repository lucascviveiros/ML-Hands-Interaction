using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class EyeTracking : MonoBehaviour {

    #region Public Variables
    public GameObject Camera;
    public MLPersistentBehavior[] persistentBehavior;
    public AudioSource[] AudioClips;
    public GameObject[] _names;
    public GameObject[] _objects;
    public GameObject[] _HaloFocus;
    public GameObject[] gestureTutorial;
    public GameObject _centerLeftHand, _centerRightHand;
    public GameObject _allHolograms;
    public GameObject videoAR;
    public GameObject _planes;
    public GameObject canvasGestures;
    public GameObject _myHands;
    public GameObject canvasMissingSDK;
    public GameObject generalCanvas;
    public Text _text, _tutorial, _title;
    public VideoScript myVideo;
    public PlaneRecognition planeRecognition;
    #endregion

    #region Private Variables
    private Vector3 _heading;
    private MLInputController _controller;
    private int position;
    private bool onChange, onMoving, onPauseVideo, onHomeButton, onReset, onSkip;
    private Vector3 lp1, lp2, lp3, lp4, lp5, lp6, lp7;
    private Vector3 planeVector;
    private string planeID;
    private List<GameObject> listPlanes;
    private MLHandKeyPose[] gestures;
    private IEnumerator coroutine;
    #endregion

    #region Unity Methods
    public void Awake()
    {
        canvasGestures.SetActive(false);
        _tutorial.enabled = true;

        lp1 = _objects[0].transform.localPosition;
        lp2 = _objects[1].transform.localPosition;
        lp3 = _objects[2].transform.localPosition;
        lp4 = _objects[3].transform.localPosition;
        lp5 = _objects[4].transform.localPosition;
        lp6 = _objects[5].transform.localPosition;
        lp7 = _objects[6].transform.localPosition;

        _objects[0].transform.GetChild(0).GetComponent<Animator>().enabled = false;//insect
        _objects[0].transform.GetChild(1).GetComponent<Animator>().enabled = false;
        _objects[1].transform.GetChild(0).GetComponent<Animator>().enabled = false;//face
        _objects[2].transform.GetChild(0).GetComponent<Animator>().enabled = false;//astro
        _objects[3].transform.GetChild(0).GetComponent<Animator>().enabled = false;//wkz
        _objects[3].transform.GetChild(1).GetComponent<Animator>().enabled = false;
        _objects[6].GetComponent<Animator>().enabled = false;//pinpin

        for (int i = 0; i < _objects.Length; i++)
        {
            _objects[i].GetComponent<Collider>().enabled = true;
            _objects[i].GetComponent<Collider>().isTrigger = true;
        }

        foreach (GameObject obj in gestureTutorial) 
        {
            obj.SetActive(false);
        }
    }

    private IEnumerator StartTutorial() 
    {
        foreach (GameObject obj in gestureTutorial)
        {
            obj.SetActive(false);
        }

        int count = 0;

        if (count == 0 && onSkip)
        {
            _tutorial.text = "Look for the ground and the tables to detect the planes in the environment";
            count++;
        }

        if (count == 1 && onSkip)
        {
            yield return new WaitForSeconds(6);
            _tutorial.text = "Use your right hand for the basic interactions with the Holograms";
            count++;
        }

        if (count == 2 && onSkip) 
        {
            yield return new WaitForSeconds(6);
            _tutorial.text = "You can move each Hologram individually !";
            if (onSkip)
            {
                gestureTutorial[0].SetActive(true);
            }
            count++;
        }
        if (count == 3 && onSkip)
        {
            yield return new WaitForSeconds(6);
            if (onSkip)
            {
                gestureTutorial[0].SetActive(false);
                gestureTutorial[1].SetActive(true);
            }
            _tutorial.text = "You can also reset the position of all Holograms with your left hand";
            count++;
        }
        if (count == 4 && onSkip) 
        {
            yield return new WaitForSeconds(6);
            if (onSkip)
            {
                gestureTutorial[1].SetActive(false);
            }
            _tutorial.text = "After find a plane and a good position";
            count++;
        }
        if (count == 5 && onSkip)
        {
            yield return new WaitForSeconds(4);
            if (onSkip)
            {
                gestureTutorial[2].SetActive(true);
            }
            _tutorial.text = "Use the Positive Gesture for seting the Hologram";
            count++;
        }
        if (count == 6 && onSkip)
        {
            yield return new WaitForSeconds(6);
            if (onSkip)
            {
                gestureTutorial[2].SetActive(false);
                gestureTutorial[3].SetActive(true);
            }
            _tutorial.text = "Use the Ok Gesture for playing the animations and hide the planes mesh";
            count++;
        }
        if (count == 7 && onSkip)
        {
            yield return new WaitForSeconds(6);
            if (onSkip)
            {
                gestureTutorial[3].SetActive(false);
            }
            _tutorial.text = "HomeButton for hiding the Tutorial and the hands tracking";
            count++;
        }
        if (count == 8 && onSkip)
        {
            yield return new WaitForSeconds(4);
            _tutorial.text = "Enjoy!";
            count++;
            if (!onHomeButton)
            {
                showCanvas(true);
                onHomeButton = true;
                _allHolograms.SetActive(true);
            }
        }
        if (count == 9 && onSkip)
        {
            yield return new WaitForSeconds(2);
            _tutorial.text = "";
        }
    }

    private void Start()
    {
        onSkip = true;
        onChange = false; 
        onMoving = false;
        onPauseVideo = false;
        onHomeButton = false;

        StartCoroutine(StartTutorial());

#if !UNITY_EDITOR && PLATFORM_LUMIN
        //Initiate controller
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonDown += OnButtonDown;

        //Initiate Eyes
        MLEyes.Start();

        //Initiate Hands
        MLHands.Start();

        gestures = new MLHandKeyPose[4]; //Initiate Array Gestures
        gestures[0] = MLHandKeyPose.Thumb; //Positive gesture
        gestures[1] = MLHandKeyPose.C; //"C" gesture
        gestures[2] = MLHandKeyPose.Finger; //Forefinger gesture
        gestures[3] = MLHandKeyPose.Ok; //Ok gesture

        MLHands.KeyPoseManager.EnableKeyPoses(gestures, true, false);
#endif
        //Seting Objects in array 
        _objects[0] = GameObject.Find("INSECT");
        _objects[1] = GameObject.Find("FACEMASK");
        _objects[2] = GameObject.Find("ASTRONAUT");
        _objects[3] = GameObject.Find("WKZ");
        _objects[4] = GameObject.Find("ZIGGO");
        _objects[5] = GameObject.Find("GOTHAER");
        _objects[6] = GameObject.Find("PINPIN");

        planeVector = new Vector3(0, -1.6f, 0);

        foreach (GameObject halo in _HaloFocus)
        {
            halo.SetActive(false);
        }

        foreach (GameObject name in _names)
        {
            name.SetActive(false);
        }

        _allHolograms.SetActive(false);

    }

    private void Update()
    {

#if !UNITY_EDITOR && PLATFORM_LUMIN
        if (MLEyes.IsStarted || MLHands.IsStarted)
        {
            RaycastHit rayHit;
            _heading = MLEyes.FixationPoint - Camera.transform.position;

            if (Physics.Raycast(Camera.transform.position, _heading, out rayHit, 12.0f))
            {
                if (onChange == false)
                {
                    switch (rayHit.collider.name)
                    {
                        case "INSECT":
                            changeHologram("Top Insecten", 0, "Name1", false);
                            position = 0;
                            break;
                        case "FACEMASK":
                            changeHologram("FaceMask", 1, "Name2", false);
                            position = 1;
                            break;
                        case "ASTRONAUT":
                            changeHologram("Astronaut", 2, "Name3", false);
                            position = 2;
                            break;
                        case "WKZ":
                            changeHologram("WKZ-maatje", 3, "Name4", false);
                            position = 3;
                            break;
                        case "ZIGGO":
                            changeHologram("Ziggo", 4, "Name5", false);
                            position = 4;
                            break;
                        case "GOTHAER":
                            changeHologram("Gothaer", 5, "Name6", true);
                            position = 5;
                            break;
                        case "PINPIN":
                            changeHologram("Rabo PinPin", 6, "Name7", false);
                            position = 6;
                            break;
                    }
                }

                if (rayHit.collider.tag == "Planes")
                {
                    planeID = rayHit.collider.name;
                    listPlanes = planeRecognition._planeCache;

                    for (int i = 0; i < listPlanes.Count; i++)
                    {
                        if (planeID == listPlanes[i].name)
                        {
                            Renderer rend = rayHit.collider.GetComponent<Renderer>();
                            rend.material.SetColor("_Color", new Color(0, 255, 128));
                            //_planeID.text = listPlanes[i].name;
                            //_debug.text = "Find PLane: " + listPlanes[i].transform.position;
                            planeVector = listPlanes[i].transform.position;
                        }
                    }
                }
            }

            if (GetGesture(MLHands.Left, MLHandKeyPose.C) && onHomeButton)  //Reset Position All Holograms 
            {
                onReset = true;
                myVideo.setStopVideo();
                _allHolograms.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); //reset same size
                _planes.GetComponent<Renderer>().enabled = true;
                hideHalos();
                hideNames();
                position = 555; 

                for (int i = 0; i < _objects.Length; i++)
                {
                    _objects[i].GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                    _objects[i].GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);
                    _objects[i].transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    _objects[i].GetComponent<Collider>().isTrigger = true;
                }
                //_objects[2].transform.GetChild(0).GetComponent<Animator>().Rebind();
                //_objects[2].transform.GetChild(0).GetComponent<Animator>().enabled = false;
                //Animations
                _objects[0].transform.GetChild(0).GetComponent<Animator>().Rebind();
                _objects[0].transform.GetChild(0).GetComponent<Animator>().enabled = false; //insect

                _objects[0].transform.GetChild(1).GetComponent<Animator>().Rebind();
                _objects[0].transform.GetChild(1).GetComponent<Animator>().enabled = false;

                _objects[1].transform.GetChild(0).GetComponent<Animator>().Rebind();
                _objects[1].transform.GetChild(0).GetComponent<Animator>().enabled = false; //face

                _objects[2].transform.GetChild(0).GetComponent<Animator>().Rebind();
                _objects[2].transform.GetChild(0).GetComponent<Animator>().enabled = false; //astro

                _objects[3].transform.GetChild(0).GetComponent<Animator>().Rebind();
                _objects[3].transform.GetChild(0).GetComponent<Animator>().enabled = false; //wkz

                _objects[3].transform.GetChild(1).GetComponent<Animator>().Rebind();
                _objects[3].transform.GetChild(1).GetComponent<Animator>().enabled = false;

                _objects[6].GetComponent<Animator>().Rebind();
                _objects[6].GetComponent<Animator>().enabled = false; //pinpin

                //Position
                Vector3 posTo = Camera.transform.position + (Camera.transform.forward * 2.0f);

                //Update Position
                _objects[0].transform.position = posTo + lp1;
                _objects[1].transform.position = posTo + lp2;
                _objects[2].transform.position = posTo + lp3;
                _objects[3].transform.position = posTo + lp4;
                _objects[4].transform.position = posTo + lp5;
                _objects[5].transform.position = posTo + lp6;
                _objects[6].transform.position = posTo + lp7;

                //Update Rotation
                _objects[0].transform.rotation = Quaternion.Euler(0, Camera.transform.eulerAngles.y + 180, 0);
                _objects[1].transform.rotation = Quaternion.Euler(0, Camera.transform.eulerAngles.y + 180, 0);
                _objects[2].transform.rotation = Quaternion.Euler(0, Camera.transform.eulerAngles.y + 180, 0);
                _objects[3].transform.rotation = Quaternion.Euler(0, Camera.transform.eulerAngles.y + 180, 0);
                _objects[4].transform.rotation = Quaternion.Euler(0, Camera.transform.eulerAngles.y + 180, 0);
                _objects[5].transform.rotation = Quaternion.Euler(0, Camera.transform.eulerAngles.y + 180, 0);
                _objects[6].transform.rotation = Quaternion.Euler(0, Camera.transform.eulerAngles.y + 180, 0);
            }

            else if (GetGesture(MLHands.Right, MLHandKeyPose.Finger) && onHomeButton) //individual
            {
                onMoving = true;
                _planes.GetComponent<Renderer>().enabled = true;
                _objects[position].GetComponent<Collider>().isTrigger = false;
                _objects[position].GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
                _objects[position].GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, 0f, 0f);

                switch (position)
                {
                    case 0:
                        _objects[0].transform.GetChild(0).GetComponent<Animator>().enabled = false; //insect
                        _objects[0].transform.GetChild(1).GetComponent<Animator>().enabled = false;
                        break;
                    case 1:
                        _objects[1].transform.GetChild(0).GetComponent<Animator>().enabled = false; //face

                        break;
                    case 2:
                        _objects[2].transform.GetChild(0).GetComponent<Animator>().Rebind();
                        _objects[2].transform.GetChild(0).GetComponent<Animator>().enabled = false; //astro

                        break;
                    case 3:
                        _objects[3].transform.GetChild(0).GetComponent<Animator>().enabled = false; //wkz
                        _objects[3].transform.GetChild(1).GetComponent<Animator>().enabled = false; //wkz
                        break;
                    case 5:
                        onPauseVideo = false;
                        break;
                    case 6:
                        _objects[6].GetComponent<Animator>().enabled = false; //pinpin
                        break;
                }

                if (_objects[position].transform.position.y < planeVector.y + 0.01f)
                {
                    _objects[position].transform.position = new Vector3(_objects[position].transform.position.x, planeVector.y + 0.01f, _objects[position].transform.position.z);
                }
            }

            else if (GetGesture(MLHands.Right, MLHandKeyPose.Thumb) && onHomeButton)
            {
                _planes.GetComponent<Renderer>().enabled = false;

                if (!AudioClips[0].isPlaying)
                {
                    AudioClips[0].Play();
                }

                _objects[position].GetComponent<Collider>().isTrigger = true;
                _objects[position].transform.rotation = Quaternion.Euler(0, Camera.transform.eulerAngles.y + 180, 0);

                if (!onReset) { 
                    switch (position)
                    {
                        case 0:
                            _objects[0].transform.GetChild(0).GetComponent<Animator>().enabled = true; //insect
                            _objects[0].transform.GetChild(1).GetComponent<Animator>().enabled = true;
                            break;
                        case 1:
                            _objects[1].transform.GetChild(0).GetComponent<Animator>().enabled = true; //face

                            break;
                        case 2:
                            _objects[2].transform.GetChild(0).GetComponent<Animator>().enabled = true; //astro

                            break;
                        case 3:
                            _objects[3].transform.GetChild(0).GetComponent<Animator>().enabled = true; //wkz
                            _objects[3].transform.GetChild(1).GetComponent<Animator>().enabled = true; //wkz

                            break;
                        case 5:
                            onPauseVideo = true;
                            break;
                        case 6:
                            _objects[6].GetComponent<Animator>().enabled = true; //pinpin
                            break;
                    }
                }

                onMoving = false; foreach (GameObject obj in _names)
                {
                    obj.SetActive(false);
                }

            }

            else if (GetGesture(MLHands.Right, MLHandKeyPose.Ok) && onMoving == false && onHomeButton) //Set animations
            {
                if (!AudioClips[1].isPlaying) 
                {
                    AudioClips[1].Play();
                }
                onReset = false;
                _planes.GetComponent<Renderer>().enabled = false;
                onPauseVideo = true;

                _objects[0].transform.GetChild(0).GetComponent<Animator>().enabled = true; //insect
                _objects[0].transform.GetChild(1).GetComponent<Animator>().enabled = true;
                _objects[1].transform.GetChild(0).GetComponent<Animator>().enabled = true; //face                                                                                      
                _objects[2].transform.GetChild(0).GetComponent<Animator>().enabled = true; //astro         
                _objects[3].transform.GetChild(0).GetComponent<Animator>().enabled = true;  //wkz
                _objects[3].transform.GetChild(1).GetComponent<Animator>().enabled = true;
                _objects[6].GetComponent<Animator>().enabled = true; //pinpin
            }

            if (GetGesture(MLHands.Right, MLHandKeyPose.NoPose) || GetGesture(MLHands.Right, MLHandKeyPose.NoHand))
            {
                onMoving = false;
                onPauseVideo = true;
                position = 999;
            }

            if (onMoving == true)
            {
                _planes.GetComponent<Renderer>().enabled = true;
                onChange = true;
                _HaloFocus[position].SetActive(true);
                _objects[position].GetComponent<Collider>().isTrigger = true;

                _objects[position].transform.position = _centerRightHand.transform.position + Camera.transform.forward * 2f;

                if (_objects[position].transform.position.y < planeVector.y + 0.01f)
                {
                    _objects[position].transform.position = new Vector3(_objects[position].transform.position.x, planeVector.y + 0.01f, _objects[position].transform.position.z);
                }

                _objects[position].transform.rotation = Quaternion.Euler(0, Camera.transform.eulerAngles.y + 180, 0);

                foreach (MLPersistentBehavior per in persistentBehavior)
                {
                    per.UpdateBinding();
                }
            }

            if (onMoving == false)
            {
                onChange = false;
                hideHalos();
            }

            else
            {
                hideNames();
                myVideo.setPauseVideo();
       
            }           
        }
        else
        {
            canvasMissingSDK.SetActive(true);
            generalCanvas.SetActive(false);
        }
#endif
    }

    public void showCanvas(bool flag) 
    {
        canvasGestures.SetActive(flag);
        _myHands.SetActive(flag);
    }

    private void OnButtonDown(byte controller_id, MLInputControllerButton button)
    {
        if (button == MLInputControllerButton.HomeTap)
        {
            onHomeButton = !onHomeButton;
            showCanvas(onHomeButton);
            if (onHomeButton) 
            {
                _allHolograms.SetActive(true);
            }

            if (onSkip)
            {
                _tutorial.enabled = false;
                StopCoroutine(StartTutorial());
                onSkip = false;

                foreach (GameObject obj in gestureTutorial)
                {
                    obj.SetActive(false);
                }
            }
        }

        if (button == MLInputControllerButton.Bumper) 
        {
            if (onHomeButton) 
            {
                _tutorial.enabled = true;
                StartCoroutine(StartTutorial());
                onSkip = true;
            }
        }
    }

    public bool GetGesture(MLHand hand, MLHandKeyPose type)
    {
        if (hand != null)
        {
            if (hand.KeyPose == type)
            {
                if (hand.KeyPoseConfidence > 0.9f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void OnDisable()
    {
        MLInput.Stop();
        MLEyes.Stop();
        MLHands.Stop();
    }

    private void onBigger()
    {
        _allHolograms.transform.localScale += new Vector3(0.1F, 0.1F, 0.1F);
    }

    private void onSmaller()
    {
        _allHolograms.transform.localScale += new Vector3(-0.1F, -0.1F, -0.1F);
    }

    private void hideNames()
    {
        foreach (GameObject obj in _names)
        {
            obj.SetActive(false);
        }
    }
    
    private void hideHalos()
    {
        foreach (GameObject halo in _HaloFocus)
        {
            halo.SetActive(false);
        }
    }

    private void changeHologram(string textName, int pos, string name, bool onVideo)
    {
        _text.text = textName;
        position = pos;

        if (!onVideo)
        {
            myVideo.setPauseVideo();
        }
        else if (pos == 5 && onVideo) 
        {
            myVideo.setPlayVideo();
        }


        foreach (GameObject obj in _names)
        {
            if (obj.name.ToString() == name)
            {
                obj.SetActive(true);
            }
            else if (obj.name.ToString() != name)
            {
                obj.SetActive(false);
            }
        }
    }
#endregion
}

