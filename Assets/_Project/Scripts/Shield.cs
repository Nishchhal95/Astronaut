using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Renderer _renderer;
    [SerializeField] AnimationCurve _DisplacementCurve;
    [SerializeField] float _DisplacementMagnitude;
    [SerializeField] float _LerpSpeed;
    [SerializeField] float _DisolveSpeed;
    public bool shieldOn;
    private bool isProcessing;
    Coroutine _disolveCoroutine;
    public Collider shieldCollider;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.SetFloat("_Disolve", 1);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        HitShield(hit.point);
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    OpenCloseShield();
        //}
    }

    public void HitShield(Vector3 hitPos)
    {       
        StopCoroutine(Coroutine_HitDisplacement());
        _renderer.material.SetVector("_HitPos", hitPos);
        StartCoroutine(Coroutine_HitDisplacement());
    }

    public void OpenCloseShield()
    {
        // float target = 0;
        // if (shieldOn)
        // {
        //     target = 1;
        // }
        // shieldOn = !shieldOn;
        // if (_disolveCoroutine != null)
        // {
        //     StopCoroutine(_disolveCoroutine);
        // }
        // _disolveCoroutine = StartCoroutine(Coroutine_DisolveShield(target));

        if (isProcessing)
        {
            return;
        }
        shieldOn = !shieldOn;
        StartCoroutine(DissolveShieldRoutine(shieldOn));
    }

    IEnumerator Coroutine_HitDisplacement()
    {
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_DisplacementStrength", _DisplacementCurve.Evaluate(lerp) * _DisplacementMagnitude);
            lerp += Time.deltaTime*_LerpSpeed;
            yield return null;
        }
    }

    IEnumerator Coroutine_DisolveShield(float target)
    {
        if(target == 0)
        {
            yield return new WaitForSecondsRealtime(1f);
        }
        float start = _renderer.material.GetFloat("_Disolve");
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_Disolve", Mathf.Lerp(start,target,lerp));
            lerp += Time.deltaTime * _DisolveSpeed;
            yield return null;
        }
    }

    IEnumerator DissolveShieldRoutine(bool open)
    {
        isProcessing = true;
        if (open)
        {
            yield return new WaitForSecondsRealtime(1f);
        }
        int target = open ? 0 : 1;
        float start = _renderer.material.GetFloat("_Disolve");
        float lerp = 0;
        while (lerp < 1)
        {
            _renderer.material.SetFloat("_Disolve", Mathf.Lerp(start,target,lerp));
            lerp += Time.deltaTime * _DisolveSpeed;
            yield return null;
        }

        _renderer.material.SetFloat("_Disolve", target);
        isProcessing = false;

        shieldCollider.enabled = open;
    }

    private void OnTriggerEnter(Collider other)
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position,
            (other.transform.position - transform.position).normalized * 10f);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.GetComponent<Bullet>() != null)
            {

                Destroy(other.gameObject);
                HitShield(hits[i].point);
                return;
            }
        }

    }
}
