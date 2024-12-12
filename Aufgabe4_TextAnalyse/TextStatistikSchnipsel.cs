﻿namespace Praktikum08
{
    class TextStatistik
    {
        static int Index(char c)
        {
            // Kleinbuchstaben (ohne Umlaute)
            if (c >= 'a' && c <= 'z')
                return c - 'a';
            // Großbuchstaben (ohne Umlaute)
            else if (c >= 'A' && c <= 'Z')
                return c - 'A';
            // alles andere
            else
                return -1;
        }

        static void Main(string[] args)
        {
            // Längeres Beispiel
            string[] lines = {
                "Alan Mathison Turing (1912-1954) war ein britischer Logiker,",
                "Mathematiker, Kryptoanalytiker und Informatiker.",
                "Das von ihm entwickelte Berechenbarkeitsmodell der Turingmaschine ",
                "bildet eines der Fundamente der Theoretischen Informatik.",
                "Während des Zweiten Weltkrieges war er maßgeblich an der Entzifferung",
                "der mit der deutschen Rotor-Chiffriermaschine Enigma verschlüsselten deutschen Funksprüche beteiligt.",
                "Nach ihm benannt sind der Turing Award, die bedeutendste Auszeichnung in der Informatik,",
                "sowie der Turing-Test zum Überprüfen des Vorhandenseins von künstlicher Intelligenz.",
            };
            // Kurzes Beispiel
            //string[] lines = { "Guten Tag", 
            //                   "und guten Morgen" };

            // Demo für Index-Funktion
            foreach (char c in "abc,xyz")
                Console.WriteLine($"{c}: {Index(c)}");
        }
    }
}