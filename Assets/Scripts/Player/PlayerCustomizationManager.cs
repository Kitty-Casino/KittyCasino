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

    // These variables hold the position and prefab that is currently equipped to the player

    public Transform hatSlot;
    public GameObject currentHat;
    public GameObject hatInstance;

    public Transform eyeSlot;
    public GameObject currentEyes;
    public GameObject eyeInstance;

    public Transform neckSlot;
    public GameObject currentNeck;
    public GameObject neckInstance;

    public Transform shirtSlot;
    public GameObject currentShirt;
    public GameObject shirtInstance;

    public Transform handSlot;
    public GameObject currentHands;
    public GameObject handInstance;

    public Transform righthandSlot;
    public GameObject currentRighthand;
    public GameObject righthandInstance;

    public GameObject empty;
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

        if (hatSlot == null || eyeSlot == null || neckSlot == null || shirtSlot == null || handSlot == null || righthandSlot == null) 
        {
            isNull = true;
        }
        
        if(isNull)
        {
            empty = new GameObject();
            hatSlot = GameObject.Find("HatAttachPoint").transform;
            eyeSlot = GameObject.Find("EyeAttachPoint").transform;
            neckSlot = GameObject.Find("NeckAttachPoint").transform;
            shirtSlot = GameObject.Find("ShirtAttachPoint").transform;
            handSlot = GameObject.Find("HandAttachPoint").transform;
            righthandSlot = GameObject.Find("RightHandAttachPoint").transform;
            
            spawnPoint = FindSpawnPoint();
            if (spawnPoint != null)
            {
                playerInstance.transform.position = spawnPoint.transform.position;
                playerInstance.transform.rotation = spawnPoint.transform.rotation;
            }
            else
            {
                Debug.Log("Spawn point not found, using a default spawn");
                playerInstance.transform.position = Vector3.zero;
                playerInstance.transform.rotation = Quaternion.identity;
            }

            isNull = false;
            ApplyCustomization();
        }
       
        
    }

    private GameObject FindSpawnPoint()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        GameObject spawnPoint = GameObject.Find(sceneName + "SpawnPoint");

        return spawnPoint;
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
            // DontDestroyOnLoad(playerInstance);
        }
        
    }

    public void DestroyPlayer()
    {
        if(playerInstance != null)
        {
            Destroy(playerInstance);
        }
    }

    public void ApplyCustomization()
    {
        if(shirtInstance != null)
        {
            ApplyShirt(shirtInstance);
        }
        if(handInstance != null)
        {
            ApplyHands(handInstance);
        }
        if(neckInstance != null)
        {
            ApplyNeck(neckInstance);
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
        
    }
    public void ApplyShirt(GameObject shirtPrefab)
    {
        shirtInstance = shirtPrefab;
        if (playerInstance != null)
        {
           // if(shirtPrefab.GetComponent<ShirtScript>() != null)
    
            if (currentShirt != null)
            {
                Destroy(currentShirt);
            }

            currentShirt = Instantiate(shirtPrefab);
            currentShirt.transform.SetParent(shirtSlot);
            currentShirt.transform.localPosition = Vector3.zero;
            currentShirt.transform.localRotation = Quaternion.identity;
            // else
            // {
            //     shirtInstance = empty;
            // }
        }
 
    }

    public void ApplyEyes(GameObject eyesPrefab)
    {
        eyeInstance = eyesPrefab;
        if (playerInstance != null)
        {
            // if(eyesPrefab.GetComponent<EyewearScript>() != null)
     
            if (currentEyes != null)
            {
                Destroy(currentEyes);
            }
  
            currentEyes = Instantiate(eyesPrefab);
            currentEyes.transform.SetParent(eyeSlot);
            currentEyes.transform.localPosition = Vector3.zero;
            currentEyes.transform.localRotation = Quaternion.identity;
            // else
            // {
            //     eyeInstance = empty;
            // }

        }
    }
    public void ApplyNeck(GameObject neckPrefab)
    {
        neckInstance = neckPrefab;
        if (playerInstance != null)
        {
            // if(neckPrefab.GetComponent<NeckwearScript>() != null)

            if (currentNeck != null)
            {
                Destroy(currentNeck);
            }

            currentNeck = Instantiate(neckPrefab);
            currentNeck.transform.SetParent(neckSlot);
            currentNeck.transform.localPosition = Vector3.zero;
            currentNeck.transform.localRotation = Quaternion.identity;
            // else
            // {
            //     neckInstance = empty;
            // }
        }
            
    }

    public void ApplyHands(GameObject handsPrefab)
    {
        handInstance = handsPrefab;
        if (playerInstance != null)
        {
            // if(handsPrefab.GetComponent<HandwearScript>() != null)
            if (currentHat != null)
            {
                Destroy(currentHands);
            }

            currentHands = Instantiate(handsPrefab);
            currentHands.transform.SetParent(handSlot);
            currentHands.transform.localPosition = Vector3.zero;
            currentHands.transform.localRotation = Quaternion.identity;
            // else
            // {
            //     handInstance = empty;
            // }
        }
    }

    public void ApplyRightHands(GameObject righthandsPrefab)
    {
        righthandInstance = righthandsPrefab;
        if (playerInstance != null)
        {
            if(currentHat != null)
            {
                Destroy(currentRighthand);
            }

            currentRighthand = Instantiate(righthandsPrefab);
            currentRighthand.transform.SetParent(righthandSlot);
            currentRighthand.transform.localPosition = Vector3.zero;
            currentHands.transform.localRotation = Quaternion.identity;
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
            // else
            // {
            //     hatInstance = empty;
            // }
        }
  
    }

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

    
}
