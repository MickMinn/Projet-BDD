using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

static double Moyenne(List<int> L)
{
    double S = 0;
    for (int k = 0; k < L.Count; k++)
    {
        S += L[k];
    }
    S = S / L.Count;
    return S;
}

int n = 0;
string variables = "";
string tables = "";
string conditions = "";
string orderby = "";
string groupby = "";
string requete = "";
string reponseManu = "";

Console.WriteLine("Souhaitez vous saisir une requete SQL ? (Tapez 'OUI' ou 'NON')");
string reponse = Console.ReadLine().ToLower();
if (reponse == "non")
{
    Console.WriteLine("Au revoir");
}
if (reponse == "oui")
{
    Console.WriteLine("Vous pouvez vous impregner des requetes 1 à 6 saisies dans le code ci-desus");
    Console.WriteLine("1-Saisir la requête manuellement");
    Console.WriteLine("2-Saisir une requete simple");
    Console.WriteLine("3-Saisir une requete avec order by");
    Console.WriteLine("4-Saisir une requete avec group by");
    Console.WriteLine("5-Ajouter une colonne");
    Console.WriteLine("6-Supprimer une colonne");
    Console.WriteLine("7-Modifier une colonne");
    Console.WriteLine("8-Mode Démo");
    Console.WriteLine("9-Module Statistiques");
    Console.WriteLine("10-Quitter");
    Console.WriteLine("Faites votre choix");
    n = Convert.ToInt32(Console.ReadLine());
    switch (n)
    {
        case 1:
            Console.WriteLine("\n" + "------Requete Manuelle------" + "\n");
            Console.WriteLine("Saisissez manuellement votre requête");
            reponseManu = Console.ReadLine();
            Console.WriteLine("\n" + "------Fin Requete Manuelle------" + "\n");
            break;
        case 2:
            Console.WriteLine("\n" + "------Requete Simple------" + "\n");
            Console.WriteLine("Entrez les variables à afficher");
            variables = Console.ReadLine();
            Console.WriteLine("Entrez les tables sur lesquels effectuer la recherche");
            tables = Console.ReadLine();
            Console.WriteLine("Entrez les conditions de recherche");
            conditions = Console.ReadLine();
            Console.WriteLine("\n" + "------Fin Requete Simple------" + "\n");
            break;
        case 3:
            Console.WriteLine("\n" + "------Requete Avec Order By------" + "\n");
            Console.WriteLine("Entrez les variables à afficher");
            variables = Console.ReadLine();
            Console.WriteLine("Entrez les tables sur lesquels effectuer la recherche");
            tables = Console.ReadLine();
            Console.WriteLine("Entrez les conditions de recherche");
            conditions = Console.ReadLine();
            Console.WriteLine("Entrez l'ordre d'affichage des résultats");
            orderby = Console.ReadLine();
            Console.WriteLine("\n" + "------Fin Requete Avec Order By------" + "\n");
            break;
        case 4:
            Console.WriteLine("\n" + "------Requete Avec Group By------" + "\n");
            Console.WriteLine("Entrez les variables à afficher");
            variables = Console.ReadLine();
            Console.WriteLine("Entrez les tables sur lesquels effectuer la recherche");
            tables = Console.ReadLine();
            Console.WriteLine("Entrez les conditions de recherche");
            conditions = Console.ReadLine();
            Console.WriteLine("Entrez la classification des résultats");
            groupby = Console.ReadLine();
            Console.WriteLine("\n" + "------Fin Requete Avec Group By------" + "\n");
            break;
        case 5:
            Console.WriteLine("\n" + "------Requete Manuelle------" + "\n");
            Console.WriteLine("Saisissez votre requête (de la forme : INSERT INTO proprietaire (idP,adresse) VALUES (405, ’Paris’);");
            reponseManu = Console.ReadLine();
            Console.WriteLine("\n" + "------Fin Requete Manuelle------" + "\n");
            break;
        case 6:
            Console.WriteLine("\n" + "------Requete Manuelle------" + "\n");
            Console.WriteLine("Saisissez votre requête (de la forme : DELETE FROM client WHERE nomC = ’DURAND’;");
            reponseManu = Console.ReadLine();
            Console.WriteLine("\n" + "------Fin Requete Manuelle------" + "\n");
            break;
        case 7:
            Console.WriteLine("\n" + "------Requete Manuelle------" + "\n");
            Console.WriteLine("Saisissez votre requête (de la forme : UPDATE clients SET prenomC = ’toto’ WHERE nomC =’Durand’;");
            reponseManu = Console.ReadLine();
            Console.WriteLine("\n" + "------Fin Requete Manuelle------" + "\n");
            break;
        case 8:
            bool rep = true;
            while (rep == true)
            {
                Console.WriteLine("1-Nombre de clients");
                Console.WriteLine("2-Noms des clients avec le cumul de toutes ses commandes en euros");
                Console.WriteLine("3-Liste des produits ayant une quantité en stock <= 2");
                Console.WriteLine("4-Nombres de pièces et/ou vélos fournis par fournisseur.");
                Console.WriteLine("5-Export en XML / JSON d’une table");
                Console.WriteLine("6-Quitter");
                Console.WriteLine("Faites votre choix !!!");
                int repNbr = Convert.ToInt32(Console.ReadLine());
                if (repNbr == 1)
                {
                    string C1 = Commande("select count(ClientParticulier.IdClient) from ClientParticulier;");
                    string C2 = Commande("select count(ClientPro.IdBoutique) from ClientPro;");
                    C1.Substring(0, C1.Length - 1);
                    C2.Substring(0, C2.Length - 1);
                    int resultat = Convert.ToInt32(C1)+ Convert.ToInt32(C2);
                    Console.WriteLine("\n"+"Nombre de clients : " + resultat+"\n");
                }
                if (repNbr == 2)
                {
                    Console.WriteLine("\n" + "-------Dépenses Bicyclette-------"+"\n");
                    List<string[]> L1=Commandetab("select ClientParticulier.NomC,ClientParticulier.PrenomC,sum(CommandeB.QuantiteB*Bicyclette.Prix) from Bicyclette natural join CommandeB natural join ClientParticulier group by ClientParticulier.IdClient;");
                    Console.WriteLine("\n" + "-------Dépenses Pièces-------" + "\n");
                    List<string[]> L2=Commandetab("select ClientParticulier.NomC,ClientParticulier.PrenomC,sum(CommandeP.QuantiteP*Pieces.PrixU) from Pieces natural join CommandeP natural join ClientParticulier group by ClientParticulier.IdClient;");
                    //string[] tabRes = new tab
                    List<string[]> ResultatSomme = new List<string[]>();
                    for (int k1=0; k1<L1.Count(); k1++)
                    {
                        int RS = Convert.ToInt32(L1[k1][2]);
                        for (int k2=0;k2<L2.Count(); k2++)
                        {
                            if (L1[k1][0] == L2[k2][0] && L1[k1][1]==L2[k2][1])
                            {
                                RS+=Convert.ToInt32(L2[k2][2]);
                            }
                            
                        }
                        ResultatSomme.Add(new string[]{ L1[k1][0],L1[k1][1], Convert.ToString(RS)});
                    }
                    for (int k1=0;k1<L2.Count(); k1++)
                    {
                        for (int k2=0;k2<ResultatSomme.Count();k2++)
                        {
                            if (!(ResultatSomme[k2][0] == L2[k1][0] && ResultatSomme[k2][1] == L2[k1][1]))
                            {
                                ResultatSomme.Add(L2[k1]);
                            }
                        }
                    }
                    for(int k = 0; k < ResultatSomme.Count(); k++)
                    {
                        foreach (string l in ResultatSomme[k])
                        {
                            Console.Write(l+ " ");
                        }
                        Console.WriteLine("");
                    }
                }
                if (repNbr == 3)
                {
                    Console.WriteLine("-------Bicyclettes-------");
                    CommandeListe("select Bicyclette.NomBicyclette,Stock.Quantite from Stock natural join Bicyclette where Stock.TypeProduit='Bicyclette' and Stock.IdProduitS=Bicyclette.IdProduitB and Stock.Quantite<=2;");
                    Console.WriteLine("\n"+"-------Pièces-------");
                    CommandeListe("select Pieces.DescPieces,Stock.Quantite from Stock natural join Pieces where Stock.TypeProduit='Pieces' and Stock.IdProduitS=Pieces.IdProduitP and Stock.Quantite<=2;");
                }
                if (repNbr == 4)
                {
                    reponseManu = "";
                }
                if (repNbr == 5)
                {
                    reponseManu = "";
                }
                if (repNbr == 6)
                {
                    Console.WriteLine("Fin de la Démo");
                    rep = false;
                }
            }
            break;
        case 9:

            break;
        case 10:
            Console.WriteLine("Au revoir");
            break;
    }
}
Console.WriteLine();

