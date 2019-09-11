using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using System.Web;

namespace UpdateTranslations
{
  /**
   * The purpose of this program is to update CKEditor translations.  This program will use the CKEditorMapping.csv file to map the file name of the .js translation file to the TM langcd.  For each translation file, the given translations will be updated.
   * @param args - The arguments for the program will be as follows:
   * 1) Path to the CKEditorMapping.csv file
   * 2) Path to the {JS Variable}.csv file.  The JS Variable is the key to update.  The file should contain 2 columns one for the lngcd and one for the translation.
   * 3) Path to the translation folder for the files to update
   * 
   * */
  class Program
  {
	static void Main(string[] args)
	{
	  try
	  {


		string mappingPath = "";
		string translationPath = "";
		string translationFolderPath = "";

		if (args.Length > 0)
		  mappingPath = args[0];
		if (args.Length > 1)
		  translationPath = args[1];
		if (args.Length > 2)
		  translationFolderPath = args[2];

		bool isGood = false;
		if (string.IsNullOrEmpty(mappingPath))
		  Console.WriteLine("Mapping File Path not provided.");
		else if (string.IsNullOrEmpty(translationPath))
		  Console.WriteLine("Translation File Path not provided.");
		else if (string.IsNullOrEmpty(translationFolderPath))
		  Console.WriteLine("CKEditor Translation Folder Path not provided.");
		else
		  isGood = true;
		if (!isGood)
		  return;


		string jsVariable = "";

		Dictionary<string, int> cultureToLangCdMap = new Dictionary<string, int>();
		Dictionary<int, string> langCdToTranslationMap = new Dictionary<int, string>();

		//Parse the mapping file
		using (TextFieldParser csvParser = new TextFieldParser(mappingPath))
		{
		  csvParser.CommentTokens = new string[] { "#" };
		  csvParser.SetDelimiters(new string[] { "," });
		  csvParser.HasFieldsEnclosedInQuotes = true;

		  // Skip the row with the column names
		  csvParser.ReadLine();

		  while (!csvParser.EndOfData)
		  {
			// Read current line fields, pointer moves to the next line.
			string[] fields = csvParser.ReadFields();
			string cultureCode = fields[0];
			int langCd = 0;
			if (fields[1] != "n/a")
			{
			  int.TryParse(fields[1], out langCd);
			  cultureToLangCdMap.Add(cultureCode, langCd);
			}
		  }
		}

		//Parse the translation file
		using (TextFieldParser csvParser = new TextFieldParser(translationPath))
		{
		  csvParser.CommentTokens = new string[] { "#" };
		  csvParser.SetDelimiters(new string[] { "," });
		  csvParser.HasFieldsEnclosedInQuotes = true;

		  while (!csvParser.EndOfData)
		  {
			// Read current line fields, pointer moves to the next line.
			string[] fields = csvParser.ReadFields();


			int langCd = -1;
			int.TryParse(fields[0], out langCd);
			if (langCd != -1)
			  langCdToTranslationMap.Add(langCd, fields[1]);
		  }
		}


		DirectoryInfo d = new DirectoryInfo(translationFolderPath);

		FileInfo fl = new FileInfo(translationPath);
		char[] seperator = { '.' };
		jsVariable = fl.Name.Split(seperator)[0];

		foreach (FileInfo file in d.GetFiles("*.js"))
		{
		  StreamReader reader = file.OpenText();
		  string translationText = reader.ReadToEnd();
		  reader.Close();

		  string cultureCode = file.Name.Split(seperator)[0];
		  int langCd = -1;
		  if (cultureToLangCdMap.ContainsKey(cultureCode))
		  {
			langCd = cultureToLangCdMap[cultureCode];
		  }

		  if (langCd == 0)
			continue;//Nothing to do for english.

		  string translation = "";
		  if (langCd != -1 && langCdToTranslationMap.ContainsKey(langCd))
			translation = langCdToTranslationMap[langCd];

		  {
			string missingText = " // MISSING";
			int indexVariableStart = translationText.IndexOf(jsVariable + ":");
			int indexEndOfLine = translationText.IndexOf("\r\n", indexVariableStart);
			int indexMissingString = translationText.IndexOf(missingText, indexVariableStart);
			if (langCd != -1 && !string.IsNullOrEmpty(translation))
			{
			  //Find the field in the JS and replace with the new translation
			  int indexStringStart = translationText.IndexOf("'", indexVariableStart) + 1;
			  int indexStringEnd = translationText.IndexOf("'", indexStringStart);
			  translationText = translationText.Remove(indexStringStart, indexStringEnd - indexStringStart);
			  translationText = translationText.Insert(indexStringStart, HttpUtility.JavaScriptStringEncode(translation));

			  //Remove the missing comment if present
			  if ((indexMissingString > indexVariableStart && indexMissingString < indexEndOfLine))
				translationText = translationText.Remove(indexMissingString, indexEndOfLine - indexMissingString);
			}
			else//Find the text and put in the missing comment
			{
			  if (!(indexMissingString > indexVariableStart && indexMissingString < indexEndOfLine))
				translationText = translationText.Insert(indexEndOfLine, missingText);
			}
		  }

		  using (Stream writer = File.Open(file.FullName, FileMode.Create))
		  {
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			writer.Write(uTF8Encoding.GetBytes(translationText), 0, uTF8Encoding.GetByteCount(translationText));
			writer.Flush();
			writer.Close();
		  }
		}
	  }
	  catch (Exception e)
	  {
		Console.WriteLine(e.Message);
		Console.WriteLine(e.StackTrace);
	  }
	}
  }
}
