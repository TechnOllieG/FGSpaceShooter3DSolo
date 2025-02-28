﻿using UnityEngine;

namespace FG
{
    [RequireComponent(typeof(WeaponManager))]
    public class BaseWeapon : MonoBehaviour
    {
        public enum WeaponMode
        {
            All,
            Alternating,
        }
        
        public bool enableScript = false;

        public string Name => GetType().Name;

        public bool Enabled
        {
            get => enableScript;
            set => enableScript = value;
        }

        public WeaponMode weaponMode = WeaponMode.All;

        protected bool shootAll = false;
        protected bool shootAlternating = false;
        protected Transform[] localWeaponOutputs;
        protected Transform cameraTransform;
        protected WeaponManager weaponManager;
        protected CanvasManager canvasManager;

        private void Awake()
        {
            weaponManager = GetComponent<WeaponManager>();
            localWeaponOutputs = weaponManager.weaponOutputs;
            cameraTransform = GameManager.PlayerCamera.transform;
            canvasManager = GameManager.CanvasManagerInstance;
            SetWeaponMode();
        }

        private void OnValidate()
        {
            SetWeaponMode();
        }

        private void SetWeaponMode()
        {
            switch(weaponMode)
            {
                case WeaponMode.All:
                    shootAll = true;
                    shootAlternating = false;
                    break;
                case WeaponMode.Alternating:
                    if (GetComponent<WeaponManager>().weaponOutputs.Length == 1)
                    {
                        Debug.Log("There is only one weapon output, alternating is not an option");
                        weaponMode = WeaponMode.All;
                        goto case WeaponMode.All;
                    }
                    shootAll = false;
                    shootAlternating = true;
                    break;
            }
        }
    }
}
