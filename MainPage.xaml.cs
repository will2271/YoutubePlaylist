using Newtonsoft.Json;
//using static Android.Provider.MediaStore.Audio;

namespace YoutubePlaylist;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }


    async Task<string> ProcessRepositoriesAsync(HttpClient client)
    {        
        var json = await client.GetStringAsync(
             "https://youtube.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=3&playlistId=PLruRi1CGkSSVlLlQCs9CNusYumuTWIL8z&fields=pageInfo%2C%20items%2Fsnippet(title%2C%20resourceId%2FvideoId)&key=AIzaSyAVpU_4x8B9dKtZZcEhWpli0YOfTACDzFM");

        playList playList = JsonConvert.DeserializeObject<playList>(json);
        return json;
    }

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        HttpClient client = new HttpClient();
        CounterBtn.Text = await ProcessRepositoriesAsync(client);
        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}

public class playList
{
    public List<item> items { get; set; }
}

[Serializable]
public class item
{
    public snippet snippet { get; set; }

}

[Serializable]
public class snippet
{
    public string title { get; set; }
    public resourceId resourceId { get; set; }
}

[Serializable]
public class resourceId
{
    public string kind { get; set; }
    public string videoId { get; set; }
}

