using AutoMapper;
using Sutido.API.ViewModels.Requests;
using Sutido.API.ViewModels.Responses;
using Sutido.Model.Entites;
using ViewModels.Requests;
using ViewModels.Responses;

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

            //=================================================================================================
            // Tutor Profile
            //==Request==
            CreateMap<TutorProfileUpdateRequest, TutorProfile>();
            CreateMap<TutorProfileReviewRequest, TutorProfile>();
            CreateMap<TutorProfileRequest, TutorProfile>();

            //==Response==
            CreateMap<TutorProfile, TutorProfileResponse>()
                //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)) // khác tên
                .ForMember(dest => dest.Certifications, opt => opt.MapFrom(src => src.Certifications));

            //=================================================================================================
            // Certification
            //==Request==
            CreateMap<CertificationRequest, Certification>();
            CreateMap<CertificationReviewRequest, Certification>();

            //==Response==
            CreateMap<Certification, CertificationResponse>();

            //=================================================================================================
            // Booking
            //==Request==
            CreateMap<BookingRequest, Booking>()
                .ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => "Pending"))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTimeOffset.UtcNow));

            //==Response==
            CreateMap<Booking, BookingResponse>();

            //=================================================================================================
            //Post
            //==Request==
            CreateMap<PostRequest, Post>()
                .ForMember(dest => dest.CreatorUser, opt => opt.Ignore());
            CreateMap<UpdatePostRequest, Post>();

            //==Response==
            CreateMap<Post, PostResponse>();
            CreateMap<Post, PostDetailsResponse>();
        }
    }
}
