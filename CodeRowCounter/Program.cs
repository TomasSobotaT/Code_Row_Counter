int countRowsAll = 0;               //počet řádek kodu
int countRowsWithoutEmpty = 0;      // počet řádků bez prázných řádků

string stringExtensions = "";       // string který zadá uživatel obsahující ppřípony souborů
string path = "";                   // cesta ke složce kterou budeme prohledávat
string volba = "";                 // hledat i v podsložkách?

List<string> extensions = new();    // list přípon které budeme hledat
List<string> files = new();         // list souborů které budeme prohledávat

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("***** CODE ROW COUTER *****");
Console.ResetColor();
Console.WriteLine();


//zadání přípon souborů ve  kterých chceme hledat
while (stringExtensions == "")
{
    Console.WriteLine("Zadejte přípony souborů které chcete prohledat. Více přípon oddělte čárkou.");
    stringExtensions = Console.ReadLine();

}
extensions = stringExtensions.Split(',').Select(x => x.Trim()).ToList();
extensions = extensions.Select(x => "*." + x).ToList();
extensions.Write();



//zadání cesty ke složce ve které chce me hledat soubory
Console.WriteLine(@"Zadej složku ve které chcete spočítat řádky kodu v souborech.");
Console.WriteLine(@"Ve formátu: C:\slozkaNaProhledani");
path = Console.ReadLine();

while (path == "" || !Directory.Exists(path))
{
    Console.WriteLine(@"Zadej správnou složku ve formátu: C:\slozkaNaProhledani");
    path = Console.ReadLine();
}

Console.WriteLine();


// včetně podsložek?
Console.WriteLine("Hledat soubory i v podsložkách?");
Console.WriteLine("a - ano / n - ne");
volba = Console.ReadLine().ToLower().Trim();

while (volba != "a" && volba != "n")
{
    Console.WriteLine("Zadej správnou jednu z možností: a - ano / n - ne");
    volba = Console.ReadLine();
}

SearchOption searchOption = new SearchOption();

if (volba == "a")
{
    searchOption = SearchOption.AllDirectories;
}
else
{
    searchOption = SearchOption.TopDirectoryOnly;
}


// najdeme soubory s danými připonami 
foreach (var item in extensions)
{
    files.AddRange(Directory.GetFiles(path, item, searchOption));
}

// spočte řádky kodu
foreach (var item in files)
{
    using (StreamReader sr = new StreamReader(item))
    {
        string radek;
        while ((radek = sr.ReadLine()) != null)
        {
            countRowsAll++;

            if (!string.IsNullOrEmpty(radek))
                {
                 countRowsWithoutEmpty++;
                }
        }
    }

}
Console.WriteLine();
Console.Write("Počet řádků kódu: ");
Console.WriteLine(countRowsAll);
Console.Write("Počet řádků kódu bez prázdných řádek: ");
Console.WriteLine(countRowsWithoutEmpty);

Console.ReadKey();




/// <summary>
/// extension metoda pro vypis do konzole
/// </summary>
public static class ExtensionForConsoleWrite
{
    public static void Write(this List<string> extensions)
    {

        Console.Write("Zadané přípony: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(string.Join(", ", extensions));
        Console.ResetColor();
        Console.WriteLine();

    }
}




