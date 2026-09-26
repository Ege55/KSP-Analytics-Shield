using UnityEngine;
using UnityEngine.Analytics;

[KSPAddon(KSPAddon.Startup.Instantly, true)]
public sealed class KSPAnalyticsShield : MonoBehaviour
{
    private const float RecheckIntervalSeconds = 2f;
    private float nextCheck;
    private static bool hasLoggedStatus;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        EnforceDisabled("startup");
        nextCheck = Time.unscaledTime + RecheckIntervalSeconds;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            EnforceDisabled("focus");
        }
    }

    private void Update()
    {
        if (Time.unscaledTime < nextCheck)
        {
            return;
        }

        nextCheck = Time.unscaledTime + RecheckIntervalSeconds;
        EnforceDisabled("watchdog");
    }

    private static void EnforceDisabled(string reason)
    {
        bool changed = Analytics.initializeOnStartup || Analytics.enabled || Analytics.deviceStatsEnabled;
        Analytics.initializeOnStartup = false;
        Analytics.enabled = false;
        Analytics.deviceStatsEnabled = false;

        if (!hasLoggedStatus || changed)
        {
            string version = typeof(KSPAnalyticsShield).Assembly.GetName().Version.ToString();
            Debug.Log("[KSP Analytics Shield " + version + "] Unity Analytics disabled (" + reason
                + "). initializeOnStartup=" + Analytics.initializeOnStartup
                + ", enabled=" + Analytics.enabled
                + ", deviceStatsEnabled=" + Analytics.deviceStatsEnabled);
            hasLoggedStatus = true;
        }
    }
}
