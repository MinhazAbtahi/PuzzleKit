using System.Collections;
using System.Collections.Generic;

public class Puzzle 
{
    public string DatabaseKey { get; set; } //this prop holds the key set by Firebase when the level is POSTed (unique key by Push) -- useable for example when trying to UPDATE specific levels
    public string name;
    public string category;
    public string image;
    public int user_count;

    public Puzzle(string name, string category, string image, int user_count)
    {
        this.name = name;
        this.category = category;
        this.image = image;
        this.user_count = user_count;
    }
}
