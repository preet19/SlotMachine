﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 	App name = Slot Machine
    Author's name = Dilpreet Singh
    Student	ID = 200306382
    App	Creation Date = 15/12/2016
    App description = This program IS used to build a slot machine
 */

namespace SlotMachine
{
    public partial class SlotMachineForm : Form
    {
        private int playerMoney = 1000;
        private int winnings = 0;
        private int jackpot = 5000;
        private float turn = 0.0f;
        private int playerBet = 0;
        private float winNumber = 0.0f;
        private float lossNumber = 0.0f;
        private string[] spinResult;
        private string fruits = "";
        private float winRatio = 0.0f;
        private float lossRatio = 0.0f;
        private int grapes = 0;
        private int bananas = 0;
        private int oranges = 0;
        private int cherries = 0;
        private int bars = 0;
        private int bells = 0;
        private int sevens = 0;
        private int blanks = 0;

        private Random random = new Random();

        public SlotMachineForm()
        {
            InitializeComponent();
        }

        /* Utility function to show Player Stats */
        private void showPlayerStats()
        {
            winRatio = winNumber / turn;
            lossRatio = lossNumber / turn;
            string stats = "";
            stats += ("Jackpot: " + jackpot + "\n");
            stats += ("Player Money: " + playerMoney + "\n");
            stats += ("Turn: " + turn + "\n");
            stats += ("Wins: " + winNumber + "\n");
            stats += ("Losses: " + lossNumber + "\n");
            stats += ("Win Ratio: " + (winRatio * 100) + "%\n");
            stats += ("Loss Ratio: " + (lossRatio * 100) + "%\n");
            MessageBox.Show(stats, "Player Stats");
        }

        /* Utility function to reset all fruit tallies*/
        private void resetFruitTally()
        {
            grapes = 0;
            bananas = 0;
            oranges = 0;
            cherries = 0;
            bars = 0;
            bells = 0;
            sevens = 0;
            blanks = 0;
        }

        /* Utility function to reset the player stats */
        private void resetAll()
        {
            playerMoney = 1000;
            winnings = 0;
            jackpot = 5000;
            turn = 0;
            playerBet = 0;
            winNumber = 0;
            lossNumber = 0;
            winRatio = 0.0f;
        }

        /* Check to see if the player won the jackpot */
        private void checkJackPot()
        {
            /* compare two random values */
            var jackPotTry = this.random.Next(51) + 1;
            var jackPotWin = this.random.Next(51) + 1;
            if (jackPotTry == jackPotWin)
            {
                MessageBox.Show("You Won the $" + jackpot + " Jackpot!!", "Jackpot!!");
                playerMoney += jackpot;
                jackpot = 1000;
            }
        }

        /* Utility function to show a win message and increase player money */
        private void showWinMessage()
        {
            playerMoney += winnings;
            MessageBox.Show("You Won: $" + winnings, "Winner!");
            resetFruitTally();
            checkJackPot();
        }

        /* Utility function to show a loss message and reduce player money */
        private void showLossMessage()
        {
            playerMoney -= playerBet;
            MessageBox.Show("You Lost!", "Loss!");
            resetFruitTally();
        }

        /* Utility function to check if a value falls within a range of bounds */
        private bool checkRange(int value, int lowerBounds, int upperBounds)
        {
            return (value >= lowerBounds && value <= upperBounds) ? true : false;

        }

        /* When this function is called it determines the betLine results.
    e.g. Bar - Orange - Banana */
        private string[] Reels()
        {
            string[] betLine = { " ", " ", " " };
            int[] outCome = { 0, 0, 0 };

            for (var spin = 0; spin < 3; spin++)
            {
                outCome[spin] = this.random.Next(65) + 1;

                if (checkRange(outCome[spin], 1, 27))
                {  // 41.5% probability
                    betLine[spin] = "blank";
                    blanks++;
                }
                else if (checkRange(outCome[spin], 28, 37))
                { // 15.4% probability
                    betLine[spin] = "Grapes";
                    grapes++;
                }
                else if (checkRange(outCome[spin], 38, 46))
                { // 13.8% probability
                    betLine[spin] = "Banana";
                    bananas++;
                }
                else if (checkRange(outCome[spin], 47, 54))
                { // 12.3% probability
                    betLine[spin] = "Orange";
                    oranges++;
                }
                else if (checkRange(outCome[spin], 55, 59))
                { //  7.7% probability
                    betLine[spin] = "Cherry";
                    cherries++;
                }
                else if (checkRange(outCome[spin], 60, 62))
                { //  4.6% probability
                    betLine[spin] = "Bar";
                    bars++;
                }
                else if (checkRange(outCome[spin], 63, 64))
                { //  3.1% probability
                    betLine[spin] = "Bell";
                    bells++;
                }
                else if (checkRange(outCome[spin], 65, 65))
                { //  1.5% probability
                    betLine[spin] = "Seven";
                    sevens++;
                }

            }
            return betLine;
        }

