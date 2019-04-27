using System.Collections.Generic;

namespace UTNotifications
{
    /// <summary>
    /// Represents a received notification
    /// </summary>
    public class ReceivedNotification
    {
        public ReceivedNotification(string title, string text, int id, IDictionary<string, string> userData, string notificationProfile, int badgeNumber)
        {
            this.title = title;
            this.text = text;
            this.id = id;
            this.userData = userData;
            this.notificationProfile = notificationProfile;
            this.badgeNumber = badgeNumber;
        }

        /// <summary>
        /// The title.
        /// </summary>
        public readonly string                      title;

        /// <summary>
        /// The text.
        /// </summary>
        public readonly string                      text;

        /// <summary>
        /// The id.
        /// </summary>
        public readonly int                         id;

        /// <summary>
        /// The user data provided by you in <c>Manager.PostLocalNotification</c>, <c>Manager.ScheduleNotification</c> or <c>Manager.ScheduleNotificationRepeating</c>
        /// or by your server in a push notification payload.
        /// </summary>
        /// <remarks>
        /// When the ReceivedNotification is an argument of Manager.OnNotificationClicked event handler, stores the user data of the clicked notification button
        /// (if specified). When the notification itself is clicked (even when there are custom buttons), stores the user data of the notification itself.
        /// </remarks>
        public readonly IDictionary<string, string> userData;

        /// <summary>
        /// The name of the notification profile (sound, icon and other notification attributes).
        /// </summary>
        public readonly string                      notificationProfile;

        /// <summary>
        /// The badge number.
        /// </summary>
        public readonly int                         badgeNumber;
    }
}