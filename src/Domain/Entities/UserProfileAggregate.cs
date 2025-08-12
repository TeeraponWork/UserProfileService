using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public sealed class UserProfileAggregate
    {
        public Guid Id { get; private set; }              // ProfileId (PK)
        public Guid UserId { get; private set; }          // อ้างไปยัง Auth Service
        public string? DisplayName { get; private set; }
        public Sex Sex { get; private set; }
        public DateOnly? DateOfBirth { get; private set; }
        public int? HeightCm { get; private set; }
        public decimal? WeightKg { get; private set; }
        public string? BloodType { get; private set; }    // ถ้าต้องการจำกัดค่า ค่อยเปลี่ยนเป็น enum ทีหลัง
        public List<ChronicCondition> ChronicConditions { get; private set; } = new();
        public List<Allergy> Allergies { get; private set; } = new();

        public DateTime CreatedAtUtc { get; private set; }
        public DateTime UpdatedAtUtc { get; private set; }

        private UserProfileAggregate() { } // EF

        public static UserProfileAggregate Create(Guid userId, string? displayName, Sex sex, DateOnly? dob)
        {
            var now = DateTime.UtcNow;
            return new UserProfileAggregate
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DisplayName = string.IsNullOrWhiteSpace(displayName) ? null : displayName.Trim(),
                Sex = sex,
                DateOfBirth = dob,
                CreatedAtUtc = now,
                UpdatedAtUtc = now
            };
        }

        public void UpdateBasics(string? displayName, Sex sex, DateOnly? dob, string? bloodType)
        {
            DisplayName = string.IsNullOrWhiteSpace(displayName) ? null : displayName.Trim();
            Sex = sex;
            DateOfBirth = dob;
            BloodType = string.IsNullOrWhiteSpace(bloodType) ? null : bloodType.Trim();
            Touch();
        }

        public void UpdateMeasurements(int? heightCm, decimal? weightKg)
        {
            HeightCm = heightCm;
            WeightKg = weightKg;
            Touch();
        }

        public void AddChronicCondition(string name, string? notes = null)
        {
            ChronicConditions.Add(new ChronicCondition(name, notes));
            Touch();
        }

        public void RemoveChronicCondition(string name)
        {
            ChronicConditions.RemoveAll(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            Touch();
        }

        public void AddAllergy(string name, string? severity = null)
        {
            Allergies.Add(new Allergy(name, severity));
            Touch();
        }

        public void RemoveAllergy(string name)
        {
            Allergies.RemoveAll(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            Touch();
        }

        private void Touch() => UpdatedAtUtc = DateTime.UtcNow;
    }
}
