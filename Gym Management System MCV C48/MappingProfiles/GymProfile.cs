using AutoMapper;
using Gym_Management_System_MCV_C48.Entities;
using Gym_Management_System_MCV_C48.ViewModels.MemberViewModels;
using Gym_Management_System_MCV_C48.ViewModels.MembershipViewModels;
using Gym_Management_System_MCV_C48.ViewModels.PlanViewModels;
using Gym_Management_System_MCV_C48.ViewModels.SessionViewModels;
using Gym_Management_System_MCV_C48.ViewModels.TrainerViewModels;

namespace Gym_Management_System_MCV_C48.MappingProfiles
{
    public class GymProfile : Profile
    {
        public GymProfile()
        {
            // Plan mappings
            CreateMap<Plan, PlanViewModel>();
            CreateMap<PlanCreateViewModel, Plan>();
            CreateMap<Plan, PlanEditViewModel>().ReverseMap();

            // Member mappings
            CreateMap<Member, MemberListViewModel>()
                .ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.ActivePlanName, opt => opt.MapFrom(src =>
                    src.Memberships.Where(m => m.EndDate > DateTime.Now).OrderByDescending(m => m.EndDate).Select(m => m.Plan.Name).FirstOrDefault()))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src =>
                    src.Memberships.Any(m => m.EndDate > DateTime.Now) ? "Active" : "Inactive"));

            CreateMap<MemberCreateViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));

            CreateMap<Member, MemberEditViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.CurrentPhoto, opt => opt.MapFrom(src => src.Photo));

            CreateMap<MemberEditViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));

            CreateMap<Member, MemberDetailsViewModel>()
                .ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Subscriptions, opt => opt.MapFrom(src => src.Memberships.Select(m => new MemberSubscriptionItemViewModel
                {
                    Id = m.Id,
                    PlanName = m.Plan != null ? m.Plan.Name : "",
                    Price = m.Plan != null ? m.Plan.Price : 0,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Status = m.Status,
                    IsActive = m.IsActive
                })));

            // Trainer mappings
            CreateMap<Trainer, TrainerListViewModel>()
                .ForMember(dest => dest.SessionsCount, opt => opt.MapFrom(src => src.Sessions.Count));

            CreateMap<TrainerCreateViewModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));

            CreateMap<Trainer, TrainerEditViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));

            CreateMap<TrainerEditViewModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));

            CreateMap<Trainer, TrainerDetailsViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Sessions, opt => opt.MapFrom(src => src.Sessions.Select(s => new TrainerSessionItemViewModel
                {
                    Id = s.Id,
                    Description = s.Description,
                    Capacity = s.Capacity,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                })));

            // Session mappings
            CreateMap<Session, SessionListViewModel>()
                .ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name));

            CreateMap<SessionCreateViewModel, Session>();
            CreateMap<Session, SessionEditViewModel>();
            CreateMap<SessionEditViewModel, Session>();

            // Membership mappings
            CreateMap<Membership, MembershipListViewModel>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.MemberEmail, opt => opt.MapFrom(src => src.Member.Email))
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Plan.Name))
                .ForMember(dest => dest.PlanPrice, opt => opt.MapFrom(src => src.Plan.Price));

            CreateMap<MembershipCreateViewModel, Membership>();
        }
    }
}

