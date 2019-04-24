/*312639 Hangman
Cameron Heinz
Want to play a game?
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _312639_Hangman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] incorrectGuessed = new string[5];
        string[] word = new string[10];
        string[] correctGuessed = new string[10];
        int RNG = 0;
        int counter = 5;
        int winnerCheck = 0;
        string wordUsed = null;
        string wordrecreated = null;
        string guessedLetter = null;
        string lblWrong = null;
        string line = null;
        

        Random random = new Random();
        System.IO.StreamReader streamReader = new System.IO.StreamReader("words.txt");
        public MainWindow()
        {
            InitializeComponent();

            Rectangle a = new Rectangle();
            a.Height = 300;
            a.Width = 10;
            myCanvas.Children.Add(a);
            a.Fill = Brushes.Black;
            Canvas.SetLeft(a, 700);
            Canvas.SetTop(a, 50);

            Rectangle b = new Rectangle();
            b.Height = 10;
            b.Width = 150;
            myCanvas.Children.Add(b);
            b.Fill = Brushes.Black;
            Canvas.SetLeft(b, 625);
            Canvas.SetTop(b, 350);

            Rectangle c = new Rectangle();
            c.Height = 10;
            c.Width = 100;
            myCanvas.Children.Add(c);
            c.Fill = Brushes.Black;
            Canvas.SetLeft(c, 600);
            Canvas.SetTop(c, 50);

            Rectangle d = new Rectangle();
            d.Height = 50;
            d.Width = 10;
            myCanvas.Children.Add(d);
            d.Fill = Brushes.Black;
            Canvas.SetLeft(d, 600);
            Canvas.SetTop(d, 50);
                                      
            
            ImageBrush Image = new ImageBrush();
            Image.ImageSource = new BitmapImage(new Uri
               ("http://moziru.com/images/wild-west-clipart-background-1.jpg"));
            myCanvas.Background = Image;
        }
        //label method
        private void CreateLabel(int i, string content)
        {
            Label wordLabel = new Label();
            wordLabel.Content = content;
            Canvas.SetTop(wordLabel, 80);
            Canvas.SetLeft(wordLabel, (223 + (i * 10)));
            myCanvas2.Children.Add(wordLabel);
            wordLabel.FontSize = 18;
        }

        private void StartProgram(object sender, RoutedEventArgs e)
        {
            lblOutput.Content = "Welcome to hangman! A random word will be selected and you must find it." + Environment.NewLine + "You can guess with LOWER CASE LETTERS or WORDS" + Environment.NewLine + "If you guess more than 5 wrong letters or words you lose! good luck.";
            RNG = random.Next(0, 9);//each word is attached to a number
            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();
                //checking which word is being used
                if (line.Contains(RNG.ToString()))
                {
                    this.wordUsed = line.Substring(line.IndexOf(RNG.ToString()) + 2, line.Length - 2);//removing the number
                }
            }
            //loop that splits the word into an array of letters
            for (int i = 0; i < wordUsed.Length; i++)
            {
                word[i] = wordUsed.Substring(i, 1);

            }
            //loopp that create equally as many underscores
            for (int i = 0; i < wordUsed.Length; i++)
            {
                CreateLabel(i, "_");
            }
        }

        private void GuessLetters(object sender, RoutedEventArgs e)
        {
            guessedLetter = txtGuess.Text;

            for (int i = 0; i < wordUsed.Length; i++)
            {
                //check correct letters
                if (guessedLetter == word[i])
                {
                    correctGuessed[i] = guessedLetter;
                    CreateLabel(i, guessedLetter);
                }
                //check for whole word winner
                if (guessedLetter == wordUsed)
                {
                    lblOutput.Content = "You win!";
                    CreateLabel(i, guessedLetter);
                    i = wordUsed.Length;//ends loop early
                    winnerCheck = 1;//makes sure that there arent two "wins"
                }
                //check for incorrect letters
                if (!wordUsed.Contains(guessedLetter))
                {
                    
                    counter--;
                    incorrectGuessed[i] = guessedLetter;
                    lblWrong = lblWrong + incorrectGuessed[i] + " ";
                    lblWrongLetters.Content = lblWrong;
                    i = wordUsed.Length;
                    if (counter != 0)
                    {
                        lblOutput.Content = "Your guess of " + "\"" + txtGuess.Text + "\"" + " was incorrect! You have " + counter.ToString() + " guesses remaining";
                    }
                   
                    //check for loss
                    if (counter == 0)
                    {
                        lblOutput.Content = "You lose! The word you were looking for was: " + wordUsed;
                    }
                }

            }
            //check for letter winner
            if (winnerCheck == 0)
            {
                wordrecreated = correctGuessed[0] + correctGuessed[1] + correctGuessed[2] + correctGuessed[3] + correctGuessed[4] + correctGuessed[5] + correctGuessed[6] + correctGuessed[7] + correctGuessed[8];
                if (wordrecreated == wordUsed)
                {
                    lblOutput.Content = "You win!";
                }
            }

            if (counter == 4)
            {
                //head
                Ellipse a = new Ellipse();
                a.Height = 75;
                a.Width = 75;
                myCanvas.Children.Add(a);
                a.Fill = Brushes.Black;
                Canvas.SetLeft(a, 568);
                Canvas.SetTop(a, 99);
            }

            if(counter == 3)
            {
                //body
                Rectangle f = new Rectangle();
                f.Height = 100;
                f.Width = 25;
                myCanvas.Children.Add(f);
                f.Fill = Brushes.Black;
                Canvas.SetLeft(f, 595);
                Canvas.SetTop(f, 150);
            }

            if (counter == 2)
            {
                //right leg
                Rectangle g = new Rectangle();
                g.Height = 75;
                g.Width = 25;
                myCanvas.Children.Add(g);
                g.Fill = Brushes.Black;
                Canvas.SetLeft(g, 610);
                Canvas.SetTop(g, 240);
            }

            if(counter == 1)
            {
                //Left leg
                Rectangle h = new Rectangle();
                h.Height = 75;
                h.Width = 25;
                myCanvas.Children.Add(h);
                h.Fill = Brushes.Black;
                Canvas.SetLeft(h, 580);
                Canvas.SetTop(h, 240);
            }

            if (counter == 0)
            {
                //arms
                Rectangle i = new Rectangle();
                i.Height = 25;
                i.Width = 100;
                myCanvas.Children.Add(i);
                i.Fill = Brushes.Black;
                Canvas.SetLeft(i, 555);
                Canvas.SetTop(i, 185);

            }
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            streamReader.DiscardBufferedData();
            streamReader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            myCanvas2.Children.Clear();

            for (int i = 0; i < 10; i++)
            {
                word[i] = null;
                line = null;
            }
            lblWrongLetters.Content = "";
            lblOutput.Content = "";
            counter = 5;
            lblWrong = null;
        }
    }
}

