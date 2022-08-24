using AutoMapper;
using Microsoft.EntityFrameworkCore;
using products.Domain.Carts.Contracts;
using products.Domain.Carts.DTOs;
using products.Domain.Carts.Entities;
using products.Domain.Infra.Context;

namespace products.Domain.Infra.Repositories;
public class CartRepository : ICartRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;
    public CartRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public Task<bool> ApplyVoucher(int userId, string voucher)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CleanCart(int userId)
    {
        var cartHeader = await _db.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
        if (cartHeader is not null)
        {
            _db.CartItems.RemoveRange(_db.CartItems.Where(c => c.CartHeaderId == cartHeader.Id));
            await _db.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> DeleteCartItem(int cartItemId)
    {
        try
        {
            CartItem cartItem = await _db.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);
            var total = _db.CartItems.Where(c => c.CartHeaderId == cartItem.CartHeaderId).Count();
            _db.CartItems.Remove(cartItem);

            if (total == 1)
            {
                var cartHeaderRemove = await _db.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartItem.CartHeaderId);
                _db.CartHeaders.Remove(cartHeaderRemove);
            }
            await _db.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<CartDTO> GetCartByUserID(int id)
    {
        Cart cart = new Cart
        {
            CartHeader = await _db.CartHeaders.FirstOrDefaultAsync(c => c.UserId == id)
        };
        cart.Items = _db.CartItems.Where(c => c.CartHeaderId == cart.CartHeader.Id).Include(i => i.Item);
        return _mapper.Map<CartDTO>(cart);
    }

    public Task<bool> SeleteVoucher(int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<CartDTO> UpdateCart(CartDTO cartDTO)
    {
        Cart cart = _mapper.Map<Cart>(cartDTO);
        await SaveProductInDatabase(cartDTO, cart);
        var cartHeader = await _db.CartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);
        if (cartHeader is null) await CreateCartHeaderAndItems(cart);
        await UpdateQuantityAndItems(cartDTO, cart, cartHeader);
        return _mapper.Map<CartDTO>(cart);
    }

    private async Task UpdateQuantityAndItems(CartDTO cartDTO, Cart cart, CartHeader? cartHeader)
    {
        var cartDetail = await _db.CartItems
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.ItemId == cartDTO.Items.FirstOrDefault().ItemId
                        && x.CartHeaderId == cartHeader.Id);

        if (cartDetail is null)
        {
            cart.Items.FirstOrDefault().CartHeaderId = cartHeader.Id;
            cart.Items.FirstOrDefault().Item = null;
            _db.CartItems.Add(cart.Items.FirstOrDefault());
            await _db.SaveChangesAsync();
        }
        else
        {
            cart.Items.FirstOrDefault().Item = null;
            cart.Items.FirstOrDefault().Quantity += cartDetail.Quantity;
            cart.Items.FirstOrDefault().Id = cartDetail.Id;
            cart.Items.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
            _db.CartItems.Update(cart.Items.FirstOrDefault());
            await _db.SaveChangesAsync();
        }
    }

    private async Task CreateCartHeaderAndItems(Cart cart)
    {
        _db.CartHeaders.Add(cart.CartHeader);
        await _db.SaveChangesAsync();

        cart.Items.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
        cart.Items.FirstOrDefault().Item = null;

        _db.CartItems.Add(cart.Items.FirstOrDefault());
        await _db.SaveChangesAsync();
    }

    private async Task SaveProductInDatabase(CartDTO cartDTO, Cart cart)
    {
        var item = await _db.Itens.FirstOrDefaultAsync(i => i.Id == cartDTO.Items.FirstOrDefault().ItemId);
        if (item is null)
        {
            _db.Itens.Add(cart.Items.FirstOrDefault().Item);
            await _db.SaveChangesAsync();
        }
    }
}
