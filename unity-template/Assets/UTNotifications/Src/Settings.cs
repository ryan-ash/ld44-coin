﻿#if !UNITY_WEBPLAYER && !UNITY_SAMSUNGTV

#if UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7 || UNITY_4_8 || UNITY_4_9
#define UNITY_4
#endif

using UnityEngine;
using System.IO;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UTNotifications
{
    /// <summary>
    /// UTNotifications settings. Edit in Unity Editor: <c>"Edit -> Project Settings -> UTNotifications"</c>
    /// </summary>
    public class Settings : ScriptableObject
    {
        //public
        public static Settings Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = Resources.Load(m_assetName) as Settings;
                    if (m_instance == null)
                    {
                        m_instance = CreateInstance<Settings>();
#if UNITY_EDITOR
                        EditorApplication.update += Update;
#endif
                    }
#if UNITY_EDITOR
                    else
                    {
                        m_instance.CheckAssetVersionUpdated();
                    }
#endif
                }

                return m_instance;
            }
        }

#if UNITY_EDITOR
        public static bool ExportMode
        {
            get
            {
                string[] args = System.Environment.GetCommandLineArgs();
                foreach (string arg in args)
                {
                    if (arg == "-gvh_disable")
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public static string FullPath(string assetsRelatedPath)
        {
            return Path.Combine(Application.dataPath, assetsRelatedPath);
        }

        public static string MainAndroidManifestFullPath
        {
            get
            {
                return FullPath("Plugins/Android/AndroidManifest.xml");
            }
        }

        public static string AssetsRelatedRootPath
        {
            get
            {
                return "UTNotifications";
            }
        }

        public static string PluginsFullPath
        {
            get
            {
                return FullPath("Plugins");
            }
        }

        public static string AssetsRelatedResourcesPath
        {
            get
            {
                return Path.Combine(AssetsRelatedRootPath, "Resources");
            }
        }

        public static string AssetsRelatedEditorPath
        {
            get
            {
                return Path.Combine(AssetsRelatedRootPath, "Editor");
            }
        }

        public static string AssetsRelatedDemoServerPath
        {
            get
            {
                return Path.Combine(AssetsRelatedEditorPath, "DemoServer");
            }
        }
#endif

        public const string Version = "1.7.3";

        public List<NotificationProfile> NotificationProfiles
        {
            get
            {
                if (m_notificationProfiles.Count == 0 || m_notificationProfiles[0].profileName != DEFAULT_PROFILE_NAME)
                {
                    m_notificationProfiles.Insert(0, new NotificationProfile() { profileName = DEFAULT_PROFILE_NAME });
                }

                return m_notificationProfiles;
            }
        }

        public string PushPayloadTitleFieldName
        {
            get
            {
                return m_pushPayloadTitleFieldName;
            }
            set
            {
                if (m_pushPayloadTitleFieldName != value)
                {
                    m_pushPayloadTitleFieldName = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public string PushPayloadTextFieldName
        {
            get
            {
                return m_pushPayloadTextFieldName;
            }
            set
            {
                if (m_pushPayloadTextFieldName != value)
                {
                    m_pushPayloadTextFieldName = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public string PushPayloadIdFieldName
        {
            get
            {
                return m_pushPayloadIdFieldName;
            }
            set
            {
                if (m_pushPayloadIdFieldName != value)
                {
                    m_pushPayloadIdFieldName = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public string PushPayloadUserDataParentFieldName
        {
            get
            {
                return m_pushPayloadUserDataParentFieldName;
            }
            set
            {
                if (m_pushPayloadUserDataParentFieldName != value)
                {
                    m_pushPayloadUserDataParentFieldName = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public string PushPayloadNotificationProfileFieldName
        {
            get
            {
                return m_pushPayloadNotificationProfileFieldName;
            }
            set
            {
                if (m_pushPayloadNotificationProfileFieldName != value)
                {
                    m_pushPayloadNotificationProfileFieldName = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public string PushPayloadBadgeFieldName
        {
            get
            {
                return m_pushPayloadBadgeFieldName;
            }
            set
            {
                if (m_pushPayloadBadgeFieldName != value)
                {
                    m_pushPayloadBadgeFieldName = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public string PushPayloadButtonsParentName
        {
            get
            {
                return m_pushPayloadButtonsParentName;
            }
            set
            {
                if (m_pushPayloadButtonsParentName != value)
                {
                    m_pushPayloadButtonsParentName = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

#if UNITY_EDITOR
        public delegate void OnLibVersionChangedHandler();
        public static event OnLibVersionChangedHandler OnLibVersionChanged;
#endif

        public string GooglePlayServicesLibVersion
        {
            get
            {
                return m_googlePlayServicesLibVersion;
            }
            set
            {
                if (m_googlePlayServicesLibVersion != value)
                {
                    m_googlePlayServicesLibVersion = value;
#if UNITY_EDITOR
                    Save();
                    if (OnLibVersionChanged != null)
                    {
                        OnLibVersionChanged();
                    }
#endif
                }
            }
        }

        public string AndroidSupportLibVersion
        {
            get
            {
                return m_androidSupportLibVersion;
            }
            set
            {
                if (m_androidSupportLibVersion != value)
                {
                    m_androidSupportLibVersion = value;
#if UNITY_EDITOR
                    Save();
                    if (OnLibVersionChanged != null)
                    {
                        OnLibVersionChanged();
                    }
#endif
                }
            }
        }

        public bool PushNotificationsEnabledIOS
        {
            get
            {
                return m_pushNotificationsEnabledIOS;
            }
            set
            {
                if (m_pushNotificationsEnabledIOS != value)
                {
                    m_pushNotificationsEnabledIOS = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public bool PushNotificationsEnabledFirebase
        {
            get
            {
                return m_pushNotificationsEnabledFirebase;
            }
            set
            {
                if (m_pushNotificationsEnabledFirebase != value)
                {
                    m_pushNotificationsEnabledFirebase = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }
        
        public bool PushNotificationsEnabledAmazon
        {
            get
            {
                return m_pushNotificationsEnabledAmazon;
            }
            set
            {
                if (m_pushNotificationsEnabledAmazon != value)
                {
                    m_pushNotificationsEnabledAmazon = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public bool PushNotificationsEnabledWindows
        {
            get
            {
                return m_pushNotificationsEnabledWindows;
            }
            set
            {
                if (m_pushNotificationsEnabledWindows != value)
                {
                    m_pushNotificationsEnabledWindows = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public ShowNotifications AndroidShowNotificationsMode
        {
            get
            {
                return m_androidShowNotificationsMode;
            }
            set
            {
                if (m_androidShowNotificationsMode != value)
                {
                    m_androidShowNotificationsMode = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public bool AndroidRestoreScheduledNotificationsAfterReboot
        {
            get
            {
                return m_androidRestoreScheduledNotificationsAfterReboot;
            }
#if UNITY_EDITOR
            set
            {
                if (m_androidRestoreScheduledNotificationsAfterReboot != value)
                {
                    m_androidRestoreScheduledNotificationsAfterReboot = value;
                    Save();
                }
            }
#endif
        }

        public NotificationsGroupingMode AndroidNotificationsGrouping
        {
            get
            {
                return m_androidNotificationsGrouping;
            }
            set
            {
                if (m_androidNotificationsGrouping != value)
                {
                    m_androidNotificationsGrouping = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

        public GooglePlayUpdatingIfRequiredMode AllowUpdatingGooglePlayIfRequired
        {
            get
            {
                return m_allowUpdatingGooglePlayIfRequired;
            }

            set
            {
                if (m_allowUpdatingGooglePlayIfRequired != value)
                {
                    m_allowUpdatingGooglePlayIfRequired = value;
#if UNITY_EDITOR
                    Save();
#endif
                }
            }
        }

		public bool AndroidShowLatestNotificationOnly
		{
			get
			{
				return m_androidShowLatestNotificationOnly;
			}

#if UNITY_EDITOR
			set
			{
				if (value != m_androidShowLatestNotificationOnly)
				{
					m_androidShowLatestNotificationOnly = value;
					Save();
				}
			}
#endif
		}

        public string FirebaseSenderID
        {
            get
            {
                return m_firebaseSenderID;
            }
#if UNITY_EDITOR
            set
            {
                if (m_firebaseSenderID != value)
                {
                    m_firebaseSenderID = value;
                    Save();
                }
            }
#endif
        }

#if UNITY_EDITOR
        public static string[] GetAndroidDebugSignatureMD5AndSHA256()
        {
#if UNITY_EDITOR_WIN
            string homeDir = System.Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            string javaHome = JavaHome;

            if (string.IsNullOrEmpty(javaHome))
            {
                string error = "<JDK path not found. Please make sure that JDK is installed>";
                Debug.LogError(error);
                return new string[] { error, error };
            }

            string keytool = javaHome + "\\bin\\keytool.exe";
#else
            string homeDir = System.Environment.GetEnvironmentVariable("HOME");
            string keytool = "keytool";
#endif

            string debugKeystore = Path.Combine(homeDir, ".android/debug.keystore");
            if (!File.Exists(debugKeystore))
            {
                string error = "<debug.keystore file not found. Please build an Android version at least once.>";
                Debug.LogError(error);
                return new string[] { error, error };
            }

            string keytoolOutput = RunCommand(keytool, "-list -v -alias androiddebugkey -storepass android -keystore \"" + debugKeystore + "\"");
            int indexMD5 = keytoolOutput.IndexOf("MD5:");
            int indexSHA256 = keytoolOutput.IndexOf("SHA256:");

            if (indexMD5 < 0 || indexSHA256 < 0)
            {
                string message = "<Unable to read \"debug.keystore\" file. Look http://stackoverflow.com/questions/8576732/there-is-no-debug-keystore-in-android-folder >";
                Debug.LogError(message + "\n" + keytoolOutput);
                return new string[]{message, message};
            }
            else
            {
                indexMD5 += 4;
                indexSHA256 += 7;
                while (char.IsWhiteSpace(keytoolOutput[indexMD5])) ++indexMD5;
                while (char.IsWhiteSpace(keytoolOutput[indexSHA256])) ++indexSHA256;

                return new string[] {
                    keytoolOutput.Substring(indexMD5, keytoolOutput.IndexOf('\n', indexMD5) - indexMD5),
                    keytoolOutput.Substring(indexSHA256, keytoolOutput.IndexOf('\n', indexSHA256) - indexSHA256)
                };
            }
        }

        public static string GetAmazonAPIKey()
        {
            string file = Path.Combine(PluginsFullPath, "Android/assets/api_key.txt");

            if (File.Exists(file))
            {
                return File.ReadAllText(file);
            }
            else
            {
                return "";
            }
        }

        public static void SetAmazonAPIKey(string value)
        {
            if (Settings.ExportMode)
            {
                return;
            }
            
            if (string.IsNullOrEmpty(value))
            {
                string file = Path.Combine(PluginsFullPath, "Android/assets/api_key.txt");
                
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            else
            {
                string dir = Path.Combine(PluginsFullPath, "Android/assets");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.WriteAllText(dir + "/api_key.txt", value);
            }

            AssetDatabase.Refresh();
        }
#endif

        public bool WindowsDontShowWhenRunning
        {
            get
            {
                return m_windowsDontShowWhenRunning;
            }

#if UNITY_EDITOR
            set
            {
                if (m_windowsDontShowWhenRunning != value)
                {
                    m_windowsDontShowWhenRunning = value;
                    Save();
                }
            }
#endif
        }

#if UNITY_EDITOR
        public string WindowsIdentityName
        {
            get
            {
#if !UNITY_4
                return PlayerSettings.WSA.packageName;
#else
                return PlayerSettings.Metro.packageName;
#endif
            }

            set
            {
#if !UNITY_4
                PlayerSettings.WSA.packageName = value;
#else
                PlayerSettings.Metro.packageName = value;
#endif
            }
        }

        public string WindowsCertificatePublisher
        {
            get
            {
#if !UNITY_4
                return PlayerSettings.WSA.certificateIssuer;
#else
                return PlayerSettings.Metro.certificateIssuer;
#endif
            }
        }

        public bool WindowsCertificateIsCorrect(string publisher)
        {
            //Correct certificate publisher format: 00E3DE9D-D280-4DAF-907B-9DC894310E32
            return System.Text.RegularExpressions.Regex.IsMatch(publisher, "^[A-F0-9]{8}-[A-F0-9]{4}-[A-F0-9]{4}-[A-F0-9]{4}-[A-F0-9]{12}$");
        }

        public const string WRONG_CERTIFICATE_MESSAGE = "Wrong Windows Store certificate! Please create right one in Unity platform settings or associate app with the store in Visual Studio in order to make push notifications work. For details please read the UTNotifications manual.";
#endif

#if UNITY_EDITOR
        public static string GetAndroidResourceLibFolder()
        {
            return Path.Combine(PluginsFullPath, "Android/UTNotificationsRes/");
        }

        public static string GetAndroidResourceFolder(string resType)
        {
            return Path.Combine(GetAndroidResourceLibFolder(), "res/" + resType);
        }

        public static string GetIOSResourceFolder()
        {
            return Path.Combine(PluginsFullPath, "iOS/Raw");
        }

		[MenuItem(m_settingsMenuItem)]
        public static void EditSettigns()
        {
            EditorPrefs.SetBool(m_shownEditorPrefKey, true);
            Selection.activeObject = Instance;
        }

        public void Save()
        {
            if (Settings.ExportMode)
            {
                return;
            }
            
            AndroidManifestManager.Update();
            EditorUtility.SetDirty(this);
        }
#endif

        public const string DEFAULT_PROFILE_NAME = "default";
        public const string DEFAULT_PROFILE_NAME_INTERNAL = "__default_profile";

        /// <summary>
        /// Stores attributes of a notification profile.
        /// </summary>
        [System.Serializable]
        public struct NotificationProfile
        {
            public string profileName;
            public string iosSound;
            public string androidChannelName;
            public string androidChannelDescription;
            public string androidIcon;
            public string androidLargeIcon;
            public string androidIcon5Plus;
            [UnityEngine.Serialization.FormerlySerializedAs("androidIconBGColorSpecified")]
            public bool colorSpecified;
            [UnityEngine.Serialization.FormerlySerializedAs("androidIconBGColor")]
            public Color androidColor;
            public string androidSound;
            public bool androidHighPriority;
        }

        public enum ShowNotifications
        {
            WHEN_CLOSED_OR_IN_BACKGROUND = 0,
            WHEN_CLOSED = 1,
            ALWAYS = 2
        }

        public enum NotificationsGroupingMode
        {
            /// <summary>
            /// Don't group
            /// </summary>
            NONE = 0,

            /// <summary>
            /// Group by notifications profiles
            /// </summary>
            BY_NOTIFICATION_PROFILES = 1,

            /// <summary>
            /// Use "notification_group" user data value as a grouping key
            /// </summary>
            FROM_USER_DATA = 2,

            /// <summary>
            /// All the app's notifications will belong to a single group
            /// </summary>
            ALL_IN_A_SINGLE_GROUP = 3
        }

        public enum GooglePlayUpdatingIfRequiredMode
        {
            /// <summary>
            /// Don't suggest updating.
            /// </summary>
            DISABLED = 0,

            /// <summary>
            /// If required, suggest updating only once.
            /// </summary>
            ONCE = 1,

            /// <summary>
            /// If required, suggest updating every time UTNotifications.Manager is initialized.
            /// </summary>
            EVERY_INITIALIZE = 2
        }

    //private
#if UNITY_EDITOR
        private static string RunCommand(string command, string args)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            
            string output = process.StandardOutput.ReadToEnd();
            
            process.WaitForExit();

            return output;
        }
#endif

        [SerializeField]
        private List<NotificationProfile> m_notificationProfiles = new List<NotificationProfile>();

        [SerializeField]
        private string m_pushPayloadTitleFieldName = "title";

        [SerializeField]
        private string m_pushPayloadTextFieldName = "text";

        [SerializeField]
        private string m_pushPayloadIdFieldName = "id";

        [SerializeField]
        private string m_pushPayloadUserDataParentFieldName = "";

        [SerializeField]
        private string m_pushPayloadNotificationProfileFieldName = "notification_profile";

        [SerializeField]
        private string m_pushPayloadBadgeFieldName = "badge_number";

        [SerializeField]
        private string m_pushPayloadButtonsParentName = "buttons";

        [SerializeField]
        private string m_googlePlayServicesLibVersion = "11.8.0+";

        [SerializeField]
        private string m_androidSupportLibVersion = "25.3.1+";

        [SerializeField]
        private ShowNotifications m_androidShowNotificationsMode = ShowNotifications.WHEN_CLOSED_OR_IN_BACKGROUND;

#pragma warning disable 414
        [SerializeField]
        private bool m_android4CompatibilityMode = false;
#pragma warning restore 414

        [SerializeField]
        private bool m_androidRestoreScheduledNotificationsAfterReboot = true;
        
        [SerializeField]
        private NotificationsGroupingMode m_androidNotificationsGrouping = NotificationsGroupingMode.NONE;

		[SerializeField]
		private bool m_androidShowLatestNotificationOnly = false;

        [SerializeField]
        private bool m_pushNotificationsEnabledIOS = false;

        [SerializeField]
        private bool m_pushNotificationsEnabledFirebase = false;

        [SerializeField]
        private bool m_pushNotificationsEnabledAmazon = false;

        [SerializeField]
        private bool m_pushNotificationsEnabledWindows = false;

        [SerializeField]
        private GooglePlayUpdatingIfRequiredMode m_allowUpdatingGooglePlayIfRequired = GooglePlayUpdatingIfRequiredMode.ONCE;

        [SerializeField]
        private string m_firebaseSenderID = "";

#pragma warning disable 414
        [SerializeField]
        private string m_assetVersionSaved = "";

        [SerializeField]
        private bool m_windowsDontShowWhenRunning = true;
#pragma warning restore 414

        private const string m_assetName = "UTNotificationsSettings";
        private const string m_settingsMenuItem = "Edit/Project Settings/UTNotifications";
        private static Settings m_instance;

#if UNITY_EDITOR
        private static void Update()
        {
            if (Settings.ExportMode)
            {
                return;
            }
            
            string resourcesFullPath = FullPath(AssetsRelatedResourcesPath);
            if (!Directory.Exists(resourcesFullPath))
            {
                Directory.CreateDirectory(resourcesFullPath);
                AssetDatabase.Refresh();
            }

            AssetDatabase.CreateAsset(m_instance, Path.Combine("Assets/" + AssetsRelatedResourcesPath, m_assetName + ".asset"));
            m_instance.SaveVersion();
            AssetDatabase.Refresh();

            EditorApplication.update -= Update;
        }

        private static string m_shownEditorPrefKey
        {
            get
            {
                if (m_shownEditorPrefKeyCached == null)
                {
                    m_shownEditorPrefKeyCached = "UTNotificationsSettingsShown." + PlayerSettings.productName;
                }

                return m_shownEditorPrefKeyCached;
            }
        }

        private static string m_shownEditorPrefKeyCached = null;

        [InitializeOnLoad]
        public class SettingsHelper : ScriptableObject
        {
            static SettingsHelper()
            {
                EditorApplication.update += Update;
            }

            private static void Update()
            {
                if (!EditorPrefs.GetBool(m_shownEditorPrefKey, false) &&
                    !File.Exists(Path.Combine(FullPath(AssetsRelatedResourcesPath), m_assetName + ".asset")))
                {
                    if (EditorUtility.DisplayDialog("UTNotifications", "Please configure UTNotifications.\nYou can always edit its settings in menu:\n" + m_settingsMenuItem, "Now", "Later"))
                    {
                        EditSettigns();
                    }
                }
                EditorPrefs.SetBool(m_shownEditorPrefKey, true);

                Instance.CheckOutdatedSettings();
                Instance.CheckAndroidPlugin();
                Instance.CheckWSAPlugin();

                EditorApplication.update -= Update;
            }
        }

        private void CheckOutdatedSettings()
        {
            CheckAndroid4Compatibility();
            CheckNotificationProfiles();
        }

        private void CheckAndroid4Compatibility()
        {
            if (m_android4CompatibilityMode)
            {
                EditorUtility.DisplayDialog("UTNotifications", "Android 4.4 Compatibility Mode option was automatically disabled as it's not supported by Android 6+.\nPlease configure the default notification profile and any other profiles to avoid pure white square notification icons on Android 5+.", "OK");
                m_android4CompatibilityMode = false;
                Save();
            }
        }

        private void CheckNotificationProfiles()
        {
            var profiles = NotificationProfiles;
            if (profiles != null)
            {
                if (!Directory.Exists(GetAndroidResourceLibFolder()))
                {
                    foreach (var it in profiles)
                    {
                        if (!string.IsNullOrEmpty(it.androidIcon) && !string.IsNullOrEmpty(it.androidIcon5Plus) && !string.IsNullOrEmpty(it.androidLargeIcon) && !string.IsNullOrEmpty(it.androidSound))
                        {
                            EditorUtility.DisplayDialog("UTNotifications", "Notification Profiles Android storage structure changed in UTNotifications 1.6.\nPlease configure all notification profiles once again!", "OK");

                            for (int i = 0; i < profiles.Count; ++i)
                            {
                                var profile = profiles[i];
                                profile.androidIcon = profile.androidIcon5Plus = profile.androidLargeIcon = profile.androidSound = string.Empty;
                                profiles[i] = profile;
                            }

                            Save();
                            return;
                        }
                    }
                }
            }
        }

        private void CheckAndroidPlugin()
        {
#if UNITY_ANDROID
            CheckAndroidManifest();
            CheckFacebookSDK();
            CheckResManifest();
            CheckProguard();
#endif
        }

        private void RemoveEclipseADTPrefix(string path, string extenstion)
        {
            string targetFileName = path + "/." + extenstion;
            if (!File.Exists(targetFileName))
            {
                string originalFileName = path + "/eclipse_adt." + extenstion;
                if (File.Exists(originalFileName))
                {
                    File.Copy(originalFileName, targetFileName);
                }
            }
        }

#if UNITY_ANDROID
        private void CheckAndroidManifest()
        {
            string manifestFile = Settings.MainAndroidManifestFullPath;
            if (!File.Exists(manifestFile) || File.ReadAllText(manifestFile).IndexOf("universal.tools.notifications") < 0)
            {
                AndroidManifestManager.Update();
            }
        }

        private void CheckFacebookSDK()
        {
			bool updateAssetDatabase = false;
			
            string facebookAndroidLibsPath = Path.Combine(Application.dataPath, "FacebookSDK/Plugins/Android/libs");

            if (Directory.Exists(facebookAndroidLibsPath))
            {
                string[] facebookSupportLibraryFiles = Directory.GetFiles(facebookAndroidLibsPath, "support-v4-*");

                if (facebookSupportLibraryFiles != null)
                {
                    foreach (string it in facebookSupportLibraryFiles)
                    {
						updateAssetDatabase = true;
                        Debug.Log("UTNotifications: deleting a duplicated Android library: " + it);
                        File.Delete(it);
                    }
                }
            }

			if (updateAssetDatabase)
			{
				UnityEditor.AssetDatabase.Refresh();
			}
        }

        private void CheckResManifest()
        {
            string resManifestFile = Path.Combine(GetAndroidResourceLibFolder(), "AndroidManifest.xml");
            if (File.Exists(resManifestFile) && !File.ReadAllText(resManifestFile).Contains("android:minSdkVersion"))
            {
                string defaultResManifestFile = Path.Combine(Settings.FullPath(Settings.AssetsRelatedEditorPath), "Android/Res/AndroidManifest.xml");
                File.Copy(defaultResManifestFile, resManifestFile, true);
            }
        }

        private void CheckProguard()
        {
#if UNITY_2017_1_OR_NEWER
            string proguardFileText;
            if (EditorUserBuildSettings.androidDebugMinification != AndroidMinification.None ||
                EditorUserBuildSettings.androidReleaseMinification != AndroidMinification.None)
            {
                string proguardFileName = Path.Combine(PluginsFullPath, "Android/proguard-user.txt");
                if (!File.Exists(proguardFileName))
                {
                    Debug.Log("UTNotifications: Enabling User Proguard File");

                    string proguardFileNameDisabled = proguardFileName + ".DISABLED";
                    if (File.Exists(proguardFileNameDisabled))
                    {
                        proguardFileText = File.ReadAllText(proguardFileNameDisabled);
                        File.Move(proguardFileNameDisabled, proguardFileName);
                    }
                    else
                    {
                        proguardFileText = "";
                    }
                }
                else
                {
                    proguardFileText = File.ReadAllText(proguardFileName);
                }

                string updatedProguardFileText = AddLineIfMissing(proguardFileText, "# UTNotifications Proguard Config (please do not edit)");
                updatedProguardFileText = AddLineIfMissing(updatedProguardFileText, "-keep public class universal.tools.notifications.* { *; }");
                updatedProguardFileText = AddLineIfMissing(updatedProguardFileText, "-dontwarn com.amazon.device.messaging.*");
                updatedProguardFileText = AddLineIfMissing(updatedProguardFileText, "-dontwarn universal.tools.notifications.AdmIntentService");
                if (updatedProguardFileText != proguardFileText)
                {
                    Debug.Log("UTNotifications: Adding Proguard exclusions");
                    File.WriteAllText(proguardFileName, updatedProguardFileText);
        
                    AssetDatabase.Refresh();
                }
            }
#endif
        }

        private static string AddLineIfMissing(string text, string line)
        {
            string result;
            if (!text.Contains(line))
            {
                result = text + "\n" + line;
            }
            else
            {
                result = text;
            }

            return result;
        }
#endif

        private void CheckWSAPlugin()
        {
            if (Settings.ExportMode)
            {
                return;
            }
        
            CheckWSADllPlatforms();
            CheckWSAUnprocessed();

            string metroPath = Path.Combine(PluginsFullPath, "Metro");
            string wsaPath = Path.Combine(PluginsFullPath, "WSA");
            string rightPath;
            string wrongPath;
#if !UNITY_4
            rightPath = wsaPath;
            wrongPath = metroPath;
#else
            rightPath = metroPath;
            wrongPath = wsaPath;
#endif

            if (Directory.Exists(wrongPath))
            {
                if (!Directory.Exists(rightPath))
                {
                    Directory.CreateDirectory(rightPath);
                }

                if (Directory.Exists(wrongPath + "/UTNotifications"))
                {
                    Directory.Move(wrongPath + "/UTNotifications", rightPath + "/UTNotifications");
                }

                string[] filesToMove = Directory.GetFiles(wrongPath, "UTNotifications.*");
                if (filesToMove != null && filesToMove.Length > 0)
                {
                    foreach (string file in filesToMove)
                    {
                        File.Move(file, rightPath + "/" + Path.GetFileName(file));
                    }
                }

                AssetDatabase.Refresh();
            }

            //Delete old bin folder
            if (Directory.Exists(rightPath + "/UTNotifications/bin"))
            {
                Directory.Delete(rightPath + "/UTNotifications/bin", true);
                AssetDatabase.Refresh();
            }
        }

        private void CheckWSADllPlatforms()
        {
#if !UNITY_4 && !UNITY_5_0 && !UNITY_5_1 && !UNITY_5_2
            string pluginPath = "Assets/Plugins/UTNotifications.dll";

            PluginImporter pluginImporter = ((PluginImporter)AssetImporter.GetAtPath(pluginPath));
            if (pluginImporter.GetCompatibleWithAnyPlatform() || pluginImporter.GetCompatibleWithPlatform(BuildTarget.WSAPlayer))
            {
                pluginImporter.SetCompatibleWithAnyPlatform(false);
                pluginImporter.SetCompatibleWithPlatform(BuildTarget.WSAPlayer, false);
                pluginImporter.SetCompatibleWithEditor(true);
                pluginImporter.SaveAndReimport();
                AssetDatabase.ImportAsset(pluginPath);
            }
#endif
        }

        private void CheckWSAUnprocessed()
        {
#if UNITY_4
            string dllName = "UTNotifications.dll";
            if (System.Array.IndexOf(PlayerSettings.Metro.unprocessedPlugins, dllName) < 0)
            {
                List<string> unprocessedPlugins = PlayerSettings.Metro.unprocessedPlugins != null ? new List<string>(PlayerSettings.Metro.unprocessedPlugins) : new List<string>();
                unprocessedPlugins.Add(dllName);
                PlayerSettings.Metro.unprocessedPlugins = unprocessedPlugins.ToArray();
            }
#endif
        }

#if UNITY_EDITOR
        private void CheckAssetVersionUpdated()
        {
            if (m_assetVersionSaved != Version)
            {
                string updateMessage = "";
                int savedVersionIndex = -1;
                for (int i = 0; i < m_assetBundleUpdateMessages.Length; ++i)
                {
                    if (m_assetBundleUpdateMessages[i].version == m_assetVersionSaved)
                    {
                        savedVersionIndex = i;
                        break;
                    }
                }

                for (int i = savedVersionIndex + 1; i < m_assetBundleUpdateMessages.Length; ++i)
                {
                    updateMessage += m_assetBundleUpdateMessages[i].text + "\n";
                }

                if (updateMessage.Length > 0)
                {
                    const string lastLine = "\nWould you like to open UTNotifications Settings?";
                    if (EditorUtility.DisplayDialog("UTNotifications has been updated to version " + Version, updateMessage + lastLine, "Yes", "No"))
                    {
                        EditSettigns();
                    }
                }

                SaveVersion();
            }
        }

        private void SaveVersion()
        {
            m_assetVersionSaved = Version;
            Save();
        }
#endif

        private bool MoveFilesStartingWith(string pathFrom, string pathTo, string startsWith)
        {
            bool moved = false;

            if (Directory.Exists(pathFrom))
            {
                if (Settings.ExportMode)
                {
                    return false;
                }
            
                string[] files = Directory.GetFiles(pathFrom, startsWith + "*");
                if (files != null)
                {
                    foreach (string file in files)
                    {
                        if (!Directory.Exists(pathTo))
                        {
                            Directory.CreateDirectory(pathTo);
                        }

                        string fullPathTo = pathTo + "/" + Path.GetFileName(file);
                        if (File.Exists(fullPathTo))
                        {
                            File.Delete(fullPathTo);
                        }
                        File.Move(file, fullPathTo);
                        moved = true;
                    }
                }
            }

            return moved;
        }
#endif

#if UNITY_EDITOR_WIN
        private static string m_javaHome;

        private static string JavaHome
        {
            get
            {
                string home = m_javaHome;
                if (home == null)
                {
                    home = System.Environment.GetEnvironmentVariable("JAVA_HOME");
                    if (string.IsNullOrEmpty(home) || !Directory.Exists(home))
                    {
                        home = CheckForJavaHome(Microsoft.Win32.Registry.CurrentUser);
                        if (home == null)
                        {
                            home = CheckForJavaHome(Microsoft.Win32.Registry.LocalMachine);
                        }
                    }
                    
                    if (home != null && !Directory.Exists(home))
                    {
                        home = null;
                    }
                    
                    m_javaHome = home;
                }
                
                return m_javaHome;
            }
        }
        
        private static string CheckForJavaHome(Microsoft.Win32.RegistryKey key)
        {
            using (Microsoft.Win32.RegistryKey subkey = key.OpenSubKey(@"SOFTWARE\JavaSoft\Java Development Kit"))
            {
                if (subkey == null)
                {
                    return null;
                }
                
                object value = subkey.GetValue("CurrentVersion", null, Microsoft.Win32.RegistryValueOptions.None);
                if (value != null)
                {
                    using (Microsoft.Win32.RegistryKey currentHomeKey = subkey.OpenSubKey(value.ToString()))
                    {
                        if (currentHomeKey == null)
                        {
                            return null;
                        }
                        
                        value = currentHomeKey.GetValue("JavaHome", null, Microsoft.Win32.RegistryValueOptions.None);
                        if (value != null)
                        {
                            return value.ToString();
                        }
                    }
                }
            }
            
            return null;
        }
#endif

        private class UpdateMessage
        {
            public UpdateMessage(string version, string text)
            {
                this.version = version;
                this.text = text;
            }
            
            public readonly string version;
            public readonly string text;
        }

#if UNITY_EDITOR
        private readonly UpdateMessage[] m_assetBundleUpdateMessages =
        {
            new UpdateMessage("1.6.1.1",
@"- Google Cloud Messaging was replaced with Firebase Cloud Messaging (requires configuring and updating UTNotifications settings, please refer to the manual section 'Migrating from GCM to FCM').
- Play Services Resolver has been updated: may become incompatible with outdated versions of some 3rd party plugins, but it's required to support latest versions of Android SDK.
- Push notifications providers names changed: GooglePlay -> FCM, Amazon -> ADM, iOS -> APNS, Windows -> WNS (important for future updates, as FCM can be used with both Android and iOS in future)."),
            new UpdateMessage("1.6.2",
@"- iOS local notifications clicks handling issue fixed.
- iOS: Automated turning on, if enabled, push notifications capability with Xcode 8 & Unity 5.
- Other fixes"),
            new UpdateMessage("1.6.3",
@"- Android: Google Play Services Resolver has been updated to support latest versions of Android SDK.
- Various minor fixes and improvements."),
            new UpdateMessage("1.7.0",
@"- (Breaking change!) Android, Windows Store/UWP: No need to URL-Encode push notifications payload anymore (see DemoServer)
- (Breaking change!) Minimal supported version of Android is now 4.1
- (Breaking change!) Base64 iOS tokens encoding is not supported anymore
- Android: Android O Notification Channels are fully supported and integrated with notification profiles (please update any existing profiles)
- Android: Custom notification buttons
- Android: High Priority/Heads-Up notifications (notification profile settings)
- Android: Small notification icon background color customization (notification profile settings)
- Android: UTNotifications.Manager.Instance.NotificationsAllowed() works correctly, like earlier in iOS
- Android: An option to open a URL instead of the app on a notification tap
- Android: An option to offer to update the system Google Play if the installed version is too old for FCM
- Android: Ignoring any misformatted push messages to support AppsFlyer-like services
- Android: Google Play Services Lib and Android Support Library versions can be specified in the asset settings
- Android: Automated patching of the Proguard config to make sure UTNotifications classes are not removed when minifying the .apk
- Android: (Fix) READ_PHONE_STATE permission is not requested anymore when notification profiles are configured
- iOS: A workaround for a rare iOS bug when local notifications could be duplicated
- iOS: HTTP/2 API in DemoServer
- ShortcutBadger and Google Play Services Resolver are updated to the latest versions
- A number of other fixes and improvements"),
            new UpdateMessage("1.7.1",
@"- (Fixed) Android 5: tap on a notification doesn't open the app, if it's minimized
- (Fixed) Android: PushNotificationsEnabled() fixed"),
            new UpdateMessage("1.7.2",
@"- New Sample UI
- Android: Notification Profile color setting is now also used for notification titles in Android 8+
- Android: Importing FCM-related native libraries only if FCM push notifications are enabled
- (Fixed) Android: Cloud build
- (Fixed) Android: Unicode Characters >10000 in local notifications
- (Fixed) Android: Couldn't open the application on a notification click in some devices
- Other fixes and improvements"),
        };
#endif
    }
}

#endif
