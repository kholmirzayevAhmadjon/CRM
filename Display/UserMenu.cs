using CRM_system_for_training_centers.Enums;
using CRM_system_for_training_centers.Models.Users;
using CRM_system_for_training_centers.Services;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRM_system_for_training_centers.Display
{
    public class UserMenu
    {
        private readonly UserService _userService;
        public UserMenu(UserService userService)
        {
            _userService = userService;
        }

        public async void Create()
        {
            Console.Clear();
            Console.Write("Enter FirstName :");
            string firstName = Console.ReadLine();
            
            while (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter FirstName :");
                firstName = Console.ReadLine();
            }

            Console.Write("Enter LastName :");
            string lastName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter LastName :");
                lastName = Console.ReadLine();
            }

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

            Console.WriteLine("1.Teacher, 2.Student");
            int choice;
            Console.Write("Enter Role:");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 3)
            {
                Console.WriteLine("Was entered in the wrong format .Try agin !");
                Console.Write("Enter Role :");
            }
            Role[] value = (Role[])Enum.GetValues(typeof(Role));

            UserCreationModel user = new UserCreationModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                Role = value[choice-1]
            };
            try
            {
                var category = _userService.CreateAsync(user).Result;
                AnsiConsole.Markup("[orange3] Registration completed successfully![/]\n");
                var table = new Table();

                table.AddColumn("[slateblue1]Id[/]");
                table.AddColumn("[slateblue1]FisrtName[/]");
                table.AddColumn("[slateblue1]LastName[/]");
                table.AddColumn("[slateblue1]Email[/]");
                table.AddColumn("[slateblue1]Role[/]");

                table.AddRow(category.Id.ToString(), category.FirstName, category.LastName, category.Email, category.Role.ToString());
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


        public async void CreateAdmin()
        {
            Console.Clear();
            Console.Write("Enter FirstName :");
            string firstName = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter FirstName :");
                firstName = Console.ReadLine();
            }

            Console.Write("Enter LastName :");
            string lastName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter LastName :");
                lastName = Console.ReadLine();
            }

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

            Console.WriteLine("3.Reseptions, 4.Admin");
            int choice;
            Console.Write("Enter Role:");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 2 || choice > 5)
            {
                Console.WriteLine("Was entered in the wrong format .Try agin !");
                Console.Write("Enter Role :");
            }
            Role[] value = (Role[])Enum.GetValues(typeof(Role));

            UserCreationModel user = new UserCreationModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                Role = value[choice - 1]
            };
            try
            {
                var category = _userService.CreateAsync(user).Result;
                AnsiConsole.Markup("[orange3] Registration completed successfully![/]\n");
                var table = new Table();

                table.AddColumn("[slateblue1]Id[/]");
                table.AddColumn("[slateblue1]FisrtName[/]");
                table.AddColumn("[slateblue1]LastName[/]");
                table.AddColumn("[slateblue1]Email[/]");
                table.AddColumn("[slateblue1]Role[/]");

                table.AddRow(category.Id.ToString(), category.FirstName, category.LastName, category.Email, category.Role.ToString());
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
            Console.Write("Enter user Id to delete :");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter user Id to delete :");
            }
            try
            {
                _userService.DeleteAsync(id);
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
                var category = await _userService.GetByIdAsync(id);
                var table = new Table();

                table.AddColumn("[slateblue1]Id[/]");
                table.AddColumn("[slateblue1]FirstName[/]");
                table.AddColumn("[slateblue1]LastName[/]");
                table.AddColumn("[slateblue1]Email[/]");
                table.AddColumn("[slateblue1]Role[/]");

                table.AddRow(category.Id.ToString(), category.FirstName, category.LastName, category.Email, category.Role.ToString());
                AnsiConsole.Write(table);
                Console.WriteLine("Enter any keyword to continue");
                Console.ReadKey();
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async void GetByEmailPassvordAsyc()
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


            Console.WriteLine("1.Admin, 2.ProjectManager, 3.TeamMember");
            int choice;
            Console.Write("Enter Role:");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 4)
            {
                Console.WriteLine("Was entered in the wrong format .Try agin !");
                Console.Write("Enter Role :");
            }
            Role[] value = (Role[])Enum.GetValues(typeof(Role));

            Role role = value[choice - 1];

            try
            {
                var circl = await _userService.GetByEmailPassword(email, password);
                Console.WriteLine(circl);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }

        public async void GetAll()
        {
            Console.Clear();
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]FirstName[/]");
            table.AddColumn("[slateblue1]LastName[/]");
            table.AddColumn("[slateblue1]Email[/]");
            table.AddColumn("[slateblue1]Role[/]");

            var users = await _userService.GetAllAsync();
            foreach (var item in users)
            {
                table.AddRow(item.Id.ToString(), item.FirstName, item.LastName, item.Email, item.Role.ToString());
            }

            AnsiConsole.Write(table);
            Console.WriteLine("Enter any keyword to continue");
            Console.ReadKey();
            Console.Clear();
        }

        public async void Update()
        {
            Console.Clear();
            Console.Write("Enter user Id to update :");
            long id;
            while (!long.TryParse(Console.ReadLine(), out id) || id < 0)
            {
                Console.WriteLine("Was entered in the wrong format .Try agin !");
                Console.Write("Enter user Id to update :");
            }
            Console.Write("Enter new FirstName :");
            string firstName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(firstName))
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter new FirstName :");
                firstName = Console.ReadLine();
            }
            Console.Write("Enter new LastName :");
            string lastName = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter new LastName :");
                lastName = Console.ReadLine();
            }

            Console.Write("Enter new Email (email@gmail.com):");
            string email = Console.ReadLine();
            while (!Regex.IsMatch(email, @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]{2,}$"))
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter new Email (email@gmail.com):");
                email = Console.ReadLine();
            }

            string password = AnsiConsole.Prompt(
            new TextPrompt<string>("Enter password :")
             .PromptStyle("red")
             .Secret());
            Console.WriteLine("1.ProjectManager, 2.TeamMember");
            int choice;
            Console.Write("Enter Role:");
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 3)
            {
                Console.WriteLine("Was entered in the wrong format .Try agin !");
                Console.Write("Enter Role :");
            }
            Role[] value = (Role[])Enum.GetValues(typeof(Role));

            UserUpdateModel user = new UserUpdateModel();
            {
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Email = email;
                user.Password = password;
                user.Role = value[choice];
            };
            try
            {
                var updatedUser = await _userService.UpdateAsync(id, user);
                AnsiConsole.Markup("[orange3]Succesful updated[/]\n");
                var table = new Table();

                table.AddColumn("[slateblue1]Id[/]");
                table.AddColumn("[slateblue1]FisrtName[/]");
                table.AddColumn("[slateblue1]LastName[/]");
                table.AddColumn("[slateblue1]Email[/]");

                table.AddRow(updatedUser.Id.ToString(), updatedUser.FirstName, updatedUser.LastName, updatedUser.Email);
                AnsiConsole.Write(table);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);
            }
            Console.WriteLine("Enter any keyword to continue");
            Console.ReadKey();
            Console.Clear();
        }

        public void Display()
        {
            bool circle = true;
            while (circle)
            {
                var category = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[darkorange3_1]-- Welcome to UserMenu --[/]")
                .PageSize(10)
                .AddChoices(new[] {
            "Update", "Delete", "GetById" , "GetAll", "Exit",
                }));
                switch (category)
                {
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
}
