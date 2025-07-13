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
        Console.WriteLine("5.See Inventory");
        Console.WriteLine("6.Sell Pet");
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
            case 5:
                storage(context);
                break;
            case 6:
                buyingPet(context);
                break;

        }

    }
}
void addPet(PetContext context)
{
    Console.Write("Enter Pet Name:");
    string name = Console.ReadLine();

    Console.Write("Pet Type(Cat,Dog,Fish,Bird) : ");
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

void buyingPet(PetContext context)
{
    Console.Write("Pet Type(Cat,Dog,Fish or Bird) : ");
    string pettype = Console.ReadLine();

    Console.Write("Enter Name : ");
    string name = Console.ReadLine();

    Console.Write("Enter Quantity : ");
    int quantity = int.Parse(Console.ReadLine());

    Console.Write("Enter Price : ");
    decimal price = decimal.Parse(Console.ReadLine());

    var buy = new BuyingRecord
    {
        PetType = pettype,
        Name = name,
        Quantity = quantity,
        Price = price,
    };
    context.BuyingRecords.Add(buy);
    context.SaveChanges();
    Console.WriteLine("Added To storage");

}
void storage (PetContext context)
{
    var cart = context.BuyingRecords.Where(b => !b.IsAddedToStore).ToList();
    var cagelist = context.cages.ToList();
    if (!cart.Any())
    {
        Console.WriteLine("No Pet in Storage");
    }
    else
    {
        foreach (var c in cart)
        {
            Console.WriteLine($"ID: {c.Id}, Pet Type:{c.PetType}, Name: {c.Name}, price: {c.Price},Quantity: {c.Quantity}");
        }
        Console.Write("Enter ID To Action : ");
        int pid = int.Parse(Console.ReadLine());
        Console.WriteLine("Do You Want To Buy it For Your Store? : 1.Yes     2.No");
        int action = int.Parse(Console.ReadLine());
        if(action==1)
        {
            Console.Write("Enter Selling Price : ");
            decimal price = decimal.Parse(Console.ReadLine());

            foreach (var cage in cagelist)
            {
                Console.WriteLine($"ID: {cage.Id}, Name: {cage.Name}");
            }
            Console.WriteLine("Enter Cage Id To Caged The Pet : ");
            int cid = int.Parse(Console.ReadLine());

            var selectedRecord = context.BuyingRecords.FirstOrDefault(b => b.Id == pid);
            if (selectedRecord == null)
            {
                Console.WriteLine("Invalid ID.");
                return;
            }
            var pet = new PetDetails
            {
                Name = selectedRecord.Name,
                Quantity = selectedRecord.Quantity,
                Type = selectedRecord.PetType,
                Price = price,
                CageId = cid
            };
            context.petDetails.Add(pet);
            selectedRecord.IsAddedToStore = true;
            context.SaveChanges();
            Console.WriteLine("Pet Add To Store");
        }

        else if (action==2)
        {
            var petdelete = context.BuyingRecords.Find(pid);
            if (petdelete != null)
            {
                context.BuyingRecords.Remove(petdelete);
                context.SaveChanges();
            }
        }
        else
        {
            Console.WriteLine("Invalid Input");
        }
    }
}