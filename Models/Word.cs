using System.ComponentModel.DataAnnotations;

namespace Mordle;

public class Word
{
    [Key]
    public int Id { get; set; }
    public string word { get; set; }
    public int difficulty { get; set; }

    public Word(int Id, string word, int difficulty = 1)
    {
        this.Id = Id;
        this.word = word;
        this.difficulty = difficulty;
    }
}