using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace Unity.Advertisement.IosSupport
{
    using AOT;
    /// <summary>
    /// Provides access to Apple's App Tracking Transparency (ATT) API on iOS. Calls have no effect on other platforms.
    /// </summary>
    public class ATTrackingStatusBinding
    {
#if UNITY_IOS
        [DllImport("__Internal")] private static extern void InterfaceTrackingAuthorizationRequest(RequestAuthorizationTrackingCompleteHandler callback);
        [DllImport("__Internal")] private static extern int InterfaceGetTrackingAuthorizationStatus();
#endif
        /// <summary>
        /// Callback invoked when an authorization tracking request completes.
        /// </summary>
        /// <param name="status">The resulting authorization status, as the integer value of an <c>AuthorizationTrackingStatus</c>.</param>
        public delegate void RequestAuthorizationTrackingCompleteHandler(int status);
        private static RequestAuthorizationTrackingCompleteHandler _requestAuthorizationTrackingCompleteCallback = null;

        /// <summary>
        /// The enumerated states of an authorization tracking request.
        /// </summary>
        public enum AuthorizationTrackingStatus
        {
            /// <summary>
            /// The user has not yet been asked to authorize tracking.
            /// </summary>
            NOT_DETERMINED = 0,
            /// <summary>
            /// Authorization to track is restricted, for example by device settings, and the user cannot change it.
            /// </summary>
            RESTRICTED,
            /// <summary>
            /// The user denied authorization to track.
            /// </summary>
            DENIED,
            /// <summary>
            /// The user authorized tracking.
            /// </summary>
            AUTHORIZED
        }

        /// <summary>
        /// This method allows you to <a href="https://developer.apple.com/documentation/apptrackingtransparency/attrackingmanager/3547037-requesttrackingauthorization">request the user permission dialogue</a>.
        /// </summary>
        public static void RequestAuthorizationTracking()
        {
            RequestAuthorizationTracking(null);
        }

        /// <summary>
        /// This method allows you to <a href="https://developer.apple.com/documentation/apptrackingtransparency/attrackingmanager/3547037-requesttrackingauthorization">request the user permission dialogue</a>.
        /// </summary>
        /// <param name="callback">Invoked with the resulting authorization status when the request completes. Can be null.</param>
        public static void RequestAuthorizationTracking(RequestAuthorizationTrackingCompleteHandler callback)
        {
#if UNITY_IOS
            if (Application.platform != RuntimePlatform.IPhonePlayer) return;
            if (_requestAuthorizationTrackingCompleteCallback != null)
            {
                throw new InvalidOperationException("App tracking transparency request is already triggered and awaiting completion");
            }
            _requestAuthorizationTrackingCompleteCallback = callback;

            InterfaceTrackingAuthorizationRequest(AppTransparencyTrackingRequestCompleted);
#endif
        }

        /// <summary>
        /// This method allows you to check the app tracking transparency (ATT) <a href="https://developer.apple.com/documentation/apptrackingtransparency/attrackingmanager/3547038-trackingauthorizationstatus">authorization status</a>.
        /// </summary>
        /// <returns>An <c>AuthorizationTrackingStatus</c> enum value.</returns>
        public static AuthorizationTrackingStatus GetAuthorizationTrackingStatus()
        {
#if UNITY_IOS
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return (AuthorizationTrackingStatus)InterfaceGetTrackingAuthorizationStatus();
            }
#endif
            return AuthorizationTrackingStatus.NOT_DETERMINED;
        }

        /// <summary>
        /// Called by the native iOS plugin when an authorization tracking request completes. Not intended to be called directly.
        /// </summary>
        /// <param name="status">The resulting authorization status, as the integer value of an <c>AuthorizationTrackingStatus</c>.</param>
        [MonoPInvokeCallback(typeof(RequestAuthorizationTrackingCompleteHandler))]
        public static void AppTransparencyTrackingRequestCompleted(int status)
        {
            _requestAuthorizationTrackingCompleteCallback?.Invoke(status);
            _requestAuthorizationTrackingCompleteCallback = null;
        }
        
    }
}
