    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
    public Sprite playerActive;
    public Sprite playerReplay;
    private SpriteRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIndicatorActive()
    {
        _renderer.enabled = true;
        _renderer.sprite = playerActive;
    }

    public void SetIndicatorReplay()
    {
        _renderer.enabled = true;
        _renderer.sprite = playerReplay;
    }

    public void SetIndicatorInvisible()
    {
        _renderer.enabled = false;
    }
}
