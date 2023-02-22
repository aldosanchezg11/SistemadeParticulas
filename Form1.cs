using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SistemadeParticulas
{
    public partial class Form1 : Form
    {
        private List<Particle> particles = new List<Particle>();
        List<Particle> sparks = new List<Particle>();
        //float windSpeed = 2.0f;
        //PointF windDirection = new PointF(1, 0);
        Bitmap bmp;
        //private Stopwatch stopwatch;
        //private readonly PointF Center;

        //windDirection = PointF.Normalize(windDirection);

        public Form1()
        {
            InitializeComponent();
            //stopwatch = new Stopwatch();
            //stopwatch.Restart();
            PointF center = new PointF(pictureBox1.Width / 2.0f, pictureBox1.Height / 2.0f);
            //Create Fire Particle
            Random Ran = new Random();
            for (int i = 0; i < 100; i++)
            {
                particles.Add(new Particle
                {
                    Position = new PointF(pictureBox1.Width / 2, pictureBox1.Height),
                    Velocity = new PointF((float)Ran.NextDouble() * 2 - 1, (float)Ran.NextDouble() * -4),
                    color = Color.FromArgb(255, 255, 128, 0)
                });
            }
            //Create Sparks
            for (int i = 0; i < 50; i++)
            {
                Particle spark = new Particle();
                spark.Type = ParticleType.Spark;
                spark.Position = new PointF(pictureBox1.Width / 2, pictureBox1.Height);
                spark.Velocity = new PointF(Ran.Next(-20, 21) / 10.0f, -Ran.Next(20, 31) / 10.0f);
                spark.color = Color.FromArgb(255, 255, Ran.Next(180, 256), 0);
                spark.Size = 8.0f;
                spark.Alpha = 255;
                sparks.Add(spark);

                
            }
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random Ran = new Random();
            //float elapsedTime = (float)stopwatch.ElapsedMilliseconds / 1000.0f;
            //stopwatch.Restart();
            Bitmap bmp =new Bitmap(pictureBox1.Width, pictureBox1.Height);

            foreach (Particle particle in particles)
            {
                particle.Position = new PointF(particle.Position.X + particle.Velocity.X, particle.Position.Y + particle.Velocity.Y);
                particle.Velocity = new PointF(particle.Velocity.X + (float)Ran.NextDouble() * 0.2f - 0.1f, particle.Velocity.Y + 0.1f);
                particle.Size = (float)Ran.Next(10, 20);
                particle.Alpha = Ran.Next(50, 256);
                //float distance = PointF.Distance(particle.Position, Center);
                //if (particle.Type == ParticleType.Fire)
                //{
                //    PointF windVelocity = new PointF(windDirection.X * windSpeed * elapsedTime, windDirection.Y * windSpeed * elapsedTime);
                //    particle.Velocity = new PointF(particle.Velocity.X + windVelocity.X, particle.Velocity.Y + windVelocity.Y);
                //}
            }
            //Render Particles
            using (Graphics g = Graphics.FromImage(bmp)) 
            {
                g.Clear(Color.Black);
                foreach (Particle particle in particles)
                {
                    Color particleColor = Color.FromArgb(particle.Alpha, particle.color.R, particle.color.G, particle.color.B);
                    g.FillEllipse(new SolidBrush(particleColor), particle.Position.X, particle.Position.Y, particle.Size, particle.Size);
                }
            }

            using (Graphics g = Graphics.FromImage(bmp))
            {
                foreach (Particle spark in sparks)
                {
                    spark.Position = new PointF(spark.Position.X + spark.Velocity.X, spark.Position.Y + spark.Velocity.Y);
                    spark.Size = Math.Max(0, spark.Size - 0.05f);
                    spark.Alpha = (int)(spark.Alpha * 0.95);

                    if (spark.Alpha <= 0 || spark.Size <= 0)
                    {
                        spark.Alpha = 0;
                        spark.Size = 0;
                        continue;
                    }
                    g.FillEllipse(new SolidBrush(Color.FromArgb(spark.Alpha, spark.color)), spark.Position.X, spark.Position.Y, spark.Size, spark.Size);
                }
            }
            pictureBox1.Image = bmp;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //int arrowLength = 50;
            //int arrowWidth = 10;
            //Pen pen = new Pen(Color.White, 2.0f);
            //PointF arrowEnd = new PointF(windDirection.X * arrowLength + pictureBox1.Width / 2, pictureBox1.Height / 2);
            //PointF[] arrowPoints = new PointF[]
            //{
            //    new PointF(arrowEnd.X - arrowWidth, arrowEnd.Y - arrowWidth),
            //    new PointF(arrowEnd.X, arrowEnd.Y),
            //    new PointF(arrowEnd.X - arrowWidth, arrowEnd.Y + arrowWidth)
            //};
            //g.DrawLine(pen, pictureBox1.Width / 2, pictureBox1.Height / 2, arrowEnd.X, arrowEnd.Y);
            //g.DrawPolygon(pen, arrowPoints);
        }
    }
}