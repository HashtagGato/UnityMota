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
		public  AudioClip a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a18, a19, a20, a21,a22, a23, a24, a25, a26, a27, a28, a29, a30, a31, a32, a33, a34, a35, a36, a37, a38, a39, a40,a41, a42, a43, a44, a45, a46, a47, a48, a49, a50;
		AudioClip[] audios;
		public AudioSource fuente;
		private fin iE;
		private inicioEscaneo preg;
		private bool ban=false;
		private int idPreg;
        #endregion // PRIVATE_MEMBER_VARIABLES



        #region UNTIY_MONOBEHAVIOUR_METHODS
    
        void Start()
		{
			fuente = GetComponent<AudioSource> ();
			AudioClip[] auxAudios = {a1, a2,a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16, a17, a18, a19, a20, a21,a22, a23, a24, a25, a26, a27, a28, a29, a30, a31, a32, a33, a34, a35, a36, a37, a38, a39, a40,a41, a42, a43, a44, a45, a46, a47, a48, a49, a50};
			audios = auxAudios;
			iE = GameObject.Find("scriptFin").GetComponent<fin> ();
			preg = GameObject.Find("ScriptMov").GetComponent<inicioEscaneo> ();
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
			sName = iE.getnEdificio ();
			idPreg = (iE.getidPregunta ()-1);
			Debug.Log (" pregunta "+idPreg);
			cResps = GameObject.Find ("CanvasResp").GetComponent<Canvas> ();
			cPreg = GameObject.Find ("CanvasPreg" + sName).GetComponent<Canvas> ();
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
            }
            else
            {
				OnTrackingLost ();
				cResps.enabled = false;
				cPreg.enabled = false;
				cSig.enabled = false;
				if (!ban) {
					ban = true;
				} else {
					fuente.clip = audios [idPreg];
					fuente.Stop ();
				}
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
			if (mTrackableBehaviour.TrackableName.Equals (sName)) {
				cResps.enabled = true;
				cPreg.enabled = true;
				fuente.clip = audios [idPreg];
				fuente.Play ();
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
