
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace floppy_cat.Views;

public class ObstaclesSpawner
{
    public float spawnTime = 100f;
    public float spawnTimer = 0f;
    
    public Canvas canvas;

    public float rightSpawnOffset = -45f;

    public List<Obstacle> obstacles = new List<Obstacle>();
    public List<Counter> counters = new List<Counter>();
    public void Init(Canvas canvas)
    {
        this.canvas = canvas;
        UpdateHandler.updateEvent += Update;
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
        Canvas.SetRight(rt, rightSpawnOffset);
        Obstacle obt = new Obstacle();
        obt.Init(rt, this);
        
        Rectangle rb = new Rectangle();
        rb.Fill = new SolidColorBrush(Colors.Red);
        rb.Height = 120 - rh;
        rb.Width = 30;
        Canvas.SetBottom(rb, 0);
        Canvas.SetRight(rb, rightSpawnOffset);
        Obstacle obb = new Obstacle();
        obb.Init(rb, this);
        
        Rectangle c = new Rectangle();
        c.Height = canvas.Bounds.Height;
        c.Width = 15;
        Canvas.SetBottom(c, 0);
        Canvas.SetRight(c, rightSpawnOffset);
        Counter cc = new Counter();
        cc.Init(c, this);
        
        canvas.Children.Add(rt);
        canvas.Children.Add(rb);
        canvas.Children.Add(c);

        obstacles.Add(obt);
        obstacles.Add(obb);
        counters.Add(cc);

        spawnTimer = spawnTime;
    }

    private void Update()
    {
        spawnTimer--;
        if (spawnTimer <= 0)
        {
            Spawn();
        }
    }

    public void DeleteAll()
    {
        for (int i = 0; i < obstacles.Count; i++)
        {
            obstacles[i].Delete();
            obstacles[i] = null;
        }
        
        for (int i = 0; i < counters.Count; i++)
        {          
            counters[i].Delete();
        }

        obstacles.Clear();
        counters.Clear();

    }
}