using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace floppy_cat.Views;

public class Obstacle
{
    private float speed = 1.5f;
    
    private Rectangle transform;
    
    public void Init(Rectangle transform)
    {
        this.transform = transform;
        UpdateHandler.updateEvent += Update;
    }
    
    public void Update()
    {
        Canvas.SetRight(transform, Canvas.GetRight(transform) + speed);

        if (transform.Bounds.Intersects(StaticData.floppy.collider.Bounds))
        {
            StaticData.floppy.collider.Fill = new SolidColorBrush(Colors.Red);
            UpdateHandler.updateEvent -= Update;
            UpdateHandler.timeScale = 0;
            UpdateHandler.end = true;
            EndScreen es = new EndScreen(MainWindow.instance.Count);
            es.Show();
        }
    }
}