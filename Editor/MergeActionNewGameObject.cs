﻿using UnityEngine;
using UnityEditor;

namespace GitMerge
{
    public class MergeActionNewGameObject : MergeAction
    {
        public MergeActionNewGameObject(GameObject ours, GameObject theirs)
            : base(ours, theirs)
        {
            UseTheirs();
        }

        protected override void ApplyOurs()
        {
            if(ours)
            {
                ObjectDictionaries.RemoveInstanceOf(theirs);
                GameObject.DestroyImmediate(ours);
            }
        }

        protected override void ApplyTheirs()
        {
            if(!ours)
            {
                ours = theirs.InstantiateForMerging();
                ObjectDictionaries.SetAsInstance(ours, theirs);
            }
        }

        public override void OnGUI()
        {
            var defaultOptionColor = merged ? Color.gray : Color.white;

            GUI.color = usingOurs ? Color.green : defaultOptionColor;
            if(GUILayout.Button("Don't add GameObject"))
            {
                UseOurs();
            }
            GUI.color = usingTheirs ? Color.green : defaultOptionColor;
            if(GUILayout.Button("Add new GameObject"))
            {
                UseTheirs();
            }
        }
    }
}