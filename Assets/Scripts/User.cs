using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    public string DatabaseKey { get; set; } //this prop holds the key set by Firebase when the level is POSTed (unique key by Push) -- useable for example when trying to UPDATE specific levels
    public string name;
    public string email;
    public string sex;
    public string dob;
    public string city_residence;

    public User(string name, string email, string sex, string dob, string city_residence)
    {
        this.name = name;
        this.email = email;
        this.sex = sex;
        this.dob = dob;
        this.city_residence = city_residence;
    }
}
