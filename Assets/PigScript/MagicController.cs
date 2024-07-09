using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class MagicController : MonoBehaviour
{


    [SerializeField] private Animator m_SpriteAnimator = null;

    [SerializeField] PlayerInput m_playerInput;
    [SerializeField] private CharacterController _controller;

    [SerializeField] private string[] magicCode;
    private bool magicBookCheck;
    public bool magicStart;

    [Header("Magic Prefab")]
    [SerializeField] private GameObject magicPortal = null;
    [SerializeField] private GameObject castingEffect = null;

    [SerializeField] private GameObject firesphere = null;
    [SerializeField] private GameObject snowsphere = null;
    [SerializeField] private GameObject thundersphere = null;

    [SerializeField] private GameObject shieldPrefab = null;

    [SerializeField] public TMP_Text wordOutput = null;
    [SerializeField] public TMP_Text MagicBook = null;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < magicCode.Length; i++)
        {
            MagicBook.text += " " + magicCode[i] + "<br>";


        }
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!castingEffect.activeInHierarchy)
            {
                castingEffect.SetActive(true);

                //playerMove1Script.enabled = !playerMove1Script.enabled;

                _controller.enabled = false;
                m_SpriteAnimator.SetBool("CastStart", true);

            }
            else
            {
                castingEffect.SetActive(false);
                m_SpriteAnimator.SetBool("CastStart", false);
                // playerMove1Script.enabled = true;


            }
            magicStart = !magicStart;
            m_playerInput.enabled = !m_playerInput.enabled;
            _controller.enabled = true;
            wordOutput.text = "";

        }

        if (magicStart)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                for (int i = 0; i < magicCode.Length; i++)
                {
                    if (magicCode[i] == wordOutput.text)
                    {
                        m_SpriteAnimator.SetBool("Casted", true);
                        wordOutput.text = "";
                        magicPortal.SetActive(true);
                        Invoke("MagicPortalClose", 1.3f);
                        Invoke(magicCode[i], 0.6f);
                        Debug.Log(magicCode);
                        magicBookCheck = true;

                        break;
                    }
                }
                if (!magicBookCheck)
                {
                    m_SpriteAnimator.SetBool("CastFail", true);
                    wordOutput.text = "";
                    Debug.Log("wrong magic spell!");
                }
                /* 
                if(magicCodes==wordOutput.text){
                     wordOutput.text = "";
                     Debug.Log(magicCodes);
                 }else{
                     wordOutput.text = "";
                     Debug.Log("wrong magic spell!");
                 }
                 */
                m_playerInput.enabled = !m_playerInput.enabled;
                _controller.enabled = true;
                magicStart = !magicStart;
                magicBookCheck = false;
                castingEffect.SetActive(false);

                m_SpriteAnimator.SetBool("CastStart", false);
                Invoke("NoCast", 1f);

            }
            if (Input.anyKeyDown)
            {
                string keysPressed = Input.inputString;

                wordOutput.text += keysPressed;


            }
        }
    }



    private void fireball()
    {
        Instantiate(firesphere, new Vector3(magicPortal.transform.position.x, magicPortal.transform.position.y, magicPortal.transform.position.z), transform.rotation);

    }
    private void thunderball()
    {
        Instantiate(thundersphere, new Vector3(magicPortal.transform.position.x, magicPortal.transform.position.y, magicPortal.transform.position.z), transform.rotation);

    }
    private void snowball()
    {
        Instantiate(snowsphere, new Vector3(magicPortal.transform.position.x, magicPortal.transform.position.y, magicPortal.transform.position.z), transform.rotation);

    }

    private void shield()
    {
        Instantiate(shieldPrefab, new Vector3(magicPortal.transform.position.x, magicPortal.transform.position.y, magicPortal.transform.position.z), transform.rotation);
    }
    public void MagicPortalClose()
    {
        magicPortal.SetActive(false);
    }
    public void NoCast()
    {
        m_SpriteAnimator.SetBool("CastFail", false);
        m_SpriteAnimator.SetBool("Casted", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBall")
        {
            Destroy(other.gameObject);
            Debug.Log("player got hit");
        }
        if (other.gameObject.tag == "Health")
        {
            Destroy(other.gameObject);
            Debug.Log("Health up");
        }
        if (other.gameObject.tag == "Key")
        {
            Destroy(other.gameObject);
            Debug.Log("Key obtained");
        }
        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
            Debug.Log("Coin obtained");
        }
    }
}

