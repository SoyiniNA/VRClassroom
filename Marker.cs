using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _pensize = 5;

    //variables that we have 
    private Renderer _renderer;
    private Color[] _colors;
    private float _tipheight;


    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;
    
    //start method 
    void Start()
    {
        _renderer = _tip.GetComponent<Renderer>();
        _colors = Enumerable.Repeat(_renderer.material.color, _pensize * _pensize).ToArray();
        _tipheight = _tip.localScale.y;
    }

 //calling draw method 
    void Update()
    {
        Draw();

    }

    //function for draw method 
    private void Draw()
    {
        if (Physics.Raycast(origin: _tip.position, direction: transform.up, out _touch, _tipheight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }
                _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                var x = (int)(_touchPos.x * _whiteboard.texturesize.x - (_pensize / 2));
                var y = (int)(_touchPos.y * _whiteboard.texturesize.y - (_pensize / 2));

                if (y < 0 || y > _whiteboard.texturesize.y || x < 0 || x > _whiteboard.texturesize.x) return;

                if (_touchedLastFrame)
                {
                    //setting original point of touch
                    _whiteboard.texture.SetPixels(x, y, blockWidth: _pensize, blockHeight: _pensize, _colors);

                    //loopoing through to set group of pixels to a colour
                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, b: x, t: f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, b: y, t: f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, blockWidth: _pensize, blockHeight: _pensize, _colors);

                    }
                    //lock in the rotation
                    transform.rotation = _lastTouchRot;
                    //apply coloration to pixels
                    _whiteboard.texture.Apply();
                }

                //pushing for the next frame 
                _lastTouchPos = new Vector2(x, y);
                _lastTouchRot = transform.rotation;
                _touchedLastFrame = true;
                return;

            }
        }
        _whiteboard = null;
        _touchedLastFrame = false;
    }  
}
