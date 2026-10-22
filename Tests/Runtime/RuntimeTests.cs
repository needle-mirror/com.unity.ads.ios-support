using UnityEngine;
using NUnit.Framework;

namespace Unity.Advertisement.IosSupport.Tests
{
    /// <summary>
    /// Runtime tests for the iOS support bindings.
    /// </summary>
    public class RuntimeTests
    {
        /// <summary>
        /// Checks that the tracking authorization status is not determined when it has not been requested.
        /// </summary>
        [Test]
        [Timeout(10000)]

        //[UnityPlatform (exclude = new[] { RuntimePlatform.IPhonePlayer })]    //Need to find a way to programatically set state on the iPhone for this app otherwise, even on iOS initial state will be Not Determined.
        public void TestTrackingStatusMessageUnsupported()
        {
            Assert.That(ATTrackingStatusBinding.GetAuthorizationTrackingStatus(), Is.EqualTo(ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED));
        }
    }
}
