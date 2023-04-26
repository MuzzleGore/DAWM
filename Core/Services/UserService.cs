using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Dtos;
using DataLayer;
using DataLayer.Entities;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;
        private readonly AuthorizationService authorizationService;

        public UserService(UnitOfWork unitOfWork, AuthorizationService authorizationService)
        {
            unitOfWork = unitOfWork;
            authorizationService = authorizationService;
        }


        public string Validate(LoginDto payload)
        {
            var user = unitOfWork.Users.GetByEmail(payload.Email);

            var passwordFine = authorizationService.VerifyHashedPassword(user.PasswordHash, payload.Password);

            if (passwordFine)
            {
                var role = user.Role.Name;

                return authorizationService.GetToken(user);
            }
            else
            {
                return null;
            }

        }

        public void Register(RegisterDto payload)
        {
            if (payload == null)
            {
                return;
            }

            var user = new User
            {
                Email = payload.Email,
                PasswordHash = authorizationService.HashPassword(payload.Password),
                Role = new Role
                {
                    Name = payload.Role
                }
            };
            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();
        }

        public List<Grade> GetGrades(User user)
        {
            var student = unitOfWork.Students.GetByEmail(user.Email);
            return student.Grades;
        }

        public User GetById(int userId)
        {
            return unitOfWork.Users.GetById(userId);
        }

        public List<Grade> GetAllGrades()
        {
            var result = new List<Grade>();
            var students = unitOfWork.Students.GetAll();
            foreach (var student in students)
            {
                result.AddRange(student.Grades);
            }
            return result;
        }
    }
}
