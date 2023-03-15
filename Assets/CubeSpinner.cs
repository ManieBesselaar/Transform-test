using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpinner : MonoBehaviour
{
    bool _useCachedTransform = false;
    Transform _transform;
    [SerializeField] Color _cachedColor= Color.white;
    [SerializeField] Color _nonCachedColor = Color.red;
    Material _material;
    int _zRotation = 0;
    
    MeshRenderer _meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
       _transform = GetComponent<Transform>();  
    _material= GetComponent<MeshRenderer>().material;
        _material.color = _nonCachedColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            _meshRenderer.enabled = !_meshRenderer.enabled;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _useCachedTransform = !_useCachedTransform;
            if(_useCachedTransform)
            {
                if(_meshRenderer.enabled)
                _material.color= _cachedColor; 
            }
            else
            {
                if(_meshRenderer.enabled)
                _material.color= _nonCachedColor;
            }
           
        }    
    if(_useCachedTransform) { UseCachedTransform(); }
        else
        {
            UseTransform();
        }
    }
    void UseCachedTransform() 
    {
        _zRotation = (_zRotation +1) % 360;
        _transform.rotation = Quaternion.Euler(20, 20, _zRotation);
    }
    void UseTransform()
    {

        _zRotation = (_zRotation + 1) % 360;
        transform.rotation = Quaternion.Euler(20, 20, _zRotation);
    }

}
