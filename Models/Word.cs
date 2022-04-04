using System.ComponentModel.DataAnnotations;

namespace Mordle;

public class Word
{
    [Key]
    public int Id { get; set; }
    public string word { get; set; }

    public Word(int Id, string word)
    {
        this.Id = Id;
        this.word = word;
    }
}