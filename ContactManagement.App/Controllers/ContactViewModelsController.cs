using AutoMapper;
using ContactManagement.App.Areas.Identity.Data;
using ContactManagement.App.Extensions;
using ContactManagement.App.ViewModels;
using ContactManagement.Business.Interfaces;
using ContactManagement.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagement.App.Controllers
{
    public class ContactViewModelsController : Controller
    {
        private readonly IRepository<Contact> _repository;
        private readonly IMapper _mapper;
        private readonly ContactManagementAppContext _context;

        public ContactViewModelsController(IRepository<Contact> repository, IMapper mapper, ContactManagementAppContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ContactViewModel>>(await _repository.GetAll()));
        }

        [ClaimsAuthorize("User", "Details")]
        public async Task<IActionResult> Details(Guid id)
        {
            var ContactViewModel = await _repository.GetById(id);
            if (ContactViewModel is null)
                return NotFound();

            return View(ContactViewModel);
        }

        [ClaimsAuthorize("User", "Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("User", "Create")]
        public async Task<IActionResult> Create(ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid) return View(contactViewModel);

            var find = _repository.GetAll().Result.FirstOrDefault(p => p.Email == contactViewModel.Email);

            if(find is not null) return View(contactViewModel);

            IdentityUserClaim<string> userClaim = new IdentityUserClaim<string>();
            userClaim.UserId = contactViewModel.ID.ToString();

            var contact = _mapper.Map<Contact>(contactViewModel);
            await _repository.Add(contact);

            _context.UserClaims.Add(userClaim);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("User", "Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var contactViewModel = await _repository.GetById(id);
            if (contactViewModel is null) return NotFound();

            return View(contactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("User", "Edit")]
        public async Task<IActionResult> Edit(Guid id, ContactViewModel contactViewModel)
        {
            if (id != contactViewModel.ID) return NotFound();

            if (!ModelState.IsValid) return View(contactViewModel);

            var contact = _mapper.Map<Contact>(contactViewModel);
            await _repository.Update(contact);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("User", "Delete")]
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
        [ClaimsAuthorize("User", "Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var contactViewModel = await _repository.GetById(id);

            if (contactViewModel is null) return NotFound();

            await _repository.Remove(id);

            return RedirectToAction("Index");
        }
    }
}
