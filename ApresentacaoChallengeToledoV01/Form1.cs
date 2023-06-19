using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ApresentacaoChallengeToledoV01
{
    public partial class Form1 : Form
    {

        private Bitmap offScreenBitmap1;
        private Graphics offScreenGraphics = null;
        private int centralHorizontalLine;
        private int centralVerticalLine;
        //
        int count = 0;
        //
        int posicaoInicialVeiculo;
        int posicaoHorizontalVeiculo;
        int velocidade = 2;
        //
        bool continue_ = true;
        bool movimentoHorizontal = false;
        bool retornar = false;
        //
        Color corSemaforo1 = Color.Red;
        Color corSemaforo2 = Color.Red;
        Color corSemaforo3 = Color.Red;
        Color corSemaforo4 = Color.Red;
        //
        int paradas = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            centralHorizontalLine = (this.Width / 2) - 8;
            centralVerticalLine = (this.Height / 2) - 7;
            //
            offScreenBitmap1 = new Bitmap(this.Width, this.Height);
            offScreenGraphics = Graphics.FromImage(offScreenBitmap1);
            //
            posicaoInicialVeiculo = centralVerticalLine + 300;
            posicaoHorizontalVeiculo = 25;
        }

        private void DesenhaCenario()
        {
            //
            //DrawHorizontalLine(Color.White, 0, this.Width, centralVerticalLine);//X
            //DrawVerticalLine(Color.White, centralHorizontalLine, 0, this.Height);//Y
            //
            DrawRectangle(Color.Blue, (centralHorizontalLine - 250), (centralVerticalLine - 50) - 200, 200, 100, "GALPÃO 01");//GALPAO 1
            DrawRectangle(Color.Blue, (centralHorizontalLine + 250), (centralVerticalLine - 50) - 200, 200, 100, "GALPÃO 02");//GALPAO 2
            //
            DrawHorizontalLine(Color.Blue, (centralHorizontalLine - 150), (centralHorizontalLine + 150), centralVerticalLine - 250, 10);//LIGAÇÃO ENTRE OS GALPÕES
            DrawVerticalLine(Color.Blue, centralHorizontalLine, centralVerticalLine - 250, centralVerticalLine, 10);//
            //SEMÁFORO 01
            DrawFilledCircle(corSemaforo1, centralHorizontalLine - 90, (centralVerticalLine - 15) + 200, 30);
            DrawEllipse(Color.White, centralHorizontalLine - 90, (centralVerticalLine - 15) + 200, 30);
            //SEMÁFORO 02
            DrawFilledCircle(corSemaforo2, centralHorizontalLine - 90, (centralVerticalLine - 15), 30);
            DrawEllipse(Color.White, centralHorizontalLine - 90, (centralVerticalLine - 15), 30);
            //SEMÁFORO 03
            DrawFilledCircle(count < 1 ? Color.Red : Color.Green, centralHorizontalLine - 150, (centralVerticalLine - 15) - 335, 30);
            DrawEllipse(Color.White, centralHorizontalLine - 150, (centralVerticalLine - 15) - 335, 30);
            //SEMÁFORO 04
            DrawFilledCircle(corSemaforo4, centralHorizontalLine + 150, (centralVerticalLine - 15) - 335, 30);
            DrawEllipse(Color.White, centralHorizontalLine + 150, (centralVerticalLine - 15) - 335, 30);
            //
            DrawRectangle2(Color.Blue, (centralHorizontalLine + 0), (centralVerticalLine + 100), 100, 200, "BALANÇA ");//
            //
            DrawVerticalLine(Color.Blue, centralHorizontalLine, centralVerticalLine + 200, this.Height, 10);//
            //
            Veiculo();
            //
            this.Text = $"X : {posicaoHorizontalVeiculo} | Y : {posicaoInicialVeiculo}";
        }

        private void Veiculo()
        {
            GraphicsPath veiculo = new GraphicsPath();//VEICULO
            //
            if (posicaoInicialVeiculo > 97 && !retornar)
            {
                veiculo.AddPolygon(new PointF[] {
                new PointF(centralHorizontalLine, posicaoInicialVeiculo),
                new PointF(centralHorizontalLine - posicaoHorizontalVeiculo,  posicaoInicialVeiculo + 25),
                new PointF(centralHorizontalLine + posicaoHorizontalVeiculo,  posicaoInicialVeiculo + 25),
                });
            }
            else
            {
                if (!retornar)
                {
                    //
                    movimentoHorizontal = true;
                    //
                    veiculo.AddPolygon(new PointF[] {
                    new PointF((centralHorizontalLine) + posicaoHorizontalVeiculo, (posicaoInicialVeiculo + 14)),
                    new PointF((centralHorizontalLine - 25) + posicaoHorizontalVeiculo,  (posicaoInicialVeiculo + 14) - 25),
                    new PointF((centralHorizontalLine - 25) + posicaoHorizontalVeiculo,  (posicaoInicialVeiculo + 14) + 25),
                    });
                }
                else
                {
                    if (posicaoHorizontalVeiculo > -17 && posicaoHorizontalVeiculo > -15)
                    {
                        veiculo.AddPolygon(new PointF[] {
                        new PointF((centralHorizontalLine) + posicaoHorizontalVeiculo, (posicaoInicialVeiculo + 14)),
                        new PointF((centralHorizontalLine + 25) + posicaoHorizontalVeiculo,  (posicaoInicialVeiculo + 14) - 25),
                        new PointF((centralHorizontalLine + 25) + posicaoHorizontalVeiculo,  (posicaoInicialVeiculo + 14) + 25),
                        });
                    }
                    else
                    {
                        veiculo.AddPolygon(new PointF[] {
                        new PointF(centralHorizontalLine, (posicaoInicialVeiculo) + 40),
                        new PointF(centralHorizontalLine - 25, (posicaoInicialVeiculo - 25) + 40),
                        new PointF(centralHorizontalLine + 25, (posicaoInicialVeiculo - 25) + 40),
                        });
                    }
                }
            }
            offScreenGraphics.FillPath(Brushes.Red, veiculo);
            offScreenGraphics.DrawPath(new Pen(new SolidBrush(Color.White), 1), veiculo);
        }

        private void DrawHorizontalLine(Color color, int x1, int x2, int y, int size = 1)
        {
            offScreenGraphics.DrawLine(new Pen(new SolidBrush(color), size), x1, y, x2, y);
        }

        private void DrawVerticalLine(Color color, int x, int y1, int y2, int size = 1)
        {
            offScreenGraphics.DrawLine(new Pen(new SolidBrush(color), size), x, y1, x, y2);
        }

        private void DrawRectangle(Color color, int x, int y, int width, int height, string text = "", int size = 1)
        {
            Rectangle rectangle = new Rectangle(x - (width / 2), y - (height / 2), width, height);
            offScreenGraphics.DrawRectangle(new Pen(new SolidBrush(color), size), rectangle);
            //
            Font font = new Font("Arial Black", 12, FontStyle.Bold);
            offScreenGraphics.DrawString(text, font, new SolidBrush(Color.White), x - 51, y - 12);
        }

        private void DrawRectangle2(Color color, int x, int y, int width, int height, string text = "", int size = 1)
        {
            Rectangle rectangle = new Rectangle(x - (width / 2), y - (height / 2), width, height);
            offScreenGraphics.DrawRectangle(new Pen(new SolidBrush(color), size), rectangle);
            //
            Font font = new Font("Arial Black", 12, FontStyle.Bold);
            offScreenGraphics.DrawString(text, font, new SolidBrush(Color.White), x - 44, y - 12);
        }

        private void DrawFilledCircle(Color color, int x, int y, int circleSize)
        {
            Rectangle rectangle = new Rectangle((x - (circleSize / 2)), y, circleSize, circleSize);
            offScreenGraphics.FillEllipse(new SolidBrush(color), rectangle);
        }

        private void DrawEllipse(Color color, int x, int y, int circleSize)
        {
            Rectangle rectangle = new Rectangle((x - (circleSize / 2)), y, circleSize, circleSize);
            offScreenGraphics.DrawEllipse(new Pen(new SolidBrush(color), 2), rectangle);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (posicaoInicialVeiculo == 561 || posicaoInicialVeiculo == 420 || posicaoInicialVeiculo == 97)
            {
                continue_ = false;
                if (paradas == 1)
                {
                    corSemaforo1 = Color.Red;
                }
            }
            //
            if (posicaoInicialVeiculo == 325)
            {
                corSemaforo2 = Color.Red;
            }
            //
            if (continue_)
            {
                posicaoInicialVeiculo -= velocidade;
            }
            //
            if (movimentoHorizontal && !retornar)
            {
                posicaoHorizontalVeiculo += velocidade;
                if (posicaoHorizontalVeiculo == 151 || posicaoHorizontalVeiculo == 188)
                {
                    posicaoHorizontalVeiculo -= velocidade;
                    if (posicaoHorizontalVeiculo == 186)
                    {
                        corSemaforo4 = Color.Red;
                    }
                }
            }
            //
            if (retornar)
            {
                posicaoHorizontalVeiculo -= velocidade;
                if (posicaoHorizontalVeiculo == 117)
                {
                    corSemaforo4 = Color.Red;
                }
                if (posicaoHorizontalVeiculo == -17)
                {
                    posicaoHorizontalVeiculo += velocidade;
                }
                //
                if (posicaoHorizontalVeiculo == -15)
                {
                    posicaoInicialVeiculo += velocidade;
                    if (posicaoInicialVeiculo == 321 || posicaoInicialVeiculo == 472 || posicaoInicialVeiculo == 657)
                    {
                        posicaoInicialVeiculo -= velocidade;
                        if (posicaoInicialVeiculo == 470)
                        {
                            corSemaforo2 = Color.Red;
                        }
                    }
                    //
                    if (posicaoInicialVeiculo == 571)
                    {
                        corSemaforo1 = Color.Red;
                    }
                }
            }
            //
            DesenhaCenario();
            //
            using (var graphics = this.CreateGraphics())
            {
                // copia o bitmap auxiliar para a tela
                graphics.DrawImage(offScreenBitmap1, 0, 0);
            }
            //
            offScreenGraphics.Clear(Color.Black);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var tecla = e.KeyChar;
            if (tecla.Equals('y'))
            {
                switch (paradas)
                {
                    case 0:
                        continue_ = true;
                        posicaoInicialVeiculo++;
                        corSemaforo1 = Color.Green;
                        paradas++;
                        break;
                    case 1:
                        continue_ = true;
                        posicaoInicialVeiculo++;
                        corSemaforo2 = Color.Green;
                        paradas++;
                        break;
                    case 2:
                        posicaoHorizontalVeiculo++;
                        corSemaforo4 = Color.Green;
                        paradas++;
                        break;
                    case 3:
                        retornar = true;
                        posicaoHorizontalVeiculo--;
                        corSemaforo4 = Color.Green;
                        paradas++;
                        break;
                    case 4:
                        posicaoInicialVeiculo++;
                        corSemaforo2 = Color.Green;
                        paradas++;
                        break;
                    case 5:
                        posicaoInicialVeiculo++;
                        corSemaforo1 = Color.Green;
                        paradas++;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
