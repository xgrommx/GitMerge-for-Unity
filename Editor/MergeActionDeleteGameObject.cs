﻿using UnityEngine;
using System.Collections;

namespace GitMerge
{
    public class MergeActionDeleteGameObject : MergeAction
    {
        private GameObject copy;
        private bool oursWasActive;

        public MergeActionDeleteGameObject(GameObject ours, GameObject theirs)
            : base(ours, theirs)
        {
            oursWasActive = ours.activeSelf;
            copy = GameObject.Instantiate(ours) as GameObject;
            copy.name = ours.name;
            copy.SetActiveForMerging(false);

            UseOurs();
        }

        protected override void ApplyOurs()
        {
            if(ours == null)
            {
                ours = ObjectDictionaries.InstantiateFromMerging(copy);
                ours.SetActive(oursWasActive);
            }
        }

        protected override void ApplyTheirs()
        {
            if(ours != null)
            {
                GameObject.DestroyImmediate(ours);
            }
        }

        public override void OnGUI()
        {
            var defaultOptionColor = merged ? Color.gray : Color.white;

            GUI.color = usingOurs ? Color.green : defaultOptionColor;
            if(GUILayout.Button("Keep GameObject"))
            {
                UseOurs();
            }
            GUI.color = usingTheirs ? Color.green : defaultOptionColor;
            if(GUILayout.Button("Delete GameObject"))
            {
                UseTheirs();
            }
        }
    }
}