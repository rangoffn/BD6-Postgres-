using Prometheus;

public static class AppMetrics
{
    public static readonly Counter ContactsSaved = Metrics
        .CreateCounter("contacts_saved_total", "Total number of contacts saved");

    public static readonly Gauge ActiveContacts = Metrics
        .CreateGauge("active_contacts_count", "Number of active contacts in database");

    public static readonly Histogram ContactSaveDuration = Metrics
        .CreateHistogram("contact_save_duration_seconds", "Duration of contact save operations");
}