using Microsoft.EntityFrameworkCore;

using TestAPI.Data.Entities;
using TestAPI.Data.Models;

namespace TestAPI.Data.Services
{
    public class DoctorDbService
    {
        private readonly MedDbContext _dbContext;
        public DoctorDbService (string dbConnectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MedDbContext>();
            var options = optionsBuilder.UseSqlServer(dbConnectionString).Options;

            _dbContext = new MedDbContext(options);
        }

        public async Task AddAsync (Doctor entity)
        {
            try 
            { 
                 _dbContext.Doctors.Add(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var doctor = await _dbContext.Doctors.FindAsync(id);

            if (doctor != null) 
            { 
                _dbContext.Doctors.Remove(doctor);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Данные по id = {id} не найдены");
            }
        }

        public async Task<IEnumerable<DoctorViewModel>>  GetAllAsync()
        {
            return await GetJoinedList();
        }

        public async Task<IEnumerable<DoctorViewModel>>  GetAllAsync(int from, int perPage, string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
            {
                fieldName = "Id";
            }

            var joinedList = await GetJoinedList();

            return joinedList.OrderBy(x => x.GetType().GetProperty(fieldName).GetValue(x, null)).Skip((from-1) * perPage).Take(perPage).ToList();
        }

        public async Task<Doctor> GetByIdAsync(int id)
        {
            return await _dbContext.Doctors.FindAsync(id);
        }

        public async Task UpdateAsync(Doctor entity)
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

        private async Task<IEnumerable<DoctorViewModel>> GetJoinedList()
        {
            var doctorsList = await _dbContext.Doctors.Join(_dbContext.Cabinets, d => d.CabinetId, c => c.Id, (c, d) => new
            {
                c.Id,
                c.Surname,
                c.Name,
                c.Patronymic,
                d.CabinetNumber,
                c.SpecialityId,
                c.RegionId
            })
                .Join(_dbContext.Specialties, d => d.SpecialityId, s => s.Id, (d, s) => new
                {
                    d.Id,
                    d.SpecialityId,
                    d.Surname,
                    d.Name,
                    d.Patronymic,
                    d.CabinetNumber,
                    d.RegionId,
                    s.SpecialtyName
                }).ToListAsync();

            var query =
                from doctor in doctorsList
                join region in _dbContext.Regions on doctor.RegionId equals region.Id into gj
                from sj in gj.DefaultIfEmpty()
                select new DoctorViewModel
                {
                    Id = doctor.Id,
                    Surname = doctor.Surname,
                    Name = doctor.Name,
                    Patronymic = doctor.Patronymic,
                    Specialty = doctor.SpecialtyName,
                    Cabinet = doctor.CabinetNumber,
                    Region = sj?.RegionNumber ?? string.Empty
                };

            return query.ToList();
        }
    }
}
