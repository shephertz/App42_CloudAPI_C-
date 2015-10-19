using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.game;


namespace Test_CSharp_SDK
{
    class LeaderBoardSample
    {
        ///<summary> 
        ///Main Method To Save user score on Cloud.
        ///</summary>
        ///<param name=args>args</param>

        static void Main(string[] args)
        {
            SaveUserScore();
            
        }
        /// <summary>
        /// Test Method for saving the user score in App42 Cloud. 
        /// </summary>
        public static void SaveUserScore()
        {
            /// Enter your Public Key and Private Key Here in Constructor. You can
            /// get it once you will create a app in app42 console.

            ServiceAPI sp = new ServiceAPI("<Your_API_Key>", "<Your_Secret_Key>");

            String gameName = "PokerGame";
            String userName = "Nick";
            double gameScore = 3500;
            String description = "description";
            /// Create Instance of ScoreBoard Service
            ScoreBoardService scoreBoardService = sp.BuildScoreBoardService();
            GameService gameService = sp.BuildGameService();

            try
            {
                Game saveScore = scoreBoardService.SaveUserScore(gameName,userName,gameScore);
                Console.WriteLine("gameName is " + saveScore.GetName());
                for (int i = 0; i < saveScore.GetScoreList().Count; i++)
                {
                    Console.WriteLine("userName is : " + saveScore.GetScoreList()[i].GetUserName());
                    Console.WriteLine("score is : " + saveScore.GetScoreList()[i].GetValue());
                    Console.WriteLine("scoreId is : " + saveScore.GetScoreList()[i].GetScoreId());
                }
            }
            catch (App42Exception ex)
            {
                /// Exception Caught
                /// Do exception Handling of Score Board Service functions.
                if (ex.GetAppErrorCode() == 3002)
                {
                    Game createGame = gameService.CreateGame(gameName, description);
                    Console.WriteLine("gameName is " + createGame.GetName());
                    Game game = scoreBoardService.SaveUserScore(gameName, userName, gameScore);
                    Console.WriteLine("gameName is " + game.GetName());
                    for (int i = 0; i < game.GetScoreList().Count; i++)
                    {
                        Console.WriteLine("userName is : " + game.GetScoreList()[i].GetUserName());
                        Console.WriteLine("score is : " + game.GetScoreList()[i].GetValue());
                        Console.WriteLine("scoreId is : " + game.GetScoreList()[i].GetScoreId());
                    }
                }
                else if (ex.GetAppErrorCode() == 1401)
                {
                    Console.WriteLine("Please verify your API_KEY and SECRET_KEY From AppHq Console (Apphq.shephertz.com).");
                }
                else
                {
                    Console.WriteLine("Exception is : " + ex.ToString());
                }

            }
            Console.ReadKey();
        }
    }
}