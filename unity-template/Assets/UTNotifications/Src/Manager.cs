﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UTNotifications
{
    /// <summary>
    /// The main class providing UTNotifications API.
    /// </summary>
    /// <remarks>
    /// It's a singleton which automatically creates a single <c>GameObject</c>
    /// "UTNotificationsManager" which is not destroyed on a scene loading.
    ///
    /// Usage:
    /// 1. If you would like to use not only local/scheduled notifications
    ///    but also push notifications please subscribe to event
    ///    <c>OnSendRegistrationId</c>. <seealso cref="Manager.OnSendRegistrationId"/>
    /// 2. Call <c>UTNotifications.Manager.Instance.Initialize</c> in order
    ///    to initialize the notifications system. <seealso cref="Manager.Initialize"/>
    /// 3. Now you can start using all the <c>UTNotifications.Manager</c> API
    ///    methods (see the methods description for further information)
    /// </remarks>
    public abstract class Manager : MonoBehaviour
    {
    //public
        /// <summary>
        /// This is how you access the only instance of the Manager.
        /// </summary>
        public static Manager Instance
        {
            get
            {
                InstanceRequired();
                return m_instance;
            }
        }

        /// <summary>
        /// <c>true</c> if the Manager is initialized.
        /// </summary>
        public bool Initialized
        {
            get
            {
                return m_initialized;
            }

            protected set
            {
                m_initialized = value;
                if (value && OnInitialized != null)
                {
                    OnInitialized();
                }
            }
        }

        /// <summary>
        /// Initializes the Manager. It's safe to call more than once.
        /// </summary>
        /// <param name="willHandleReceivedNotifications">Set to <c>true</c> to be able to handle received notifications. <seealso cref="OnNotificationsReceived"/></param>
        /// <param name="startId">ID of a first received push notification</param>
        /// <param name="incrementalId">If set to <c>true</c>, each received push notfication will have an ID = previous push notification ID + 1. Otherwise all received push notifications will have ID = <c>startId</c></param>
        /// <returns><c>true</c>, if initialized successfully, <c>false</c> otherwise.</returns>
        /// <remarks>
        /// Please set <c>willHandleReceivedNotifications</c> to <c>true</c> only if you will handle received notifications using <c>OnNotificationsReceived</c>. Otherwise, all the received notifications will be stored and never cleaned.
        /// If <c>incrementalId</c> is <c>false</c> (default value), then new push notifications will replace old ones on Android so only one push notification can be shown at a time.
        /// </remarks>
        public abstract bool Initialize(bool willHandleReceivedNotifications, int startId = 0, bool incrementalId = false);

        /// <summary>
        /// Posts a local notification at once (see remarks).
        /// </summary>
        /// <param name="title">The notification title.</param>
        /// <param name="text">The notification text.</param>
        /// <param name="id">The notification ID (any notifications with the same id will be replaced by this one). <seealso cref="CancelNotification"/> <seealso cref="HideNotification"/></param>
        /// <param name="userData">(Optional) A custom IDictionary<string, string> that can be received in <c>OnNotificationsReceived<c/>. <seealso cref="OnNotificationsReceived"/></param>
        /// <param name="notificationProfile">(Optional) The name of the notification profile (sound, icon and other notification attributes).</param>
        /// <param name="badgeNumber">(Optional) The badge number, to be displayed on top of application icon, when supported. Use -1 (default) to keep previous value, 0 to hide the badge.</param>
        /// <param name="buttons">(Optional) A collection of custom buttons (Android only, ignored on the rest platforms).</param>
        /// <remarks>
        /// Note that with the default settings you will not be able to see or hear any immediate notifications on any of the supported platforms, because notifications are not shown while
        /// the application is running by default. You can modify this behaviour in UTNotifications Settings:
        /// Common Android Settings -> Show Notifications & Windows Store Settings -> Notify only when app is closed or hidden.
        /// Unfortunately, iOS doesn’t allow to control it: you can never see any notifications while the app is running on iOS.
        /// </remarks>
        public abstract void PostLocalNotification(string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null, int badgeNumber = -1, ICollection<Button> buttons = null);

        /// <summary>
        /// Schedules a local notification.
        /// </summary>
        /// <param name="triggerInSeconds">Seconds value from now when the notification will be shown.</param>
        /// <param name="title">The notification title.</param>
        /// <param name="text">The notification text.</param>
        /// <param name="id">The notification ID (any notifications with the same id will be replaced by this one). <seealso cref="CancelNotification"/> <seealso cref="HideNotification"/></param>
        /// <param name="userData">(Optional) A custom IDictionary<string, string> that can be received in <c>OnNotificationsReceived<c/>. <seealso cref="OnNotificationsReceived"/></param>
        /// <param name="notificationProfile">(Optional) The name of the notification profile (sound, icon and other notification attributes).</param>
        /// <param name="badgeNumber">(Optional) The badge number, to be displayed on top of application icon, when supported. Use -1 (default) to keep previous value, 0 to hide the badge.</param>
        /// <param name="buttons">(Optional) A collection of custom buttons (Android only, ignored on the rest platforms).</param>
        public abstract void ScheduleNotification(int triggerInSeconds, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null, int badgeNumber = -1, ICollection<Button> buttons = null);

        /// <summary>
        /// Schedules a local notification.
        /// </summary>
        /// <param name="triggerDateTime">DateTime value when the notification will be shown.</param>
        /// <param name="title">The notification title.</param>
        /// <param name="text">The notification text.</param>
        /// <param name="id">The notification ID (any notifications with the same id will be replaced by this one). <seealso cref="CancelNotification"/> <seealso cref="HideNotification"/></param>
        /// <param name="userData">(Optional) A custom IDictionary<string, string> that can be received in <c>OnNotificationsReceived<c/>. <seealso cref="OnNotificationsReceived"/></param>
        /// <param name="notificationProfile">(Optional) The name of the notification profile (sound, icon and other notification attributes).</param>
        /// <param name="badgeNumber">(Optional) The badge number, to be displayed on top of application icon, when supported. Use -1 (default) to keep previous value, 0 to hide the badge.</param>
        /// <param name="buttons">(Optional) A collection of custom buttons (Android only, ignored on the rest platforms).</param>
        public void ScheduleNotification(DateTime triggerDateTime, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null, int badgeNumber = -1, ICollection<Button> buttons = null)
        {
            ScheduleNotification(TimeUtils.ToSecondsFromNow(triggerDateTime), title, text, id, userData, notificationProfile, badgeNumber, buttons);
        }

        /// <summary>
        /// Schedules a repeating local notification.
        /// </summary>
        /// <param name="firstTriggerInSeconds">Seconds value from now when the notification will be shown the first time.</param>
        /// <param name="intervalSeconds">Seconds between the notification shows (see remarks).</param>
        /// <param name="title">The notification title.</param>
        /// <param name="text">The notification text.</param>
        /// <param name="id">The notification ID (any notifications with the same id will be replaced by this one). <seealso cref="CancelNotification"/> <seealso cref="HideNotification"/></param>
        /// <param name="userData">(Optional) A custom IDictionary<string, string> that can be received in <c>OnNotificationsReceived<c/>. <seealso cref="OnNotificationsReceived"/></param>
        /// <param name="notificationProfile">(Optional) The name of the notification profile (sound, icon and other notification attributes).</param>
        /// <param name="badgeNumber">(Optional) The badge number, to be displayed on top of application icon, when supported. Use -1 (default) to keep previous value, 0 to hide the badge.</param>
        /// <param name="buttons">(Optional) A collection of custom buttons (Android only, ignored on the rest platforms).</param>
        /// <remarks> 
        /// Please note that the actual interval may be different.
        /// On iOS there are only fixed options like every minute, every day, every week and so on. So the provided <c>intervalSeconds</c> value will be approximated by one of the available options.
        /// </remarks>
        public abstract void ScheduleNotificationRepeating(int firstTriggerInSeconds, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null, int badgeNumber = -1, ICollection<Button> buttons = null);

        /// <summary>
        /// Schedules a repeating local notification.
        /// </summary>
        /// <param name="firstTriggerDateTime">DateTime value when the notification will be shown the first time.</param>
        /// <param name="intervalSeconds">Seconds between the notification shows (see remarks).</param>
        /// <param name="title">The notification title.</param>
        /// <param name="text">The notification text.</param>
        /// <param name="id">The notification ID (any notifications with the same id will be replaced by this one). <seealso cref="CancelNotification"/> <seealso cref="HideNotification"/></param>
        /// <param name="userData">(Optional) A custom IDictionary<string, string> that can be received in <c>OnNotificationsReceived<c/>. <seealso cref="OnNotificationsReceived"/></param>
        /// <param name="notificationProfile">(Optional) The name of the notification profile (sound, icon and other notification attributes).</param>
        /// <param name="badgeNumber">(Optional) The badge number, to be displayed on top of application icon, when supported. Use -1 (default) to keep previous value, 0 to hide the badge.</param>
        /// <param name="buttons">(Optional) A collection of custom buttons (Android only, ignored on the rest platforms).</param>
        /// <remarks> 
        /// Please note that the actual interval may be different.
        /// On iOS there are only fixed options like every minute, every day, every week and so on. So the provided <c>intervalSeconds</c> value will be approximated by one of the available options.
        /// </remarks>
        public void ScheduleNotificationRepeating(DateTime firstTriggerDateTime, int intervalSeconds, string title, string text, int id, IDictionary<string, string> userData = null, string notificationProfile = null, int badgeNumber = -1, ICollection<Button> buttons = null)
        {
            ScheduleNotificationRepeating(TimeUtils.ToSecondsFromNow(firstTriggerDateTime), intervalSeconds, title, text, id, userData, notificationProfile, badgeNumber, buttons);
        }

        /// <summary>
        /// Checks whether notifications are enabled.
        /// </summary>
        public abstract bool NotificationsEnabled();

        /// <summary>
        /// Checks whether notifications are allowed to be shown by your app in the OS settings/notifications permission system dialog.
        /// </summary>
        public abstract bool NotificationsAllowed();

        /// <summary>
        /// Enables or disables showing any notifications (doesn't affect the OS settings).
        /// </summary>
        /// <remarks> 
        /// Please note that enabling may change the registrationId so <c>OnSendRegistrationId</c> event will be called. <seealso cref="OnSendRegistrationId"/>
        /// </remarks>
        public abstract void SetNotificationsEnabled(bool enabled);

        /// <summary>
        /// Checks if push notifications are enabled on the given platform.
        /// </summary>
        /// <returns><c>true</c>, if notifications are enabled, <c>false</c> otherwise.</returns>
        public abstract bool PushNotificationsEnabled();

        /// <summary>
        /// Enables push notifications, if any are configured and supported on the given platform (doesn't affect the OS settings).
        /// </summary>
        /// <returns><c>true</c>, if push notifications were enabled, <c>false</c> otherwise.</returns>
        /// <remarks>
        /// When disabled invalidates the old registration id.
        /// Please note that <c>OnSendRegistrationId</c> event will be called if enabled providing a new registrationId. <seealso cref="OnSendRegistrationId"/>
        /// </remarks>
        public abstract bool SetPushNotificationsEnabled(bool enable);

        /// <summary>
        /// Cancels the notification with the specified ID (ignored if the specified notification is not found).
        /// </summary>
        /// <remarks> 
        /// If the specified notification is scheduled or repeating all the future shows will be also canceled.
        /// </remarks>
        public abstract void CancelNotification(int id);

        /// <summary>
        /// Hides the notification with the specified ID (ignored if the specified notification is not found).
        /// </summary>
        /// <remarks>
        /// If the specified notification is scheduled or repeating, all the future shows will remain scheduled. Not supported and will be ignored on Windows Store/Universal Windows Platform.
        /// </remarks>
        public abstract void HideNotification(int id);

        /// <summary>
        /// Cancels all the notifications. <seealso cref="CancelNotification"/>
        /// </summary>
        public abstract void CancelAllNotifications();

        /// <summary>
        /// Hides all the notifications. <seealso cref="HideNotification"/>
        /// </summary>
        /// <remarks>
        /// Not supported and will be ignored on Windows Store.
        /// </remarks>
        public abstract void HideAllNotifications();

        /// <summary>
        /// Returns the app icon badge value when supported and 0 otherwise.
        /// </summary>
        public abstract int GetBadge();

        /// <summary>
        /// Sets the app icon badge value when supported.
        /// </summary>
        public abstract void SetBadge(int bandgeNumber);

        public delegate void OnInitializedHandler();
        /// <summary>
        /// Occurs when the Manager get initialized.
        /// </summary>
        public event OnInitializedHandler OnInitialized;

        public delegate void OnSendRegistrationIdHandler(string providerName, string registrationId);
        /// <summary>
        /// Occurs when the registrationId for push notifications is received and should be sent to your server.
        /// </summary>
        /// <param name="providerName">A name of a push notifications provider. May be <c>"APNS"</c>, <c>"FCM"</c>, <c>"ADM"</c> or <c>"WNS"</c></param>
        /// <param name="registrationId">The received registrationId (encoded on iOS - see remarks)</param>
        public event OnSendRegistrationIdHandler OnSendRegistrationId;

        public delegate void OnNotificationClickedHandler(ReceivedNotification notification);
        /// <summary>
        /// Occurs when a user tapped/clicked on a notification or its button.
        /// </summary>
        /// <param name="notification">Clicked notification. <seealso cref="UTNotifications.ReceivedNotification"/>
        /// <remarks>
        /// Please subscribe to the event prior to calling <c>Initialize</c> to be able to handle a click on the notification which started the app.
        /// </remarks>
        public event OnNotificationClickedHandler OnNotificationClicked;

        public delegate void OnNotificationsReceivedHandler(IList<ReceivedNotification> receivedNotifications);
        /// <summary>
        /// Occurs when one or more local, scheduled or push notifications are received.
        /// </summary>
        /// <param name="receivedNotifications">A list of received <c>ReceivedNotification</c>s. <seealso cref="UTNotifications.ReceivedNotification"/>
        /// <remarks>
        /// iOS doesn't provide a list of all notifications shown when an app wasn't running in foreground. This list will contain only a notification, which was clicked and all notifications shown when an app is running in foreground.
        /// On the rest platforms you'll receive a list of all the shown notifications.
        /// </remarks>
        public event OnNotificationsReceivedHandler OnNotificationsReceived;

    //protected
        protected bool OnSendRegistrationIdHasSubscribers()
        {
            return OnSendRegistrationId != null;
        }

        protected void _OnSendRegistrationId(string providerName, string registrationId)
        {
            OnSendRegistrationId(providerName, registrationId);
        }

        protected bool OnNotificationClickedHasSubscribers()
        {
            return OnNotificationClicked != null;
        }
        
        protected void _OnNotificationClicked(ReceivedNotification notification)
        {
            OnNotificationClicked(notification);
        }

        protected bool OnNotificationsReceivedHasSubscribers()
        {
            return OnNotificationsReceived != null;
        }

        protected void _OnNotificationsReceived(IList<ReceivedNotification> receivedNotifications)
        {
            OnNotificationsReceived(receivedNotifications);
        }

        protected virtual void OnDestroy()
        {
            m_instance = null;
            m_destroyed = true;
        }

        protected void NotSupported(string feature = null)
        {
            if (feature == null)
            {
                Debug.LogWarning("UTNotifications: not supported on this platform");
            }
            else
            {
                Debug.LogWarning("UTNotifications: " + feature + " feature is not supported on this platform");
            }
        }

        protected bool CheckInitialized()
        {
            if (!m_initialized)
            {
                UnityEngine.Debug.LogError("Please call UTNotifications.Manager.Instance.Initialize(...) first!");
            }

            return m_initialized;
        }

    //private
        private static void InstanceRequired()
        {
            if (!m_instance && !m_destroyed)
            {
                GameObject gameObject = new GameObject("UTNotificationsManager");
                m_instance = gameObject.AddComponent<ManagerImpl>();
                DontDestroyOnLoad(gameObject);
            }
        }

        private static Manager m_instance = null;
        private static bool m_destroyed = false;

        private bool m_initialized = false;
    }
}