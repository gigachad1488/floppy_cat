
using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace floppy_cat.Views;

public class ObstaclesSpawner
{
    private float spawnTimer = 1000f;
    
    private Canvas canvas;
    public void Init(Canvas canvas)
    {
        this.canvas = canvas;
        UpdateHandler.updateEvent += Update;
        Spawn();
    }
    public void Spawn()
    {
        Random random = new Random();
        float rh = random.Next(-50, 50);
        
        Rectangle rt = new Rectangle();
        rt.Fill = new SolidColorBrush(Colors.Red);
        rt.Height = 120 + rh;
        rt.Width = 30;
        Canvas.SetTop(rt, 0);
        Canvas.SetRight(rt, 20);
        Obstacle obt = new Obstacle();
        obt.Init(rt);
        
        Rectangle rb = new Rectangle();
        rb.Fill = new SolidColorBrush(Colors.Red);
        rb.Height = 120 - rh;
        rb.Width = 30;
        Canvas.SetBottom(rb, 0);
        Canvas.SetRight(rb, 20);
        Obstacle obb = new Obstacle();
        obb.Init(rb);
        
        Rectangle c = new Rectangle();
        c.Fill = new SolidColorBrush(Colors.Transparent);
        c.Height = 400;
        c.Width = 15;
        Canvas.SetBottom(c, 0);
        Canvas.SetRight(c, 10);
        Counter cc = new Counter();
        cc.Init(c);
        
        canvas.Children.Add(rt);
        canvas.Children.Add(rb);
        canvas.Children.Add(c);
        spawnTimer = 100f;
    }

    private void Update()
    {
        spawnTimer--;
        if (spawnTimer <= 0)
        {
            Spawn();
        }
    }
}