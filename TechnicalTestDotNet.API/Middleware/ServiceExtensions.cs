using TechnicalTestDotNet.Core.Interfaces.Auth;
using TechnicalTestDotNet.Core.Interfaces.Courses;
using TechnicalTestDotNet.Core.Interfaces.Qualifications;
using TechnicalTestDotNet.Core.Interfaces.Students;
using TechnicalTestDotNet.Core.Interfaces.Teachers;
using TechnicalTestDotNet.DataAccess.Services.Repositories.Auth;
using TechnicalTestDotNet.DataAccess.Services.Repositories.Courses;
using TechnicalTestDotNet.DataAccess.Services.Repositories.Qualifications;
using TechnicalTestDotNet.DataAccess.Services.Repositories.Students;
using TechnicalTestDotNet.DataAccess.Services.Repositories.Teachers;

namespace TechnicalTestDotNet.API.Middleware
{
    public static class ServiceExtensions
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<ITeachersRepository, TeachersRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICoursesRepository, CoursesRepository>();
            services.AddScoped<IQualificationsRepository, QualificationsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
