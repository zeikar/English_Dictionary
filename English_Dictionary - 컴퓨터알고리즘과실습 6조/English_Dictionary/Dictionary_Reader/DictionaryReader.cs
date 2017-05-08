using System;
using System.IO;

public class DictionaryReader
{
   public Word[] words;

    public DictionaryReader()
    {
        try
        {   // Open the text file using a stream reader.
            using (StreamReader sr = new StreamReader("../../DictionaryDB.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                String line = sr.ReadToEnd();
                String[] tempArr;

                // split with delimeters
                tempArr = line.Split(new string[] { " /// ", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                words = new Word[tempArr.Length / 2];

                // parse key and definition
                for (int i = 0; i < tempArr.Length / 2; i++)
                {
                    words[i] = new Word(tempArr[i * 2], tempArr[i * 2 + 1]);
                }
            }
        }
        // error occurs
        catch (Exception e)
        {
            System.Windows.Forms.MessageBox.Show(e.ToString());
        }
    }
}
