using PetShop;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Diagnostics;
using Azure;
using System.Security.Cryptography;

using (var context = new PetContext())
{
        User currentUser;
        if (!context.users.Any(u => u.Role == "Owner"))
        {
            Console.Write("Enter your name to register as Owner: ");
            string ownerName = Console.ReadLine();
            var owner = new User
            {
                Name = ownerName,
                Role = "Owner"
            };
            context.users.Add(owner);
            context.SaveChanges();
            Console.WriteLine($"Owner account created: {ownerName}\n");
        }
        Console.Write("Enter your name to log in: ");
        string loginName = Console.ReadLine();
        currentUser = context.users.FirstOrDefault(u => u.Name == loginName);
        if (currentUser == null)
        {
            currentUser = new User
            {
                Name = loginName,
                Role = "Customer"
            };
            context.users.Add(currentUser);
            context.SaveChanges();
            Console.WriteLine($"You are registered as a Customer.\n");
        }
    bool reminder = currentUser.Role == "Owner";
    while (true)
    {
        if (reminder)
        {
            Console.WriteLine("1.See Pet List");
            Console.WriteLine("2.Add A Pet");
            Console.WriteLine("3.See Sales Report");
            Console.WriteLine("4.Feed Pet");
            Console.WriteLine("5.See Inventory");
            Console.WriteLine("7.Add New Cage");
        }
        else
        {
            Console.WriteLine("1.See Pet List");
            Console.WriteLine("6.Sell Pet");

        }
        Console.Write("Select an option: ");
        int input = int.Parse(Console.ReadLine());
        if (reminder)
        {
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
                
                case 3:
                    sealsReport(context);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;

            }
        }
        else
        {
            switch (input)
            {
                case 6:
                    CustomerSellPet(context);
                    break;
                case 1:
                    petList(context);
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }

    }
}
void addPet(PetContext context)
{
    storage(context);
}