MySqlConnection maConnexion = null;
try
{
    string connexionString = "SERVER=localhost;PORT=3306;" + "DATABASE=VeloMax;" + "UID=root;PASSWORD=Liban123?";
    maConnexion = new MySqlConnection(connexionString);
    maConnexion.Open();
}
catch (MySqlException e)
{
    Console.WriteLine("ErreurConnexion : " + e.ToString());
    return;
}

if (n == 1)
{
    requete = reponseManu;
}
if (n == 2)
{
    requete = "select " + variables + " from " + tables + " where " + conditions + ";";
}
if (n == 3)
{
    requete = "select " + variables + " from " + tables + " where " + conditions + " order by " + orderby + ";";

}
if (n == 4)
{
    requete = "select " + variables + " from " + tables + " where " + conditions + " group by " + groupby + ";";
}
if (n == 5)
{
    requete = reponseManu;
}
if (n == 6)
{
    requete = reponseManu;
}
if (n == 7)
{
    requete = reponseManu;
}
if (n == 8)
{
    requete = reponseManu;
}
if (n == 9)
{
    requete = reponseManu;
}

MySqlCommand command1 = maConnexion.CreateCommand();
command1.CommandText = requete;
MySqlDataReader Reader = command1.ExecuteReader();
string[] valueString = new string[Reader.FieldCount];

