using System;
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
    

    public Rectangle collider;


    private float jumpForce = 10f;
    private float currentJumpForce = 0;
    private float jumpTimer = 0;
    
    private float currentRotation = 0;

    private bool jumping;
    
    public void Init(Rectangle collider)
    {

        this.collider = collider;

        UpdateHandler.updateEvent += Update;
        StaticData.floppy = this;
    }
    
    public void Update()
    {
        Canvas.SetBottom(collider, Canvas.GetBottom(collider) - g);

        if (jumping)
        {
            Canvas.SetBottom(collider, Canvas.GetBottom(collider) + currentJumpForce);
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

        if (Canvas.GetBottom(collider) < -10  || Canvas.GetBottom(collider) > 350 )
        {
            StaticData.floppy.collider.Fill = new SolidColorBrush(Colors.Red);
            UpdateHandler.updateEvent -= Update;
            UpdateHandler.timeScale = 0;
            UpdateHandler.end = true;
            EndScreen es = new EndScreen(MainWindow.instance.Count);
            es.Show();
        }
    }

    public void Jump()
    {
        jumping = true;
        jumpTimer = 60f;
        currentJumpForce = jumpForce;
    }
}