using System.ComponentModel.DataAnnotations;

namespace ObedientChild.Domain.LifeEnergy
{
    public class TrusteeLifeEnergyAccount
    {
        public int TrusteeId { get; set; }

        [Required]
        public virtual Trustee Trustee { get; set; }

        public int LifeEnergyAccountId { get; set; }

        [Required]
        public virtual LifeEnergyAccount LifeEnergyAccount { get; set; }

        public TrusteeLifeEnergyAccount() { }
        public TrusteeLifeEnergyAccount(ApplicationUser user, LifeEnergyAccount account) 
        {
            TrusteeId = user.TrusteeId;
            LifeEnergyAccount = account;
        }
    }
}
