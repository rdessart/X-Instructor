using System.IO;
using System.IO.Compression;
using System.Text.Json;
using XInstructor.Common.Models;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace XInstructor.Common.Services;

public class NavigraphService
{
    private HttpClient _downloadClient;
    private HttpClient _navigraphClient;
    public static string AppPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "X-Instructor");
    public static string DatabasePath = Path.Combine(AppPath, "database.db");

    public NavigraphService()
    {
        _downloadClient = new HttpClient()
        {
            BaseAddress = new Uri("http://download.euc1.navigraph.com"),
        };

        _navigraphClient = new HttpClient()
        {
            BaseAddress = new Uri("http://www.navigraph.com"),
        };
    }

    public async Task<string?> GetAirac()
    {
        var response = await _navigraphClient.GetAsync("/api/1/fmsdata/index");
        if (!response.IsSuccessStatusCode) return null;
        string content = await response.Content.ReadAsStringAsync();
        NavigraphAiracResult? result;
        try
        {
            result = JsonSerializer.Deserialize<NavigraphAiracResult>(content);
        }
        catch(JsonException ex)
        {
            Console.WriteLine(ex);
            return null;
        }
        if(result is null) return null;
        string[] items = result.Url.Substring(7).Split('/');
        return items[4];
    }

    public void CleanUpDirectory()
    {
        if (!Directory.Exists(AppPath))
            Directory.CreateDirectory(AppPath);

        foreach (string file in Directory.GetFiles(AppPath))
        {
            File.Delete(file);
        }
    }

    public async Task<bool> DownloadNavigationDatabase()
    {
        CleanUpDirectory();
        string downloadUrl = "/data/fmsdata/fmsclient/{0}/master_simtoolkitpro_{1}.zip";
        string? airac_long = await GetAirac();
        //var airac_long = "2401r1";

        if (airac_long is null) return false;

        downloadUrl = string.Format(downloadUrl, airac_long, airac_long.Substring(0, 4));
        Console.WriteLine(downloadUrl);

        var response = await _downloadClient.GetAsync(downloadUrl);

        if (!response.IsSuccessStatusCode) return false;

        var stream = await response.Content.ReadAsStreamAsync();

        ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Read);
        

        archive.ExtractToDirectory(AppPath);
        FileInfo? largestFile = null;
        foreach (string file in Directory.GetFiles(AppPath))
        {
            string fullpath = Path.Combine(AppPath, file);
            var fileinfo = new FileInfo(fullpath);
            Console.WriteLine($"{fullpath} - {fileinfo.Length} byte(s)");
            if ( largestFile is null || fileinfo.Length > largestFile.Length ) 
                largestFile = fileinfo;
        }
        if (File.Exists(DatabasePath))
            File.Delete(DatabasePath);

        largestFile?.MoveTo(DatabasePath);

        foreach (string file in Directory.GetFiles(AppPath))
        {
            if (!file.EndsWith("database.db"))
            {
                File.Delete(file);
            }
        }


        return true;
    }
}
