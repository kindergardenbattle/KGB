// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SplinePosition.cs" company="Exit Games GmbH">
//   Part of: Photon Unity Networking Demos
// </copyright>
// <summary>
//  Original: http://catlikecoding.com/unity/tutorials/curves-and-splines/
//  Used in SlotRacer Demo
// </summary>
// <author>developer@exitgames.com</author>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace Photon.Pun.Demo.SlotRacer.Utils
{
	[ExecuteInEditMode]
	public class SplinePosition : MonoBehaviour {

		public BezierSpline Spline;
		public bool reverse;
		public bool lookForward;
		public float currentDistance = 0f;

		public float currentClampedDistance;
        private float LastDistance;

		public void SetPositionOnSpline(float position)
		{
			this.currentDistance = position;
			ExecutePositioning ();
		}

        private void Update()
		{
			ExecutePositioning ();
		}

        private void ExecutePositioning()
		{
			if(this.Spline==null || this.LastDistance == this.currentDistance )
			{
				return;
			}
			LastDistance = this.currentDistance;

			// move the transform to the new point
			this.transform.position = this.Spline.GetPositionAtDistance(currentDistance,this.reverse);

			if (this.lookForward) {
				this.transform.LookAt(this.Spline.GetPositionAtDistance(currentDistance+1,this.reverse));
			}
		}


	}
}