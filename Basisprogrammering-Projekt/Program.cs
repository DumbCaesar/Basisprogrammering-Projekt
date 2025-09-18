using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Basisprogrammering_Projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        // =====================================
        //               Menu
        //           Author: Nicolas
        //          Vejledning: Oliver
        // =====================================

        static void Menu()
        {
            while (true) // hele menuen foregår i et while loop
            {
                Console.Clear();
                int spilValg = 0; 
                Console.WriteLine("Indtast tallet på det spil du vil spille \n");
                Console.WriteLine("1. Hangman");
                Console.WriteLine("2. Fire på stribe");
                Console.WriteLine("3. Hovedregning");
                Console.WriteLine("4. Sæt tal i rækkefølge");
                if (int.TryParse(Console.ReadLine(), out spilValg)) // læser spillerens input
                {
                    if (spilValg > 0 && spilValg < 5)
                    {
                        // hvis spillerens input er et tal fra 1 til 4 starter spillet med det nummer i switchen
                        switch (spilValg)
                        {
                            case 1:
                                Hangman();
                                break;
                            case 2:
                                FirePaaStribe();
                                break;
                            case 3:
                                Console.Clear();
                                HovedRegning();
                                break;
                            case 4:
                                NumbersInOrder();
                                break;
                        }
                    }
                }
                // loopet starter forfra hvis spilleren skriver et tal, som ikke er mellem 1 og 4, eller hvis de skriver et bogstav
                Console.WriteLine("Du skal indtaste et tal mellem 1 og 4\nTryk en tast for at prøve igen");
                Console.ReadKey();
                continue;
            }
        }

        // =====================================
        //               Hangman
        //                  
        //           Author: Nicolas
        // =====================================

        static void Hangman() // funktionen, som starter hangman
        {
            // et array, som indeholder alle de mulige ord
            string[] wordList = { "TIGER", "CRANES", "GLUE", "BICYCLE", "ELEPHANT", "CRASH", "FLUSH", "READING", "CONSTRUCTION", "COMPUTER", "WATCH", "SHOWER", "COLLECT", "GROUND", "FURNITURE", "HUNDRED", "KITCHEN", "MAGNET", "THROUGH", "DROUGHT"};
            
            // genererer et random nummer, som er mellem 0 og det antal ord der er i wordList arrayet
            Random random = new Random();
            int randomNumber = random.Next(wordList.Length);
            
            string randomWord = wordList[randomNumber]; // bruger det tilfældige tal til at vælge et ord fra wordList arrayet
            int randomWordLength = randomWord.Length; // laver en integer variabel, som er længden på det valgte ord
            
            int lives = 5; // lives variabel med værdi på 5

            string word = ""; // laver en string som er blank
            for (int i = 0; i < randomWordLength; i++) // et loop, der sætter en bindestreg for hvert bogstag i det valgte ord
            {
                word += "-";
            }

            while (lives > 0 && word.Contains("-")) // while loop, der kører så længe spilleren har mere end 0 liv og ordet ikke er gættet
            {
                Console.Clear(); // clearer så spillet ikke ser rodet ud efter man har gættet
                Console.WriteLine(word); // Skriver ordet som spilleren skal gætte
                Console.WriteLine("Lives left: " + lives); // viser antal liv
                Console.WriteLine("Guess a letter");
                string input = Console.ReadLine().ToUpper(); // læser spillerens input og laver det til stort bogstav

                char guess = Convert.ToChar(input); // converter spillerens input til char
                bool correct = false; // laver en bool sat til false

                for (int i = 0; i < randomWordLength; i++) 
                {
                    if (randomWord[i] == guess) // tjekker spillerens gæt passer men et bogstav i ordet
                    {
                        word = word.Remove(i, 1).Insert(i, guess.ToString()); // fjerner et en bindestreg og indsætter et bogstav
                        correct = true; // sætter boolen til true, så spilleren ikke mister liv
                    }
                }
                if (!correct)
                {
                    lives--; // spilleren mister liv hvis de gætter forkert
                }
            }
            if (word.Contains("-"))
            {
                // hvis ordet stadig indeholder bindestreger er spillet tabt, og ordet vises på skærmen
                Console.Clear();
                Console.WriteLine("You Lose! The word was: " + randomWord);
            }
            else
            {
                // spillet er vundet
                Console.Clear();
                Console.WriteLine("You Win! The word was: " + randomWord);
            }
            // til sidst får spilleren mulighed for at spille igen eller vende tilbage til menuen
            Console.WriteLine("To play again, type r");
            Console.WriteLine("To return to menu, type anything else");
            string endInput = Console.ReadLine(); // læser spillerens input
            if(endInput == "r")
            {
                // hvis spilleren skriver r, starter funktionen Hangman igen
                Hangman();
            }
            else
            {
                // hvis spilleren skriver andet end r, vender de tilbage til menuen
                Menu();
            }
        }


        // =====================================
        //         Sæt tal i rækkefølge
        //                  
        //           Author: Oliver
        // =====================================

        // funktion der starter sæt tal i rækkefølge spillet
        static void NumbersInOrder()
        {
            // globale variabler for spillet

            int[] numbers = new int[10]; // her oprettes et tomt numbers array og der tildels 10 pladser. 
            InitNumberArray();  // Programmet initilserer numbers array efter det er erklæret
                                // og fylder den med 10 random numre. 
            int[] sortedArray = SortArray(numbers); // sortedArray bliver et sorteret array af numbers.
            int numberOfTry = 0; // Holder styr på mængden af forsøg spilleren bruger på at løse arrayet.
            int posOne = 0; // global varialbel på position 1. Bruges til at skifte pladser i arrayet.
            int posTwo = 0; // global variabel på position 2
            bool playing = true; // boolean om spilleren spiller stadig.
           
            while (playing) // main loop for spillet. Ser om spillet kører, så længde det gør er spillet stadig i gang.
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Du kan til en hver tid slutte spillet ved at skrive 000");
                Console.ResetColor();
                PrintNumberArray(); // Hvert loop startes der med at printes numbers array.
                Console.WriteLine();
                Console.WriteLine("Indtast positionen der skal flyttes"); // spilleren indtaster posOne
                if (!int.TryParse(Console.ReadLine(), out posOne)) // ser om inputtet er et nummer
                    continue; // hvis ikke starter den loopet forfra. (continue) indtil input indeholder et nummer.

                if(posOne == 000)
                {
                    Menu();
                }

                if(posOne < 0 || posOne > numbers.Length -1) // Tjekker om posOne er mindre end 0, eller større end længden af array -1.
                    // Hvis det er sandt betyder det at posOne er "out of bounds", og har ikke en tilsvarende værdi i array.
                    // da array har positioner fra 0-9.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du kan ikke vælge et index ude fra array.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.ResetColor();
                    continue;
                }

                Console.WriteLine("Indtast positionen der skal erstattes"); // posTwo
                if (!int.TryParse(Console.ReadLine(), out posTwo))
                    continue;

                if (posTwo == 000)
                {
                    Menu();
                }

                if (posTwo < 0 || posTwo > numbers.Length -1) // Identisk til den ovenover, kun variable er skiftet.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du kan ikke vælge et index ude fra array.");
                    Console.ReadKey();
                    Console.Clear();
                    Console.ResetColor();
                    continue;
                }

                if(posOne == posTwo) // Tjekker om de 2 postioner er identiske til hinanden, hvis det er sandt kan de ikke byttes. 
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Du kan ikke bytte den samme position..");
                    Console.ReadKey();
                    Console.Clear();
                    Console.ResetColor();
                    continue;
                }

                SetNumberOrder(posOne, posTwo); // bytter de to pladser hvis de eksisterer.
                numberOfTry++; // inkrementerer forsøg.

                if (IsMatch()) // hvis numbers array matcher sortedArray så vil spillet være vundet.
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Tillykke alle tal er sorteret korrekt.!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine($"Du brugte {numberOfTry} forsøg."); // udskriver mængden af forsøg brugt.
                    Console.ResetColor();
                    playing = false; // spillet er slut, playing = false.
                }
            }
           
            // InitNumerArray() funktionen tildeler numbers array 10 random værdier mellem 1-99.
            // For at sikre at det samme nummer ikke tildeles to gange, bruges der Fisher-Yates shuffle metoden.
            // som sikrer at det er så random som det kan blive af tal.
           
            void InitNumberArray()
            {
                int[] pool = new int[99]; // Her oprettes der et midlertidigt int[] pool array. Som holder 99 numre.
                for (int i = 0; i < pool.Length; i++) // Simpel for loop der tildeler hvert index i arrayet, i.
                {
                    pool[i] = i; // feks pool[6] = 6
                }

                Random random = new Random(); // Vi bruger Random klassen til at kunne tildele random værdier.

                // Fisher-Yates shuffle.
                for (int i = pool.Length -1; i > 0; i--) // for loop der tildeler længden af pool -1 til i. og kører indtil i > 0.
                {
                    int j = random.Next(i + 1); // int j tildeles i + 1. Det gøres fordi at
                    // arrayet ikke ellers vil holde værdien, eksempel:
                    // pool[0, 1, 2, 3, 4] = random.Next(5) her er værdien 5 ikke inkluderet, derfor skrives + 1.
                    int temp = pool[i]; // Vi shuffler
                    pool[i] = pool[j]; // Bytter plads
                    pool[j] = temp; // bytter plads igen.
                    // SetNumberOrder funktion har en mere detaljeret forklaring på hvad der sker her. Hvor der også shuffles.
                }

                for (int i = 0; i < numbers.Length; i++) // Her tildes der 10 værdier, som er random, da vi har shufflet ovenover.
                {
                    numbers[i] = pool[i]; // tildeler 10 værdier. 
                }
            }

            // SetNumberOrder() funktionen står for at bytte to værdier i numbers array.
            // Funktionen tager to parameter (int posOne, int posTwo), som er  de to værdier der skal bytte plads.

            void SetNumberOrder(int posOne, int posTwo)
            {
                if (posOne >= 0 && posOne < numbers.Length && // Der tjekkes om posOne og posTwo er >= 0
                    posTwo >= 0 && posTwo < numbers.Length) // som det laveste index et array kan have.
                    // Derudover tjekkes der om de to numre er mindre end længden af numbers array, da index
                    // selvfølgelig ikke kan være større end længden af arrayet. Hvis det er sandt kan de to numre bytte plads.
                {
                    // gamle kode, var omvendt fra nuværende SetNumberOrder.
                    // nye gør det nemmere at forstå og mere intuitivt og logisk.

                    //int temp = numbers[posOne];
                    //numbers[posOne] = numbers[posTwo];
                    //numbers[posTwo] = temp;

                    // Her opretter vi et int temp, som skal fungere som et midlertidigt variabel.
                    // Det gøres for at vi kan bytte værdier mellem posOne og PosTwo.

                    int temp = numbers[posTwo];  // Her tildeler vi numbers[posTwo] værdien til temp.
                    numbers[posTwo] = numbers[posOne]; // Her tildes numbers[posOne] værdien til numbers[posTwo]
                    numbers[posOne] = temp; // og til sidst bliver numbers[posOne] tildelt værdien temp
                    // som indeholder værdien fra posTwo, og dermed har posOne og posTwo byttet plads.
                }
            }

            // SortArray() funktion returnerer et int[] og tager ind et int[] array.
            // Denne funktions opgave består i at returne en sortert udgave af numbers[].
            // Den er nødvendig, i dette spils logik for at kunne sammenligne med spillerens placeringer.
            // numbers[] == sortedArray[]

            int[] SortArray(int[] array)
            {
                int[] copy = (int[])array.Clone(); // Laver en kopi, ellers bliver det sortered array
                                                   // skrevet ud i stedet for det usorterede, somehow.
                Array.Sort(copy); // sorterer kopien i stigende rækkefølge.
                return copy; // og returnerer den, som er et krav, da funktionen skal retunere et int[].
            }

            // IsMatch() funktionen er en boolean funktion. Den ser om numbers sekvensen er den samme som
            // sortedArray, hvis det er sandt retunerer den true, ellers false.
            // Denne funktion er derfor essentiel for at se om spilleren/brugeren har svaret korrekt. 
            // Derfor er det funktionen der afgør om spilleren har vundet eller ej.

            bool IsMatch()
            {
                return numbers.SequenceEqual(sortedArray); // SequenceEqual er en funktion fra Linq.
                // Som gør det nemmere. Alternativt kan det f.eks. gøres med et par for loops. 
            }

            // Funktionen PrintNumerArray() printer numbers array ud efter hver runde, altså når spilleren
            // har rykket 2 tal i arrayet.

            void PrintNumberArray()
            {
                for (int i = 0; i < numbers.Length; ++i) // dette for loop tager længden af numbers array.
                {
                    Console.Write(i + "  "); // og skriver index tal over hver position i arrayet.
                }
                Console.WriteLine(); // spacing
                Console.WriteLine();
                
                for (int i = 0; i < numbers.Length; i++) // tager længende af numbers array igen.
                {
                    Console.Write(numbers[i] + " "); // og udskriver værdierne i numbers array, med list spacing mellem.
                }
            }
           
            // Denne funktion PrintSortedArray() printer det sorteret array ud, men benyttes ikke i koden nuværende.
            // og er derfor udkommenteret. Funktionen er næsten identisk til PrintNumberArray i funktionalitet.
            // Der er kun nogle små differancer og ekstra funktionalitet ved den overstående.

            //void PrintSortedArray()
            //{
            //    for (int i = 0; i < sortedArray.Length; i++)
            //    {
            //        Console.Write(sortedArray[i] + " ");
            //    }
            //}
        }

        // =====================================
        //            Hovedregning
        //                  
        //           Author: Oliver
        // =====================================

        // funktion der starter hovedregning spillet
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
                    if (answer == 000)
                        Menu();
                    if(answer == calculationVal[pos]) // Hvis spillerens gæt = den position vi er nået
                        // så må gættet være korrekt. Der sammenlignes derfor værdien i calculationVal[pos] med spillerens gæt.
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Korrekt: {calculationExa[pos]} = ({calculationVal[pos]})");
                        points++;
                        Console.ReadKey();
                        Console.Clear();
                        Console.ResetColor();
                    }
                    else // Hvis spilleren skriver et forkert resultat til regningseksemplet
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Forkert: {calculationExa[pos]} != ({answer})");
                        Console.ReadKey();
                        Console.Clear();
                        Console.ResetColor();
                    }
                    pos++; // positionen inkrementeres, så begge arrays er ved næste index, som positionen indikerer.
                }
                else // Hvis spilleren ikke indtaster et nummer vises denne besked, og loopet begynder forfra.
                // Så længe der er flere eksempler.
                {
                    Console.Clear();
                    Console.WriteLine("Indtast venligst et nummer: ");
                }
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Du fik: {points} ud af {totalPoints}"); // Udskriver mængden af points
            // som spilleren fik ud af den totale mængde af mulige points
            Console.ResetColor();
            Console.ReadKey();

            // GreetMessage() funktionen forklarer hvordan spillet virker, og giver en kort og kvik introduktion.
            void GreetMessage()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Velkommen til HovedRegning");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("1. Spillet er simpel, du skal udregne noget Matematik.");
                Console.WriteLine("2. Spillet stiger i sværhedsgrad efter hver opgave.");
                Console.WriteLine("3. Du skal derfor benytte de såkaldte 'regneregler' igen :)");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Du kan til en hver tid slutte spillet ved at skrive 000");
                Console.ForegroundColor= ConsoleColor.Cyan;
                Console.WriteLine("Held og lykke!");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Tryk på hvilken som helst tast for at begynder spillet..");
                Console.ResetColor();
                Console.ReadKey();
                Console.Clear();
            }
        }

        // =====================================
        //           Fire på stribe
        //                  
        //           Author: Oliver
        // =====================================

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
                    if (colNum == 000)
                        Menu();
                    // sikrer at spilleren ikke kan gå uden for boardet
                    if (colNum < 0 || colNum >= board.GetLength(1)) // Kolonne nummeret spilleren indtaster må ikke være
                                                                    // mindre end 0, og må ikke være større end mængden af kolonner i boardet.
                    {
                        Console.Clear(); // Clear konsol, fjerner tidligere input.
                        PrintBoard(); // Printer board ud igen.
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Ugyldig kolonne. Prøv igen."); // Fejlmeddelse udskrives
                        Console.ResetColor();
                        continue; // Hvis der er en fejl, skal startes loopet forfra. 
                        // Så brugeren kan indtaste en kolonne igen indtil, det er en valid kolonne.
                    }

                    PrintBoard(); // Sørger for at boardet altid overrrider en error, så layout forbliver identisk.

                    // Her ved vi at kolonnenummeret brugeren har indtastet er valid
                    // og vi angiver kolonnenummeret som parameter i SetField() funktionen, som sætter spillerens spillebrik
                    // som er currentPiece. Den tjekker hvilken spillers tur det er og sætter den afhængig af dette.

                    if (!SetField(colNum, currentPiece)) // Hvis der returneres false, starter while loopet forfra
                                                         // Hvilket betyder at noget spilleren har indtastet ikke er valid
                    {
                        continue;
                    }

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
                    Console.Clear(); // Clear konsol, fjerner tidligere input.
                    PrintBoard(); // Printer board ud igen.
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Ugyldigt input. Indtast et tal mellem 0 og 6.");
                    Console.ResetColor();
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
            // Returnerer true eller false, som while loopet modtager.

            bool SetField(int col, string piece)
            {
                if (board[0, col] != ".") // hvis række 0, col[i] != "." så må det betyde at der ikke er flere ledige pladser.
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Der er ikke flere ledige pladser i kolonne {col}");
                    Console.ResetColor();
                    return false; // returnere false
                }

                for (int row = board.GetLength(0) - 1; row >= 0; row--) // Der loppes gennem rækkerne. 
                                                                        // Vi får længden og minuser med -1 for, at få det korrekte index i board.
                                                                        // Derefter lopper vi fra bunden af, da brikkerne skal fylde op fra bunden.
                {
                    if (board[row, col] == ".") // Vi ser om positionen vi er ved, board[row, col]
                    {
                        board[row, col] = piece; // indeholder et "." som placeholder, hvis den gør erstatter vi den med en spiller brik.
                        return true; // og vi bryder ud af loopet.
                    }
                }
                return false;
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
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Du kan til en hver tid slutte spillet ved at skrive 000");

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
