using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1_Radio;

public class RadioStorageManager
{
    private static RadioStorageManager instance;
    public static RadioStorageManager Instance => instance ?? (instance = new RadioStorageManager());
    private List<string> favoriteIds = new();
    private ConcurrentQueue<string> lastStationsQueue = new();
    private const int maxHistoryCount = 10;
    private readonly string favoritesFilePath = "favorites.json";
    private readonly string historyFilePath = "history.json";

    private RadioStorageManager()
    {
        _= LoadAllDataAsync();
    }
    public async Task LoadAllDataAsync()
    {
        favoriteIds = await LoadListAsync(favoritesFilePath);
        var historyList = await LoadListAsync(historyFilePath);
        lastStationsQueue = new ConcurrentQueue<string>(historyList);
    }
    private async Task<List<string>> LoadListAsync (string path)
    {
        try
        {
            if(File.Exists(path))
            {
                using var reader = new StreamReader(path);
                {
                    string json = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<List<String>>(json) ?? new List<String>();
                }
                
            }
        }
        catch { }
        return new List<String>();

    }
    private async Task SaveListAsync(string path,List<string> list)
    {
        try
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            await File.WriteAllTextAsync(path, json);
        }
        catch { }
    }

    private async Task SaveFavoritesAsync()
    {
        await SaveListAsync(favoritesFilePath, favoriteIds);
    }
    private async Task SaveHistoryAsync()
    {
        var listToSave = lastStationsQueue.ToList();
        await SaveListAsync(historyFilePath, listToSave);
    }

    public List<string> GetFavorites => favoriteIds;
    public bool IsFavorite(string stationId) => favoriteIds.Contains(stationId);

    public async Task AddFavoriteAsync(string stationId)
    {
        if (!favoriteIds.Contains(stationId) && !string.IsNullOrEmpty(stationId))
        {
            favoriteIds.Add(stationId);
            await SaveFavoritesAsync();

        }
    }
    public async Task RemoveFavoriteAsync(string stationId)
    {
        if (favoriteIds.Contains(stationId) )
        {
            favoriteIds.Remove(stationId);
            await SaveFavoritesAsync();

        }
    }

    public List<string> GetLastStation() => lastStationsQueue.ToList();
    public async Task AddHistoryAsync(string stationId)
    {
        if (string.IsNullOrEmpty(stationId)) return;
        var list = lastStationsQueue.ToList();
        list.Remove(stationId);
        list.Add(stationId);
        while(list.Count > maxHistoryCount)
        {
            list.RemoveAt(0);

        }
        lastStationsQueue = new ConcurrentQueue<string>(list);
        await SaveHistoryAsync();
    }


}
