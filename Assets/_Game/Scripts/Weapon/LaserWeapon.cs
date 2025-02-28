﻿using UnityEngine;

namespace FG
{
    public class LaserWeapon : BaseWeapon, IWeapon
    {
        public float coolDown = 0.2f;
        public GameObject shot;
        public float bulletSpeed = 500f;
        public float damageToApply = 2f;
        public float despawnTime = 10f;

        private int _alternatingOrder = 0;

        public float Shoot()
        {
            GameObject[] currentShot;
            if (shootAll)
            {
                currentShot = new GameObject[localWeaponOutputs.Length];
                for (int i = 0; i < localWeaponOutputs.Length; i++)
                {
                    currentShot[i] = Instantiate(shot, localWeaponOutputs[i].position, transform.rotation);
                }
            }
            else if (shootAlternating)
            {
                currentShot = new GameObject[1];
                currentShot[0] = Instantiate(shot, localWeaponOutputs[_alternatingOrder].position, transform.rotation);
                
                _alternatingOrder++;
                if (_alternatingOrder > localWeaponOutputs.Length - 1)
                {
                    _alternatingOrder = 0;
                }
            }
            else
            {
                Debug.Log("Either shootAll or shootAlternating should be true");
                return coolDown;
            }

            foreach(GameObject obj in currentShot)
            {
                Shot shotScript = obj.GetComponentInChildren<Shot>();
                shotScript.damageToApply = damageToApply;
                shotScript.despawnTime = despawnTime;
                
                Rigidbody tempBody = obj.GetComponent<Rigidbody>();
                tempBody.velocity = bulletSpeed * transform.forward + GetComponent<Rigidbody>().velocity;
            }

            return coolDown;
        }
    }
}
