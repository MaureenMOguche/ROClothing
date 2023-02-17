using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROClothing.Data.Repository.IRepository;
using ROClothing.Models.ViewModels;
using System.Security.Claims;

namespace ROClothing.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM shoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVM = new()
            {
                cartList = _unitOfWork.ShoppingCartRepo.GetAll(x => x.ApplicationUserId == claim.Value, includeProperties: "Product"),
            };

            foreach (var item in shoppingCartVM.cartList)
            {
                shoppingCartVM.cartTotal += (item.Product.Price * item.NoOfItems);
            }

            return View(shoppingCartVM);
        }


        public IActionResult MinusCartItem(int cartId) 
        {
            var cart = _unitOfWork.ShoppingCartRepo.FindFirst(x => x.Id == cartId);

            if (cart.NoOfItems <= 1)
            {
                _unitOfWork.ShoppingCartRepo.Remove(cart);
            }
            else
            {
                _unitOfWork.ShoppingCartRepo.DecrementShoppingCart(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PlusCartItem(int cartId)
        {
            var cart = _unitOfWork.ShoppingCartRepo.FindFirst(x => x.Id == cartId);
            
            if (cart.NoOfItems >= 1000)
            {
                cart.NoOfItems = 1000;
            }
            else
            {
                _unitOfWork.ShoppingCartRepo.IncrementShoppingCart(cart, 1);

            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveCartItem(int cartId)
        {
            var cart = _unitOfWork.ShoppingCartRepo.FindFirst(x => x.Id == cartId);
            _unitOfWork.ShoppingCartRepo.Remove(cart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
