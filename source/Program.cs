// See https://aka.ms/new-console-template for more information
using EntityTutorial;
using Microsoft.EntityFrameworkCore;

var ctx = new MyDbContext();

//ctx.Students.ExecuteDelete();
//ctx.Courses.ExecuteDelete();
//ctx.Cities.ExecuteDelete();
//ctx.Countries.ExecuteDelete();

// |CREATE/ADD|
//// (1)
///
//// creating the objects
//var student1 = new Student();
//student1.FirstName = "Verca";
//student1.LastName = "Pavlikova";
//Console.WriteLine(student1);
//var student2 = new Student("Irena", "Katzova");
//Console.WriteLine(student2);

//// adding them to the database
//ctx.Students.Add(student1);
//ctx.Students.Add(student2);

//// (2)
//// and it needs to be saved
////ctx.SaveChanges();
//Console.WriteLine($"We have {ctx.Students.Count()} students in the database");



//// Add in one line
//ctx.Students.Add(new Student("Alan", "Turing"));
//ctx.SaveChanges();

//// Dummy Data
//for (int i = 0; i < 10; i++)
//{
//    ctx.Add(new Student("Verca"+i, "Pavlikova"+i));
//    ctx.Add(new Student("Irena" + i, "Katzova"+i));
//}


//// Look at the IDs!
//// (3)
//var city = new City();
//city.Name = "Mardrid";
//city.Population = 20000;

//var result = ctx.Add(city);
////Console.WriteLine(result);

//var country = new Country();
//country.Name = "Portugal";
//country.CapitalCity = city;
//ctx.Countries.Add(country);

//Console.WriteLine(country.CapitalCity.Name + " is the Capitol City of");
//Console.WriteLine(country.Name);

//ctx.SaveChanges();


// (4)
//city.Name = "Barcelona";
//country.Name = "Spain";

// (5) Multiple adding?
//var course = new Course("DotNet", "C#, F#,...");
//ctx.Courses.Add(course);
//ctx.Courses.Add(course);
//ctx.SaveChanges();


//// |READ|
/// (1)
//var myStudent = ctx.Students.Where(x => x.Id == 1);
//var someStudents = ctx.Students.Where(x => x.FirstName == "Alan");
//var myStudents = ctx.Students.Where(s => s.FirstName == "Verca").ToList<Student>();

// (2)
//WE CAN SEE THE TYPE !
//var allMyStudents = ctx.Students.ToList();
//Console.WriteLine("\nAll students:");
//foreach (var student in allMyStudents)
//{
//    Console.WriteLine(student);
//}

// (3)
//Console.WriteLine("All cities:");
//if (ctx.Cities.Any())
//{
//    Console.WriteLine(ctx.Cities.Count());
//    var allCities = ctx.Cities;

//    if (allCities.Any())
//    {
//        foreach (var city in allCities.ToList())
//        {
//            Console.WriteLine(city.Name);
//            Console.WriteLine(city.Population);
//            Console.WriteLine();
//        }
//    }
//}

// |DELETE|
//// (1)
//// wil this remove the students? Linq operations should not have a side effect
//foreach(var student in allMyStudents)
//{
//    ctx.Students.Remove(student);
//    Console.WriteLine($"Was {student} deleted?");
//}

//// (2)
// wil this remove the students?
//ctx.Students.ExecuteDelete();

// is this needed?
//ctx.SaveChanges();

// |UPDATE|
// will it change?
// (1)
//Console.WriteLine();
//var someStudents = ctx.Students.Where(x => x.FirstName == "Alan");

//if (someStudents.Any())
//{
//    var studentToUpdate = someStudents.First();
//    Console.WriteLine($"1. We have the student {studentToUpdate}");

//    studentToUpdate.FirstName = "Updated"+ studentToUpdate.FirstName;
//    studentToUpdate.LastName = "Updated" + studentToUpdate.LastName;
//    Console.WriteLine($"We have the student {studentToUpdate}");

////    ctx.SaveChanges(); // yes, with this line
//} 

//// (2)
///
//var pCountry = ctx.Countries.Where(x => x.Name == "Portugal").First();

//Console.WriteLine($"Capital City of {pCountry.Name} is {pCountry.CapitalCity.Name}");

//pCountry.CapitalCity = new City { Name = "Barcelona" };

//Console.WriteLine($"Capital City of {pCountry.Name} is {pCountry.CapitalCity.Name}");

//ctx.SaveChanges();

//// (3)
///

//// (4)
//try
//{
//    var allStudents = ctx.Students.ToList();
//    var course = ctx.Courses.First();

//    foreach (var student in allStudents)
//    {
//        course.Students.Add(student);
//    }

//    ctx.SaveChanges();
//}

//catch
//{

//}


//// (5)
//var course = ctx.Courses.First();
//var students = course.Students;

//Console.WriteLine("\nAll Students");
//foreach (var student in course.Students)
//{
//    Console.WriteLine(student);
//}

//// (6)
//var firstStudent = students.First();
//students.Remove(firstStudent);

//Console.WriteLine("\nAll Students");
//foreach (var student in course.Students)
//{
//    Console.WriteLine(student);
//}

//ctx.SaveChanges();


//// |Bonus| with the Blog
var blog = new Blog
{
    Name = "MyBlog",
    Posts = new List<Post>()
};

var post = new Post
{
    Text = "Post Content"
};

//blog.Posts.Add(post);

// (1)
//ctx.Blogs.Add(blog);

// (2)
//ctx.SaveChanges();

// (3)
//post = ctx.Posts.First();
//Console.WriteLine(post.Blog.Name);

// (4)
//blog = ctx.Blogs.First();

//ctx.Blogs.ExecuteDelete();

