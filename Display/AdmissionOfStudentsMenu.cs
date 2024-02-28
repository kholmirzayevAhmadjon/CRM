using CRM_system_for_training_centers.Interfaces;
using CRM_system_for_training_centers.Models.AddmissionOfStudents;
using CRM_system_for_training_centers.Services;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace CRM_system_for_training_centers.Display
{
    public class AdmissionOfStudentsMenu
    {
        private readonly AdmissionOfStudentService _admissionOfStudentService;
        private string FirstName;

        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Description { get; private set; }

        public AdmissionOfStudentsMenu(AdmissionOfStudentService admissionOfStudentService)
        {
            _admissionOfStudentService = admissionOfStudentService;
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

            Console.Write("Enter phoneNumber :");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id < 100000000 || id >= 1000000000)
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter phoneNumber :");
            }
            string phoneNumber = $"+998{id}";

            Console.Write("Enter Description :");
            string descreption = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(descreption))
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter Description :");
                descreption = Console.ReadLine();
            }

            var admission = new AdmissionOfStudentsCreationModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Description = descreption,
            };

            try
            {
                var category = _admissionOfStudentService.CreateAsync(admission).Result;
                AnsiConsole.Markup("[orange3] Registration completed successfully![/]\n");
                var table = new Table();

                table.AddColumn("[slateblue1]Id[/]");
                table.AddColumn("[slateblue1]FisrtName[/]");
                table.AddColumn("[slateblue1]LastName[/]");
                table.AddColumn("[slateblue1]Email[/]");
                table.AddColumn("[slateblue1]PhoneNumber[/]");
                table.AddColumn("[slateblue1]Descreption[/]");

                table.AddRow(category.Id.ToString(), category.FirstName, category.LastName, category.Email, category.PhoneNumber, category.Description);
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

            Console.Write("Enter AdmissionOfStudentId to Update :");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter AdmissionOfStudentId to Update :");
            }

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

            Console.Write("Enter phoneNumber :");
            int phone;
            while (!int.TryParse(Console.ReadLine(), out phone) || phone < 100000000 || phone >= 1000000000)
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter phoneNumber :");
            }
            string phoneNumber = $"+998{phone}";

            Console.Write("Enter Description :");
            string descreption = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(descreption))
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter Description :");
                descreption = Console.ReadLine();
            }

            AdmissionOfStudentsUpdateModel admission = new AdmissionOfStudentsUpdateModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = phoneNumber,
                Description = descreption,
            };

            try
            {
                var category = _admissionOfStudentService.UpdateAsync(id, admission).Result;
                AnsiConsole.Markup("[orange3] Registration completed successfully![/]\n");
                var table = new Table();

                table.AddColumn("[slateblue1]Id[/]");
                table.AddColumn("[slateblue1]FisrtName[/]");
                table.AddColumn("[slateblue1]LastName[/]");
                table.AddColumn("[slateblue1]Email[/]");
                table.AddColumn("[slateblue1]PhoneNumber[/]");
                table.AddColumn("[slateblue1]Descreption[/]");

                table.AddRow(category.Id.ToString(), category.FirstName, category.LastName, category.Email, category.PhoneNumber, category.Description);
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
            Console.Write("Enter AdmissionOfStudentID to delete :");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter AdmissionOfStudentID to delete :");
            }
            try
            {
                _admissionOfStudentService.DeleteAsync(id);
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
            Console.Write("EnterAdmissionOfStudentID :");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id) || id < 0)
            {
                Console.WriteLine("Was entered in the wrong format .Try again !");
                Console.Write("Enter AdmissionOfStudentID :");
            }
            try
            {
                var category = await _admissionOfStudentService.GetByIdAsync(id);
                var table = new Table();

                table.AddColumn("[slateblue1]Id[/]");
                table.AddColumn("[slateblue1]FirstName[/]");
                table.AddColumn("[slateblue1]LastName[/]");
                table.AddColumn("[slateblue1]Email[/]");
                table.AddColumn("[slateblue1]PhoneNumber[/]");
                table.AddColumn("[slateblue1]Descreption[/]");

                table.AddRow(category.Id.ToString(), category.FirstName, category.LastName, category.Email, category.PhoneNumber, category.Description);
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

        public async void GetAll()
        {
            Console.Clear();
            var table = new Table();

            table.AddColumn("[slateblue1]Id[/]");
            table.AddColumn("[slateblue1]FirstName[/]");
            table.AddColumn("[slateblue1]LastName[/]");
            table.AddColumn("[slateblue1]Email[/]");
            table.AddColumn("[slateblue1]PhoneNumber[/]");
            table.AddColumn("[slateblue1]Descreption[/]");

            var admissions =await _admissionOfStudentService.GetAllAsync();
            foreach (var category in admissions)
            {
                table.AddRow(category.Id.ToString(), category.FirstName, category.LastName, category.Email, category.PhoneNumber, category.Description);
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
                .Title("[darkorange3_1]-- Welcome to Addmission Of Student Menu --[/]")
                .PageSize(10)
                .AddChoices(new[] {
                 "Create", "Update", "Delete", "GetById" , "GetAll", "Exit",
                }));
                switch (category)
                {
                    case "Create":
                        Delete();
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
}
