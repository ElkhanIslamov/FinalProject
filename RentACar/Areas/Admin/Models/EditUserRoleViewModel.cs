namespace RentACar.Areas.Admin.Models
{
    public class EditUserRoleViewModel
    {
        public required string UserId { get; set; }
        public required string FullName { get; set; }
        public List<string> CurrentRoles { get; set; } = [];
        public List<string> AllRoles { get; set; } = [];
    }
}
