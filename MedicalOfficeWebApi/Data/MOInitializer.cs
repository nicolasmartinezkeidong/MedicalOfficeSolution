﻿using MedicalOfficeWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MedicalOfficeWebApi.Data
{
    public class MOInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            MedicalOfficeContext context = applicationBuilder.ApplicationServices.CreateScope()
                .ServiceProvider.GetRequiredService<MedicalOfficeContext>();
            try
            {
                //Delete the database if you need to apply a new Migration
                context.Database.EnsureDeleted();
                //Create the database if it does not exist and apply the Migration
                context.Database.Migrate();

                // Look for any Doctors.  Since we can't have patients without Doctors.
                if (!context.Doctors.Any())
                {
                    context.Doctors.AddRange(
                     new Doctor
                     {
                         FirstName = "Valentina",
                         MiddleName = "K",
                         LastName = "Galarce"
                     },

                     new Doctor
                     {
                         FirstName = "Noah",
                         MiddleName = "R",
                         LastName = "Martinez"
                     },
                     new Doctor
                     {
                         FirstName = "Liam",
                         LastName = "Williams"
                     });
                    context.SaveChanges();
                }
                if (!context.Patients.Any())
                {
                    context.Patients.AddRange(
                    new Patient
                    {
                        FirstName = "Oliver",
                        MiddleName = "Logan",
                        LastName = "Smith",
                        OHIP = "1231231234",
                        DOB = DateTime.Parse("1955-09-01"),
                        ExpYrVisits = 6,
                        DoctorID = context.Doctors.FirstOrDefault(d => d.FirstName == "Valentina" && d.LastName == "Galarce").ID
                    },
                    new Patient
                    {
                        FirstName = "Lucas",
                        MiddleName = "James",
                        LastName = "Lee",
                        OHIP = "1321321324",
                        DOB = DateTime.Parse("1964-04-23"),
                        ExpYrVisits = 2,
                        DoctorID = context.Doctors.FirstOrDefault(d => d.FirstName == "Valentina" && d.LastName == "Galarce").ID
                    },
                    new Patient
                    {
                        FirstName = "Barney",
                        LastName = "Wilson",
                        OHIP = "3213213214",
                        DOB = DateTime.Parse("1964-02-22"),
                        ExpYrVisits = 2,
                        DoctorID = context.Doctors.FirstOrDefault(d => d.FirstName == "Noah" && d.LastName == "Martinez").ID
                    },
                    new Patient
                    {
                        FirstName = "Emma",
                        MiddleName = "Olivia",
                        LastName = "Roy",
                        OHIP = "4124124123",
                        DOB = DateTime.Parse("1987-02-22"),
                        ExpYrVisits = 2,
                        DoctorID = context.Doctors.FirstOrDefault(d => d.FirstName == "Liam").ID
                    });
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
