using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public class City
{
    public string Name { get; set; }
    public int Population { get; set; }
    public City(string name, int population)
    {
        Name = name;
        Population = population;
    }
}

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public int Height { get; set; }
    public string? Allergies { get; set; }

    public Person(string firstName, string lastName, string city, int height, string? allergies)
    {
        FirstName = firstName;
        LastName = lastName;
        City = city;
        Height = height;
        Allergies = allergies;
    }

    public override string ToString() => $"{FirstName} {LastName} ({City}, {Height}cm)";
}

internal class Program
{
    static int[] numbers = {
        106,104,10,5,117,174,95,61,74,145,77,95,72,59,114,95,61,
        116,106,66,75,85,104,62,76,87,70,17,141,39,199,91,37,139,
        88,84,15,166,118,54,42,123,53,183,95,101,112,26,41,135,70,
        48,59,69,109,93,110,153,178,117,5
    };

    static City[] cities = {
        new City("Toronto", 100200),
        new City("Hamilton", 80923),
        new City("Ancaster", 4039),
        new City("Brantford", 500890),
    };

    static Person[] persons = {
        new Person("Cedric","Coltrane","Toronto",157,null),
        new Person("Hank","Spencer","Peterborough",158,"Sulfa, Penicillin"),
        new Person("Sara","di","29",145,null),
        new Person("Daphne","Seabright","Ancaster",146,null),
        new Person("Rick","Bennett","Ancaster",220,null),
        new Person("Amy","Leela","Hamilton",172,"Penicillin"),
        new Person("Woody","Bashir","Barrie",153,null),
        new Person("Tom","Halliwell","Hamilton",179,"Codeine, Sulfa"),
        new Person("Rachel","Winterbourne","Hamilton",163,null),
        new Person("John","West","Oakville",138,null),
        new Person("Jon","Doggett","Hamilton",194,"Peanut Oil"),
        new Person("Angel","Edwards","Brantford",176,null),
        new Person("Brodie","Beck","Carlisle",157,null),
        new Person("Beanie","Foster","Ancaster",154,"Ragweed, Codeine"),
        new Person("Nino","Andrews","Hamilton",186,null),
        new Person("John","Farley","Hamilton",213,null),
        new Person("Nea","Kobayakawa","Toronto",147,null),
        new Person("Laura","Halliwell","Brantford",146,null),
        new Person("Lucille","Maureen","Hamilton",184,null),
        new Person("Jim","Thoma","Ottawa",173,null),
        new Person("Roderick","Payne","Halifax",58,null),
        new Person("Sam","Threep","Hamilton",199,null),
        new Person("Bertha","Crowley","Delhi",125,"Peanuts, Gluten"),
        new Person("Roland","Edge","Brantford",199,null),
        new Person("Don","Wiggum","Hamilton",189,null),
        new Person("Anthony","Maxwell","Oakville",92,null),
        new Person("James","Sullivan","Delhi",139,null),
        new Person("Anne","Marlowe","Pickering",165,"Peanut Oil"),
        new Person("Kelly","Hamilton","Stoney",84,null),
        new Person("Charles","Andonuts","Hamilton",62,null),
        new Person("Temple","Russert","Hamilton",166,"Sulphur"),
        new Person("Don","Edwards","Hamilton",215,null),
        new Person("Alice","Donovan","Hamilton",167,null),
        new Person("Stone","Cutting","Hamilton",110,null),
        new Person("Neil","Allan","Cambridge",203,null),
        new Person("Cross","Gordon","Ancaster",125,null),
        new Person("Phoebe","Bigelow","Thunder",183,null),
        new Person("Harry","Kuramitsu","Hamilton",210,null)
    };

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1a - Numbers > 80");
            Console.WriteLine("1b - Numbers descending");
            Console.WriteLine("1c - Numbers formatted");
            Console.WriteLine("1d - Count numbers 70 < n < 100");
            Console.WriteLine("2a - Persons with height");
            Console.WriteLine("2b - Transformed names");
            Console.WriteLine("2c - Distinct allergies");
            Console.WriteLine("2d - Tallest person(s)");
            Console.WriteLine("2e - Persons from big cities (>100k)");
            Console.WriteLine("2f - Persons in/not in selected cities");
            Console.WriteLine("3  - Persons to XML");
            Console.WriteLine("x  - Exit");
            Console.Write("Choose option: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1a":
                    var greater80 = numbers.Where(n => n > 80);
                    Console.WriteLine(string.Join(", ", greater80));
                    break;

                case "1b":
                    var descending = numbers.OrderByDescending(n => n);
                    Console.WriteLine(string.Join(", ", descending));
                    break;

                case "1c":
                    var formatted = from n in numbers select $"Number: {n}";
                    Console.WriteLine(string.Join("\n", formatted));
                    break;

                case "1d":
                    var rangeQuery = from n in numbers where n < 100 && n > 70 select n;
                    Console.WriteLine("Count: " + rangeQuery.Count());
                    break;

                case "2a":
                    Console.Write("Enter height: ");
                    int h = int.Parse(Console.ReadLine());
                    var heightQuery = from p in persons where p.Height == h select p;
                    foreach (var p in heightQuery) Console.WriteLine(p);
                    break;

                case "2b":
                    var transformed = persons.Select(p => $"{p.FirstName[0]}. {p.LastName}");
                    Console.WriteLine(string.Join(", ", transformed));
                    break;

                case "2c":
                    var allergies = persons
                        .Where(p => !string.IsNullOrEmpty(p.Allergies))
                        .SelectMany(p => p.Allergies.Split(","))
                        .Select(a => a.Trim())
                        .Distinct();
                    Console.WriteLine(string.Join(", ", allergies));
                    break;

                case "2d":
                    int maxHeight = persons.Max(p => p.Height);
                    var tallest = persons.Where(p => p.Height == maxHeight);
                    foreach (var p in tallest) Console.WriteLine(p);
                    break;

                case "2e":
                    var bigCityPersons = from p in persons
                                         join c in cities on p.City equals c.Name
                                         where c.Population > 100000
                                         select p;
                    foreach (var p in bigCityPersons) Console.WriteLine(p);
                    break;

                case "2f":
                    var selectedCities = new List<string> { "Toronto", "Hamilton", "Brantford" };
                    var inCities = persons.Where(p => selectedCities.Contains(p.City));
                    var notInCities = persons.Where(p => !selectedCities.Contains(p.City));
                    Console.WriteLine("-- In selected cities --");
                    foreach (var p in inCities) Console.WriteLine(p);
                    Console.WriteLine("-- Not in selected cities --");
                    foreach (var p in notInCities) Console.WriteLine(p);
                    break;

                case "3":
                    var xml = new XElement("Persons",
                        persons.Select(p =>
                            new XElement("Person",
                                new XElement("FirstName", p.FirstName),
                                new XElement("LastName", p.LastName),
                                new XElement("City", p.City),
                                new XElement("Height", p.Height),
                                p.Allergies != null ? new XElement("Allergies", p.Allergies) : null
                            )
                        )
                    );
                    Console.WriteLine(xml);
                    break;

                case "x":
                    return;

                default:
                    Console.WriteLine("Unknown option");
                    break;
            }
        }
    }
}
