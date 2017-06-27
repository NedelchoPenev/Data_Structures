using System;
using System.Collections.Generic;

using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> byEmail;

    private Dictionary<string, SortedSet<Person>> byDomain;
    private Dictionary<string, SortedSet<Person>> byNameAndTown;

    private OrderedDictionary<int, SortedSet<Person>> byAgeRange;

    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> byTownAndAge;

    public PersonCollection()
    {
        this.byEmail = new Dictionary<string, Person>();
        this.byDomain = new Dictionary<string, SortedSet<Person>>();
        this.byNameAndTown = new Dictionary<string, SortedSet<Person>>();
        this.byAgeRange = new OrderedDictionary<int, SortedSet<Person>>();
        this.byTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.byEmail.ContainsKey(email))
        {
            return false;
        }

        var person = new Person(email, name, age, town);
        var domain = this.ExtractDomain(email);
        var nameTown = this.AppendNameTown(name, town);

        this.byEmail.Add(email, person);
        this.byDomain.AppendValueToKey(domain, person);
        this.byNameAndTown.AppendValueToKey(nameTown, person);
        this.byAgeRange.AppendValueToKey(age, person);
        this.byTownAndAge.EnsureKeyExists(town);
        this.byTownAndAge[town].AppendValueToKey(age, person);
        return true;
    }

    private string AppendNameTown(string name, string town)
    {
        return name + town;
    }

    private string ExtractDomain(string email)
    {
        var index = email.IndexOf("@");
        return email.Substring(index + 1);
    }

    public int Count
    {
        get
        {
            return this.byEmail.Count;
        }
    }

    public Person FindPerson(string email)
    {
        if (!this.byEmail.ContainsKey(email))
        {
            return null;
        }

        return this.byEmail[email];
    }

    public bool DeletePerson(string email)
    {
        if (!this.byEmail.ContainsKey(email))
        {
            return false;
        }

        var person = this.byEmail[email];
        var domain = this.ExtractDomain(email);
        var nameTown = this.AppendNameTown(person.Name, person.Town);

        this.byEmail.Remove(email);
        this.byDomain[domain].Remove(person);
        this.byNameAndTown[nameTown].Remove(person);
        this.byAgeRange[person.Age].Remove(person);
        this.byTownAndAge[person.Town][person.Age].Remove(person);
        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.byDomain.GetValuesForKey(emailDomain);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var nameTown = this.AppendNameTown(name, town);
        return this.byNameAndTown.GetValuesForKey(nameTown);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var personsInRange = this.byAgeRange.Range(startAge, true, endAge, true);
        foreach (var byAge in personsInRange)
        {
            foreach (var person in byAge.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (!this.byTownAndAge.ContainsKey(town))
        {
            yield break;
        }

        var personsInRange = this.byTownAndAge[town].Range(startAge, true, endAge, true);

        foreach (var byAge in personsInRange)
        {
            foreach (var person in byAge.Value)
            {
                yield return person;
            }
        }
    }
}
