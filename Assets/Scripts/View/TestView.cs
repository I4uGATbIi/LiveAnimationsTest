using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;

namespace Game
{
    public class TestView : View
    {
        public Signal poolIsEmpty = new Signal();

        private GameObject[] figurePool;

        private Touch workingTouch = new Touch();
        private Transform workingFigure;

        [Inject] public OnDrag OnDragSignal { get; private set; }

        private void FixedUpdate()
        {
            if (figurePool == null)
                figurePool = GameObject.FindGameObjectsWithTag("Pool");
            if (isPoolEmpty())
            {
                poolIsEmpty.Dispatch();
            }

            if (Input.touches.Length > 0)
            {
                OnDragSignal.Dispatch();
            }
        }

        private bool isPoolEmpty()
        {
            var result = true;
            foreach (var pool in figurePool)
            {
                if (pool.transform.childCount > 0)
                    result = false;
            }

            return result;
        }

        private void ResolveTouch()
        {
            if (!(Input.touches.Where(touch => touch.Equals(workingTouch)).Count() > 0))
            {
                workingFigure = null;

                foreach (var touch in Input.touches)
                {
                    RaycastHit2D[] touchedElements = Physics2D.RaycastAll(
                        Camera.main.ScreenToWorldPoint(touch.position),
                        Vector2.positiveInfinity);
                    foreach (var touchedElement in touchedElements)
                    {
                        if (touchedElement.transform.CompareTag("Figure"))
                        {
                            workingFigure = touchedElement.transform;
                            workingTouch = touch;
                            break;
                        }
                    }
                }
            }

            if (workingFigure != null && Input.touches.Where(touch => touch.Equals(workingTouch)).Count() > 0)
            {
                
            }
        }
    }
}