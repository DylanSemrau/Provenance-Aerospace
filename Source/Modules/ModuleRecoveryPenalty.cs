using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProvenanceAerospace
{
    public class ModuleRecoveryPenalty : PartModule, IPartCostModifier
    {
        [KSPField]
        public float recoveryPenalty = 0.0f;

        private float moduleCost = 0.0f;
        private float defaultModuleCost = 0.0f;

        public void Start()
        {
            defaultModuleCost = part.partInfo.cost;

            GameEvents.OnVesselRecoveryRequested.Add(OnVesselRecoveryRequested);
        }

        private void OnVesselRecoveryRequested(Vessel vessel)
        {
            moduleCost = defaultModuleCost - (defaultModuleCost * (1 - recoveryPenalty));
        }

        public float GetModuleCost(float defaultCost, ModifierStagingSituation sit)
        {
            return -moduleCost;
        }

        public ModifierChangeWhen GetModuleCostChangeWhen()
        {
            return ModifierChangeWhen.FIXED;
        }

        public void OnDestroy()
        {
            GameEvents.OnVesselRecoveryRequested.Remove(OnVesselRecoveryRequested);
        }
    }
}

