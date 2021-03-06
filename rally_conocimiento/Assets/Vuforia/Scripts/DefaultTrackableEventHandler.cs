/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Vuforia
{
	
    /// <summary>
    /// A custom handler that implements the ITrackableEventHandler interface.
    /// </summary>
    public class DefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler
    {
        #region PRIVATE_MEMBER_VARIABLES
 
		private TrackableBehaviour mTrackableBehaviour;
		private Canvas cResps, cPreg, cSig;
		private string sName;
<<<<<<< HEAD
<<<<<<< HEAD
		private sig_edificio sigEdif;
		GameObject gameObjectScript;

<<<<<<< HEAD
=======
    
>>>>>>> parent of 10f3239... yaaaaaaa por fin
=======
>>>>>>> parent of a382d6b... Audio de pregunta
=======
    
>>>>>>> parent of 10f3239... yaaaaaaa por fin
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
<<<<<<< HEAD
<<<<<<< HEAD
        {
=======
		{
			gameObjectScript = GameObject.Find("script");
			sigEdif = gameObjectScript.GetComponent<sig_edificio>();
>>>>>>> parent of a382d6b... Audio de pregunta
=======
        {
>>>>>>> parent of 10f3239... yaaaaaaa por fin
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			sName = "CanvasPreg"+mTrackableBehaviour.TrackableName;
			cResps = GameObject.Find ("CanvasResp").GetComponent<Canvas> ();
			cPreg = GameObject.Find (sName).GetComponent<Canvas> ();
			cSig = GameObject.Find ("CanvasSiguiente").GetComponent<Canvas> ();
            if (mTrackableBehaviour)
            {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
        {
			
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
				OnTrackingFound ();
<<<<<<< HEAD
<<<<<<< HEAD
				Debug.Log (" sig edi" + sigEdif.obtenerEd ());
				if (mTrackableBehaviour.TrackableName.Equals (sigEdif.obtenerEd ())) {
					cResps.enabled = true;
					cPreg.enabled = true;
				}
<<<<<<< HEAD
=======
<<<<<<< HEAD
                //pregunta.Play();
                Debug.Log (" sig edi" + sigEdif.obtenerEd ());
				if (mTrackableBehaviour.TrackableName.Equals (sigEdif.obtenerEd ())) {
					cResps.enabled = true;
					cPreg.enabled = true;
                    
                }
=======
				cResps.enabled = true;
				cPreg.enabled = true;
>>>>>>> parent of 10f3239... yaaaaaaa por fin
>>>>>>> parent of 731f822... Revert "Revert "yaaaaaaa por fin""
=======
>>>>>>> parent of a382d6b... Audio de pregunta
=======
				cResps.enabled = true;
				cPreg.enabled = true;
>>>>>>> parent of 10f3239... yaaaaaaa por fin
            }
            else
            {
				OnTrackingLost ();
				cResps.enabled = false;
				cPreg.enabled = false;
				cSig.enabled = false;
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }


        private void OnTrackingLost()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
			}
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS

    }

}
