/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/
using UnityEngine;
using UnityEngine.UI;

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
		private Canvas cResps, cPreg;
		private Text tPreg, tResp1, tResp2, tResp3, tResp4;
    
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
        {

			cResps = GameObject.Find ("CanvasResp").GetComponent<Canvas> ();
			cPreg = GameObject.Find ("CanvasPreg").GetComponent<Canvas> ();
			tPreg = GameObject.Find ("preguntaText").GetComponent<Text> ();
			tResp1 = GameObject.Find ("TextA").GetComponent<Text> ();
			tResp2 = GameObject.Find ("TextB").GetComponent<Text> ();
			tResp3 = GameObject.Find ("TextC").GetComponent<Text> ();
			tResp4 = GameObject.Find ("TextD").GetComponent<Text> ();
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
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
				OnTrackingFound();
				cResps.enabled = true;
				cPreg.enabled = true;
				tResp1.text = "Resp A";//Asignar lo consumido por el webService, en cada una
				tResp2.text = "Resp B";
				tResp3.text = "Resp C";
				tResp4.text = "Resp D";
				tPreg.text = "Hola mi nombre es haruko y esta es tu siguiente pregunta, ¿cuántos años se toma para viajar a la luna?";
            }
            else
            {
				OnTrackingLost();
				cResps.enabled = false;
				cPreg.enabled = false;
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
            Debug.Log("Trackable " + mTrackableBehaviour.name + " found");
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