        /* This function calculates the player's winnings, if any */
        private void determineWinnings()
        {
            if (blanks == 0)
            {
                if (grapes == 3)
                {
                    winnings = playerBet * 10;
                    pictureBox1.Image = Properties.Resources.grapes;
                    pictureBox2.Image = Properties.Resources.grapes;
                    pictureBox3.Image = Properties.Resources.grapes;
                }
                else if (bananas == 3)
                {
                    winnings = playerBet * 20;
                    pictureBox1.Image = Properties.Resources.banana;
                    pictureBox2.Image = Properties.Resources.banana;
                    pictureBox3.Image = Properties.Resources.banana;
                }
                else if (oranges == 3)
                {
                    winnings = playerBet * 30;
                    pictureBox1.Image = Properties.Resources.orange;
                    pictureBox2.Image = Properties.Resources.orange;
                    pictureBox3.Image = Properties.Resources.orange;
                }
                else if (cherries == 3)
                {
                    winnings = playerBet * 40;
                    pictureBox1.Image = Properties.Resources.cherry;
                    pictureBox2.Image = Properties.Resources.cherry;
                    pictureBox3.Image = Properties.Resources.cherry;
                }
                else if (bars == 3)
                {
                    winnings = playerBet * 50;
                    pictureBox1.Image = Properties.Resources.bar;
                    pictureBox2.Image = Properties.Resources.bar;
                    pictureBox3.Image = Properties.Resources.bar;
                }
                else if (bells == 3)
                {
                    winnings = playerBet * 75;
                    pictureBox1.Image = Properties.Resources.bell;
                    pictureBox2.Image = Properties.Resources.bell;
                    pictureBox3.Image = Properties.Resources.bell;
                }
                else if (sevens == 3)
                {
                    winnings = playerBet * 100;
                    pictureBox1.Image = Properties.Resources.seven;
                    pictureBox2.Image = Properties.Resources.seven;
                    pictureBox3.Image = Properties.Resources.seven;
                }
                else if (grapes == 2)
                {
                    winnings = playerBet * 2;
                    pictureBox1.Image = Properties.Resources.grapes;
                    pictureBox2.Image = Properties.Resources.grapes;
                   
                }
                else if (bananas == 2)
                {
                    winnings = playerBet * 2;
                    pictureBox1.Image = Properties.Resources.banana;
                    pictureBox2.Image = Properties.Resources.banana;
                    
                }
                else if (oranges == 2)
                {
                    winnings = playerBet * 3;
                    pictureBox1.Image = Properties.Resources.orange;
                    pictureBox2.Image = Properties.Resources.orange;
                    
                }
                else if (cherries == 2)
                {
                    winnings = playerBet * 4;
                    pictureBox1.Image = Properties.Resources.cherry;
                    pictureBox2.Image = Properties.Resources.cherry;
                }
                else if (bars == 2)
                {
                    winnings = playerBet * 5;
                    pictureBox1.Image = Properties.Resources.bar;
                    pictureBox2.Image = Properties.Resources.bar;
                }
                else if (bells == 2)
                {
                    winnings = playerBet * 10;
                    
                    pictureBox1.Image = Properties.Resources.bell;
                    pictureBox2.Image = Properties.Resources.bell;
                }
                else if (sevens == 2)
                {
                    winnings = playerBet * 20;
                    pictureBox1.Image = Properties.Resources.seven;
                    pictureBox2.Image = Properties.Resources.seven;
                }
                else if (sevens == 1)
                {
                    winnings = playerBet * 5;
                    pictureBox1.Image = Properties.Resources.seven;
                }
                else if (grapes == 1)
                {
                    winnings = playerBet * 5;
                    pictureBox2.Image = Properties.Resources.grapes;
                }
                else if (bananas == 1)
                {
                    winnings = playerBet * 5;
                    pictureBox1.Image = Properties.Resources.banana;
                }

                else
                {
                    winnings = playerBet * 1;
                    
                }
                winNumber++;
                showWinMessage();
            }
            else
            {
                pictureBox1.Image = Properties.Resources.lose;
                pictureBox2.Image = Properties.Resources.lose;
                pictureBox3.Image = Properties.Resources.lose;
                lossNumber++;
                showLossMessage();
            }

        }

        private void SpinPictureBox_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.spin;
            pictureBox2.Image = Properties.Resources.spin;
            pictureBox3.Image = Properties.Resources.spin;


            if (Convert.ToInt32(textBox4.Text) > playerMoney)
            {
                SpinPictureBox.Enabled = false;
                MessageBox.Show("pleasepress reset button and start again");
            }

            else
            {
               
                playerBet = 10; // default bet amount

                if (playerMoney == 0)
                {
                    if (MessageBox.Show("You ran out of Money! \nDo you want to play again?", "Out of Money!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        resetAll();
                        showPlayerStats();
                    }
                }
                else if (playerBet > playerMoney)
                {
                    MessageBox.Show("You don't have enough Money to place that bet.", "Insufficient Funds");
                }
                else if (playerBet < 0)
                {
                    MessageBox.Show("All bets must be a positive $ amount.", "Incorrect Bet");
                }
                else if (playerBet <= playerMoney)
                {

                    spinResult = Reels();
                    fruits = spinResult[0] + " - " + spinResult[1] + " - " + spinResult[2];
                    MessageBox.Show(fruits);
                    determineWinnings();
                    turn++;
                    showPlayerStats();
                    textBox1.Text = playerMoney.ToString();
                    textBox2.Text = playerBet.ToString();
                    textBox3.Text = jackpot.ToString();

                }
                else
                {
                    MessageBox.Show("Please enter a valid bet amount");
                }

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            resetAll();
            SpinPictureBox.Enabled = true;
            textBox1.Text = playerMoney.ToString();
            textBox2.Text = playerBet.ToString();
            textBox3.Text = jackpot.ToString();
            MessageBox.Show("All stats have been reset");
        }

        private void quitGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SlotMachineForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("please enter something");
            }
            else if (textBox4.Text == "-")
            {
                MessageBox.Show("can't be negative");
            }
           else if (Convert.ToInt32 (textBox4.Text) > playerMoney )
            {
                MessageBox.Show("you don't have enough money. please enter a amount less then"+
                                playerMoney);
            }
            else if (Convert.ToInt32(textBox4.Text) <=0)
            {
                MessageBox.Show("please enter a valid bid");
            }
        }
    }

}
