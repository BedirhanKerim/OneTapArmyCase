using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneTapArmyCore
{
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] private Camera camera;
      //  [SerializeField] private MovementManager movementManager;
        [SerializeField] private ParticleSystem pointerParticle;
        public Transform armyLocatorObj;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Sol tık kontrolü
            {
                PointGround();
            }
        }

        // Update is called once per frame


        private void PointGround()
        {
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) // Eğer bir objeye çarparsa
                {
                    Vector3 hitPosition = hit.point; // Çarpışma noktasını al
                    armyLocatorObj.position = new Vector3(hitPosition.x, armyLocatorObj.position.y, hitPosition.z);
                    pointerParticle.transform.position = hit.point;
                    pointerParticle.Play();
                   // movementManager.MovePlayerArmy();
                    GameEventManager.Instance.OnOnMovePlayerArmy();
                }
            }
        }
    }
}