while (Reader.Read())
{
    for (int k = 0; k < Reader.FieldCount; k++)
    {
        Console.Write(Reader.GetName(k) + " : ");
        valueString[k] = Reader.GetValue(k).ToString();
        Console.WriteLine(valueString[k]);
    }
    Console.WriteLine();
}

static MySqlConnection connexion()
{

    MySqlConnection maConnexion = null;
    try
    {
        string connexionString = "SERVER=localhost;PORT=3306;" + "DATABASE=VeloMax;" + "UID=root;PASSWORD=Liban123?";
        //Console.WriteLine(connexionString);
        maConnexion = new MySqlConnection(connexionString);
        maConnexion.Open();
        return maConnexion;
    }
    catch (MySqlException e)
    {
        Console.WriteLine(" ErreurConnexion : " + e.ToString());
        return maConnexion;
    }
}
static string Commande(string Texte_de_la_commande) // Permet de faire une commande SQL
{
    MySqlConnection Connect = connexion();
    MySqlCommand command = Connect.CreateCommand();
    command.CommandText = Texte_de_la_commande;

    MySqlDataReader reader = command.ExecuteReader();
    string currentRowAsString = "";
    while (reader.Read())                           // parcours ligne par ligne
    {
        currentRowAsString = "";
        for (int i = 0; i < reader.FieldCount; i++)    // parcours cellule par cellule
        {
            string valueAsString = reader.GetValue(i).ToString();  // recuperation de la valeur de chaque cellule sous forme d'un string (voir cependant les differentes methodes disponibles !!)
            if (valueAsString != "")
            {
                currentRowAsString += valueAsString+",";
            }
            if (currentRowAsString.Length > 0)
            {
                currentRowAsString = currentRowAsString.Substring(0, currentRowAsString.Length - 1);
            }
        }
    }
    return currentRowAsString;
}
static string CommandeListe(string Texte_de_la_commande) // Permet de faire une commande SQL
{
    MySqlConnection Connect = connexion();
    MySqlCommand command = Connect.CreateCommand();
    command.CommandText = Texte_de_la_commande;

    MySqlDataReader reader = command.ExecuteReader();
    string currentRowAsString = "";
    string[] ValueString = new string[reader.FieldCount];
    while (reader.Read())                           // parcours ligne par ligne
    {
        currentRowAsString = "";
        for (int k = 0; k < reader.FieldCount; k++)
        {
            Console.Write(reader.GetName(k) + " : ");
            ValueString[k] = reader.GetValue(k).ToString();
            Console.WriteLine(ValueString[k]);
        }
        Console.WriteLine();
    }
    return currentRowAsString;
}
static List<string[]> Commandetab(string Texte_de_la_commande) // Permet de faire une commande SQL
{
    MySqlConnection Connect = connexion();
    MySqlCommand command = Connect.CreateCommand();
    command.CommandText = Texte_de_la_commande;

    MySqlDataReader reader = command.ExecuteReader();
    List<string[]> currentRowAsString = new List<string[]>();
    string[] ValueString = new string[reader.FieldCount];
    while (reader.Read())                           // parcours ligne par ligne
    {
        for (int k = 0; k < reader.FieldCount; k++)
        {
            //Console.Write(reader.GetName(k) + " : ");
            ValueString[k] = (reader.GetValue(k).ToString());
            //Console.WriteLine(ValueString[k]);
        }
        currentRowAsString.Add(ValueString);
        //Console.WriteLine();
    }
    return currentRowAsString;
}
Reader.Close();
command1.Dispose();
