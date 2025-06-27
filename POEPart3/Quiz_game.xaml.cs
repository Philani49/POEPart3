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
using System.Windows.Shapes;

namespace POEPart3
{
    /// <summary>
    /// Interaction logic for Quiz_game.xaml
    /// </summary>
    public partial class Quiz_game : Window
    {
        //global declaration  for all instance and variables for game quiz
        //List[Generic]
        private List<QuizQuestion> quizData;
        private int questionIndex = 0;
        private int currentScore = 0;


        //buttons 
        private Button selectedChoice = null;
        private Button correctChoiceButton = null;


        public Quiz_game()
        {
            InitializeComponent();
            LoadQuizData();
            showQuiz();
            clearStyle();

        }

       

        //method to show the quiz on the buttons
        private void showQuiz()
        {
            //check if the user is not done playing
            if (questionIndex >= quizData.Count)
            {
                //show complete message
                MessageBox.Show("well done you have completed the quiz with " + currentScore);

                //then rest the game
                currentScore = 0;
                currentScore = 0;
                questionIndex = 0;

                DisplayScore.Text = "";
                showQuiz();
                //stop the execute
                return;

            }
            //get current index quiz
            correctChoiceButton = null;
            selectedChoice = null;

            //then get all the questions value
            var currentQuiz = quizData[questionIndex];

            //display the question to the user
            DisplayQuestion.Text = currentQuiz.Question;


            //add the choices to the button
            var shuffled = currentQuiz.Choices.OrderBy(_ => Guid.NewGuid()).ToList();

            //then add by index
            FirstChoiceButton.Content = shuffled[0];
            SecondChoiceButton.Content = shuffled[1];
            ThirdChoiceButton.Content = shuffled[2];

            //correct one
            FourthChoiceButton.Content = currentQuiz.CorrectChoice;

            clearStyle();

        }
        //method to rest the buttons
        private void clearStyle()
        {
            //use foreach to rest
            foreach (Button choice in new[] { FirstChoiceButton, SecondChoiceButton, ThirdChoiceButton, FourthChoiceButton })
            {

                choice.Background = Brushes.DarkSlateBlue;

            }
        }//end of clear style method
         //method to load the quiz data
        private void LoadQuizData()
        {
            //store info
            quizData = new List<QuizQuestion>()
{
    new QuizQuestion
    {
     Question= "What is required to ,make a strong password" ,
     CorrectChoice ="Atleast 8 character mixed with letters and numbers",
     Choices = new List<string>
     {
         "Your name and birth",
         "Simple word like password",
         "Repeating the same answer"
     }

    },
    new QuizQuestion
    {
        Question= "What is Phishing",
        CorrectChoice = "A scam to steal your information",
        Choices = new List<string>
        {
            "A secure website",
            "A type of malware",
            "A new social media trend"
        }

    },
    new QuizQuestion
    {
        Question= "What should you do if you receive a suspicious email",
        CorrectChoice ="Delete it without clicking any links",
        Choices = new List<string>
        {
            "Forwarward it to friends",
            "Open the link and see what it says","" +
            "Reply to the sender"
        }
    },
    new QuizQuestion
    {
        Question ="How often should you change your password",
        CorrectChoice ="Every 6 months or when it has been breached",
        Choices = new List<string>
        {
            "Never",
            "Once every 10 years",
            "Only when you remember"
        }
    },
    new QuizQuestion
    {
        Question ="What is two-factor authentication(2FA)",
        CorrectChoice =" confirming user identity with two different methods",
        Choices =new List<string>
        {
            "Using two random words for password",
            "Combining letters and numbers in a password",
            "Using same password twice"
        }
    },
    new QuizQuestion
    {
        Question ="Which of the following you should not share online",
        CorrectChoice ="Your full name , address or phone numbers",
        Choices = new List<string>
        {
            "Your favorite color",
            "Your hobbies",
            "Your favorite movie"

        }
    },
    new QuizQuestion
    {
        Question =" What is the best way to track for multiple password",
        CorrectChoice ="Use a password manager",
        Choices = new List<string>
        {
            "Use the same password for all accounts",
            "Write it on paper and keep in the wallet",
            "Memorize all passwords"
        }
    },
    new QuizQuestion
    {
        Question ="What does safe browsing mean",
        CorrectChoice ="Avoiding  suspicious website and unknown links",
        Choices = new List<string>
        {
            "Visiting websites you have bookmarked",
            "Browsing only during the day",
            "Using the same password for all sites"
        }
    },
    new QuizQuestion
    {
        Question ="What is digital footprint",
        CorrectChoice ="Everything you do on the internet that can be tracked",
        Choices = new List<string>
        {
            "A type of password",
            "A virus on your computer",
            "The footprint you leave at the beach"
        }
    },
    new QuizQuestion
    {
        Question ="What should you do when you password has been compromised",
        CorrectChoice ="Change it immediately, starting with your most important accounts",
        Choices = new List<string>
        {
            "Ignore it an keep using the same password",
            "Tell your friends your password",
            "Write it down so that you dont forget"
        }
    },



};

        }//end of the method for the load quiz data

        private void HandleAnswerSelection(object sender, RoutedEventArgs e)
        {
            //use sender object name to get the selected button
            selectedChoice = sender as Button;

            string chosen = selectedChoice.Content.ToString();

            //then check with the correct on the corrent quiz
            string correct = quizData[questionIndex].CorrectChoice;

            //then check if correct or not by if state ment
            if (chosen == correct)
            {

                //then set the background color 
                selectedChoice.Background = Brushes.Green;
                //assing to hold
                correctChoiceButton = selectedChoice;

            }
            else
            {
                //if incorrect
                selectedChoice.Background = Brushes.Red;
                correctChoiceButton = selectedChoice;

            }
        }

        private void HandleNextQuestion(object sender, RoutedEventArgs e)
        {
            //check if the user selected one of the choices
            if (selectedChoice == null)
            {
                //then show error messGE
                MessageBox.Show("Choose one of the 4 choices");
            }
            else
            {
                //then add points, and only if correct
                string chosen = selectedChoice.Content.ToString();
                string correct = quizData[questionIndex].CorrectChoice;

                //check if correct
                if (chosen == correct)
                {
                    // then add points 
                    currentScore++;
                    //thenshow the score
                    DisplayScore.Text = "Score :" + currentScore;

                    //then move to the next index question
                    questionIndex++;

                    //show the question again for the next one
                    showQuiz();
                }
                else
                {
                    //move to the next question
                    questionIndex++;
                    showQuiz();
                }

            }//end of else

        }
    }
}
