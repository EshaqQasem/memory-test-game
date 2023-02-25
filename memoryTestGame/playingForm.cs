using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace memoryTestGame
{

    public partial class playingForm : Form
    {
        public playingForm(mainForm mf, int lnum)
        {
            InitializeComponent();
            pMainForm = mf;
            currentLevel = pMainForm.levels[lnum];

        }

        mainForm pMainForm;
        Level currentLevel;

        Button[] Squares;
        Thread thread;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.InitializeGame();
        }




        void InitializeGame()
        {

            Squares = new Button[currentLevel.Order * currentLevel.Order];
            /*this.panSquares.Width = currentLevel.Order * 50;
            this.Width = this.panSquares.Width + 120;
            this.panSquares.Height = currentLevel.Order * 50;
            this.Height = this.panSquares.Height ;
            */
            for (int i = 0; i < currentLevel.Order * currentLevel.Order; i++)
            {
                Squares[i] = new Button();
                Squares[i].Size = new System.Drawing.Size(50, 50);
                Squares[i].Location = new Point(i % currentLevel.Order * Squares[i].Width, i / currentLevel.Order * Squares[i].Height);
                Squares[i].BackColor = Color.CadetBlue;
                Squares[i].Click += Squares_Click;
                Squares[i].Tag = i;
                this.panSquares.Controls.Add(Squares[i]);

            }

            pMainForm.lblResult.Visible = true;
            pMainForm.lblResult.Text = this.lblResult.Text = "0";
            result = 0;
            pressedSquares = 0;
            this.Text = "level " + currentLevel.levelNum.ToString();
          // panSquares.Enabled = false;
            thread = new Thread(this.startPlay);
            thread.Start();
           
          // panSquares.Enabled = true;

           
        }

        void startPlay()
        {
            
            setRandomIndexes(currentLevel.rememberedSquareNums);
            randomNums.ForEach(i => Squares[i].BackColor = Color.White);


            Thread.Sleep(3000);
            
            randomNums.ForEach(i => Squares[i].BackColor = Color.CadetBlue);


        }

        List<int> randomNums = new List<int>();
        Random r = new Random();
        void setRandomIndexes(int n)
        {
            randomNums.Clear();
            while (randomNums.Count < n)
            {

                int t = r.Next(0, currentLevel.Order * currentLevel.Order);

                if (!randomNums.Contains(t))
                {
                    randomNums.Add(t);
                }
            }

        }


        int result = 0;
        int pressedSquares = 0;
        private void Squares_Click(object sender, EventArgs e)
        {
            Button tmp = ((Button)sender);
            if (tmp.BackColor == Color.CadetBlue && !thread.IsAlive) // منع النقر على المربع الذي تم النقر عليه من قبل
            {
                pressedSquares++;

                if (randomNums.Contains((int)tmp.Tag))
                {
                    tmp.BackColor = Color.Green;
                    randomNums.Remove((int)tmp.Tag);
                    result++;
                }

                else
                {
                    tmp.BackColor = Color.Red;
                    System.Media.SystemSounds.Beep.Play();
                    result--;
                }

                pMainForm.lblResult.Text = this.lblResult.Text = result.ToString();

                if (result == currentLevel.rememberedSquareNums)
                {
                    int t = currentLevel.levelNum;
                    if (t < pMainForm.levels.Count)  // not to open not found Level //win the last Level
                    {
                        pMainForm.levels[t].Open();
                        pMainForm.LastOpenedLevelNum = currentLevel.levelNum+1;
                        DialogResult res = DialogResult.Yes;// MessageBox.Show("You Win! ,Do You Want To Countinue to Level " + (currentLevel.levelNum + 1), "", MessageBoxButtons.YesNo);
                        if (res == DialogResult.No)
                        {
                            this.Close();
                        }
                        else
                        {

                            currentLevel = pMainForm.levels[t];
                           
                            ((levelTitleVeiw)pMainForm.panLevelsVeiw.Controls[t]).Icon = levelTitleVeiw.unlockedIcon;
                           
                            foreach (Button b in Squares)
                                b.Dispose();


                            this.InitializeGame();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Congraslation! You have win All the Levels");
                        this.Close();
                    }
                }
                else if (pressedSquares == currentLevel.levelNum)
                {
                    
                    randomNums.ForEach(num => Squares[num].BackColor = Color.LightGreen);
                    if (currentLevel.levelNum != 1)
                    {

                        pMainForm.levels[currentLevel.levelNum - 1].Close();
                        pMainForm.LastOpenedLevelNum = currentLevel.levelNum-1;
                        ((levelTitleVeiw)pMainForm.panLevelsVeiw.Controls[currentLevel.levelNum - 1]).Icon = levelTitleVeiw.lockedIcon;

                        currentLevel = pMainForm.levels[currentLevel.levelNum - 2];
                        
                        MessageBox.Show("You lose ,You have to Go back to the prevous level");
                    }
                    else
                        MessageBox.Show("You lose ");
                    foreach (Button b in Squares)
                        b.Dispose();

                    this.InitializeGame();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void playingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            pMainForm.lblResult.Visible = false;
        }
    }
}
