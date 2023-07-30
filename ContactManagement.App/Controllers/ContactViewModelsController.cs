using AutoMapper;
using ContactManagement.App.ViewModels;
using ContactManagement.Business.Interfaces;
using ContactManagement.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.App.Controllers
{
    public class ContactViewModelsController : Controller
    {
        private readonly IRepository<Contact> _repository;
        private readonly IMapper _mapper;

        public ContactViewModelsController(IRepository<Contact> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ContactViewModel>>(await _repository.GetAll()));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var ContactViewModel = await _repository.GetById(id);
            if (ContactViewModel is null)
                return NotFound();

            return View(ContactViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid) return View(contactViewModel);

            var contact = _mapper.Map<Contact>(contactViewModel);
            await _repository.Add(contact);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var contactViewModel = await _repository.GetById(Id);
            if (contactViewModel is null) return NotFound();

            return View(contactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ContactViewModel contactViewModel)
        {
            if (id != contactViewModel.ID) return NotFound();

            if (!ModelState.IsValid) return View(contactViewModel);

            var contact = _mapper.Map<Contact>(contactViewModel);
            await _repository.Update(contact);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var contactViewModel = await _repository.GetById(id);
            if (contactViewModel is null)
            {
                return NotFound();
            }

            return View(contactViewModel);
        }

        // POST: ContactViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contactViewModel = await _repository.GetById(id);

            if (contactViewModel is null) return NotFound();

            await _repository.Remove(id);

            return RedirectToAction("Index");
        }
    }
}
