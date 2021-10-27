using System;
using System.Collections.Generic;
using System.Linq;

namespace Deckofcards
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Deck deck1 = new Deck();
            //Console.WriteLine(deck1);
            //Console.WriteLine(deck1.Count());
            //Console.WriteLine(deck1.Shuffle());

            Player player1 = new Player(1);
            Player player2 = new Player(2);
            Player player3 = new Player(3);

            Dealer dealer1 = new Dealer("Jack",99);

           

            deck1.Shuffle(deck1);
            
            player1.Draw(deck1,2);
            player2.Draw(deck1,3);
            player3.Draw(deck1,3);
            dealer1.Draw(deck1, 2);

            Console.WriteLine(player1);
            Console.WriteLine(player2);
            Console.WriteLine(player3);
            Console.WriteLine(dealer1);
           
            //once the dealer is called again the dealer's hand will be revealed
            Console.WriteLine(dealer1);



            Console.WriteLine(deck1.Count());
            */
            //START OF PROGRAM
            //list of variables used throughout program
            int userInput = 0;
            int handcount = 0;

            MainMenu();

            //consider it to be 1 for invalid selection;
            void MainMenu()
            {
                Console.WriteLine("Hello, Welcome to getRekt Casino!");
                Console.WriteLine("Please choose from the following games");
                Console.WriteLine("1: BlackJack");
                //Console.WriteLine("2: NA");
               // Console.WriteLine("3: NA");
                Console.WriteLine("Please enter the corresponding number");
                userInput = Convert.ToInt16(Console.ReadLine());

                if (userInput == 1) { BlackJackMain(); }
                else { InvalidInput(1,null,null,null); }
            }
           
            //overload Input Method for other invalid types //overload doesnt work iun main???
            void InvalidInput(int question, List<Player> aListofPlayers, Dealer aDealer, Deck aDeck )
            {
                Console.WriteLine("Invalid Input please try again");
                if (question == 1) { MainMenu(); }
                else if (question == 3) { BlackJackMain(); }
                else if (question == 4) { BlackJackNewHand(aListofPlayers, aDealer, aDeck); }

                else { MainMenu(); }

            }
            //consider to be 3 for invalid selection;
            void BlackJackMain()
            {
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                Console.WriteLine("Hello, Welcome to BlackJack!");
                Console.WriteLine("How Many Players would you like? \nPlease Enter a Number:");
                userInput = Convert.ToInt16(Console.ReadLine());
                if (userInput > 0)
                {
                    //create a list of players
                    List<Player> aListofPlayers = new List<Player>();
                    //add the requested players to the list;
                    for (int i = 1; i < userInput + 1; i++)
                    {
                        aListofPlayers.Add(new Player(i));
                    }
                    //create a dealer
                    Dealer aDealer = new Dealer("Camilla", -1);
                    //add dealer to list
                    aListofPlayers.Add(aDealer);

                    //create the first deck
                    Deck aDeck = new Deck();
                    aDeck.Shuffle(aDeck);

                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine("Loading Game");
                    //fake a loading screen
                    /*for (int i = 0; i < 1000000; i++)
                    {
                        if (i % 1000 == 0)
                        {
                            Console.WriteLine(".");
                        }
                    }*/
                    
                    BlackJackNewHand(aListofPlayers, aDealer, aDeck);
                }
                else { InvalidInput(3,null,null,null); }

            }


            //consider this to be invalid 4
            void BlackJackNewHand(List<Player> players, Dealer dealer, Deck aDeck)
            {
                Console.WriteLine("Hand #: "+handcount+ "\n~~~~~~~~~~~~~~~~~~~~~~~~");
                

                //starting this variable to be used
                
                //have each player place bets                

                foreach (Player player in players)
                {
                    if (player.Id != -1)
                    {
                        
                        Console.WriteLine($"Place Bet for Player {player.Id}");
                        Console.WriteLine($"Current Chip Count: {player.ChipCount}");
                        player.Bet = Convert.ToInt16(Console.ReadLine());                        
                        //if invalid number or higher bet than chipcount
                        if (player.Bet < 0 || player.Bet > player.ChipCount) { InvalidInput(4, players, dealer, aDeck); }
                        player.ChipCount -= player.Bet;
                        Console.WriteLine(player.ChipCount);
                        Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~");

                    }
                }
                //show everyones intial hand
                Console.WriteLine("Dealing\n~~~~~~~~~~~~~~~~~~~~~~~~");
                foreach (var player in players)
                {
                    player.Draw(aDeck, 2);
                    Console.WriteLine(player);
                }


                //have each player play their turn
                foreach (Player player in players)
                {
                    //dealers id will always be negative 1
                    if (player.Id != -1)
                    {
                        Turn(player, dealer, aDeck);
                        //show players final hand
                        Console.WriteLine(player);
                    }
                    //lastly do the dealers turn

                    else { Console.WriteLine("Dealer's Turn"); DealerTurn(players, dealer, aDeck); }
                }
                
                //use dealers outcome to quickly determine if players win

                    foreach (Player player in players)
                    { if (player.PlayerOutcome != "Bust" && player.PlayerOutcome != "BlackJack" && player.Id != -1)
                        {
                            if (dealer.DealerBlackJackValue(dealer.Hand) > 21) { BlackJackPayout("DealerBust", player); }
                            else if (dealer.DealerBlackJackValue(dealer.Hand) > player.BlackJackValue(player.Hand)) { Console.WriteLine($"Dealer beats Player {player.Id}!"); BlackJackPayout("DealerWin", player); }
                            else if (dealer.DealerBlackJackValue(dealer.Hand) < player.BlackJackValue(player.Hand)) { Console.WriteLine($"Dealer loses to Player {player.Id}!"); BlackJackPayout("PlayerWin", player); }
                            else if (dealer.DealerBlackJackValue(dealer.Hand) == player.BlackJackValue(player.Hand)) { BlackJackPayout("Tie", player); }

                           

                        }
                    //remove all cards from players and dealers hands
                    player.Hand.RemoveRange(0, player.Hand.Count());

                }



                
                //reset dealers method counter
                dealer.MethodCounter = 0;
                dealer.cardCount = 0;






                //check and reshuffle deck if needed
                aDeck.CheckDeck(aDeck);
                //increse handcount for when we call the new hand function again
                handcount++;
                Console.WriteLine("Would you like to play another Hand?");
                Console.WriteLine("1: Yes");
                Console.WriteLine("2: No");
                int again = 2;
                again = Convert.ToInt32(Console.ReadLine());

                if (again == 1) { BlackJackNewHand(players, dealer, aDeck); }
                else { Console.WriteLine("Leaving Table");MainMenu(); }


            }
            //making a turn method for each player

            void Turn(Player player, Dealer dealer, Deck aDeck)
            {
                //Play the players hand
                
                
                    Console.WriteLine(player);
 
                    if (player.BlackJackValue(player.Hand) < 21)
                    {
                        Console.WriteLine("What Would you like to do?");
                        Console.WriteLine("1: Hit");
                        Console.WriteLine("2: Stand");
                        userInput = Convert.ToInt16(Console.ReadLine());
                        if (userInput == 1) { Hit(player,dealer, aDeck); }
                        else if (userInput != 2) { Console.WriteLine("Invalid Input - Try Again"); Turn(player, dealer, aDeck); }
                    }
                    //if player = 21
                    else if (player.BlackJackValue(player.Hand) == 21 && player.Hand.Count()==2) { BlackJackPayout("BlackJack",player); player.PlayerOutcome = "BlackJack"; }
                    //if player over 21
                    else if (player.BlackJackValue(player.Hand) > 21) { BlackJackPayout("Bust", player); player.PlayerOutcome = "Bust"; }

                    //add split function and double function


                    
                

            }
            //making Dealer turn

            string DealerTurn(List<Player> players, Dealer dealer, Deck deck)
            {
                
                string DealerOutcome = "";
                Console.WriteLine(dealer);
                if (dealer.DealerBlackJackValue(dealer.Hand) > 21) { Console.WriteLine("Dealer Busts"); Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"); ; DealerOutcome = "DealerBust"; }
                else if (dealer.DealerBlackJackValue(dealer.Hand) < 17) { dealer.Draw(deck, 1); Console.WriteLine("Dealer draws"); DealerTurn(players, dealer, deck); }
                else { Console.WriteLine($"Dealer Stays with {dealer.DealerBlackJackValue(dealer.Hand)}!"); Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"); }

                
                return DealerOutcome;
                

            }
            //hit method
            void Hit(Player player,Dealer dealer, Deck deck)
            {
                player.Draw(deck, 1);
                Turn(player, dealer, deck);
            }

            //payouts
            void BlackJackPayout(string type, Player player)
            {
                if (type == "BlackJack") { player.ChipCount += player.Bet * 3; Console.WriteLine($"Player {player.Id} get Blackjack and is awarded {player.Bet * 2}! "); }
                else if (type == "DealerBust") { player.ChipCount += player.Bet * 2; Console.WriteLine($"Player {player.Id} wins {player.Bet}! "); }
                else if (type == "Bust") { Console.WriteLine($"Player {player.Id} busted and loses {player.Bet}"); }
                else if (type == "DealerWin") { Console.WriteLine($"Dealer wins, Player {player.Id} loses {player.Bet}"); }
                else if (type == "Tie") { Console.WriteLine($"Player {player.Id} ties and gets {player.Bet} back"); player.ChipCount += player.Bet; }
                else if (type == "PlayerWin") { player.ChipCount += player.Bet * 2; Console.WriteLine($"Player {player.Id} wins and is awarded {player.Bet}!"); }

                //tell player their chip count
                Console.WriteLine($"Player {player.Id} Chip Count : "+ player.ChipCount);
                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            }




        }
        
    }
}
