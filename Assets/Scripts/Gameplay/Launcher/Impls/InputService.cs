using System;
using Gameplay.Launcher.Interfaces;
using UnityEngine;

namespace Gameplay.Launcher.Impls
{
    public class InputService : MonoBehaviour, IInputService
    {
        public event Action<Vector2> OnClick = delegate { };
        public event Action<Vector2> OnDrag = delegate { };
        public event Action<Vector2> OnRelease = delegate { };

        private bool _pressed;
        private Vector2 _lastPos;

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 pos = touch.position;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _pressed = true;
                        _lastPos = pos;
                        OnClick(pos);
                        break;

                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        if (_pressed)
                        {
                            OnDrag(pos);
                            _lastPos = pos;
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        if (_pressed)
                        {
                            OnRelease(pos);
                            _pressed = false;
                        }
                        break;
                }

                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _pressed = true;
                _lastPos = Input.mousePosition;
                OnClick(_lastPos);
            }
            else switch (_pressed)
            {
                case true when Input.GetMouseButton(0):
                {
                    var pos = (Vector2)Input.mousePosition;
                    OnDrag(pos);
                    _lastPos = pos;
                    break;
                }
                case true when Input.GetMouseButtonUp(0):
                {
                    var pos = (Vector2)Input.mousePosition;
                    OnRelease(pos);
                    _pressed = false;
                    break;
                }
            }
        }
    }
}