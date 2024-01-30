using Microsoft.EntityFrameworkCore;
using TheKnife.Entities.Efos;
using TheKnife.EntityFramework;

namespace TheKnife.Services.Services
{
    public interface IContactsService
    {
        Task<List<ContactsEfo>> GetAllContacts();
        Task<ContactsEfo> GetContactById(int id);
        Task<ContactsEfo> SendContact(ContactsEfo contact);
        Task DeleteContact(int id);
    }

    public class ContactsService : IContactsService
    {
        private readonly TheKnifeDbContext _context;

        public ContactsService(TheKnifeDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContactsEfo>> GetAllContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<ContactsEfo> GetContactById(int id)
        {
            ContactsEfo contact = await _context.Contacts.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null)
            {
                throw new Exception("Contacto não encontrado");
            }

            return contact;
        }

        public async Task<ContactsEfo> SendContact(ContactsEfo contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task DeleteContact(int id)
        {
            ContactsEfo contact = await _context.Contacts
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null)
            {
                throw new Exception("Contacto não encontrado");
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}
