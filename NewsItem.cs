using System.Collections;
using Avalonia.Media.Imaging;

namespace jobEvent;

public class NewsItem
{
    public int Id { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string imagePath { get; set; }
    public string link { get; set; }
    public string date { get; set; }
    
    public Bitmap Image => new Bitmap("./Assets/icons.png");
    public string ShortDescription
    {
        get
        {
            const int maxLength = 100; // Максимальная длина описания
            if (description.Length > maxLength)
            {
                return description.Substring(0, maxLength) + "...";
            }
            return description;
        }
    }
}