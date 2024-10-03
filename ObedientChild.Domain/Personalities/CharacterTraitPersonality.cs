namespace ObedientChild.Domain.Personalities
{
    public class CharacterTraitPersonality
    {
        public int PersonalityId { get; set; }

        public virtual Personality Personality { get; set; }

        public int CharacterTraitId { get; set; }

        public virtual CharacterTrait CharacterTrait { get; set; }

        public CharacterTraitPersonality() { }

        public CharacterTraitPersonality(int personalityId, int characterTraitId)
        {
            PersonalityId = personalityId;
            CharacterTraitId = characterTraitId;
        }
    }
}
