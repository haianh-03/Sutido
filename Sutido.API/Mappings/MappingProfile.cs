using AutoMapper;
using Sutido.API.ViewModels.Requests;
using Sutido.API.ViewModels.Responses;
using Sutido.Model.Entites;

namespace Sutido.Service.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User
            //==Request==
            CreateMap<CustomerRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<ProfileRequest, User>();
            CreateMap<StaffRequest, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            //==Response==
            CreateMap<User, ProfileResponse>();
            CreateMap<User, UserResponse>();

            // Tutor Profile
            //==Request==
            CreateMap<TutorProfileUpdateRequest, TutorProfile>();
            CreateMap<TutorProfileReviewRequest, TutorProfile>();
            CreateMap<TutorProfileRequest, TutorProfile>();
            //==Response==
            CreateMap<TutorProfile, TutorProfileResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.status)) // khác tên
                .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.Certifications));

            // Certification
            //==Request==
            CreateMap<CertificationRequest, Certification>();
            CreateMap<CertificationReviewRequest, Certification>();
            //==Response==
            CreateMap<Certification, CertificationResponse>();
        }
    }
}
