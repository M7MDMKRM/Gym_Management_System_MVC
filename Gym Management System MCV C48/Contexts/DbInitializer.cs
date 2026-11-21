using Gym_Management_System_MCV_C48.Entities;
using Gym_Management_System_MCV_C48.Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System_MCV_C48.Contexts
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(GymDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!await context.Plans.AnyAsync())
            {
                var plans = new List<Plan>
                {
                    new Plan
                    {
                        Name = "Basic Monthly",
                        Price = 700,
                        DurationDays = 30,
                        Description = "Full gym floor access during staffed hours.",
                        IsActive = true
                    },
                    new Plan
                    {
                        Name = "Standard Quarterly",
                        Price = 1800,
                        DurationDays = 90,
                        Description = "Gym floor + group classes, one free PT session.",
                        IsActive = true
                    },
                    new Plan
                    {
                        Name = "Premium Half-Year",
                        Price = 3200,
                        DurationDays = 180,
                        Description = "Unlimited classes, locker and a monthly body scan.",
                        IsActive = true
                    },
                    new Plan
                    {
                        Name = "Annual Elite",
                        Price = 5500,
                        DurationDays = 365,
                        Description = "Everything included, 4 PT sessions and guest passes.",
                        IsActive = true
                    },
                    new Plan
                    {
                        Name = "Student Monthly",
                        Price = 450,
                        DurationDays = 30,
                        Description = "Off-peak access for students with a valid ID.",
                        IsActive = false
                    }
                };

                await context.Plans.AddRangeAsync(plans);
                await context.SaveChangesAsync();
            }

            if (!await context.Trainers.AnyAsync())
            {
                var trainers = new List<Trainer>
                {
                    new Trainer
                    {
                        Name = "Ahmed Hassan",
                        Email = "ahmed.hassan@routegym.com",
                        Phone = "01012345678",
                        DateOfBirth = new DateOnly(1992, 4, 15),
                        Gender = Gender.Male,
                        HireDate = DateTime.Now.AddYears(-2),
                        Specialties = Specialties.Boxing,
                        Address = new Address { BuildingNumber = 12, Street = "Tahrir St", City = "Cairo" }
                    },
                    new Trainer
                    {
                        Name = "Sara Ali",
                        Email = "sara.ali@routegym.com",
                        Phone = "01123456789",
                        DateOfBirth = new DateOnly(1995, 8, 22),
                        Gender = Gender.Female,
                        HireDate = DateTime.Now.AddYears(-1),
                        Specialties = Specialties.Yoga,
                        Address = new Address { BuildingNumber = 45, Street = "Nasr Road", City = "Nasr City" }
                    },
                    new Trainer
                    {
                        Name = "Mohamed Mahmoud",
                        Email = "mohamed.m@routegym.com",
                        Phone = "01234567890",
                        DateOfBirth = new DateOnly(1990, 11, 5),
                        Gender = Gender.Male,
                        HireDate = DateTime.Now.AddYears(-3),
                        Specialties = Specialties.CrossFit,
                        Address = new Address { BuildingNumber = 8, Street = "El Gomhouria St", City = "Giza" }
                    }
                };

                await context.Trainers.AddRangeAsync(trainers);
                await context.SaveChangesAsync();
            }

            if (!await context.Members.AnyAsync())
            {
                var members = new List<Member>
                {
                    new Member
                    {
                        Name = "Omar Khaled",
                        Email = "omar.khaled@example.com",
                        Phone = "01512345678",
                        DateOfBirth = new DateOnly(1998, 3, 10),
                        Gender = Gender.Male,
                        Address = new Address { BuildingNumber = 14, Street = "Makram Ebeid", City = "Cairo" }
                    },
                    new Member
                    {
                        Name = "Nourhan Youssef",
                        Email = "nourhan.y@example.com",
                        Phone = "01098765432",
                        DateOfBirth = new DateOnly(2001, 7, 25),
                        Gender = Gender.Female,
                        Address = new Address { BuildingNumber = 22, Street = "Abbas El Akkad", City = "Cairo" }
                    }
                };

                await context.Members.AddRangeAsync(members);
                await context.SaveChangesAsync();

                var basicPlan = await context.Plans.FirstOrDefaultAsync(p => p.Name == "Basic Monthly");
                var standardPlan = await context.Plans.FirstOrDefaultAsync(p => p.Name == "Standard Quarterly");
                var omar = await context.Members.FirstOrDefaultAsync(m => m.Email == "omar.khaled@example.com");
                var nourhan = await context.Members.FirstOrDefaultAsync(m => m.Email == "nourhan.y@example.com");

                if (basicPlan != null && omar != null)
                {
                    context.Memberships.Add(new Membership
                    {
                        MemberId = omar.Id,
                        PlanId = basicPlan.Id,
                        StartDate = DateTime.Today.AddDays(-5),
                        EndDate = DateTime.Today.AddDays(25)
                    });
                }

                if (standardPlan != null && nourhan != null)
                {
                    context.Memberships.Add(new Membership
                    {
                        MemberId = nourhan.Id,
                        PlanId = standardPlan.Id,
                        StartDate = DateTime.Today.AddDays(-10),
                        EndDate = DateTime.Today.AddDays(80)
                    });
                }

                await context.SaveChangesAsync();
            }

            if (!await context.Sessions.AnyAsync())
            {
                var ahmed = await context.Trainers.FirstOrDefaultAsync(t => t.Specialties == Specialties.Boxing);
                var sara = await context.Trainers.FirstOrDefaultAsync(t => t.Specialties == Specialties.Yoga);
                var mohamed = await context.Trainers.FirstOrDefaultAsync(t => t.Specialties == Specialties.CrossFit);

                var sessions = new List<Session>();

                if (ahmed != null)
                {
                    sessions.Add(new Session
                    {
                        Description = "Morning Boxing Drill & Footwork",
                        Capacity = 15,
                        StartDate = DateTime.Today.AddDays(1).AddHours(9),
                        EndDate = DateTime.Today.AddDays(1).AddHours(10).AddMinutes(30),
                        TrainerId = ahmed.Id
                    });
                }

                if (sara != null)
                {
                    sessions.Add(new Session
                    {
                        Description = "Sunset Relaxation Yoga & Flexibility",
                        Capacity = 20,
                        StartDate = DateTime.Today.AddDays(2).AddHours(18),
                        EndDate = DateTime.Today.AddDays(2).AddHours(19).AddMinutes(15),
                        TrainerId = sara.Id
                    });
                }

                if (mohamed != null)
                {
                    sessions.Add(new Session
                    {
                        Description = "High Intensity CrossFit & Stamina",
                        Capacity = 12,
                        StartDate = DateTime.Today.AddDays(3).AddHours(11),
                        EndDate = DateTime.Today.AddDays(3).AddHours(12).AddMinutes(30),
                        TrainerId = mohamed.Id
                    });
                }

                if (sessions.Any())
                {
                    await context.Sessions.AddRangeAsync(sessions);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}

