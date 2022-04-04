using System.Data;

namespace Mordle;

public class Word
{
    public string word;
    public Word(string word)
    {
        this.word = word;
    }

    public void OnModelCreating()
    {

    }
}