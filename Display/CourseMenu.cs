using CRM_system_for_training_centers.Enums;
using CRM_system_for_training_centers.Models.Course;
using CRM_system_for_training_centers.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CRM_system_for_training_centers.Display;

public class CourseMenu
{
    private readonly CourseService _courseService;
    private readonly UserService _userService;

    public CourseMenu(CourseService coursSservice, UserService userService)
    {
        _courseService = coursSservice;
        _userService = userService;
    }

    public async void Create()
    {
        Console.Clear();
        Console.Write("Enter Course Name :");
        string name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Course Name :");
            name = Console.ReadLine();
        }

        Console.Write("Enter Course Description :");
        string descreption = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(descreption))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Course Description :");
            descreption = Console.ReadLine();
        }

        Console.Write("Enter the price of the course :");
        decimal prise;
        while (!decimal.TryParse(Console.ReadLine(), out prise) || prise < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter the price of the course :");
        }

        Console.Write("Enter the course start time (example HH:MM) : ");
        string startTime = Console.ReadLine();
        while (!DateTime.TryParseExact(startTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime soat))
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter the course start time (example HH:MM) : ");
            startTime = Console.ReadLine();
        }
        if (DateTime.TryParseExact(startTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime startSoat))
        {
           startTime = startSoat.ToString("HH:mm");
        }


        Console.Write("Enter the course end time example (example HH:MM) : ");
        string endTime = Console.ReadLine();
        while (!DateTime.TryParseExact(endTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime soat))
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter the course end time example (example HH:MM) : ");
            endTime = Console.ReadLine();
        }
        if (DateTime.TryParseExact(startTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime endSoat))
        {
            startTime = startSoat.ToString("HH:mm");
        }

        Console.Write("Enter Course duration :");
        string duration = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(duration))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Course duration :");
            duration = Console.ReadLine();
        }

        Console.Write("Enter Teacher Id to delete :");
        long teacherId;
        while (!long.TryParse(Console.ReadLine(), out teacherId) || teacherId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Teacher Id to delete :");
        }

        Console.Write("Enter how many days the course will be in a week :");
        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter how many days the course will be in a week :");
        }

        Weekdays[] weekdays = new Weekdays[count];
        int sum =0;
        bool ex = true;
        while (ex)
        {
            Console.WriteLine(" 1.Monday 2.Tuesday 3.Wednesday 4.Thursday 5.Friday 6.Saturday 7.Sunday");
            int choice;
            Console.Write("Enter Weekday:");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 8)
            {
                Console.WriteLine("Was entered in the wrong format .Try agin !");
                Console.Write("Enter Weekday :");
            }
            Weekdays[] value = (Weekdays[])Enum.GetValues(typeof(Weekdays));

            var result = value[choice-1];
            
            if(!weekdays.Contains(result))
            {
                weekdays[sum] = result;
                sum++;
            }

            else
            {
                await Console.Out.WriteLineAsync("Only one lesson can be taught per day");
            }

            if (count == sum)
            {
                ex = false;
            }
        }

        var course = new CourseCreationModel
        {
            Name = name,
            Description = descreption,
            Prise = prise,
            StartTime = startTime,
            EndTime = endTime,
            Duration = duration,
            Teacher_id = teacherId,
            Weekdays = weekdays.ToList()
        };

        try
        {
            var category =_courseService.CreateAsync(course).Result;
            AnsiConsole.Markup("[orange3] Registration completed successfully![/]\n");
            
            var table2 = new Table();
            table2.AddColumn("[slateblue1]Weekday[/]");       

            for (int i = 0; i<count; i++)
            {
                table2.AddRow(category.Weekdays[i].ToString());
            }

            var table = new Table();
            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]Name[/]");
            table.AddColumn("[slateblue1]Descreption[/]");
            table.AddColumn("[slateblue1]Prise[/]");
            table.AddColumn("[slateblue1]StartTime[/]");
            table.AddColumn("[slateblue1]EndTime[/]");
            table.AddColumn("[slateblue1]Duration[/]");
            table.AddColumn("[slateblue1]TeacherId[/]");

            table.AddRow(category.Id.ToString(), category.Name, category.Description, category.Prise.ToString(), category.StartTime,
                         category.EndTime, category.Duration, category.Teacher_id.ToString());
            AnsiConsole.Write(table);
            AnsiConsole.Write(table2);
            Console.WriteLine("Enter any keyword to continue");
            Console.ReadKey();
            Console.Clear();
        }
        catch (Exception eex)
        {
            Console.WriteLine(eex.Message);
            Thread.Sleep(800);
        }
    }

    public async void Update()
    {

        Console.Clear();

        Console.Write("Enter Course to update :");
        long id;
        while (!long.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Course Id to update :");
        }


        Console.Write("Enter Course Name :");
        string name = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Course Name :");
            name = Console.ReadLine();
        }

        Console.Write("Enter Course Description :");
        string descreption = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(descreption))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Course Description :");
            descreption = Console.ReadLine();
        }

        Console.Write("Enter the price of the course :");
        decimal prise;
        while (!decimal.TryParse(Console.ReadLine(), out prise) || prise < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter the price of the course :");
        }

        Console.Write("Enter the course start time (example HH:MM) : ");
        string startTime = Console.ReadLine();
        while (!DateTime.TryParseExact(startTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime soat))
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter the course start time (example HH:MM) : ");
            startTime = Console.ReadLine();
        }
        if (DateTime.TryParseExact(startTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime startSoat))
        {
            startTime = startSoat.ToString("HH:mm");
        }


        Console.Write("Enter the course end time example (example HH:MM) : ");
        string endTime = Console.ReadLine();
        while (!DateTime.TryParseExact(endTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime soat))
        {
            Console.WriteLine("Was entered in the wrong format .Try agin !");
            Console.Write("Enter the course end time example (example HH:MM) : ");
            endTime = Console.ReadLine();
        }
        if (DateTime.TryParseExact(startTime, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime endSoat))
        {
            startTime = startSoat.ToString("HH:mm");
        }

        Console.Write("Enter Course duration :");
        string duration = Console.ReadLine();

        while (string.IsNullOrWhiteSpace(duration))
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Course duration :");
            duration = Console.ReadLine();
        }

        Console.Write("Enter Teacher Id to delete :");
        long teacherId;
        while (!long.TryParse(Console.ReadLine(), out teacherId) || teacherId < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter Teacher Id to delete :");
        }

        Console.Write("Enter how many days the course will be in a week :");
        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter how many days the course will be in a week :");
        }

        Weekdays[] weekdays = new Weekdays[count];
        int sum = 0;
        bool ex = true;
        while (ex)
        {
            Console.WriteLine(" 1.Monday 2.Tuesday 3.Wednesday 4.Thursday 5.Friday 6.Saturday 7.Sunday");
            int choice;
            Console.Write("Enter Weekday:");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 8)
            {
                Console.WriteLine("Was entered in the wrong format .Try agin !");
                Console.Write("Enter Weekday :");
            }
            Weekdays[] value = (Weekdays[])Enum.GetValues(typeof(Weekdays));

            var result = value[choice - 1];

            if (!weekdays.Contains(result))
            {
                weekdays[sum] = result;
                sum++;
            }

            else
            {
                await Console.Out.WriteLineAsync("Only one lesson can be taught per day");
            }

            if (count == sum)
            {
                ex = false;
            }
        }

        var course = new CourseUpdateModel
        {
            Name = name,
            Description = descreption,
            Prise = prise,
            StartTime = startTime,
            EndTime = endTime,
            Duration = duration,
            Teacher_id = teacherId,
            Weekdays = weekdays.ToList()
        };

        try
        {
            var category = _courseService.UpdateAsync(id, course).Result;
            AnsiConsole.Markup("[orange3] Succesful updated [/]\n");
            var table2 = new Table();
            table2.AddColumn("[slateblue1]Weekday[/]");

            for (int i = 0; i < count; i++)
            {
                table2.AddRow(category.Weekdays[i].ToString());
            }

            var table = new Table();
            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]Name[/]");
            table.AddColumn("[slateblue1]Descreption[/]");
            table.AddColumn("[slateblue1]Prise[/]");
            table.AddColumn("[slateblue1]StartTime[/]");
            table.AddColumn("[slateblue1]EndTime[/]");
            table.AddColumn("[slateblue1]Duration[/]");
            table.AddColumn("[slateblue1]TeacherId[/]");

            table.AddRow(category.Id.ToString(), category.Name, category.Description, category.Prise.ToString(), category.StartTime,
                         category.EndTime, category.Duration, category.Teacher_id.ToString());
            AnsiConsole.Write(table);
            AnsiConsole.Write(table2);
            Console.WriteLine("Enter any keyword to continue");
            Console.ReadKey();
            Console.Clear();
        }
        catch (Exception eex)
        {
            Console.WriteLine(eex.Message);
            Thread.Sleep(1000);
        }
    }

    public async void Delete()
    {
        Console.Clear();
        Console.Write("Enter user Id to delete :");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter user Id to delete :");
        }
        try
        {
            _courseService.DeleteAsync(id);
            AnsiConsole.Markup("[orange3] Succesful deleted [/]\n"); ;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async void GetById()
    {
        Console.Clear();
        Console.Write("Enter user Id :");
        int id;
        while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
        {
            Console.WriteLine("Was entered in the wrong format .Try again !");
            Console.Write("Enter user Id :");
        }
        try
        {
            var category = _courseService.GetByIdAsync(id).Result;
            var table2 = new Table();
            table2.AddColumn("[slateblue1]Weekday[/]");

            var count = category.Weekdays.Count;

            for (int i = 0; i < count; i++)
            {
                table2.AddRow(category.Weekdays[i].ToString());
            }

            var table = new Table();
            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]Name[/]");
            table.AddColumn("[slateblue1]Descreption[/]");
            table.AddColumn("[slateblue1]Prise[/]");
            table.AddColumn("[slateblue1]StartTime[/]");
            table.AddColumn("[slateblue1]EndTime[/]");
            table.AddColumn("[slateblue1]Duration[/]");
            table.AddColumn("[slateblue1]TeacherId[/]");

            table.AddRow(category.Id.ToString(), category.Name, category.Description, category.Prise.ToString(), category.StartTime,
                         category.EndTime, category.Duration, category.Teacher_id.ToString());
            AnsiConsole.Write(table);
            AnsiConsole.Write(table2);
            Console.WriteLine("Enter any keyword to continue");
            Console.ReadKey();
            Console.Clear();
        }
        catch (Exception eex)
        {
            Console.WriteLine(eex.Message);
            Thread.Sleep(1000);
        }
    }

    public async void GetAll()
    {
        Console.Clear();

        var courses = await _courseService.GetAllAsync();

        var table = new Table();
        table.AddColumn("[slateblue1]Id[/]");
        table.AddColumn("[slateblue1]Name[/]");
        table.AddColumn("[slateblue1]Descreption[/]");
        table.AddColumn("[slateblue1]Prise[/]");
        table.AddColumn("[slateblue1]StartTime[/]");
        table.AddColumn("[slateblue1]EndTime[/]");
        table.AddColumn("[slateblue1]Duration[/]");
        table.AddColumn("[slateblue1]TeacherId[/]");

        foreach (var category in courses)
        {

        table.AddRow(category.Id.ToString(), category.Name, category.Description, category.Prise.ToString(), category.StartTime,
                     category.EndTime, category.Duration, category.Teacher_id.ToString());
        }
        AnsiConsole.Write(table);
        Console.WriteLine("Enter any keyword to continue");
        Console.ReadKey();
        Console.Clear();

    }

    public async void Display()
    {
        bool circle = true;
        while (circle)
        {
            var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[darkorange3_1]-- Welcome to Course Menu --[/]")
            .PageSize(10)
            .AddChoices(new[] {
            "Create", "Update", "Delete", "GetById" , "GetAll", "Exit",
            }));
            switch (category)
            {
                case "Creatw":
                    Create();
                    break;
                case "Update":
                    Update();
                    break;

                case "Delete":
                    Delete();
                    break;
                case "GetById":
                    GetById();
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
