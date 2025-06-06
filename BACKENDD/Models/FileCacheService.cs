public class FileCacheService
{
    private readonly string _cachePath = Path.Combine(Directory.GetCurrentDirectory(), "Cache");

    public async Task SetAsync(string key, string data, TimeSpan lifetime)
    {
        var filePath = Path.Combine(_cachePath, key);
        await File.WriteAllTextAsync(filePath, data);

        // Удалить файл после истечения времени
        var deleteTime = DateTime.Now.Add(lifetime);
        var cleanupTask = new Timer(_ =>
        {
            if (File.Exists(filePath)) File.Delete(filePath);
        }, null, lifetime, Timeout.InfiniteTimeSpan);
    }

    public async Task<string> GetAsync(string key)
    {
        var filePath = Path.Combine(_cachePath, key);
        return File.Exists(filePath) ? await File.ReadAllTextAsync(filePath) : null;
    }
}