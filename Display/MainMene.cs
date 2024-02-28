using CRM_system_for_training_centers.Enums;
using CRM_system_for_training_centers.Interfaces;
using CRM_system_for_training_centers.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Display;

public class MainMene
{
    private readonly UserService userService;
    private readonly CourseService courseService;
    private readonly PaymentService paymentService;
    private readonly AdmissionOfStudentService admissionOfStudentService; 
    private readonly AddStudentService addstudentService;
    private readonly AttandenseService attandenseService;
    
    private readonly UserMenu userMenu;
    private readonly CourseMenu courseMenu;
    private readonly PaymentMenu paymentMenu;
    private readonly AdmissionOfStudentsMenu addmiddionOfStudentsMenu;
    private readonly AddStudentToCourseMenu addstudentToCourseMenu;
    private readonly AttendanseMenu attendanseMenu;

    public MainMene()
    {
        userService = new UserService();
        courseService = new CourseService(userService);
        paymentService = new PaymentService();
        admissionOfStudentService = new AdmissionOfStudentService();
        addstudentService = new AddStudentService(userService, courseService);
        attandenseService = new AttandenseService();

        userMenu = new UserMenu(userService);
        courseMenu = new CourseMenu(courseService, userService);
        paymentMenu = new PaymentMenu(paymentService);
        addmiddionOfStudentsMenu = new AdmissionOfStudentsMenu(admissionOfStudentService);
        addstudentToCourseMenu = new AddStudentToCourseMenu(addstudentService, courseService, userService);
        attendanseMenu = new AttendanseMenu(attandenseService);
    }




    public async void Main()
    {
    key1:
        bool circle = true;
        while (circle)
        {
            Console.Clear();
            var category = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[darkorange3_1]-- Welcome to Haad CRM --[/]")
            .PageSize(10)
            .AddChoices(new[] {
              "Sign in", "Sign up", "Exit"
            }));
            switch (category)
            {
                case "Sign in":
                    {
                        Console.Write("Enter Email (email@gmail.com):");
                        string email = Console.ReadLine();
                        while (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]{3,}$"))
                        {
                            Console.WriteLine("Was entered in the wrong format .Try again !");
                            Console.Write("Enter Email (email@gmail.com):");
                            email = Console.ReadLine();
                        }

                        string password = AnsiConsole.Prompt(
                       new TextPrompt<string>("Enter password :")
                        .PromptStyle("red")
                        .Secret());

                        var result = userService.GetByEmailPassword(email, password).Result;

                       
                            if (result.Role == Role.Admin)
                            {
                                bool res = true;
                                while (res)
                                {
                                    Console.Clear();
                                    var table = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("[darkorange3_1]-- Welcome to Admin Menu --[/]")
                                    .PageSize(10)
                                    .AddChoices(new[] {
                                      "User Menu", "Create Admin and Reseptions", "Payment Menu", "Course Menu", "Exit"
                                    }));
                                    switch (table)
                                    {
                                    case "User Menu":
                                        userMenu.Display();
                                        break;
                                    
                                    case "Create Admin and Reseptions":
                                        userMenu.CreateAdmin();
                                       
                                        break;
                                    case "Payment Menu":
                                        paymentMenu.Display();
                                        break;
                                  
                                    case "Course Menu":
                                        courseMenu.Display();
                                        break;

                                        case "Exit":
                                            goto key1;
                                    }
                                }
                            }

                            if (result.Role == Role.Receptionist)
                            {
                                bool ex = true;
                                while (ex)
                                {
                                    Console.Clear();
                                    var table = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("[darkorange3_1]-- Welcome to Reseptions Menu --[/]")
                                    .PageSize(10)
                                    .AddChoices(new[] {
                                       "User Menu", "Course Menu", "Add Students of course", "Admission of students", "Payment Menu", "Exit"
                                    }));
                                    switch (table)
                                    {
                                    case "User Menu":
                                        userMenu.Display();
                                        break;

                                    case "Course Menu":
                                        courseMenu.Display();
                                        break;

                                    case "Add Students of course":
                                        addstudentToCourseMenu.Display();
                                        break;

                                    case "Admission of students":
                                        addmiddionOfStudentsMenu.Display();
                                        break;

                                    case "Payment Menu":
                                        paymentMenu.Display();
                                        break;

                                    case "Exit":
                                            goto key1;
                                    }
                                }
                            }

                            if (result.Role == Role.Teacher)
                            {
                                bool ex = true;
                                while (ex)
                                {
                                    Console.Clear();
                                    var table = AnsiConsole.Prompt(
                                    new SelectionPrompt<string>()
                                    .Title("[darkorange3_1]-- Welcome to Teacher Menu --[/]")
                                    .PageSize(10)
                                    .AddChoices(new[] {
                                       "Update data", "Delete data", "View data", "View courses", "View the list of students of courses", "Exit"
                                    }));
                                    switch (table)
                                    {
                                    case "Update data":
                                        userMenu.Update();
                                        break;

                                    case "Delete data":
                                        userMenu.Delete();
                                        break;

                                    case "View data":
                                        userMenu.GetById();
                                        break;

                                    case "View courses":
                                        courseMenu.GetById();
                                        break;

                                    case "View the list of students of courses":
                                        addstudentToCourseMenu.GetAll();
                                        break;
                                    case "Exit":
                                            goto key1;
                                    }
                                }
                            }

                        if (result.Role == Role.Student)
                        {
                            bool ex = true;
                            while (ex)
                            {
                                Console.Clear();
                                var table = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("[darkorange3_1]-- Welcome to Teacher Menu --[/]")
                                .PageSize(10)
                                .AddChoices(new[] {
                                 "User Menu", "Get All Course", "Get By Id Course", "Exit"
                                }));
                                switch (table)
                                {
                                    case "User Menu":
                                        userMenu.Display();
                                        break;

                                    case "Get All Course":
                                        courseMenu.GetAll();
                                        break;

                                    case "Get By Id Course":
                                        courseMenu.GetById();
                                        break;

                                    case "Exit":
                                        goto key1;
                                }
                            }
                        }

                        else
                        {
                            await Console.Out.WriteLineAsync("You are not registered, please register");
                            Thread.Sleep(3000);
                            goto key;
                        }
                    }
                    break;
                case "Sign up":
                key:
                    userMenu.Create();
                    Thread.Sleep(3000);
                    break;
                case "Exit":
                    circle = false;
                    break;
            }
        }
    }
}

