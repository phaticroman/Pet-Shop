using PetShop;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics;
using Azure;

using (var context = new PetContext())
{
    while (true)
    {
        Console.WriteLine("1.See Pet List");
        Console.WriteLine("2.Add A Pet");
        Console.WriteLine("3.See Sales Report");
        Console.WriteLine("4.Feed Pet");
        Console.WriteLine("5.Buy Pet");
        Console.WriteLine("6.Buy Pet");
        Console.WriteLine("7.Add New Cage");
        int input = int.Parse(Console.ReadLine());

        switch (input)
            {
                case 1:
                    petList(context);
                    break;
                case 2:
                    addPet(context);
                    break;
                case 7:
                    createCage(context);
                    break;
                case 4:
                    feeding(context);
                    break;

            }

    }
}
void addPet(PetContext context)
{
    Console.Write("Enter Pet Name:");
    string name = Console.ReadLine();

    Console.Write("Pet Type");
    string type = Console.ReadLine();

    Console.Write("Price : ");
    decimal price = decimal.Parse(Console.ReadLine());

    Console.Write("Quantity : ");
    int quantity = int.Parse(Console.ReadLine());

    var cagelist = context.cages.ToList();

    foreach (var c in cagelist)
    {
        Console.WriteLine($"{c.Id}:{c.Name}");
    }
    Console.WriteLine("Enter Cage Id To Caged The Pet : ");
    int cid = int.Parse(Console.ReadLine());
    var pet = new PetDetails
    {
        Name = name,
        Type = type,
        Quantity = quantity,
        CageId = cid,
        Price = price,
    };
    context.petDetails.Add(pet);
    context.SaveChanges();
    Console.WriteLine("Pet Added To Store SuccessFully");
}


void petList(PetContext context)
{
    var petlist = context.petDetails.ToList();
    IEnumerable<PetDetails> pet = Enumerable.Empty<PetDetails>();

    Console.WriteLine("                          Roman's Pet Shop                           ");
    Console.WriteLine("1.Dog");
    Console.WriteLine("2.Cat");
    Console.WriteLine("3.Bird");
    Console.WriteLine("4.Fish");

    int input = int.Parse(Console.ReadLine());

    if (input==1)
    {
        pet = petlist.Where(d => d.Type == "Dog");
    }
    else if (input == 2)
    {
        pet = petlist.Where(c => c.Type == "Cat");
    }
    else if (input == 3)
    {
         pet = petlist.Where(b => b.Type == "Bird");
    }
    else if (input == 4)
    {
        pet = petlist.Where(f => f.Type == "Fish");
    }
    else
    {
        Console.WriteLine("Invalid Input");
    }
    foreach (var p in pet)
    {
        Console.WriteLine($"ID: {p.Id}, Name: {p.Name}");
    }

    Console.Write("Enter ID To Buy : ");

}


void cageList(PetContext context)
{
    var cagelist = context.cages.ToList();

    foreach (var c in cagelist)
    {
        Console.WriteLine($"{c.Id}:{c.Name} -> {c.Capacity}");
    }
}

void createCage(PetContext context)
{
    bool yn = false;
    Console.Write("Enter Cage Name : ");
    string name = Console.ReadLine();
    Console.Write("Enter Cage Capacity : ");
    int count = int.Parse(Console.ReadLine());
    Console.Write("Is it a Aquarium : Yes or No ");
    string comment = Console.ReadLine();
    if (comment=="Yes")
    {
        yn = true;
    }
    var cage = new Cage
    {
        Name = name,
        Capacity = count,
        IsAquarium = yn,

    };
    context.cages.Add(cage);
    context.SaveChanges();
    Console.WriteLine("Cage Added SuccessFull");
}

void feeding(PetContext context)
{
    var cage = context.cages.ToList();
    foreach(var c in cage)
    {
        Console.WriteLine($"{c.Id}:{c.Name}");
    }

    Console.Write("Enter Cage Id To Feed : ");
    int cid = int.Parse(Console.ReadLine());
    Console.Write("Feed Name : ");
    string name = Console.ReadLine();
    var feed = new FeedSchedule
    {
        Food = name,
        CageId = cid,
        Time = DateTime.Now
    };
    context.FeedSchedules.Add(feed);
    context.SaveChanges();
    Console.WriteLine("Feeding Successfull");
}