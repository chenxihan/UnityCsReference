// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

namespace UnityEngine.UIElements
{
    // This is basically the same as the standard layout update except for 1 thing :
    // - Only call dirty repaint when the layout rect has changed instead of "yogaNode.HasNewLayout"
    internal class UIRLayoutUpdater : BaseVisualTreeUpdater
    {
        const int kMaxValidateLayoutCount = 5;

        public override string description
        {
            get { return "UIR Update Layout"; }
        }

        public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
        {
            if ((versionChangeType & (VersionChangeType.Layout | VersionChangeType.Hierarchy)) == 0)
                return;

            var yogaNode = ve.yogaNode;
            if (yogaNode != null && yogaNode.IsMeasureDefined)
            {
                yogaNode.MarkDirty();
            }
        }

        public override void Update()
        {
            // update flex once
            int validateLayoutCount = 0;
            while (visualTree.yogaNode.IsDirty)
            {
                // Doing multiples layout pass require to update the styles or else the
                // elements may not be initialized properly and the resulting layout will be invalid.
                if (validateLayoutCount > 0)
                    panel.ApplyStyles();

                panel.duringLayoutPhase = true;
                visualTree.yogaNode.CalculateLayout();
                panel.duringLayoutPhase = false;
                using (new EventDispatcherGate(visualTree.panel.dispatcher))
                {
                    UpdateSubTree(visualTree, validateLayoutCount);
                }

                if (validateLayoutCount++ >= kMaxValidateLayoutCount)
                {
                    Debug.LogError("Layout update is struggling to process current layout (consider simplifying to avoid recursive layout): " + visualTree);
                    break;
                }
            }
        }

        private void UpdateSubTree(VisualElement ve, int currentLayoutPass)
        {
            Rect yogaRect = new Rect(ve.yogaNode.LayoutX, ve.yogaNode.LayoutY, ve.yogaNode.LayoutWidth, ve.yogaNode.LayoutHeight);
            Rect lastRect = ve.renderData.lastLayout;
            bool rectChanged = false;

            VersionChangeType changeType = 0;

            // If the last layout rect is different than the current one we must dirty transform on children
            if ((lastRect.width != yogaRect.width) || (lastRect.height != yogaRect.height))
            {
                changeType |= VersionChangeType.Size | VersionChangeType.Repaint;
                rectChanged = true;
            }
            if (yogaRect.position != lastRect.position)
            {
                changeType |= VersionChangeType.Transform;
                rectChanged = true;
            }

            if (changeType != 0)
                ve.IncrementVersion(changeType);

            ve.renderData.lastLayout = yogaRect;

            // ignore clean sub trees
            bool hasNewLayout = ve.yogaNode.HasNewLayout;
            if (hasNewLayout)
            {
                for (int i = 0; i < ve.hierarchy.childCount; ++i)
                {
                    UpdateSubTree(ve.hierarchy[i], currentLayoutPass);
                }
            }

            if (rectChanged)
            {
                using (var evt = GeometryChangedEvent.GetPooled(lastRect, yogaRect))
                {
                    evt.layoutPass = currentLayoutPass;
                    evt.target = ve;
                    ve.SendEvent(evt);
                }
            }

            if (hasNewLayout)
            {
                ve.yogaNode.MarkLayoutSeen();
            }
        }
    }
}
