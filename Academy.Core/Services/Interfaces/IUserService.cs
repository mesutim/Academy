using Academy.Model.Models.IdentityModels;
using Academy.Model.Models.TransactionModels;
using Academy.Model.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Academy.Core.Services.Interfaces
{
    public interface IUserService
    {
        UserForAdminViewModel GetUsers(int pageId = 1, int showCount = 10, string filterEmail = "", string filterUserName = "");
        void DeleteUser(int userId);
        User GetUserById(int userId);
        void UpdateUser(User user);
        DeleteUsersForAdminViewModel GetDeletedUsers(int pageId = 1, int showCount = 1, string filterEmail = "", string filterUserName = "");
        DeletedUserInformationViewModel GetDeleteUserInformation(int userId);
        void RecoverUser(int userId);
        int AddUserFromAdmin(CreateUserViewModel user);
        int AddUser(User user);
        EditUserViewModel GetUserForShowInEditMode(int userId);
        void EditUserFromAdmin(EditUserViewModel editUser);
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        bool ActiveAccount(string activeCode);
        User LoginUser(LoginViewModel login);
        User GetUserByEmail(string email);
        User GetUserByActiveCode(string activeCode);
        int GetUserIdByUserName(string userName);
        int BalanceUserTransaction(string username);
        int AddTransaction(Transaction transaction);
        InformationUserViewModel GetUserInformation(string username);
        User GetUserByUserName(string username);
        int BalanceUserWallet(string username);
        EditProfileViewModel GetDataForEditProfileUser(string username);
        void EditProfile(string username, EditProfileViewModel profile);
        bool CompareOldPassword(string oldPassword, string username);
        void ChangeUserPassword(string userName, string newPassword);
        public int ChargeWallet(string username, int amount, string description, bool isPay = false);
        SideBarUserPanelViewModel GetSideBarUserPanelData(string username);
        List<TransactionViewModel> GetWalletUser(string userName);
    }
}
