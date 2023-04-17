
using TextDictionary;

Console.WriteLine("Enter path:");
string? path = Utilities.EnterPath();
await Utilities.CreateAndSaveFile(path);
Console.WriteLine($"Done!Check your dictionary here {Utilities.GetNewPath(path)} \nPress any key to exit.");
Console.ReadKey();