using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IReservationsService
    {
        Task<List<ReservationsEfo>> GetAllReservations();
        Task<ReservationsEfo> GetReservationById(int id);
        Task<ReservationsEfo> SendReservation(ReservationsEfo reservation);
        Task<ReservationsEfo> UpdateReservation(int id, ReservationsEfo updateReservation);
        Task DeleteReservation(int id);
    }

    public class ReservationsService : IReservationsService
    {
        private readonly TheKnifeDbContext _context;

        public ReservationsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationsEfo>> GetAllReservations()
        {
            return await _context.Reservations.ToListAsync();
        }

        public async Task<ReservationsEfo> GetReservationById(int id)
        {
            ReservationsEfo reservation = await _context.Reservations.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                throw new Exception("Reserva não encontrada");
            }

            return reservation;
        }

        public async Task<ReservationsEfo> SendReservation(ReservationsEfo reservation)
        {
            try
            {
                await _context.Reservations.AddAsync(reservation);

                await _context.SaveChangesAsync();

                return reservation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a fazer a reserva: {ex.Message}");
            }
        }

        public async Task<ReservationsEfo> UpdateReservation(int id, ReservationsEfo updateReservation)
        {
            try
            {
                ReservationsEfo newReservation = await _context.Reservations
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (newReservation == null)
                {
                    throw new Exception("Reserva não encontrada");
                }

                newReservation.ClientName = updateReservation.ClientName;
                newReservation.PhoneNumber = updateReservation.PhoneNumber;
                newReservation.ReservationDate = updateReservation.ReservationDate;
                newReservation.ReservationTime = updateReservation.ReservationTime;
                newReservation.NumberPeople = updateReservation.NumberPeople;

                await _context.SaveChangesAsync();

                return newReservation;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro a atualizar reserva: {ex.Message}");
            }
        }

        public async Task DeleteReservation(int id)
        {
            ReservationsEfo reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                throw new Exception("Reserva não encontrada");
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
        }
    }
}
