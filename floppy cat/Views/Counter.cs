using Avalonia.Controls;
using Avalonia.Controls.Shapes;

namespace floppy_cat.Views;

public class Counter
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
            MainWindow.instance.Count++;
            StaticData.canvas.Children.Remove(transform);
            UpdateHandler.updateEvent -= Update;
        }
    }
}