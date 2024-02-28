using CRM_system_for_training_centers.Interfaces;
using CRM_system_for_training_centers.Models.AddStudentToCourse;
using CRM_system_for_training_centers.Models.Course;
using CRM_system_for_training_centers.Models.Users;
using CRM_system_for_training_centers.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Display
{
    public class AddStudentToCourseMenu
    {
        private readonly AddStudentService _addStudentService;
        private readonly CourseService _courseService;
        private readonly UserService _userService;
        public AddStudentToCourseMenu(AddStudentService addStudentService,CourseService courseService, UserService userService)
        {
            _addStudentService = addStudentService;
            _courseService = courseService;
            _userService = userService;
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

            var addStudent = new AddStudentCreationModel()
            {
                CourseId = CourseId,
                StudentId = StudentId
            };

            try
            {
                var category = _addStudentService.CreateAsync(addStudent).Result;
                AnsiConsole.Markup("[orange3] Registration completed successfully![/]\n");
                var table = new Table();

                table.AddColumn("[slateblue1]Id[/]");
                table.AddColumn("[slateblue1]CourseId[/]");
                table.AddColumn("[slateblue1]StuudentId[/]");

                table.AddRow(category.Id.ToString(), category.CourseId.ToString(), category.StudentId.ToString());
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

            Console.Clear();
            Console.Write("Enter AddStudentToCourse Id :");
            long id;
            while (!long.TryParse(Console.ReadLine(), out id) || id < 0)
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter AddStudentToCourse Id :");
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

            var addStudent = new AddStudentUpdateModel()
            {
                CourseId = CourseId,
                StudentId = StudentId
            };

            try
            {
                var category = _addStudentService.UpdateAsync(id, addStudent).Result;
                AnsiConsole.Markup("[orange3] Updated successfully![/]\n");
                var table = new Table();

                table.AddColumn("[slateblue1]Id[/]");
                table.AddColumn("[slateblue1]CourseId[/]");
                table.AddColumn("[slateblue1]StuudentId[/]");

                table.AddRow(category.Id.ToString(), category.CourseId.ToString(), category.StudentId.ToString());
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
            Console.Write("Enter AddStudentToCourse Id to delete :");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter AddStudentToCourse Id to delete :");
            }
            try
            {
                _addStudentService.DeleteAsync(id);
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
            Console.Write("Enter CourseId :");
            long CourseId;
            while (!long.TryParse(Console.ReadLine(), out CourseId) || CourseId < 0)
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter CourseId :");
            }
             var users = await _addStudentService.GetAllAsync(CourseId);


            if (users != null)
            {
                var category = _courseService.GetByIdAsync(CourseId).Result;
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

                var table1 = new Table();

                table1.AddColumn("[slateblue1]Id[/]");
                table1.AddColumn("[slateblue1]FirstName[/]");
                table1.AddColumn("[slateblue1]LastName[/]");
                table1.AddColumn("[slateblue1]Email[/]");
                table1.AddColumn("[slateblue1]Role[/]");

                var students = await _userService.GetAllAsync();
                foreach (var item in students)
                {
                    foreach (var user in users)
                    {
                       if (item.Id == user.StudentId)
                       {
                           table1.AddRow(item.Id.ToString(), item.FirstName, item.LastName, item.Email, item.Role.ToString());
                       }
                        
                    }
                }

                AnsiConsole.Write(table1);
                Console.WriteLine("Enter any keyword to continue");
                Console.ReadKey();
                Console.Clear();



                //AnsiConsole.Write(table);
                //Console.WriteLine("Enter any keyword to continue");
                //Console.ReadKey();
                //Console.Clear();
            }
        }

        public async void Display()
        {
            bool circle = true;
            while (circle)
            {
                var category = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[darkorange3_1]-- Welcome to UserMenu --[/]")
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
}
