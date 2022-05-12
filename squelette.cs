using MySql.Data.MySqlClient;


// Bien vérifier, via Workbench par exemple, que ces paramètres de connexion sont valides !!!
string connectionString = "SERVER=localhost;PORT=3306;DATABASE=TD5;UID=root;PASSWORD=Liban123?;";
MySqlConnection connection = new MySqlConnection(connectionString);
connection.Open();

MySqlCommand command = connection.CreateCommand();
command.CommandText = "SELECT * FROM Clients"; // exemple de requete bien-sur !

MySqlDataReader reader;
reader = command.ExecuteReader();

/* exemple de manipulation du resultat */
while( reader.Read() )                           // parcours ligne par ligne
{
	string currentRowAsString = "";
	for (int i = 0; i < reader.FieldCount; i++)    // parcours cellule par cellule
	{
		string valueAsString = reader.GetValue(i).ToString();  // recuperation de la valeur de chaque cellule sous forme d'une string (voir cependant les differentes methodes disponibles !!)
		currentRowAsString +=  valueAsString + ", ";
	}
	Console.WriteLine(currentRowAsString);    // affichage de la ligne (sous forme d'une "grosse" string) sur la sortie standard
}

connection.Close();