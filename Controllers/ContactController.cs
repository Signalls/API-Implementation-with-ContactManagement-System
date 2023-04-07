using ContactData;
using ContactData.Entities;
using ContactData.Repository;
using ContactManager.service;
using ContactModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    [Authorize]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly DbContactContext _dbContactContext;
        private readonly ICloudinaryServices _cloudinaryServices;

        public ContactController(IContactRepository contactRepository, DbContactContext Context, ICloudinaryServices cloudinaryServices)
        {
            _contactRepository = contactRepository;
            _dbContactContext = Context;
            _cloudinaryServices = cloudinaryServices;
        }
        [AllowAnonymous]
        [HttpPost("addContact")]
        public ActionResult AddContact([FromBody] AddContactDTo model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contact = new Contact
            {

                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                City = model.City,

            };
            var result = _contactRepository.AddNewContact(contact);

            if (result == null)
            {
                return BadRequest("fails to Create User");
            }

            return Ok(result);
        }

        [Authorize(Roles = "admin")]

        [HttpDelete("delete/{Id}")]

        public ActionResult DeleteContact(string Id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Id))
                {
                    return BadRequest("Id provided must not be empty");
                }
                var result = _contactRepository.GetConatctById(Id);

                if (result == null)
                {
                    return NotFound("No Record Found!");
                }
                var user = _contactRepository.DeleteContact(result);

                if (user)
                    return Ok($"User with id{result.Id} has been deleted");
                return BadRequest("Failde to delete user");

            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }








        }

        [Authorize(Roles = "regular")]

        [HttpPatch("photos")]
        public async Task<IActionResult> UploadPhoto(IFormFile file, string id)
        {
            var contact = _contactRepository.GetConatctById(id);
            if (contact == null)
            {
                return NotFound("Contact to upload picture to not available");
            }

            var result = await _cloudinaryServices.UploadAsync(file);

            if (result != null)
            {
                contact.ImageUrl = result["Url"];
                _contactRepository.UpdateContact(contact);


                return Ok(result["Url"]);

            }
            else
            {
                return BadRequest("Upload was not successful");
            }



        }

        [Authorize(Roles = "admin")]
        [HttpDelete("delete/list")]

        public ActionResult DeleteManyContact([FromBody] DeleteManyContactsDto model)
        {

            try
            {
                if (model.Ids.Count() < 1)
                {
                    return BadRequest("No Id was provided");
                }


                var list = new List<Contact>();
                foreach (var Id in model.Ids)
                {
                    var userId = _contactRepository.GetConatctById(Id);
                    if (userId != null)
                    {
                        list.Add(userId);
                    }
                }

                if (list.Count > 0)
                {
                    var user = _contactRepository.DeleteContact(list);
                    if (user)
                        return Ok("List of users deleted");
                }

                return NotFound("failed to delete users");

            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }



        }
        [Authorize(Roles = "regular")]
        [HttpPut("update/{Id}")]
        public ActionResult UpdateUser(string Id, [FromBody] UpdateContactDto model)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(Id))
                {
                    return BadRequest("Id provided must not be empty");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (Id != model.Id)
                {
                    return BadRequest("Id MisMatch!");
                }
                var result = _contactRepository.GetConatctById(Id);

                if (result == null)
                {
                    return NotFound("No Record Found!");
                }

                result.PhoneNumber = model.PhoneNumber;
                result.Address = model.Address;
                result.City = model.City;
                result.Email = model.Email;
                var user = _contactRepository.UpdateContact(result);
                if (user != null)
                {
                    return Ok(user);
                }
                return BadRequest("Faild to update user");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        [Authorize(Roles = "admin")]
        [HttpGet("single/{Id}")]

        public ActionResult GetContactById(string Id)
        {

            try
            {

                if (string.IsNullOrWhiteSpace(Id))
                {
                    return BadRequest("Empty parameter");
                }

                var result = _contactRepository.GetConatctById(Id);

                if (result == null)
                {
                    return NotFound("No record found");
                }

                return Ok(result);



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }





        }

        [Authorize(Roles = "admin")]

        [HttpGet("List")]

        public ActionResult GetAll(int page = 1, int perpage = 5)
        {
            var result = _contactRepository.GetAllContact();
            if (result.Count() > 0)
            {
                var paged = _contactRepository.Paginate(result.ToList(), perpage, page);

                return Ok(paged);
            }
            return Ok(result);
        }


    }



}
