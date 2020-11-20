using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Clinic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Clinic.Models { 

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ClinicContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ClinicContext>>()))
        {
            if (context.Patients.Any() || context.Doctors.Any() || context.Terms.Any())
            {   
                return;   // DB has been seeded
            }

            context.Patients.AddRange(

            new Patients
            {
                PatientId = "1",
                FirstName = "Marija",
                LastName = "Markoska",
                ScheduledDate = DateTime.Parse("2017-09-15"),
                HealthInsurance=true

            },
            new Patients
            {
                PatientId = "2",
                FirstName = "Simona",
                LastName = "Simonoska",
                ScheduledDate = DateTime.Parse("2016-09-15"),
                HealthInsurance = true

            },
            new Patients
            {
                PatientId = "3",
                FirstName = "Aleksandra",
                LastName = "Atanasoska",
                ScheduledDate = DateTime.Parse("2016-09-15"),
                HealthInsurance = false


            },
            new Patients
            {
                PatientId = "4",
                FirstName = "Andrej",
                LastName = "Andreevski",
                ScheduledDate = DateTime.Parse("2017-09-15"),
                HealthInsurance = true


            },
            new Patients
            {
                PatientId = "5",
                FirstName = "Angela",
                LastName = "Angeloska",
                ScheduledDate = DateTime.Parse("2017-09-15"),
                HealthInsurance = true


            },
            new Patients
            {
                PatientId = "6",
                FirstName = "Marko",
                LastName = "Markoski",
                ScheduledDate = DateTime.Parse("2015-09-15"),
                HealthInsurance = false


            },
            new Patients
            {
                PatientId = "7",
                FirstName = "Stefan",
                LastName = "Stefanoski",
                ScheduledDate = DateTime.Parse("2018-09-15"),
                HealthInsurance = false


            },
            new Patients
            {
                PatientId = "8",
                FirstName = "Stefani",
                LastName = "Stefanoska",
                ScheduledDate = DateTime.Parse("2017-09-15"),
                HealthInsurance = true

            }
        );
            context.SaveChanges();

            context.Doctors.AddRange(
                 new Doctors
                 {
                     FirstName = "Viktorija",
                     LastName = "Ackova",
                     Degree = "PhD",
                     OfficeNumber = "121 A",
                     HireDate = DateTime.Parse("2010-05-12")
                 },
            new Doctors
            {
                FirstName = "Pero",
                LastName = "Petreski",
                Degree = "PhD",
                OfficeNumber = "100TK",
                HireDate = DateTime.Parse("2009-10-25")
            },
            new Doctors
            {
                FirstName = "Valentin",
                LastName = "Ilievski",
                Degree = "PhD",
                OfficeNumber = "200TK",
                HireDate = DateTime.Parse("2012-10-10")
            },
            new Doctors
            {
                FirstName = "Aleksandar",
                LastName = "Aleksovski",
                Degree = "PhD",
                OfficeNumber = "300TK",
                HireDate = DateTime.Parse("2007-03-20")
            },
            new Doctors
            {
                FirstName = "Marko",
                LastName = "Velevackovski",
                Degree = "PhD",
                OfficeNumber = "400TK",
                HireDate = DateTime.Parse("2015-09-06")
            },
            new Doctors
            {
                FirstName = "Goran",
                LastName = "Dimovski",
                Degree = "PhD",
                OfficeNumber = "121A",
                HireDate = DateTime.Parse("2016-06-10")
            },
            new Doctors
            {
                FirstName = "Vladimir",
                LastName = "Lazarovski",
                Degree = "PhD",
                OfficeNumber = "500TK",
                HireDate = DateTime.Parse("2012-07-10")
            },
            new Doctors
            {
                FirstName = "Hristijan",
                LastName = "Gjorgjiovski",
                Degree = "PhD",
                OfficeNumber = "210",
                HireDate = DateTime.Parse("2015-12-05")
            }
           );
            context.SaveChanges();

            context.Terms.AddRange(
                new Terms
                {
                    Category = "Upat za uvo, nos i grlo",
                    Date = DateTime.Parse("2020-12-05"),
                    Priority = true,
                    DoctorId = context.Doctors.Single(d => d.FirstName == "Viktorija" && d.LastName == "Ackova").DoctorId
                },
                new Terms
                {
                    Category = "Upat za ocno",
                    Date = DateTime.Parse("2020-10-06"),
                    Priority = false,
                    DoctorId = context.Doctors.Single(d => d.FirstName == "Pero" && d.LastName == "Petreski").DoctorId
                },
                new Terms
                {
                    Category = "Upat za infektivno",
                    Date = DateTime.Parse("2020-12-15"),
                    Priority = true,
                    DoctorId = context.Doctors.Single(d => d.FirstName == "Valentin" && d.LastName == "Ilievski").DoctorId
                },
                new Terms
                {
                    Category = "Upat za uvo, nos i grlo",
                    Date = DateTime.Parse("2020-09-05"),
                    Priority = true,
                    DoctorId = context.Doctors.Single(d => d.FirstName == "Aleksandar" && d.LastName == "Aleksovski").DoctorId
                },
                new Terms
                {
                    Category = "Upat za ginekologija",
                    Date = DateTime.Parse("2020-11-25"),
                    Priority = false,
                    DoctorId = context.Doctors.Single(d => d.FirstName == "Marko" && d.LastName == "Velevackovski").DoctorId
                },
                new Terms
                {
                    Category = "Upat za ocno",
                    Date = DateTime.Parse("2020-11-11"),
                    Priority = true,
                    DoctorId = context.Doctors.Single(d => d.FirstName == "Goran" && d.LastName == "Dimovski").DoctorId
                },
                new Terms
                {
                    Category = "Upat za ocno",
                    Date = DateTime.Parse("2020-10-15"),
                    Priority = false,
                    DoctorId = context.Doctors.Single(d => d.FirstName == "Vladimir" && d.LastName == "Lazarovski").DoctorId
               
                }
                );
            context.SaveChanges();

            context.Scheduling.AddRange(
                new Scheduling
                {
                    TermID = 5,
                    PatientID = 5,
                    Diagnose ="COVID-19"
                },
                new Scheduling
                {
                    TermID = 1,
                    PatientID = 6,
                    Diagnose = "Hronicna bronhopleumonija"
                },
                new Scheduling
                {
                    TermID = 4,
                    PatientID = 8,
                    Diagnose = "Grip"
                },
                new Scheduling
                {
                    TermID = 7,
                    PatientID = 2,
                    Diagnose = "COVID-19"
                },
                new Scheduling
                {
                    TermID = 6,
                    PatientID = 3,
                    Diagnose = "Suva kaslica"
                },
                new Scheduling
                {
                    TermID = 3,
                    PatientID = 2,
                    Diagnose = "COVID-19"
                }

                );
            context.SaveChanges();
        }
    }
}
}

