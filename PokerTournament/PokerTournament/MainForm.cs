﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PokerTournament
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        string path = @"Players.txt";

        private void newPlayerForm_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        //Save+close button
        private void saveBtn_Click_1(object sender, EventArgs e)
        {

            List<Person> players = new List<Person>();

            players = GetPlayers();

            //create a new person object
            Person newPerson = new Person(firstNameBox.Text, lastNameBox.Text, Convert.ToInt32(ssnBox.Text));

            for (int q = 0; q < players.Count; ++q)
            {
                if (players[q].Equals(newPerson))
                {
                    MessageBox.Show("That employee already exists\nThe existing employee has been updated", "Duplicate employee message");
                    players.Remove(players[q]);
                }
            }
            players.Add(newPerson);

            SavePlayers(players);

            //Confirm that the data was saved, and close the window
            MessageBox.Show("The player has been saved, thank you!", "Save Confirmation");
            Form.ActiveForm.Close();

        }

        List<Person> GetPlayers()
        {
            // This method gets all of the saved players or creates a file to store them if it does not exiat
            FileStream infile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            List<Person> players = new List<Person>();
            BinaryFormatter bformatter = new BinaryFormatter();
            while (infile.Position < infile.Length)
            {               
                Person newPerson = (Person)bformatter.Deserialize(infile);
                players.Add(newPerson);
            }

            infile.Close();

            return players;
        }

        void SavePlayers(List<Person> players)
        {
            FileStream outfile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

            // The file is deleted with this next statement
            outfile.SetLength(0);

            BinaryFormatter bformatter = new BinaryFormatter();

            // The updated list of employees is written to the file
            foreach (Person x in players)
            {
                bformatter.Serialize(outfile, x);
            }

            outfile.Close();
        }
        //cancel button closes form
        private void cancelBtn_Click_1(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<Person> searchPlayers = new List<Person>();

            searchPlayers = GetPlayers();

            for(int x = 0; x < searchPlayers.Count; ++x)
            {
                if(Convert.ToInt32(ssnSearchForTextBox.Text) == searchPlayers[x].ssn)
                {
                    txtWeek1.Text = searchPlayers[x].Winnings.Weeks[0].Winning.ToString();
                    txtWeek2.Text = searchPlayers[x].Winnings.Weeks[1].Winning.ToString();
                    txtWeek3.Text = searchPlayers[x].Winnings.Weeks[2].Winning.ToString();
                    txtWeek4.Text = searchPlayers[x].Winnings.Weeks[3].Winning.ToString();
                    txtWeek5.Text = searchPlayers[x].Winnings.Weeks[4].Winning.ToString();
                    txtWeek6.Text = searchPlayers[x].Winnings.Weeks[5].Winning.ToString();
                    txtWeek7.Text = searchPlayers[x].Winnings.Weeks[6].Winning.ToString();
                    txtWeek8.Text = searchPlayers[x].Winnings.Weeks[7].Winning.ToString();
                }
                else
                {
                    MessageBox.Show("No players with the SSN exist", "Player not found");
                }

                ssnSearchForTextBox.Clear();
            }
        }
    }
}
