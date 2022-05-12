using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

class Programm
{
    static void AffichageListe(List<string> L)
    {
        for (int k = 0; k < L.Count; k++)
        {
            Console.Write(L[k] + " ");
        }
    }
    static void Lecteur(string Emplacement)
    {
        StreamReader Lecture = new StreamReader(Emplacement);
        int cpt = 0;
        while (Lecture.Peek() > 0)
        {
            List<string> L = new List<string>();
            char c = ';';
            string[]s=Lecture.ReadLine().Split(c);
            for(int k = 0; k < s.Length; k++)
            {
                L.Add(s[k]);
            }
            Console.WriteLine("Client n°" + (cpt + 1) + " : ");
            Console.WriteLine("Nom : "+L[0]+"\n"+"Prénom : "+L[1]+"\n"+"Age : "+L[2]+"\n"+"Numéro Permis : "+L[3]+"\n"+"Rue : "+L[4]+"\n"+"Ville : "+L[5]+"\n");
            cpt += 1;
            Console.WriteLine("Affichege de la liste des datas : " + "\n");
            AffichageListe(L);
            Console.WriteLine();
        }
        Lecture.Close();
    }
    static void Ecriture(string Emplacement)
    {
        StreamWriter Fichier = new StreamWriter(Emplacement, true);
        StreamReader Lecture = new StreamReader("D:\\Ecole\\Informatique\\BDD\\TD6\\TD6\\bin\\Debug\\clients.csv");
        while(Lecture.Peek() > 0)
        {
            string[] s = Lecture.ReadLine().Split(new char[] { ';' });
            for(int k=0; k<s.Length; k++)
            {
                Fichier.Write(s[k] + ";");
            }
            Fichier.WriteLine("");
        }
        Lecture.Close();
        Fichier.Close();
    }
    static void EcritureLigne(string Ligne, string Emplacement)
    {
        StreamWriter Fichier = new StreamWriter(Emplacement, true);
        Fichier.WriteLine(Ligne);
        Fichier.Close();
    }
    static void Main()
    {
        Lecteur("D:\\Ecole\\Informatique\\BDD\\TD6\\TD6\\bin\\Debug\\clients.csv");
        Ecriture("D:\\Ecole\\Informatique\\BDD\\TD6\\TD6\\bin\\Debug\\clients.txt");
        string ajout = "Jouvet;Louis;80;55555;rue du vent;Paris";
        EcritureLigne(ajout,"D:\\Ecole\\Informatique\\BDD\\TD6\\TD6\\bin\\Debug\\clients.txt");
        Console.ReadKey();
    }
}