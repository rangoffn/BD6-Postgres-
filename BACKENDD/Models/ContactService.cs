using BACKENDD.Models;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Metrics;
using Prometheus;

public class ContactService : IContactService
{
    private readonly AppDbContext _context;
    private readonly TelemetryClient _telemetryClient;

    public ContactService(AppDbContext context, TelemetryClient telemetryClient)
    {
        _context = context;
        _telemetryClient = telemetryClient;
    }

    public async Task<bool> SaveContactAsync(Contact contact)
    {
        using (AppMetrics.ContactSaveDuration.NewTimer())
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            AppMetrics.ContactsSaved.Inc();
            return true;
        }
    }

    public List<Contact> GetAllContacts()
    {
        var count = _context.Contacts.Count();
        AppMetrics.ActiveContacts.Set(count);
        return _context.Contacts.OrderBy(c => c.Id).ToList();
    }
}