using System;
using System.IO;
using System.Windows.Forms;

namespace RobbersLanguage
{
    public partial class FormRobbersLanguage : Form
    {
        public FormRobbersLanguage()
        {
            InitializeComponent();
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            string line = "";

            string path = Application.StartupPath + @"\robbers.txt";
            StreamReader streamReader = new StreamReader(path);

            bool finished = false;

            while (!finished)
            {
                line = streamReader.ReadLine();

                if (line == null)
                {
                    finished = true;
                }
                else
                {
                    TxtResult.Text += ConvertToRobbersLanguage(line) + Environment.NewLine;
                }
            }
        }

        private string ConvertToRobbersLanguage(string text)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            string consonants = "bcdfghjklmnpqrstvwxyz";
            string vowels = "aeiou";
            string result = "";

            int closestBefore = 0;
            int closestAfter = 0;
            int distanceBefore = 0;
            int distanceAfter = 0;

            //Itterate through the entire string.
            for (int i = 0; i < text.Length; i++)
            {
                //Pull appart each letter.
                string letter = text.Substring(i, 1);

                //Check if letter is a consonant.
                if (consonants.IndexOf(letter) != -1)
                {
                    //Add the consonant to the string result.
                    result += letter;

                    //Itterate the alphabet from the possition of the letter towards the begining.
                    for (int j = alphabet.IndexOf(letter); j >= 0 ; j--)
                    {
                        //Find the closest vowel and calculate the distance.
                        if (vowels.IndexOf(alphabet.Substring(j, 1)) != -1)
                        {
                            closestBefore = j;
                            distanceBefore = alphabet.IndexOf(letter) - j;
                            break;
                        }
                    }

                    //Itterate the alphabet the position of the letter till the end.
                    for (int j = alphabet.IndexOf(letter); j < alphabet.Length ; j++)
                    {
                        //Find the closest vowel and calculate distance.
                        if (vowels.IndexOf(alphabet.Substring(j, 1)) != -1)
                        {
                            closestAfter = j;
                            distanceAfter = j - alphabet.IndexOf(letter);
                            break;
                        }
                    }

                    //Check which vowel has closer distance and add it to the result.
                    if (distanceBefore < distanceAfter || distanceBefore == distanceAfter)
                    {
                        result += alphabet.Substring(closestBefore, 1);
                    }
                    else
                    {
                        result += alphabet.Substring(closestAfter, 1);
                    }

                    
                    if (letter == "z")
                    {
                        result += letter;
                    }
                    else
                    {
                        //Itterate the alphabet from the position of the letter till the end and
                        //find the next consonant.
                        for (int j = alphabet.IndexOf(letter) + 1; j < alphabet.Length; j++)
                        {
                            if (consonants.IndexOf(alphabet.Substring(j, 1)) != -1)
                            {
                                result += alphabet.Substring(j, 1);
                                break;
                            }
                        }
                    }        
                }
                //If it's a vowel - add the letter to the result.
                else
                {
                    result += letter;
                }
            }
            return result;
        }
    }
}
