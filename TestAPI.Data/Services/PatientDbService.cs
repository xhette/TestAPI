using Microsoft.EntityFrameworkCore;

using TestAPI.Data.Entities;
using TestAPI.Data.Models;

namespace TestAPI.Data.Services
{
    public class PatientDbService
    {
        private readonly MedDbContext _dbContext;
        public PatientDbService(string dbConnectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MedDbContext>();
            var options = optionsBuilder.UseSqlServer(dbConnectionString).Options;

            _dbContext = new MedDbContext(options);
        }

        public async Task AddAsync(Patient entity)
        {
            try
            {
                _dbContext.Patients.Add(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var patient = await _dbContext.Patients.FindAsync(id);

            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Данные по id = {id} не найдены");
            }
        }

        public async Task<IEnumerable<PatientViewModel>> GetAllAsync()
        {
            return await GetJoinedList();
        }

        public async Task<IEnumerable<PatientViewModel>> GetAllAsync(int from, int perPage, string fieldName)
        {
            var propertyInfo = typeof(Patient).GetProperty(fieldName);
            var joinedList = await GetJoinedList();

            return joinedList.OrderBy(x => propertyInfo.GetValue(x)).Skip((from - 1) * perPage).Take(perPage).ToList();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _dbContext.Patients.FindAsync(id);
        }

        public async Task UpdateAsync(Patient entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        private async Task<IEnumerable<PatientViewModel>> GetJoinedList()
        {
            var patientList = await _dbContext.Patients.Join(_dbContext.Regions, d => d.RegionId, c => c.Id, (c, d) => new PatientViewModel
            {
                Id = c.Id,
                Surname = c.Surname,
                Name = c.Name,
                Patronymic = c.Patronymic,
                Gender = c.Gender,
                Birthday = c.Birthday,
                Address = c.Address,
                Region = d.RegionNumber
            }).ToListAsync();

            return patientList;
        }
    }
}
