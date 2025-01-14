﻿static void DrawBoard(int[,] board)
{
    //Erstelle die Spalten-Beschriftung
    for(int i = 0;i<=board.GetLength(1);i++)
    {
        if(i==0)
        {
            Console.Write("   ");
        }
        else
        {
            Console.Write("| "+i+" ");
        }
    }
    Console.Write("\n");
    //Erstelle die Abgrenzung der Beschriftung zur Tabelle (---+---+---)
    for(int i = 0;i<=board.GetLength(1);i++)
    {
        Console.Write("---");
        if(i!=board.GetLength(1))
        {
        Console.Write("+");
        }
    }
    Console.Write("\n");
    //Schreibe die Zeilen auf
    for(int i = 0; i<board.GetLength(0);i++)
    {
        Console.Write(" "+(char)(i+65)+" "); //Am Anfang der Zeile ein Buchstabe
        for(int j = 0;j<board.GetLength(1);j++)
        {
            Console.Write("| ");
            switch (board[i,j])
            {
                case 0: Console.Write("  "); break;

                case 1: Console.Write("X "); break;
                
                case 2: Console.Write("O "); break;
            }

        }
        Console.Write("\n");
    }
}

static bool IsInBounds(int[,] board,int newY,int newX)
{
    bool isInBounds = (newY >= 0 && newY < board.GetLength(0)) &&  //Hilfsvariable um festzustellen ob man den Index out of Range fährt 
                      (newX >= 0 && newX < board.GetLength(1));
    return isInBounds;
}
static bool IsWinner(int[,]board,int lastmovYpos,int lastmovXpos)
{
    int searchfor = board[lastmovYpos,lastmovXpos]; //suche nach einer Reihe von dem typ des letzten Spielzugs
    int newY = lastmovYpos;
    int newX = lastmovXpos;
    
    for(int i = -1; i<=1;i++)
    {                             
        newY = lastmovYpos + i; //Verschachtelte for-schleifen dienen zu Iteration über die 8 Überprüfungsrichtungen
        for(int j = -1; j<=1;j++)   
        {                           
            newX = lastmovXpos + j; 
            if(IsInBounds(board,newY,newX)&&(!(j==0&i==0)))
            {
                if(searchfor == board[newY,newX])
                {
                    for(int k=-1;k<=2;k+=3)
                    {
                        newX = lastmovXpos + k*j;
                        newY = lastmovYpos + k*i;
                        if(IsInBounds(board,newY,newX))
                        {
                            if(searchfor == board[newY,newX])
                            {
                                //(1.Schleifendurchlauf) das Symbol ist mittig in einer dreier reihe
                                //(2.Schleifendurchlauf) das Symbol ist seitlich an einer dreier reihe
                                return true;
                            }
                        }
                    }
                }
            }
        }
    }
    return false; //alle Möglichkeiten wurden erschöpft ohne dass eine Reihe gefunden wurde
}
// Funktion "GetYX" nimmt einen String als Input und gibt ein Array mit Y,X Koordinaten zurück
// Angepasste GetYX-Funktion
static bool GetYX(string input, out int y, out int x, int maxRows, int maxCols)
{
    y = -1;
    x = -1;
    bool lastCharWasNumber = false;
    bool yWasWritten = false;
    bool xWasWritten = false;

    // Schleife durch die Eingabe
    for (int i = 0; i < input.Length; i++)
    {
        // Prüfe, ob aktuelles Zeichen eine Zahl ist (ASCII 48-57 sind 0-9)
        if ((int)input[i] >= 48 && (int)input[i] <= 57)
        {
            if (lastCharWasNumber)
            {
                x = (x * 10) + (input[i] - '0'); // Multipliziere vorherige Zahl mit 10 und addiere neue Ziffer
            }
            else if (!xWasWritten)
            {
                x = input[i] - 49; // Erste Zahl
                lastCharWasNumber = true;
                xWasWritten = true;
            }
        }
        else
        {
            lastCharWasNumber = false; // Kein Zahlenzeichen, Flag zurücksetzen
        }

        // Prüfe, ob aktuelles Zeichen ein Großbuchstabe ist (A-Z: ASCII 65-90)
        if ((int)input[i] >= 65 && (int)input[i] <= 90)
        {
            if (!yWasWritten)
            {
                y = input[i] - 65; // Konvertiere Buchstabe zu Zahl (A=0, B=1, etc.)
                yWasWritten = true;
            }
        }

        // Prüfe, ob aktuelles Zeichen ein Kleinbuchstabe ist (a-z: ASCII 97-122)
        if ((int)input[i] >= 97 && (int)input[i] <= 122)
        {
            if (!yWasWritten)
            {
                y = input[i] - 97; // Konvertiere Buchstabe zu Zahl (a=0, b=1, etc.)
                yWasWritten = true;
            }
        }
    }

    // Prüfen, ob die Eingabe innerhalb des gültigen Bereichs liegt
    if (y >= 0 && y < maxRows && x >= 0 && x < maxCols)
    {
        return true; // Gültige Eingabe
    }
    else
    {
        return false; // Ungültige Eingabe
    }
}

//Erfrage Brett-Dimensionen und Initialisiere Variablen 
Console.Write("Wie viele Spalten soll das Spielbrett haben?: ");
int spalten = Convert.ToInt32(Console.ReadLine());
Console.Write("Wie viele Zeilen soll das Spielbrett haben?: ");
int zeilen = Convert.ToInt32(Console.ReadLine());

int[,] board = new int[zeilen, spalten];
int spieler = 2,movX,movY;

//Hauptschleife des Spiels jeder Schleifendurchlauf symbolisiert einen Zug
do
{
    //Der Spieler wechselt zu Beginn jedes Zuges
    if(spieler == 1)
        spieler = 2;
    else
        spieler = 1;
    
    Console.Clear();
    Console.WriteLine($"Spieler {spieler}, du bist dran!");
    DrawBoard(board);

    // Eingabe abfragen, bis eine gültige Eingabe erfolgt
    bool validInput;
    do
    {
        Console.Write("Gib die Koordinaten deines nächsten Zuges ein (z.B. A1): ");
        string input = Console.ReadLine()!;
        validInput = GetYX(input, out movY, out movX, zeilen, spalten); //Lese aus dem Input die Koordinaten

        //Checke ob die Koordinaten i.O sind
        if (!validInput || !IsInBounds(board, movY, movX) || board[movY, movX] != 0)
        {
            Console.WriteLine("Ungültige Eingabe! Bitte gib eine freie, gültige Position ein.");
            validInput = false; // Flag zurücksetzen
        }

    } while (!validInput);

    // Spielzug setzen
    board[movY, movX] = spieler;

} while (!IsWinner(board, movY, movX)); //Wiederhole falls nicht gewonnen wurde
// Spieler X hat gewonnen, Gebe das Aus
Console.ForegroundColor = ConsoleColor.Green;
Console.Clear();
DrawBoard(board);
Console.WriteLine($"Spieler {spieler} hat gewonnen!");
Console.WriteLine("Drücke eine beliebige Taste, um zu schließen.");
Console.ReadKey();