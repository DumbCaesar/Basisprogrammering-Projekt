namespace Basisprogrammering_Projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HovedRegning();
        }

        static void HovedRegning()
        {
            // Globale variabler for HovedRegning spillet.
            string[] calculationExa = ["2 + 5 * 6", "6/3 * 2", "62 - 100 * 4"]; // Indeholder regnings eksemplerne
            int[] calculationVal = [32, 4, 152]; // Indeholder regnings værdierne. De to Arrays fungerer som et parrallel par.
            int pos = 0; // Variablet som holder positionen for hvilket index der er nået til i de to arrays.
            int points = 0; // Indeholder mængden af points som brugeren opnår.
            int totalPoints = calculationExa.Length; // Mængden af spørgsmål som spillet indeholder
            // er lig med den totale mængde points der kan opnås.

            GreetMessage(); // Kalder GreetMessage funktionen som introducerer spilleren til spillet
            // og forklarer hvordan det virker i praksis.

            // While loopet ser om den gældende position er mindre end mængden af regneeksempler.
            // Hvis det er true, så forsætter den, og omvendt stopper den hvis der returnes false.
            while (pos < calculationExa.Length)
            {
                Console.WriteLine(calculationExa[pos]); // Udskriver regneeksemplet vi er nået til.

                if (int.TryParse(Console.ReadLine(), out int answer)) // Ser om inputtet kan parses til en int.
                {
                    if(answer == calculationVal[pos]) // Hvis spillerens gæt = den position vi er nået
                        // så må gættet være korrekt. Der sammenlignes derfor værdien i calculationVal[pos] med spillerens gæt.
                    {
                        Console.WriteLine($"Korrekt: {calculationExa[pos]} = ({calculationVal[pos]})");
                        points++;
                    }
                    else // Hvis spilleren skriver et forkert resultat til regningseksemplet
                    {
                        Console.WriteLine($"Forkert: {calculationExa[pos]} != ({answer})");
                    }
                    Console.WriteLine();
                    pos++; // positionen inkrementeres, så begge arrays er ved næste index, som positionen indikerer.
                }
                else // Hvis spilleren ikke indtaster et nummer vises denne besked, og loopet begynder forfra.
                // Så længe der er flere eksempler.
                {
                    Console.WriteLine("Indtast venligst et nummer: ");
                }
            }
            Console.WriteLine($"Du fik: {points} ud af {totalPoints}"); // Udskriver mængden af points
            // som spilleren fik ud af den totale mængde af mulige points
            Console.ReadKey();

            // GreetMessage() funktionen forklarer hvordan spillet virker, og giver en kort og kvik introduktion.
            void GreetMessage()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Velkommen til HovedRegning");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("Spillet er simpel, du skal udregne noget Matematik.");
                Console.WriteLine("Spillet stiger i sværhedsgrad efter hver opgave.");
                Console.WriteLine("Du skal derfor benytte de såkaldte 'regneregler' igen :)");
                Console.WriteLine();
                Console.ForegroundColor= ConsoleColor.Cyan;
                Console.WriteLine("Held og lykke!");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Tryk på hvilken som helst tast for at begynder spillet");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
            }
        }

        // FirePaaStribe overodnet funktion som kalder spillet.

        static void FirePaaStribe()
        {
            // Nedenunder findes de globale variabler for spillet, som gør dem tilgængelig i hele spillets scope.
            // Hvilket er nødvendigt for nogle dele af koden kan fungere.

            string[,] board = new string[6, 7]; // board array, som fungerer som brættet, ved brug af et 2-dimensionel array.
            bool gameNotWon = true; // indikerer om spillet er vundet eller ej, bruges til spillets loop.
            bool playerOneTurn = true; // Globale variabel der indikerer om det er spiller 1 eller spiller 2.
            string playerOnePiece = "O"; // Spiller 1's brik.
            string playerTwoPiece = "X"; // Spiller 2's brik.
            string playerOneEnterNum = "[Spiller 1]: Indtast kolonne nummer: "; // Spiller 1's besked.
            string playerTwoEnterNum = "[Spiller 2]: Indtast kolonne nummer: "; // Spiller 2's besked.
            // Man kunne godt have lavet dette på en anden måde, uden to string sætninger, så den bliver fuld dynamisk. 
            // Men dette er halvt opnået gennem currentPiece string conditional variablet. Det fungerer fint til dette. 

            FirePaaStribeMessage(); // Kalder velkomst funktionen, der introducerer spillerne for reglerne.
            InitializeBoard(); // Denne funktion initialiserer spil brættet med [6x7] felter.

            // Main loppet der sørger for spillet er kørende indtil en af de to spillere vinder
            // eller et draw (ingen vinder)

            while (gameNotWon)
            {
                string currentPiece = playerOneTurn ? playerOnePiece : playerTwoPiece; // Conditional statement
                // Der tjekkes om det er spiller 1's tur, hvis det er sætter vi currentPiece
                // = playerOnePiece, og omvendt hvis der returnes false.
                string currentMsg = playerOneTurn ? playerOneEnterNum : playerTwoEnterNum; // Samme princip
                // som currentPiece, der tjekkes hvilken spillers tur det er, og den korrete besked vises.

                Console.WriteLine(currentMsg); //Udskriver en variation af en besked afhængig af spillerens tur. 

                if (int.TryParse(Console.ReadLine(), out int colNum)) // Læseren spillerens input, og ser om det er et valid nummer.
                {
                    // sikrer at spilleren ikke kan gå uden for boardet
                    if (colNum < 0 || colNum >= board.GetLength(1)) // Kolonne nummeret spilleren indtaster må ikke være
                                                                    // mindre end 0, og må ikke være større end mængden af kolonner i boardet.
                    {
                        Console.WriteLine("Ugyldig kolonne. Prøv igen."); // Fejlmeddelse udskrives
                        continue; // Hvis der er en fejl, skal startes loopet forfra. 
                        // Så brugeren kan indtaste en kolonne igen indtil, det er en valid kolonne.
                    }

                    SetField(colNum, currentPiece); // Her ved vi at kolonnenummeret brugeren har indtastet er valid
                    // og vi angiver kolonnenummeret som parameter i SetField() funktionen, som sætter spillerens spillebrik
                    // som er currentPiece. Den tjekker hvilken spillers tur det er og sætter den afhængig af dette.

                    PrintBoard(); // Her printer vi boardet, som der opdaterer spillet, hvis SetField() funktion er kaldt succesfuldt.

                    if (CheckWin(currentPiece)) // Her tjekkes der om spilleren der har tur, har vundet. 
                                                // Funktionen tager ind den nuværende spillers brik som parameter.
                    {
                        Console.ForegroundColor = ConsoleColor.Green; // Konsol teksfarven ændres til grøn
                        Console.WriteLine($"Spilleren med '{currentPiece}' har vundet!"); // Udskriver en besked
                        // Der oplyser at spilleren med ("O"/"X") har vundet.
                        Console.ResetColor(); // reset konsol tekstfarve/farver
                        break; // gameNotWon = false (vinder fundet)
                    }

                    if (CheckDraw()) // Her tjekkes der om alle felter i board-arrayet, ikke længere indeholder
                                     // placeholder værdien ".", hvis det er sandt, og der ingen vinder er fundet. Så er spillet et draw.
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; // Sætter konsol tekstfarven til gul.
                        Console.WriteLine("Spillet er uafgjort!"); // samme som ovenover. Ingen vundet fundet, printer til konsol.
                        Console.ResetColor();
                        break; // gameNotWon = false (ingen vinder fundet)
                    }

                    // Her skifter vi til spiller nummer 2. Vi sætter playerOneTurn ikke lig med playerOneTurn.
                    // Altså playerOneTurn = false.
                    // I dette spils logik betyder det at det er spiller 2's tur. 
                    playerOneTurn = !playerOneTurn;
                }
                else // Lidt det samme som længere op, dog sker dette hvis brugeren ikke har indtastet et nummer
                // men i stedet et bogstav eller en sætning for eksemepel.
                {
                    Console.WriteLine("Ugyldigt input. Indtast et tal mellem 0 og 6.");
                }
            }

            bool CheckDraw() // Boolean funktion. Tjekker om spillet er et draw.
            {
                for (int row = 0; row < board.GetLength(0); row++)        // loop gennem rækker
                {
                    for (int col = 0; col < board.GetLength(1); col++)    // loop gennem kolonner
                    {
                        if (board[row, col] == ".") // Tjekker om positionen indeholder placeholder "."
                        {
                            return false; // mindst et frit felt. IKKE uafgjort endnu
                        }
                    }
                }

                return true; // ingen "." tilbage. Brættet er fyldt, dermed et DRAW
            }

            // Denne funktion tjekker om en spiller (piece = "X" eller "O") har vundet.
            // Funktionen returnerer true hvis der findes fire på stribe ellers false.
            // Den undersøger hele boardet og tjekker fire retninger: vandret, lodret og diagonalt i op-ned og ned-op.
            // Funktionen er den mest komplexe af dem alle, dermed også den der tog længest at lave.. mange timer....

            bool CheckWin(string piece)
            {
                int rows = board.GetLength(0); // Antallet af rækker (6)
                int cols = board.GetLength(1); // Antallet af kolonner (7)

                // Ydre loop: looper gennem hver række.
                for (int row = 0; row < rows; row++)
                {
                    // Indre loop: looper gennem hver kolonne i rækken.
                    for (int col = 0; col < cols; col++)
                    {
                        // Hvis feltet ikke indeholder det aktuelle piece (X/O), så springer vi til næste iteration.
                        if (board[row, col] != piece)
                            continue;

                        // Tjek for fire vandret.
                        // col + 3 < cols sikrer, at vi ikke går ud over højre side af boardet.
                        if (col + 3 < cols &&
                            board[row, col + 1] == piece &&
                            board[row, col + 2] == piece &&
                            board[row, col + 3] == piece)
                        {
                            return true; // Vinder fundet vandret.
                        }

                        // Tjek for fire lodret.
                        // row + 3 < rows sikrer, at vi ikke går længere ned end sidste række.
                        if (row + 3 < rows &&
                            board[row + 1, col] == piece &&
                            board[row + 2, col] == piece &&
                            board[row + 3, col] == piece)
                        {
                            return true; // Vinder fundet lodret.
                        }

                        // Tjek for diagonal pil (ned-højre).
                        // Betingelsen sikrer at vi har plads både nedad og til højre.
                        if (row + 3 < rows && col + 3 < cols &&
                            board[row + 1, col + 1] == piece &&
                            board[row + 2, col + 2] == piece &&
                            board[row + 3, col + 3] == piece)
                        {
                            return true; // Vinder fundet diagonalt ned mod højre.
                        }

                        // Tjek for diagonal pil (ned-venstre).
                        // Betingelsen sikrer at vi kan gå nedad og til venstre uden at gå ud af array grænsen.
                        if (row + 3 < rows && col - 3 >= 0 &&
                            board[row + 1, col - 1] == piece &&
                            board[row + 2, col - 2] == piece &&
                            board[row + 3, col - 3] == piece)
                        {
                            return true; // Vinder fundet diagonalt ned mod venstre.
                        }
                    }
                }

                return false; // Ingen fire på stribe. Ingen vinder endnu.
            }

            // Her er funktionen der sørger for logikken til at sætte en "brik" på brættet.
            // Funktionen har 2 parametre der gives for spilleren kan sætte sin brik.
            // Funktionen kræver en colonne, og en string, der skal erstatte "." med en spiller brik. 

            void SetField(int col, string piece)
            {
                for (int row = board.GetLength(0) - 1; row >= 0; row--) // Der loppes gennem rækkerne. 
                                                                        // Vi får længden og minuser med -1 for, at få det korrekte index i board.
                                                                        // Derefter lopper vi fra bunden af, da brikkerne skal fylde op fra bunden.
                {
                    if (board[row, col] == ".") // Vi ser om positionen vi er ved, board[row, col]
                                                // indeholder et "." som placeholder, hvis den gør erstatter vi den med en spiller brik.
                                                // og vi bryder ud af loopet.
                    {
                        board[row, col] = piece;
                        break;
                    }
                }
            }

            // Denne funktion er meget ens med InitializeBoard() funktionen
            // Forskellen mellem de to ligger i at denne PrintBoard() funktionen
            // bare printer boardet ud, og dens værdi der er sat i de forskellige
            // positioner i det to-dimensionelle array board

            void PrintBoard()
            {
                Console.Clear(); // Consollen clear hver gang denne funktion kaldes. Så det er nemt og overskue
                                 // og spillet forblivber representabelt
                for (int colNum = 0; colNum < board.GetLength(1); colNum++) // Mængden af kolonner fås her, og vi looper gennem
                {
                    Console.Write(colNum); // og udskriver tallet til spillerne.
                }
                Console.WriteLine(); // Fungerer som seperation

                for (int row = 0; row < board.GetLength(0); row++) // Der få's mængden af rækker i board-arrayet.
                                                                   // og der loppes gennem
                {
                    for (int col = 0; col < board.GetLength(1); col++) // Her får vi mængden af kolonner
                                                                       // som fungerer som det indre loop.
                    {
                        Console.Write(board[row, col]); // Der udskrives board[row, col], som betyder at vi udskriver
                        // værdierne som befinder sig i hvert position i det 6x7 array.
                    }
                    Console.WriteLine(); // Linjeskift 
                }
            }

            // Denne funktion initialiserer brættet for, at spillet fungerer.
            // Funktionen lopper gennem hver konlonne og række, og giver den en placeholder værdi "."

            void InitializeBoard()
            {
                for (int colNum = 0; colNum < board.GetLength(1); colNum++) // Mængden af kolonner fås her, og vi looper gennem
                {
                    Console.Write(colNum); // og udskriver tallet til spillerne. (identisik til PrintBoard() øverste for loop)
                }
                Console.WriteLine();

                // Der laves et for loop for at loope gennem kolonner og rækkerne, og -
                // Der tildeles placeholderen "." for, at brækket initialiseres og synligøres for spillerne.

                for (int row = 0; row < board.GetLength(0); row++) // looper gennem første værdi, i det to-dimensionelle "board" array
                {
                    for (int col = 0; col < board.GetLength(1); col++) // looper gennem anden værdi, i det to-dimensionelle "board" array
                    {
                        board[row, col] = "."; // vi sætter placeholder værdien ved den position vi er ved 
                        Console.Write(board[row, col]); // og udskriver
                    }
                    Console.WriteLine();
                }
            }

            // Funktionen FirePaaStribeMessage(), giver en velkomstbesked til spillerne -
            // og forklarer reglerne for spillet, det er dens eneste funktion.

            static void FirePaaStribeMessage()
            {
                Console.Clear();

                // Skift farve til gul
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("=======================================");
                Console.WriteLine("   ███████╗██╗██████╗ ███████╗          ");
                Console.WriteLine("   ██╔════╝██║██╔══██╗██╔════╝          ");
                Console.WriteLine("   █████╗  ██║██████╔╝█████╗            ");
                Console.WriteLine("   ██╔══╝  ██║██╔═══╝ ██╔══╝            ");
                Console.WriteLine("   ██║     ██║██║     ███████╗          ");
                Console.WriteLine("   ╚═╝     ╚═╝╚═╝     ╚══════╝          ");
                Console.WriteLine("                                        ");
                Console.WriteLine("           FIRE PÅ STRIBE!              ");
                Console.WriteLine("=======================================");
                Console.ResetColor();

                Console.WriteLine();

                // Skift til grøn for regler
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("⭐ Regler:");
                Console.WriteLine("1. Spillerne, bestående af (2), skiftes til at vælge en kolonne (0-6).");
                Console.WriteLine("2. Din brik falder ned på den nederste ledige plads.");
                Console.WriteLine("3. Første spiller med 4 på stribe (vandret, lodret eller diagonalt) vinder!");
                Console.ResetColor();

                Console.WriteLine();
                // Cyan farve for begynd spillet besked
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Tryk på en tast for at begynde spillet...");
                Console.ResetColor();

                Console.ReadKey();
                Console.Clear();
            }





        }
    }
}
