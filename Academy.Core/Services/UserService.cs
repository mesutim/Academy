using Academy.Core.Convertors;
using Academy.Core.Generator;
using Academy.Core.Security;
using Academy.Core.Services.Interfaces;
using Academy.DataAccess.Context;
using Academy.Model.Models.IdentityModels;
using Academy.Model.Models.TransactionModels;
using Academy.Model.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Academy.Core.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserForAdminViewModel GetUsers(int pageId = 1, int showCount = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users;
            if (!string.IsNullOrEmpty(filterEmail))
                result = result.Where(u => u.Email.Contains(filterEmail));
            if (!string.IsNullOrEmpty(filterUserName))
                result = result.Where(u => u.UserName.Contains(filterUserName));
            //Show Item in page
            int take = showCount;
            int skip = (pageId - 1) * take;

            UserForAdminViewModel list = new UserForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take)
                .Select(u => new UserInfoForAdminViewModel()
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    RegisterDate = u.RegisterDate
                })
                .ToList();
            return list;
        }
        public void DeleteUser(int userId)
        {
            User user = GetUserById(userId);
            user.IsDelete = true;
            UpdateUser(user);
        }
        public User GetUserById(int userId)
        {
            return _context.Users.Find(userId);
        }
        public void UpdateUser(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }
        public DeleteUsersForAdminViewModel GetDeletedUsers(int pageId = 1, int showCount = 1, string filterEmail = "", string filterUserName = "")
        {
            IQueryable<User> result = _context.Users.IgnoreQueryFilters().Where(u => u.IsDelete == true);
            if (!string.IsNullOrEmpty(filterEmail))
                result = result.Where(u => u.Email.Contains(filterEmail));
            if (!string.IsNullOrEmpty(filterUserName))
                result = result.Where(u => u.UserName.Contains(filterUserName));
            //Show Item in page
            int take = showCount;
            int skip = (pageId - 1) * take;

            DeleteUsersForAdminViewModel list = new DeleteUsersForAdminViewModel();
            list.CurrentPage = pageId;
            list.PageCount = result.Count() / take;
            list.Users = result.OrderBy(u => u.RegisterDate).Skip(skip).Take(take)
                .Select(u => new UserInfoForAdminViewModel()
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    RegisterDate = u.RegisterDate
                })
                .ToList();

            return list;
        }
        public DeletedUserInformationViewModel GetDeleteUserInformation(int userId)
        {
            var user = _context.Users
                .IgnoreQueryFilters()
                .Where(u => u.IsDelete == true)
                .SingleOrDefault(u => u.UserId == userId);
            DeletedUserInformationViewModel information = new DeletedUserInformationViewModel();
            information.UserId = userId;
            information.UserName = user.UserName;
            information.Email = user.Email;
            information.RegisterDate = user.RegisterDate;
            //information.Wallet = BalanceUserWallet(user.UserName);

            return information;
        }
        public void RecoverUser(int userId)
        {
            User user = _context.Users
                .IgnoreQueryFilters()
                .Where(u => u.IsDelete == true)
                .SingleOrDefault(u => u.UserId == userId);
            user.IsDelete = false;
            UpdateUser(user);
        }
        public int AddUserFromAdmin(CreateUserViewModel user)
        {
            User adduser = new User();
            adduser.UserName = user.UserName;
            adduser.Email = user.Email;
            adduser.Password = PasswordHelper.EncodePasswordMd5(user.Password);
            adduser.ActiveCode = NameGenerator.GenerateUniqCode();
            adduser.IsActive = true;
            adduser.RegisterDate = DateTime.Now;

            #region Save Avatar

            if (user.UserAvatar != null)
            {
                string imagePath = "";
                adduser.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(user.UserAvatar.FileName);
                imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserAvatar/", adduser.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    user.UserAvatar.CopyTo(stream);
                }

            }

            #endregion

            return AddUser(adduser);
        }
        public int AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }
        public EditUserViewModel GetUserForShowInEditMode(int userId)
        {
            return _context.Users.Where(u => u.UserId == userId)
                .Select(u => new EditUserViewModel()
                {
                    UserId = u.UserId,
                    AvatarName = u.UserAvatar,
                    Email = u.Email,
                    UserName = u.UserName,
                    UserRoles = u.UserRoles.Select(r => r.RoleId).ToList()
                }).Single();
        }
        public void EditUserFromAdmin(EditUserViewModel editUser)
        {
            User user = GetUserById(editUser.UserId);
            user.Email = editUser.Email;
            if (!string.IsNullOrEmpty(editUser.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(editUser.Password);
            }

            if (editUser.UserAvatar != null)
            {
                //Delete old Image
                if (editUser.AvatarName != "Defult.jpg")
                {
                    string deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editUser.AvatarName);
                    if (File.Exists(deletePath))
                    {
                        File.Delete(deletePath);
                    }
                }

                //Save New Image
                user.UserAvatar = NameGenerator.GenerateUniqCode() + Path.GetExtension(editUser.UserAvatar.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", user.UserAvatar);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    editUser.UserAvatar.CopyTo(stream);
                }
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }
        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
        public bool ActiveAccount(string activeCode)
        {
            var user = _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
            if (user == null || user.IsActive)
                return false;

            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            _context.SaveChanges();

            return true;
        }
        public User LoginUser(LoginViewModel login)
        {
            string hashPassword = PasswordHelper.EncodePasswordMd5(login.Password);
            string email = FixedText.FixEmail(login.Email);
            return _context.Users.SingleOrDefault(u => u.Email == email && u.Password == hashPassword);
        }
        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }
        public User GetUserByActiveCode(string activeCode)
        {
            return _context.Users.SingleOrDefault(u => u.ActiveCode == activeCode);
        }
        public int GetUserIdByUserName(string userName)
        {
            return _context.Users.Single(u => u.UserName == userName).UserId;
        }
        public int BalanceUserTransaction(string username)
        {
            int userId = GetUserIdByUserName(username);

            var enter = _context.Transactions
                .Where(w => w.UserId == userId && w.TypeId == 1 && w.IsPay)
                .Select(w => w.Amount).ToList();

            var exit = _context.Transactions
                .Where(w => w.UserId == userId && w.TypeId == 2 && w.IsPay)
                .Select(w => w.Amount).ToList();

            return (enter.Sum() - exit.Sum());
        }
        public int AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return transaction.TransactionId;
        }
    }
}