void petList(PetContext context)
{
    var petlist = context.petDetails.Where(p=> !p.IsSold).ToList();
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
    if (pet.Count()==0)
    {
        Console.WriteLine("No Pet Remains");
    }
    else
    {
        foreach (var p in pet)
        {
            Console.WriteLine($"ID: {p.Id}, Name: {p.name}, Price: {p.Price}, Available Quantity: {p.Quantity}");
        }
        Console.Write("Enter ID To Action : ");
        int pid = int.Parse(Console.ReadLine());
        Console.WriteLine("Do You Want To Buy it? : 1.Yes     2.No");
        int action = int.Parse(Console.ReadLine());
        if (action == 1)
        {
            Console.Write("Enter Your Name : ");
            string buyername = Console.ReadLine();
            sellPet(context, pid, buyername);
        }
        else
        {
            return;
        }
    }

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
    Console.Write("Enter Cage Name : ");
    string name = Console.ReadLine();
    Console.Write("Enter Cage Capacity : ");
    int count = int.Parse(Console.ReadLine());
    var cage = new Cage
    {
        Name = name,
        Capacity = count,
        
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

void CustomerSellPet(PetContext context)
{
    Console.Write("Enter Your Name : ");
    string cname = Console.ReadLine();

    Console.Write("Pet Type(Cat,Dog,Fish or Bird) : ");
    string pettype = Console.ReadLine();

    Console.Write("Enter Breed Name : ");
    string pname = Console.ReadLine();

    Console.Write("Enter Quantity : ");
    int quantity = int.Parse(Console.ReadLine());

    Console.Write("Enter Price : ");
    decimal price = decimal.Parse(Console.ReadLine());

    var buy = new BuyingRecord
    {
        customerName = cname,
        PetType = pettype,
        petName = pname,
        Quantity = quantity,
        Price = price,
        Date = DateTime.Now,
        IsAddedToStore = false,
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
            Console.WriteLine($"ID: {c.Id}, Pet Type:{c.PetType}, Name: {c.customerName}, price: {c.Price},Quantity: {c.Quantity}");
        }
        Console.Write("Enter ID To Action : ");
        int pid = int.Parse(Console.ReadLine());
        Console.WriteLine("Do You Want To Buy it For Your Store? : 1.Yes     2.No");
        int action = int.Parse(Console.ReadLine());
        if(action==1)
        {
            Console.Write("Enter Selling Price : ");
            decimal price = decimal.Parse(Console.ReadLine());

            foreach (var c in cagelist)
            {
                Console.WriteLine($"ID: {c.Id}, Name: {c.Name}");
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
                name = selectedRecord.petName,
                Quantity = selectedRecord.Quantity,
                Type = selectedRecord.PetType,
                Price = price,
                CageId = cid,
                IsSold = false,
                BuyingRecord = selectedRecord,
            };
            context.petDetails.Add(pet);
            var cage = context.cages.FirstOrDefault(c => c.Id == cid);
            if (cage.Capacity < selectedRecord.Quantity)
            {
                Console.WriteLine("Not enough cage capacity.");
                return;
            }
            cage.Capacity -= selectedRecord.Quantity;
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


void sellPet (PetContext context,int sid,string buyername)
{
    var selectedRecord = context.petDetails.FirstOrDefault(p => p.Id == sid);
    if (selectedRecord == null || selectedRecord.IsSold)
    {
        Console.WriteLine("Invalid or already sold pet.");
        return;
    }
    Console.Write($"Available Quantity: {selectedRecord.Quantity}, How many do you want to buy? ");
    int qtyToBuy = int.Parse(Console.ReadLine());
    if (qtyToBuy <= 0 || qtyToBuy > selectedRecord.Quantity)
    {
        Console.WriteLine("Invalid quantity.");
        return;
    }
    var buyingRecord = context.BuyingRecords.FirstOrDefault(b => b.Id == selectedRecord.BuyingRecordId);
    if (buyingRecord == null)
    {
        Console.WriteLine("Buying record not found.");
        return;
    }
    decimal profitPerUnit = selectedRecord.Price - (buyingRecord.Price / buyingRecord.Quantity);
    decimal totalProfit = profitPerUnit * qtyToBuy;
    var sell = new SellingRecord
    {
        BuyerName = buyername,
        petname = selectedRecord.name,
        Price = selectedRecord.Price,
        Quantity = qtyToBuy,
        Date = DateTime.Now,
        PetType = selectedRecord.Type,
        profit = totalProfit,

    };
    context.SellingRecords.Add(sell);
    selectedRecord.Quantity -= qtyToBuy;
    if (selectedRecord.Quantity == 0)
    {
        selectedRecord.IsSold = true;
        var cage = context.cages.FirstOrDefault(c => c.Id == selectedRecord.CageId);
        if (cage != null)
        {
            cage.Capacity += qtyToBuy;
        }
    }
    context.SaveChanges();
    Console.WriteLine("Buying SuccessFul");
}

void sealsReport(PetContext context)
{
    Console.WriteLine("Sales Report Last 7 Days");
    var sevenDaysAgo = DateTime.Now.AddDays(-7);
    var recentSales = context.SellingRecords.Where(s => s.Date >= sevenDaysAgo).OrderByDescending(s => s.Date).ToList();
    if (!recentSales.Any())
    {
        Console.WriteLine("No sales in the last 7 days.");
        return;
    }

    foreach (var sale in recentSales)
    {
        Console.WriteLine($"Date       : {sale.Date}");
        Console.WriteLine($"Pet Name   : {sale.petname}");
        Console.WriteLine($"Type       : {sale.PetType}");
        Console.WriteLine($"Buyer Name : {sale.BuyerName}");
        Console.WriteLine($"Quantity   : {sale.Quantity}");
        Console.WriteLine($"Sell Price : {sale.Price} per unit");
        Console.WriteLine($"Total Profit: {sale.profit}");
        Console.WriteLine("----------------------------------");

    }
}
