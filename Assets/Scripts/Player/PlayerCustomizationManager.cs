using System.Collections;
using System.Collections.Generic;
//using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCustomizationManager : MonoBehaviour
{
    public static PlayerCustomizationManager instance; // Singleton instance

    public GameObject playerPrefab; // Reference to the player prefab
    public GameObject playerInstance; // The actual player instance
    public GameObject defaultPlayerMesh;

    // These variables hold the position and prefab that is currently equipped to the player

    public Transform hatSlot;
    public GameObject currentHat;
    public GameObject hatInstance;

    public Transform eyeSlot;
    public GameObject currentEyes;
    public GameObject eyeInstance;

    public Color currentColor;
    public Color colorInstance;

    public Transform player;
    public GameObject currentShirt;
    public GameObject shirtInstance;

    public Transform handSlot;
    public GameObject currentHands;
    public GameObject handInstance;

    public Transform righthandSlot;
    public GameObject currentRighthand;
    public GameObject righthandInstance;

    private string shirtName;
    private string hatName;
    private string eyesName;
    private string handsName;
    private string righthandName;
    private string colorName;

    private GameObject spawnPoint;
    [SerializeField] private bool isNull = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        if (playerInstance == null)
        {
            InitializePlayer();
        }

        FindAttachPoints();
        //ApplyCustomization();
        if (hatSlot == null || eyeSlot == null || player == null || handSlot == null || righthandSlot == null)
        {
            isNull = true;
        }

        
        if (spawnPoint == null && SceneManager.GetActiveScene().name != "Casino")
        {
            FindSpawnPoint();
        }
        

        if(isNull)
        {
            Debug.Log("isNull = true");

            isNull = false;
            FindAttachPoints();
            ApplyCustomization();
        }

        Customize();
    }

    private void FindAttachPoints()
    {
        if (player == null)
            player = GameObject.Find("Player(Clone)").transform;
        if (hatSlot == null)
            hatSlot = GameObject.Find("HatAttachPoint").transform;
        if (eyeSlot == null)
            eyeSlot = GameObject.Find("EyeAttachPoint").transform;
        if (handSlot == null)
            handSlot = GameObject.Find("HandAttachPoint").transform;
        if (righthandSlot == null)
            righthandSlot = GameObject.Find("RightHandAttachPoint").transform;
    }

    private void FindSpawnPoint()
    {
        string sceneName = SceneManager.GetActiveScene().name;   
        spawnPoint = GameObject.Find(sceneName + "SpawnPoint");
        if (spawnPoint != null)
        {
            Debug.Log("Spawnpoint found!");
            playerInstance.transform.position = spawnPoint.transform.position;
            playerInstance.transform.rotation = spawnPoint.transform.rotation;
        }
        else
        {
            Debug.Log("Spawn point not found, using a default spawn");
            playerInstance.transform.position = Vector3.zero;
            playerInstance.transform.rotation = Quaternion.identity;
        }
    }

    public void InitializePlayer()
    {
        if (GameObject.FindObjectOfType<PlayerController>() != null)
        {
            Destroy(GameObject.FindObjectOfType<PlayerController>());
        }

        if (playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab);
            if (currentShirt == null)
            {
                defaultPlayerMesh = GameObject.Find("Player_Base");
                currentShirt = defaultPlayerMesh;
            }
        }

        FindAttachPoints();
        ApplyCustomization();
        

    }

    public void DestroyPlayer()
    {
        if(playerInstance != null)
        {
            Destroy(playerInstance);
        }
    }

    private void Customize()
    {
        if (currentHat == null && hatInstance != null)
        {
            ApplyHead(hatInstance);
        }

        if (currentEyes == null && eyeInstance != null)
        {
            ApplyEyes(eyeInstance);
        }

        if (currentHands == null && handInstance != null)
        {
            ApplyHands(handInstance);
        }

        if (currentRighthand == null && righthandInstance != null)
        {
            ApplyRightHands(righthandInstance);
        }

        if (currentColor == colorInstance)
        {
            ApplyColor(colorInstance);
        }
        //SaveCustomization();
    }
    
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("SpawnPointX");
        PlayerPrefs.DeleteKey("SpawnPointY");
        PlayerPrefs.DeleteKey("SpawnPointZ");
        //SaveCustomization();
    }
    public void ApplyCustomization()
    {
        
        if (shirtInstance != null)
        {
            ApplyShirt(shirtInstance);
        }
        FindAttachPoints();

        if (handInstance != null)
        {
            ApplyHands(handInstance);
        }
        if(colorInstance != null)
        {
            ApplyColor(colorInstance);
        }
        if(eyeInstance != null)
        {
            ApplyEyes(eyeInstance);
        }
        if(hatInstance != null)
        {
            ApplyHead(hatInstance);
        }
        if(righthandInstance != null)
        {
            ApplyRightHands(righthandInstance);
        }
        //SaveCustomization();
    }
    /*
    public void SaveCustomization()
    {
        if (currentShirt != null)
        {
            PlayerPrefs.SetString("CurrentShirt", currentShirt.name);
        }
        else
        {
            PlayerPrefs.DeleteKey("CurrentShirt");
        }
        if (currentHat != null)
        {
            PlayerPrefs.SetString("CurrentHat", currentHat.name);
        }
        else
        {
            PlayerPrefs.DeleteKey("CurrentHat");
        }
        if (currentEyes != null)
        {
            PlayerPrefs.SetString("CurrentEyes", currentEyes.name);
        }
        else
        {
            PlayerPrefs.DeleteKey("CurrentEyes");
        }
        if (currentHands != null)
        {
            PlayerPrefs.SetString("CurrentHands", currentHands.name);
        }
        else
        {
            PlayerPrefs.DeleteKey("CurrentHands");
        }
        if (currentRighthand != null)
        {
            PlayerPrefs.SetString("CurrentRightHand", currentRighthand.name);
        }
        else
        {
            PlayerPrefs.DeleteKey("CurrentRightHand");
        }
        
        if (colorInstance != null)
        {
            PlayerPrefs.SetString("CurrentColor", colorInstance.name);
        }
        else
        {
            PlayerPrefs.DeleteKey("CurrentColor");
        }
        
        PlayerPrefs.Save();
    }

    public void LoadCustomization()
    {
        if (PlayerPrefs.HasKey("CurrentShirt"))
        {
            shirtName = PlayerPrefs.GetString("CurrentShirt");
            ApplyShirt(Resources.Load<GameObject>(shirtName));
        }

        if (PlayerPrefs.HasKey("CurrentHat"))
        {
            hatName = PlayerPrefs.GetString("CurrentHat");
            ApplyHead(Resources.Load<GameObject>(hatName));
        }

        if (PlayerPrefs.HasKey("CurrentEyes"))
        {
            eyesName = PlayerPrefs.GetString("CurrentEyes");
            ApplyEyes(Resources.Load<GameObject>(eyesName));
        }

        if (PlayerPrefs.HasKey("CurrentHands"))
        {
            handsName = PlayerPrefs.GetString("CurrentHands");
            ApplyHands(Resources.Load<GameObject>(handsName));
        }

        if (PlayerPrefs.HasKey("CurrentRightHand"))
        {
            righthandName = PlayerPrefs.GetString("CurrentRightHand");
            ApplyRightHands(Resources.Load<GameObject>(righthandName));
        }
        /*
        if (PlayerPrefs.HasKey("CurrentColor"))
        {
            colorName = PlayerPrefs.GetString("CurrentColor");
            ApplyColor(Resources.Load<Color>(colorName));
        }
        
    }
    */

    public void ApplyShirt(GameObject shirtPrefab)
    {
        shirtInstance = shirtPrefab;
        defaultPlayerMesh = shirtInstance;
        if (playerInstance != null)
        {
            if (currentShirt == null || currentShirt != shirtPrefab)
            {
                if (currentShirt != null)
                {
                    Destroy(currentShirt);
                }

                currentShirt = Instantiate(shirtPrefab);
                currentShirt.transform.SetParent(player);
                currentShirt.transform.localPosition = new Vector3(0, -1, 0);
                currentShirt.transform.localRotation = Quaternion.identity;
                ApplyColor(colorInstance);
                /*
                if (SceneManager.GetActiveScene().name == "Shop")
                {
                    string shirtType = currentShirt.GetComponentInChildren<SkinnedMeshRenderer>().name;
                    string colorType = currentColor.name;
                    Debug.Log(colorType);
                    switch (shirtType)
                    {
                        case "Cat_Base":
                            break;
                        case "Cat_Dress":
                            break;
                        case "Cat_Overall":
                            break;
                        case "Cat_RedShirt":
                            break;

                    }
                }
                else
                {

                }
                */
                
            }
        }
        else
        {
            playerInstance = Instantiate(playerPrefab);
            if (currentShirt == null)
            {
                defaultPlayerMesh = GameObject.Find("Player_Base");
                currentShirt = defaultPlayerMesh;
            }
            ApplyColor(colorInstance);
            
        }
        
    }

    public void ApplyEyes(GameObject eyesPrefab)
    {
        Debug.Log("Apply Eyes");
        eyeInstance = eyesPrefab;
        if (playerInstance != null)
        {
            // if(eyesPrefab.GetComponent<EyewearScript>() != null)
     
            if (currentEyes != null)
            {
                Destroy(currentEyes);
                Debug.Log("Destroy Old Eyes");
            }

            Debug.Log("Instantiate new eyes");
            currentEyes = Instantiate(eyesPrefab);
            currentEyes.transform.SetParent(eyeSlot);
            currentEyes.transform.localPosition = Vector3.zero;
            currentEyes.transform.localRotation = Quaternion.identity;
        }
    }
    public void ApplyColor(Color color)
    {
        colorInstance = color;
        if (playerInstance != null)
        {
            SkinnedMeshRenderer skinnedMesh = playerInstance.GetComponentInChildren<SkinnedMeshRenderer>();
            if (skinnedMesh != null)
            {
                Material material = skinnedMesh.material;
                material.color = color;
                currentColor = color;
            }
        }
    }

    public void ApplyHands(GameObject handsPrefab)
    {
        handInstance = handsPrefab;
        if (playerInstance != null)
        {
            // if(handsPrefab.GetComponent<HandwearScript>() != null)
            if (currentHands != null)
            {
                Destroy(currentHands);
            }

            currentHands = Instantiate(handsPrefab);
            currentHands.transform.SetParent(handSlot);
            currentHands.transform.localPosition = Vector3.zero;
            currentHands.transform.localRotation = Quaternion.identity;
        }
    }

    public void ApplyRightHands(GameObject righthandsPrefab)
    {
        righthandInstance = righthandsPrefab;
        if (playerInstance != null)
        {
            if(currentRighthand != null)
            {
                Destroy(currentRighthand);
            }

            currentRighthand = Instantiate(righthandsPrefab);
            currentRighthand.transform.SetParent(righthandSlot);
            currentRighthand.transform.localPosition = Vector3.zero;
            currentRighthand.transform.localRotation = Quaternion.identity;
        }
    }

    public void ApplyHead(GameObject hatPrefab)
    {
        hatInstance = hatPrefab;
        if (playerInstance != null)
        {
           // if(hatPrefab.GetComponent<HeadwearScript>() != null)
            if (currentHat != null)
            {
                Destroy(currentHat);
            }

            currentHat = Instantiate(hatPrefab);
            currentHat.transform.SetParent(hatSlot);
            currentHat.transform.localPosition = Vector3.zero;
            currentHat.transform.localRotation = Quaternion.identity;
        }
  
    }
    // -----------------------------------------------
    public bool IsHeadEquipped(GameObject hatPrefab)
    {
        return hatInstance == hatPrefab;
    }
    public void SetHeadEquipped(GameObject hatPrefab)
    {
        hatInstance = hatPrefab;
    }
    public void ClearEquippedHead()
    {
        hatInstance = null;
    }
    // ----------------------------------------------
    public bool IsHandEquipped(GameObject handPrefab)
    {
        return handInstance == handPrefab;
    }
    public void SetHandEquipped(GameObject handPrefab)
    {
        handInstance = handPrefab;
    }
    public void ClearHandEquipped()
    {
        handInstance = null;
    }
    // ----------------------------------------------
    public bool IsRightHandEquipped(GameObject righthandPrefab)
    {
        return righthandInstance == righthandPrefab;
    }

    public void SetRightHandEquipped(GameObject righthandPrefab)
    {
        righthandInstance = righthandPrefab;
    }
    public void ClearRightHandEquipped()
    {
        righthandInstance = null;
    }
    // ----------------------------------------------
    public bool IsShirtEquipped(GameObject shirtPrefab)
    {
        return shirtInstance == shirtPrefab;
    }
    public void SetShirtEquipped(GameObject shirtPrefab)
    {
        shirtInstance = shirtPrefab;
    }
    public void ClearShirtEquipped()
    {
        shirtInstance = null;
    }
    // ----------------------------------------------
    public bool IsEyewearEquipped(GameObject eyewearPrefab)
    {
        return eyeInstance == eyewearPrefab;
    }
    public void SetEyewearEquipped(GameObject eyeWearPrefab)
    {
        eyeInstance = eyeWearPrefab;
    }
    public void ClearEyewearEquipped()
    {
        eyeInstance = null;
    }
    // ----------------------------------------------
    public bool IsColorEquipped(Color color)
    {
        return colorInstance == color;
    }
    public void SetColorEquipped(Color color)
    {
        colorInstance = color;
    }
    /*
    public void ClearColorEquipped()
    {
        colorInstance = currentColor;                                                                       
    }
    */
}
