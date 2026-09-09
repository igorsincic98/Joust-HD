using System;
using UnityEngine;

public class EggTrigger : MonoBehaviour
{
    [SerializeField] private EggManager _eggManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _eggManager = gameObject.transform.GetComponentInParent<EggManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Tilemap")
        {
            Debug.Log(other.name);
            _eggManager.Touchdown();
        }
    }
}
