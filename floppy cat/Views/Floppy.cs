using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Media.Transformation;
using Avalonia.Styling;

namespace floppy_cat.Views;

public class Floppy
{
    private float g = 2f;
    public Size size = new Size(40, 40);

    private double currentPositionY;
    

    public Rectangle collider;

    private Canvas canvas;
    private ObstaclesSpawner spawner;

    public string name;


    private float jumpForce = 10f;
    private float currentJumpForce = 0;
    private float jumpTimer = 0;
    
    private float currentRotation = 0;

    private bool jumping;
    
    public void Init(Canvas canvas, ObstaclesSpawner spawner)
    {
        this.canvas = canvas;
        this.spawner = spawner;

        Random r = new Random();
        name = "flop " + r.Next(0, 1000);

        collider = new Rectangle();
        collider.Width = size.Width;
        collider.Height = size.Height;
        collider.RadiusX = size.Width / 2;
        collider.RadiusY = size.Height / 2;
        collider.Classes.Add("flop");
        currentPositionY = 200;
        Canvas.SetBottom(collider, currentPositionY);
        Canvas.SetLeft(collider, 10);
        canvas.Children.Add(collider);       

        UpdateHandler.updateEvent += Update;
        StaticData.floppy = this;
    }
    
    public void Update()
    {
        currentPositionY -= g;     

        if (jumping)
        {
            currentPositionY += currentJumpForce;
            currentJumpForce *= 0.90f;
            jumpTimer--;
            Random random = new Random();
            currentRotation += (float)random.Next(8, 13);
            
            Style buttonStyle = new Style(x => x.OfType<Rectangle>())
            {
                Setters =
                {
                    new Setter
                    {
                        Property = Rectangle.RenderTransformProperty,
                        Value = TransformOperations.Parse($"rotate({currentRotation}deg)")
 
                    }
                }
            };
        
            collider.Styles.Add(buttonStyle);
            
            if (jumpTimer <= 0)
            {
                jumping = false;
            }         
        }

        Canvas.SetBottom(collider, currentPositionY);

        if (currentPositionY < -10|| currentPositionY > canvas.Bounds.Height + 10)
        {
            StaticData.floppy.collider.Fill = new SolidColorBrush(Colors.Red);
            UpdateHandler.updateEvent -= Update;
            UpdateHandler.timeScale = 0;
            UpdateHandler.end = true;
            EndScreen es = new EndScreen(MainWindow.instance.Count, spawner, this);
            es.Show();

            Random r = new Random();
            Debug.WriteLine("DED WTF" + Canvas.GetBottom(collider));
        }
    }

    public void Jump()
    {
        jumping = true;
        jumpTimer = 40f;
        currentJumpForce = jumpForce;
    }

    public void Delete()
    {
        canvas.Children.Remove(collider);
    }
}