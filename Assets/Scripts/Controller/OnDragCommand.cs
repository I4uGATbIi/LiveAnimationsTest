using System;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine;

namespace Game
{
    public class OnDragCommand : Command
    {
        private float dist;
        private bool dragging = false;
        private Vector3 offset;
        private Transform toDrag;
        private Vector3 scale;

        [Inject] public FieldGrid FieldGrid { get; set; }

        [Inject] public CheckFieldSignal CheckFieldSignal { get; private set; }

        public override void Execute()
        {
            Vector3 v3;

            Touch touch = Input.touches[0];
            Vector3 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                Vector2 worldTouch = Camera.main.ScreenToWorldPoint(pos);
                RaycastHit2D[] hits = Physics2D.RaycastAll(worldTouch, Vector2.positiveInfinity);
                Nullable<RaycastHit2D> figureHit2D = null;
                foreach (var hit in hits)
                {
                    if (hit.collider.CompareTag("Figure"))
                    {
                        figureHit2D = hit;
                        break;
                    }
                }

                if (figureHit2D.HasValue)
                {
                    toDrag = figureHit2D.Value.transform;
                    scale = toDrag.localScale;
                    toDrag.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                    dist = figureHit2D.Value.transform.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    offset = toDrag.position - v3;
                    dragging = true;
                }
            }

            if (dragging && touch.phase == TouchPhase.Moved)
            {
                v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                v3 = Camera.main.ScreenToWorldPoint(v3);
                toDrag.position = v3 + offset;
            }

            if ((dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)))
            {
                dragging = false;
                ResolvePlacement();
                toDrag.localPosition = Vector3.zero;
                toDrag.localScale = scale;
            }
        }

        private void ResolvePlacement()
        {
            int row = -1, column = -1;
            Dictionary<Transform, Transform> elementToGrid = new Dictionary<Transform, Transform>();
            for (int i = 0; i < toDrag.childCount; i++)
            {
                Transform elementTransform = toDrag.GetChild(i);
                RaycastHit2D elementCast = Physics2D.Raycast(elementTransform.position, Vector2.positiveInfinity);
                if (!elementCast.collider.transform.CompareTag("GridBlock"))
                    return;
                string[] rowColumn = elementCast.collider.name.Split('/');
                if (rowColumn.Length <= 1 || rowColumn.Length > 2)
                {
                    return;
                }

                if (!int.TryParse(rowColumn[0], out row) ||
                    !int.TryParse(rowColumn[1], out column))
                {
                    return;
                }

                if (row < 0 || column < 0)
                {
                    return;
                }

                if (FieldGrid.Grid[row, column] == 1)
                {
                    return;
                }

                elementToGrid.Add(elementCast.transform, elementTransform);
            }

            toDrag.DetachChildren();
            foreach (KeyValuePair<Transform, Transform> element in elementToGrid)
            {
                row = column = -1;
                string[] rowColumn = element.Key.name.Split('/');
                if (!int.TryParse(rowColumn[0], out row) ||
                    !int.TryParse(rowColumn[1], out column))
                {
                    return;
                }
                if (row < 0 || column < 0)
                {
                    return;
                }
                element.Value.position = element.Key.position;
                element.Value.name = $"{row}/{column}";
                FieldGrid.Grid[row, column] = 1;
            }

            GameObject.Destroy(toDrag.gameObject);
            CheckFieldSignal.Dispatch();
        }
    }
}