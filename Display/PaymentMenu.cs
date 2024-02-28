using CRM_system_for_training_centers.Interfaces;
using CRM_system_for_training_centers.Models.AddStudentToCourse;
using CRM_system_for_training_centers.Models.Payment;
using CRM_system_for_training_centers.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Display;

public class PaymentMenu
{
    private readonly PaymentService _paymentService;

    public PaymentMenu(PaymentService paymentService)
    {
        _paymentService = paymentService;   
    }

    public async void Create()
    {
        Console.Clear();
        Console.Write("Enter CourseId :");
        long CourseId;
        while (!long.TryParse(Console.ReadLine(), out CourseId) || CourseId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter CourseId :");
        }

        Console.Write("Enter StudentId:");
        long StudentId;
        while (!long.TryParse(Console.ReadLine(), out StudentId) || StudentId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter StudentId :");
        }

        Console.Write("Enter amount: ");
        decimal amount;
        while (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter amount: ");
        }

        var payment = new PaymentCreationModel()
        {
            StudentId = StudentId,
            CourseId = CourseId,
            Amount = amount,
            PaymentDate = DateTime.UtcNow
        };

        try
        {
            var category = _paymentService.CreateAsync(payment).Result;
            AnsiConsole.Markup("[orange3] Registration completed successfully![/]\n");
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]CourseId[/]");
            table.AddColumn("[slateblue1]StudentId[/]");
            table.AddColumn("[slateblue1]Amount[/]");
            table.AddColumn("[slateblue1]PaymentData[/]");

            table.AddRow(category.Id.ToString(), category.CourseId.ToString(), category.StudentId.ToString(),
                        category.Amount.ToString(), category.PaymentDate.ToString());
            AnsiConsole.Write(table);
            Console.WriteLine("Enter any keyword to continue");
            Console.ReadKey();
            Console.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Thread.Sleep(800);
        }
    }

    public async void Update()
    {
        Console.Clear();
        Console.Write("Enter Payment to update :");
        long id;
        while (!long.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter Payment to update :");
        }

        Console.Write("Enter CourseId :");
        long CourseId;
        while (!long.TryParse(Console.ReadLine(), out CourseId) || CourseId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter CourseId :");
        }

        Console.Write("Enter StudentId:");
        long StudentId;
        while (!long.TryParse(Console.ReadLine(), out StudentId) || StudentId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter StudentId :");
        }

        Console.Write("Enter amount: ");
        decimal amount;
        while (!decimal.TryParse(Console.ReadLine(), out amount) || amount < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter amount: ");
        }

        var payment = new PaymentUpdateModel()
        {
            StudentId = StudentId,
            CourseId = CourseId,
            Amount = amount
        };

        try
        {
            var category = _paymentService.UpdateAsync(id, payment).Result;
            AnsiConsole.Markup("[orange3] Updated successfully![/]\n");
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]CourseId[/]");
            table.AddColumn("[slateblue1]StudentId[/]");
            table.AddColumn("[slateblue1]Amount[/]");
            table.AddColumn("[slateblue1]PaymentData[/]");

            table.AddRow(category.Id.ToString(), category.CourseId.ToString(), category.StudentId.ToString(),
                        category.Amount.ToString(), category.PaymentDate.ToString());
            AnsiConsole.Write(table);
            Console.WriteLine("Enter any keyword to continue");
            Console.ReadKey();
            Console.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Thread.Sleep(800);
        }
    }

    public async void Delete()
    {
        Console.Clear();
        Console.Write("Enter Payment Id to delete :");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Payment Id to delete :");
        }

        try
        {
            _paymentService.DeleteAsync(id);
            AnsiConsole.Markup("[orange3] Succesful deleted [/]\n"); ;
        }

        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async void GetAll()
    {

        Console.Clear();

        var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .PageSize(10)
            .AddChoices(new[] {
            "View Payments of a single user", "View all user Payments"
            }));
        switch (category)
        {
            case "View Payments of a single user":
                {
                    Console.Clear();

                    Console.Write("Enter StudentId:");
                    long StudentId;
                    while (!long.TryParse(Console.ReadLine(), out StudentId) || StudentId < 0)
                    {
                        Console.WriteLine("Was entered in the wrong format .Try again !");
                        Console.Write("Enter StudentId :");
                    }
                    var table1 = new Table();

                    table1.AddColumn("[slateblue1]Id[/]");
                    table1.AddColumn("[slateblue1]CourseId[/]");
                    table1.AddColumn("[slateblue1]StudentId[/]");
                    table1.AddColumn("[slateblue1]Amount[/]");
                    table1.AddColumn("[slateblue1]PaymentData[/]");

                    var payments = await _paymentService.GetAllAsync(StudentId);
                    foreach (var item in payments)
                    {
                        table1.AddRow(item.Id.ToString(), item.CourseId.ToString(), item.StudentId.ToString(),
                                     item.Amount.ToString(), item.PaymentDate.ToString());
                    }

                    AnsiConsole.Write(table1);
                    Console.WriteLine("Enter any keyword to continue");
                    Console.ReadKey();
                    Console.Clear();
                }
                break;

            case "View all user Payments":
                {
                    Console.Clear();
                    long? studentId = null;

                    var table = new Table();

                    table.AddColumn("[slateblue1]Id[/]");
                    table.AddColumn("[slateblue1]CourseId[/]");
                    table.AddColumn("[slateblue1]StudentId[/]");
                    table.AddColumn("[slateblue1]Amount[/]");
                    table.AddColumn("[slateblue1]PaymentData[/]");

                    var payments = await _paymentService.GetAllAsync(studentId);
                    foreach (var item in payments)
                    {
                        table.AddRow(item.Id.ToString(), item.CourseId.ToString(), item.StudentId.ToString(),
                                     item.Amount.ToString(), item.PaymentDate.ToString());
                    }

                    AnsiConsole.Write(table);
                    Console.WriteLine("Enter any keyword to continue");
                    Console.ReadKey();
                    Console.Clear();
                }

                break;
        }
    
    }

    public async void Display()
    {
        bool circle = true;
        while (circle)
        {
            var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[darkorange3_1]-- Welcome to Payment Menu --[/]")
            .PageSize(10)
            .AddChoices(new[] {
             "Create", "Update", "Delete", "GetAll", "Exit",
            }));
            switch (category)
            {
                case "Create":
                    Create();
                    break;

                case "Update":
                    Update();
                    break;

                case "Delete":
                    Delete();
                    break;

                case "GetAll":
                    GetAll();
                    break;

                case "Exit":
                    circle = false;
                    break;
            }
        }
    }

}
