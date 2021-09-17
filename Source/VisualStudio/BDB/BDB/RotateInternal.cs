﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BDB
{
    class ModuleBdbRotateInternal : PartModule
    {
        [KSPField(isPersistant = false)]
        public string rotation = "0,0,0";

        [KSPField(isPersistant = false)]
        public bool debug = false;

        public override void OnStart(StartState state)
        {
            GameEvents.onVesselChange.Add(OnVesselChange);
            RotateInternal();
        }

        private void OnDestroy()
        {
            GameEvents.onVesselChange.Remove(OnVesselChange);
        }

        public void OnVesselChange(Vessel v)
        {
            if (v == vessel)
                RotateInternal();
        }

        public void RotateInternal()
        {
            if (!HighLogic.LoadedSceneIsFlight)
                return;
            if (part.internalModel == null)
                return;

            Vector3 rot = Vector3.zero;
            string[] sArray = rotation.Split(',');
            if (sArray.Length < 3)
                return;
            if (sArray.Length > 0)
                rot.x = float.Parse(sArray[0]);
            if (sArray.Length > 1)
                rot.y = float.Parse(sArray[1]);
            if (sArray.Length > 2)
                rot.z = float.Parse(sArray[2]);

            part.internalModel.transform.localRotation *= Quaternion.Euler(rot);

            if (debug)
                ScreenMessages.PostScreenMessage("[ModuleBdbRotateInternal] Internal rotation: " + rotation);
        }
	}
}
