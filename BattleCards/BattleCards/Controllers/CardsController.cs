namespace BattleCards.Controllers
{
    using BattleCards.Data;
    using BattleCards.Data.Models;
    using BattleCards.Models.Cards;
    using BattleCards.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Collections.Generic;
    using System.Linq;

    public class CardsController : Controller
    {
        private readonly BattleCardsDbContext data;
        private readonly IValidator validator;

        public CardsController(BattleCardsDbContext data, IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var cards = this.data.Cards
                .Select(c => new CardsListingViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    ImageUrl = c.ImageUrl,
                    Description = c.Description,
                    Keyword = c.Keyword,
                    Attack = c.Attack.ToString(),
                    Health = c.Health.ToString()
                })
                .ToList();

            return View(cards);
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (!this.User.IsAuthenticated)
            {
                return BadRequest();
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(CreateCardFormModel model)
        {
            var modelErrors = this.validator.ValidateCard(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var card = new Card
            {
                Name = model.Name,
                ImageUrl = model.Image,
                Keyword = model.Keyword,
                Description = model.Description,
                Attack = model.Attack,
                Health = model.Health
            };

            if (this.data.Cards.Any(x => x.Name == card.Name))
            {
                return Error($"Card with {card.Name} already exists.");
            }

            this.data.Cards.Add(card);

            this.data.SaveChanges();

            return Redirect("/Cards/All");
        }

        [Authorize]
        public HttpResponse Collection()
        {
            var userCards = this.data.UserCards
                .Where(x => x.UserId == this.User.Id)
                .Select(c => c.Card)
                .Select(c => new CardsListingViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    ImageUrl = c.ImageUrl,
                    Attack = c.Attack.ToString(),
                    Health = c.Health.ToString(),
                    Keyword = c.Keyword
                })
                .ToList();

            return View(userCards);
        }

        [Authorize]
        public HttpResponse AddToCollection(int cardId)
        {
            var cardQuery = this.data.UserCards
                .Where(x => x.CardId == cardId && x.UserId == this.User.Id)
                .FirstOrDefault();

            if (cardQuery != null)
            {
                return Error($"Card is already in user's collection");
            }

            var userCard = new UserCard
            {
                CardId = cardId,
                UserId = this.User.Id
            };

            this.data.UserCards.Add(userCard);
            this.data.SaveChanges();

            return Redirect("/Cards/All");
        }

        [Authorize]
        public HttpResponse RemoveFromCollection(int cardId)
        {
            var userCard = this.data.UserCards
                .Where(x => x.CardId == cardId && x.UserId == this.User.Id)
                .FirstOrDefault();

            this.data.UserCards.Remove(userCard);
            this.data.SaveChanges();

            return Redirect("/Cards/Collection");
        }
    }
}
