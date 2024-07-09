using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigShield : MonoBehaviour
{
    bool collision;

    Renderer _renderer;

    [SerializeField] float _DisolveSpeed;
    Coroutine _disolveCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(0));

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.forward, out hit, transform.rotation, 0.5f))
        {
            collision = true;
            Debug.Log("Touched");
            _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(1));
            Destroy(gameObject, 1f);
        }
        else collision = false;



    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Touched");
        _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(1));
        Destroy(gameObject, 3f);
    }
    IEnumerator Coroutine_DisolveShield(float target)
    {
        float start = _renderer.material.GetFloat("_Disolve");
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_Disolve", Mathf.Lerp(start, target, lerp));
            lerp += Time.deltaTime * _DisolveSpeed;
            yield return null;
        }
    }
}
