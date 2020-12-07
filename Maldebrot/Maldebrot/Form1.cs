using System;
using System.Drawing;
using System.Windows.Forms;

namespace Maldebrot
{
  public partial class Form1 : Form
  {
    //смещение
    public double wx = 0, wy = 0;
    //скорость перемещения, степень приближения фрактала, скорость зума
    public double speed = 2, zoom = 2, zoomSpeed = 0.009;
    

    private void timer1_Tick(object sender, EventArgs e)
    {
      zoom -= zoomSpeed / zoom;
      CreateImage();
    }

    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Up)
      {
        wy -= speed * (5 - Math.Abs(zoom));

      }
      if (e.KeyCode == Keys.Down)
      {
        wy += speed * (5 - Math.Abs(zoom));
      }

      if (e.KeyCode == Keys.Left)
      {
        wx -= speed * (5 - Math.Abs(zoom));

      }
      if (e.KeyCode == Keys.Right)
      {
        wx += speed * (5 - Math.Abs(zoom));
      }
    }

    public Form1()
    {
      InitializeComponent();
    }
    private void Form1_Load(object sender, EventArgs e)
    {
      CreateImage();

    }

    public void CreateImage()
    {

      Bitmap frame = new Bitmap(pictureBox1.Width, pictureBox1.Height);

      frame = Library.Draw(wx, wy, pictureBox1.Width, pictureBox1.Height, zoom);

      pictureBox1.Image = frame;
      pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
    }

  }
}
