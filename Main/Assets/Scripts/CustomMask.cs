using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
namespace UnityEngine.UI
{
    public class CustomMask : MaskableGraphic, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Serializable]
        public class OnTargetAreaTouch : UnityEvent
        {
            public OnTargetAreaTouch() { }
        }
 
        [Header("target control")]
        [SerializeField]
        private RectTransform m_target;
 
        public RectTransform target
        {
            get { return m_target; }
            set
            {
                m_target = value;
                RefreshVertex();
            }
        }
 
        public OnTargetAreaTouch onTargetTouch;
 
        private Vector2 targetMin;
        private Vector2 targetMax;
 
        public bool IsRaycast = true;
 
        public void RefreshVertex()
        {
            Vector2 newMin;
            Vector2 newMax;
            if (m_target != null && m_target.gameObject.activeSelf)
            {
                GetTargetMinMax(out newMin, out newMax, m_target);
            }
            else
            {
                newMin = Vector2.zero;
                newMax = Vector2.zero;
            }
            if (targetMin != newMin || targetMax != newMax)
            {
                targetMin = newMin;
                targetMax = newMax;
                SetAllDirty();
            }
        }
        private void GetTargetMinMax(out Vector2 min, out Vector2 max, RectTransform target)
        {
            Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(transform, target);
            min = bounds.min;
            max = bounds.max;
        }
 
        protected override void OnEnable()
        {
            base.OnEnable();
            SetAllDirty();
        }
 
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
            Rect maskRect = rectTransform.rect;
 
            Vector2 v0 = new Vector2(-maskRect.width / 2, maskRect.height / 2);
            Vector2 v1 = new Vector2(maskRect.width / 2, maskRect.height / 2);
            Vector2 v2 = new Vector2(targetMin.x, targetMax.y);
            Vector2 v3 = targetMax;
            Vector2 v4 = targetMin;
            Vector2 v5 = new Vector2(targetMax.x, targetMin.y);
            Vector2 v6 = new Vector2(-maskRect.width / 2, -maskRect.height / 2);
            Vector2 v7 = new Vector2(maskRect.width / 2, -maskRect.height / 2);
            vh.AddVert(v0, color, Vector2.zero);
            vh.AddVert(v1, color, Vector2.zero);
            vh.AddVert(v2, color, Vector2.zero);
            vh.AddVert(v3, color, Vector2.zero);
            vh.AddVert(v4, color, Vector2.zero);
            vh.AddVert(v5, color, Vector2.zero);
            vh.AddVert(v6, color, Vector2.zero);
            vh.AddVert(v7, color, Vector2.zero);
            vh.AddTriangle(2, 1, 0);
            vh.AddTriangle(2, 3, 1);
            vh.AddTriangle(3, 7, 1);
            vh.AddTriangle(3, 5, 7);
            vh.AddTriangle(5, 6, 7);
            vh.AddTriangle(5, 6, 4);
            vh.AddTriangle(4, 6, 0);
            vh.AddTriangle(4, 2, 0);
        }
 
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (!IsValidEvent(eventData))
                return;
#if UNITY_EDITOR
            Debug.Log("target clicked,name: " + m_target.name);
#endif
            ExecuteEvents.Execute(m_target.gameObject, eventData, ExecuteEvents.pointerClickHandler);
            if (onTargetTouch != null)
                onTargetTouch.Invoke();
 
        }
 
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (!IsValidEvent(eventData))
                return;
            ExecuteEvents.Execute(m_target.gameObject, eventData, ExecuteEvents.pointerDownHandler);
        }
 
        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (!IsValidEvent(eventData))
                return;
            ExecuteEvents.Execute(m_target.gameObject, eventData, ExecuteEvents.pointerUpHandler);
        }
 
        private bool IsValidEvent(PointerEventData eventData)
        {
            if (m_target == null)
                return false;
            if (!IsRaycast || !m_target.gameObject.activeSelf)
                return false;
            if (!RectTransformUtility.RectangleContainsScreenPoint(m_target, eventData.position, eventData.pressEventCamera))
                return false;
            return true;
        }
 
 
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            RefreshVertex();
            base.OnValidate();
        }
#endif
    }
}