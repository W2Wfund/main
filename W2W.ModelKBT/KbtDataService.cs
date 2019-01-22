using KBTech.Lib;
using KBTech.Lib.Client;
using KBTech.Lib.Client.Payment;
using KBTech.Lib.Model;
using KBTech.Lib.Query;
using KBTech.Lib.Query.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W2W.ModelKBT.Entities;

namespace W2W.ModelKBT
{
    public class KbtDataService : BaseDataService, IDataService
    {
        #region Partner
        public Partner Login(string identity, string password)
        {
            var query = CreateQueryItem<Partner>("Контрагент", Level.All);
            var col = query.AddConditionCollection(Union.None);
            col.AddConditionItem(Union.None, "Логин", Operator.Equal, identity.Trim());
            col.AddConditionItem(Union.Or, "Контакты.Email", Operator.Equal, identity.Trim());
            col.AddConditionItem(Union.Or, "ПерсональныйНомерКонтрагента", Operator.Equal, identity.Trim());
            query.AddConditionItem(Union.And, "Пароль", Operator.Equal, password);
            //query.AddConditionItem(Union.And, "Роль.Клиент", Operator.Equal, true);
            return this.GetInstance<Partner>(query);
        }
        public Partner GetPartner(string identity)
        {
            var query = CreateQueryItem<Partner>("Контрагент", Level.All);
            query.AddConditionItem(Union.None, "Логин", Operator.Equal, identity.Trim());
            query.AddConditionItem(Union.Or, "ПерсональныйНомерКонтрагента", Operator.Equal, identity.Trim());
            //query.AddConditionItem(Union.And, "Роль.Клиент", Operator.Equal, true);
            return this.GetInstance<Partner>(query);
        }
        public Partner GetPartner(uint id_object)
        {
            var query = base.CreateQueryItem<Partner>("Контрагент", Level.All);
            query.AddConditionItem(Union.None, "id_object", Operator.Equal, id_object);
            return base.GetInstance<Partner>(query);
        }

        public Partner GetPartnerByEmail(string email)
        {
            var query = CreateQueryItem<Partner>("Контрагент", Level.All);
            query.AddConditionItem(Union.None, "Контакты.Email", Operator.Equal, email.Trim());
            //query.AddConditionItem(Union.And, "Роль.Клиент", Operator.Equal, true);
            return this.GetInstance<Partner>(query);
        }
        public IEnumerable<Partner> GetPartners()
        {
            var query = CreateQueryItem<Partner>("Контрагент", Level.All);
            return base.GetList<Partner>(query);
        }
        public void UpdatePartnerPassword(uint id_client, string password)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Пароль", password);
                client.SetObjectValues(id_client, values);
            }
        }
        public void UpdatePartnerEmailConfirmation(uint id_client, bool? emailConfirmation)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Контакты.Email.Подтвержден", emailConfirmation);
                client.SetObjectValues(id_client, values);
            }
        }
        public void UpdatePartnerEmail(uint id_client, string email)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Контакты.Email", email);
                values.Add("Контакты.Email.Подтвержден", false);
                client.SetObjectValues(id_client, values);
            }
        }
        public void UpdatePartnerProfile(
            uint id_client,
            string login,
            string lastname,
            string firstname,
            string middlename,
            string gender,
            string country,
            string city,
            DateTime? birthdate,
            string phonenumber,
            string soclink)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Логин", login);
                values.Add("Паспорт.Фамилия", lastname);
                values.Add("Паспорт.Имя", firstname);
                values.Add("Паспорт.Отчество", middlename);
                values.Add("Пол", gender);
                values.Add("Страна", country);
                values.Add("Город", city);
                values.Add("Паспорт.ДатаРождения", birthdate);
                values.Add("Контакты.ТелефонКонтактный", phonenumber);
                values.Add("СоцСети", soclink);
                if (string.IsNullOrWhiteSpace(middlename))
                    values.Add("Название", $"{firstname} {lastname}");
                else
                    values.Add("Название", $"{firstname} {middlename} {lastname}");

                client.SetObjectValues(id_client, values);
            }
        }

        public void UpdateGoogleAuthKey(uint id_client,
              string GoogleAuthKey,
              bool? GoogleAuthEnabled)
        { 
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("GoogleAuthKey", GoogleAuthKey);
                values.Add("GoogleAuthEnabled", GoogleAuthEnabled);
                client.SetObjectValues(id_client, values);
            }
        }

        public void UpdatePartnerWallets(uint id_client,
            string walletBitcoin,
            string walletEthereum,
            string walletLitecoin,
            string walletBitcoinCash,
            string walletRipple)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Кошелек.Bitcoin", walletBitcoin);
                values.Add("Кошелек.Ethereum", walletEthereum);
                values.Add("Кошелек.Litecoin", walletLitecoin);
                values.Add("Кошелек.BitcoinCash", walletBitcoinCash);
                values.Add("Кошелек.Ripple", walletRipple);
                client.SetObjectValues(id_client, values);
            }
        }
        public void UpdatePartnerAvatar(string path, uint id_client)
        {
            using (var client = new WebDataClient())
            {
                client.SetObjectValue(id_client, "Изображение", path);
            }
        }
        public IEnumerable<Partner> GetPartners(uint id_object, string searchText)
        {
            using (var client = new WebDataClient())
            {
                var query = base.CreateQueryItem<Partner>("Контрагент", Level.All);
                query.AddConditionItem(Union.And, "СсылкаНаСпонсора", Operator.Equal, id_object);
                query.AddOrder("ДатаСоздания", Direction.Desc);
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    var col = query.AddConditionCollection(Union.And);
                    col.AddConditionItem(Union.None, "Название", Operator.Like, "%" + searchText.Trim() + "%");
                    col.AddConditionItem(Union.Or, "Логин", Operator.Like, "%" + searchText.Trim() + "%");
                }
                return base.GetList<Partner>(query);
            }
        }
        public IEnumerable<Partner> GetPartners(uint rootId)
        {
            using (var client = new WebDataClient())
            {
                var query = base.CreateQueryItem<Partner>("Контрагент", Level.All);
                query.AddPlace(rootId, 0, 180);
                return base.GetList<Partner>(query);
            }
        }
        private int GetId()
        {
            using (var client = new WebDataClient())
            {
                var idList = new List<int>();
                var query = new QueryItem();
                query.AddProperty("ПерсональныйНомерКонтрагента");
                query.AddConditionItem(Union.None, "ПерсональныйНомерКонтрагента", Operator.IsNotNull);
                var items = client.Search(query).ResultItems();
                foreach (var item in items)
                    idList.Add(item.Value<int>("ПерсональныйНомерКонтрагента"));

                for (var i = 100; i < int.MaxValue; i++)
                    if (!idList.Contains(i))
                        return i;
                return 0;
            }
        }
        public uint CreatePartner(
          uint inviterId, // допустимо 0 использовать
          string firstName,
          string lastName,
          string middleName,
          string email,
          string login,
          string password)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.AddValueOrDefault("Роль.Клиент", true);
                values.AddValueOrDefault("Паспорт.Фамилия", lastName);
                values.AddValueOrDefault("Паспорт.Имя", firstName);
                values.AddValueOrDefault("Паспорт.Отчество", middleName);
                values.AddValueOrDefault("Контакты.Email", email);
                values.AddValueOrDefault("Логин", login);
                values.AddValueOrDefault("Пароль", password);
                values.AddValueOrDefault("СсылкаНаСпонсора", inviterId);
                values.AddValueOrDefault("РедакторОбъекта", null);
                values.AddValueOrDefault("ПерсональныйНомерКонтрагента", GetId());
                if (inviterId > 0)
                    return client.InsertObject(inviterId, "ФизическоеЛицо", values);
                else
                    return client.InsertObject(5, "ФизическоеЛицо", values);
            }
        }
        public void UpdatePartnerPassport(uint id_client, string passport1, string passport2, string passport3)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("ИзображениеПаспорт1", passport1);
                values.Add("ИзображениеПаспорт2", passport2);
                values.Add("ИзображениеПаспорт3", passport3);
                client.SetObjectValues(id_client, values);
            }
        }
        public void UpdateParnterVerificationStatus(uint partnerId, string status)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("СтатусВерификации", status);
                client.SetObjectValues(partnerId, values);
            }
        }
        public void UpdatePartnerActivity(uint partnerId, bool? isActive)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Активный", isActive);
                client.SetObjectValues(partnerId, values);
            }
        }
        #endregion
        #region File
        public byte[] ReadFile(string path)
        {
            using (var client = new WebDataClient())
            {
                return client.ReadFile(path);
            }
        }
        public string WriteFile(string path)
        {
            using (var client = new WebDataClient())
            {
                return client.WriteFile(path);
            }
        }
        public void DeleteFile(string path)
        {
            using (var client = new WebDataClient())
            {
                client.DeleteFile(path);
            }
        }
        #endregion
        #region Marketing
        public IEnumerable<MarketingPlace> GetStructure(uint marketingId)
        {
            var query = CreateQueryItem<MarketingPlace>("МаркетинговоеМесто", Level.All);
            query.AddConditionItem(Union.And, "СсылкаНаМаркетинг", Operator.Equal, marketingId);
            return base.GetList<MarketingPlace>(query);
        }
        public IEnumerable<MarketingPlace> GetPlaces(uint marketingId, uint partnerId)
        {
            var query = CreateQueryItem<MarketingPlace>("МаркетинговоеМесто", Level.All);
            query.AddConditionItem(Union.And, "СсылкаНаМаркетинг", Operator.Equal, marketingId);
            query.AddConditionItem(Union.And, "СсылкаНаКонтрагента", Operator.Equal, partnerId);
            return base.GetList<MarketingPlace>(query);
        }
        public void SetPlaceChildCount(uint placeId, int? count)
        {
            using (var client = new WebDataClient())
            {
                client.SetObjectValue(placeId, "ЗанятоМест", count);
            }
        }
        public uint CreatePlace(
            uint marketingId,
            uint partnerId,
            uint parentId,
            int? pos,
            decimal entrySum,
            bool isActive,
            string alias,
            string name,
            string rank,
            string user)
        {
            var values = new Dictionary<string, object>();
            values.Add("Активно", isActive);
            values.Add("Алиас", alias);
            values.Add("Ранг", rank);
            values.Add("СсылкаНаКонтрагента", partnerId);
            values.Add("СсылкаНаМаркетинг", marketingId);
            values.Add("СуммаВхода", entrySum);
            values.Add("Название", name);
            values.Add("РедакторОбъекта", user);
            values.Add("Сортировка.Позиция", pos);
            using (var client = new WebDataClient())
            {
                return client.InsertObject(parentId, "МаркетинговоеМесто", values);
            }
        }
        public MarketingPlace GetPlace(uint placeId)
        {
            var query = base.CreateQueryItem<MarketingPlace>("МаркетинговоеМесто", Level.All);
            query.AddConditionItem(Union.None, "id_object", Operator.Equal, placeId);
            return base.GetInstance<MarketingPlace>(query);
        }
        public int GetPlaceCount(uint marketingId, uint partnerId)
        {
            var query = new QueryItem();
            query.AddType("МаркетинговоеМесто", Level.All);
            query.AddConditionItem(Union.None, "СсылкаНаМаркетинг", Operator.Equal, marketingId);
            query.AddConditionItem(Union.And, "СсылкаНаКонтрагента", Operator.Equal, partnerId);

            using (var client = new WebDataClient())
            {
                return Convert.ToInt32(client.Search(query).ResultCount());
            }

        }
        public int GetReferCount(uint marketingId, uint partnerId)
        {
            var query = new QueryItem();
            query.AddType("МаркетинговоеМесто", Level.All);
            query.AddConditionItem(Union.None, "СсылкаНаМаркетинг", Operator.Equal, marketingId);
            query.AddConditionItem(Union.And, "СсылкаНаКонтрагента/СсылкаНаСпонсора", Operator.Equal, partnerId);
            query.AddConditionItem(Union.And, "СуммаВхода", Operator.More, 0);

            using (var client = new WebDataClient())
            {
                return Convert.ToInt32(client.Search(query).ResultCount());
            }
        }
        public IEnumerable<MarketingPlan> GetMarketingPlans()
        {
            var query = CreateQueryItem<MarketingPlan>("МаркетингПлан", Level.All);
            query.AddOrder("Сортировка.Позиция", Direction.Asc);
            return this.GetList<MarketingPlan>(query);
        }
        public IEnumerable<MarketingPlace> GetStructure(uint marketingId, uint rootId)
        {
            var query = CreateQueryItem<MarketingPlace>("МаркетинговоеМесто", Level.All);
            query.AddConditionItem(Union.And, "СсылкаНаМаркетинг", Operator.Equal, marketingId);
            query.AddPlace(rootId, 0, 180);
            return this.GetList<MarketingPlace>(query);
        }
        public decimal GetOwnAndRefsValue(uint id_partner)
        {
            using (var client = new WebDataClient())
            {
                var query = new QueryItem();
                query.AddType("Контрагент", Level.All);
                query.AddProperty("Остаток.ИнвестиционныйСчет", Function.Sum);
                query.AddPlace(id_partner, 0, 1);
                var item = client.Search(query).ResultItem(0);
                return item.Value<decimal?>("Остаток.ИнвестиционныйСчет") ?? 0.0m;
            }
        }
        #endregion
        #region Promo
        public IEnumerable<Article> GetNews()
        {
            var query = base.CreateQueryItem<Article>("Новость", Level.All);
            query.AddConditionItem(Union.And, "ДатаПубликации", Operator.LessOrEqual, DateTime.Now);
            query.AddConditionItem(Union.And, "ДатаЗавершения", Operator.More, DateTime.Now);
            query.AddOrder("ДатаПубликации", Direction.Desc);
            return base.GetList<Article>(query);
        }
        public Article GetArticle(int id)
        {
            var query = base.CreateQueryItem<Article>("Новость", Level.All);
            query.AddConditionItem(Union.And, "id_object", Operator.Equal, id);
            return base.GetInstance<Article>(query);
        }
        public IEnumerable<Partner> GetReviews()
        {
            var query = new QueryItem("Контрагент", Level.All, new[] {
                "ДатаПубликацииОтзыва", "Отзыв", "Название", "Изображение"
            });
            query.AddConditionItem(Union.And, "СтатусОтзыва", Operator.Equal, "Опубликован");
            query.AddOrder("ДатаПубликацииОтзыва", Direction.Desc);
            return base.GetList<Partner>(query);
        }
        public IEnumerable<Promo> GetPromos()
        {
            var query = base.CreateQueryItem<Promo>("Рекламный_материал", Level.All);
            query.AddOrder("Название", Direction.Asc);
            return base.GetList<Promo>(query);
        }
        public IEnumerable<Poll> GetPolls()
        {
            var query = base.CreateQueryItem<Poll>("Опрос", Level.All);
            query.AddConditionItem(Union.And, "ДатаПубликации", Operator.LessOrEqual, DateTime.Now);
            query.AddConditionItem(Union.And, "ДатаЗавершения", Operator.More, DateTime.Now);
            query.AddOrder("ДатаПубликации", Direction.Desc);
            var polls = base.GetList<Poll>(query);
            return polls;
        }
        public IEnumerable<PollVariant> GetPollVariants(uint pollId)
        {
            var query = base.CreateQueryItem<PollVariant>("Вариант_ответа", Level.All);
            query.AddConditionItem(Union.And, "СсылкаНаОпрос", Operator.Equal, pollId);
            query.AddOrder("Сортировка.Позиция", Direction.Asc);
            return base.GetList<PollVariant>(query);
        }
        public PollChoice GetPollChoise(uint pollId, uint partnerId)
        {
            var query = base.CreateQueryItem<PollChoice>("Голос", Level.All);
            query.AddConditionItem(Union.And, "СсылкаНаОпрос", Operator.Equal, pollId);
            query.AddConditionItem(Union.And, "СсылкаНаКонтрагента", Operator.Equal, partnerId);
            return base.GetInstance<PollChoice>(query);
        }
        public IEnumerable<Charity> GetCharity()
        {
            var query = base.CreateQueryItem<Charity>("Благотворительный_фонд", Level.All);
            query.AddOrder("Название", Direction.Asc);
            return base.GetList<Charity>(query);
        }
        public Charity GetCharity(uint id_object)
        {
            var query = base.CreateQueryItem<Charity>("Благотворительный_фонд", Level.All);
            query.AddConditionItem(Union.And, "id_object", Operator.Equal, id_object);
            return base.GetInstance<Charity>(query);
        }
        public void SelectPollVariant(uint pollVariantId, uint userId)
        {
            using (var client = new WebDataClient())
            {
                var pollId = client.GetValue<uint>(pollVariantId, "СсылкаНаОпрос");
                if (pollId > 0)
                {
                    var values = new Dictionary<string, object>();
                    values.Add("СсылкаНаВариант", pollVariantId);
                    values.Add("СсылкаНаКонтрагента", userId);
                    values.Add("СсылкаНаОпрос", pollId);
                    client.InsertObject(userId, "Голос", values);

                    //increment poll variant counter
                    var count = client.GetValue<int>(pollVariantId, "КоличествоГолосов");
                    client.SetObjectValue(pollVariantId, "КоличествоГолосов", count + 1);

                }
            }
        }
        public void AddReview(uint id_client, string review)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("ДатаПубликацииОтзыва", DateTime.Now);
                values.Add("Отзыв", review);
                values.Add("СтатусОтзыва", "На проверке");
                client.SetObjectValues(id_client, values);
            }
        }
        #endregion
        #region investment
        public InvestProgram GetInvestProgram(decimal sum)
        {
            var query = CreateQueryItem<InvestProgram>("ИнвестиционнаяПрограмма", Level.All);
            query.AddConditionItem(Union.None, "МинимальнаяСумма", Operator.LessOrEqual, sum);
            query.AddConditionItem(Union.And, "МаксимальнаяСумма", Operator.MoreOrEqual, sum);
            return base.GetInstance<InvestProgram>(query);
        }
        public uint CreateInvestment(uint companyId, uint programId, uint partnerId,
            decimal sum, bool isProlonged, DateTime date, string user)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Пролонгация", isProlonged);
                values.Add("СсылкаНаПрограмму", programId);
                values.Add("ДатаОплаты", date);
                values.Add("СсылкаНаКонтрагента", partnerId);
                values.Add("СсылкаНаОрганизацию", companyId);
                values.Add("СуммаДоговора", sum);
                values.Add("ДатаЗавершения", date.AddYears(1));
                values.Add("ДатаОформления", date);

                values.Add("РедакторОбъекта", user);
                return client.InsertObject(programId, "ИнвестиционныйДоговор", values);
            }
        }
        public IEnumerable<Investment> GetReferInvestments(uint inviterId)
        {
            using (var client = new WebDataClient())
            {
                var query = base.CreateQueryItem<Investment>("ИнвестиционныйДоговор", Level.All);
                query.AddConditionItem(Union.And, "СсылкаНаКонтрагента", Operator.NotEqual, inviterId);
                query.AddPlace(inviterId, 2, 2);
                return base.GetList<Investment>(query);
            }
        }
        public IEnumerable<Investment> GetInvestments(uint partnerId, string status)
        {
            using (var client = new WebDataClient())
            {
                var query = base.CreateQueryItem<Investment>("ИнвестиционныйДоговор", Level.All);
                if (!string.IsNullOrWhiteSpace(status))
                    query.AddConditionItem(Union.And, "Статус", Operator.Equal, status);
                query.AddConditionItem(Union.And, "СсылкаНаКонтрагента", Operator.Equal, partnerId);
                return base.GetList<Investment>(query);
            }
        }
        public IEnumerable<Investment> GetTerminatedInvestments(uint partnerId)
        {
            using (var client = new WebDataClient())
            {
                var query = base.CreateQueryItem<Investment>("ИнвестиционныйДоговор", Level.All);
                query.AddConditionItem(Union.And, "Статус", Operator.Equal, "На расторжении");
                query.AddConditionItem(Union.And, "СсылкаНаКонтрагента", Operator.Equal, partnerId);
                return base.GetList<Investment>(query);
            }
        }
        public Investment GetInvestment(uint investmentId)
        {
            var query = CreateQueryItem<Investment>("ИнвестиционныйДоговор", Level.All);
            query.AddConditionItem(Union.And, "id_object", Operator.Equal, investmentId);
            return base.GetInstance<Investment>(query);
        }
        public void SetProlonged(uint investmentId, bool isProlonged)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Пролонгация", isProlonged);
                client.SetObjectValues(investmentId, values);
            }
        }
        public void SetInvestmentStatus(uint investmentId, string status, DateTime changeDate)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Статус", status);
                values.Add("ДатаИзменения", changeDate);
                if (status == "Завершен")
                {
                    values.Add("ДатаЗавершения", changeDate);
                }
                client.SetObjectValues(investmentId, values);
            }
        }
        #endregion
        #region Finance
        public void RemovePayment(uint id_object)
        {
            using (var client = new WebDataClient())
            {
                var query = new QueryItem();
                query.AddType("Платеж", Level.All);
                query.AddConditionItem(Union.None, "id_object", Operator.Equal, id_object);
                if (client.Search(query).ResultCount() > 0)
                {
                    client.DeleteObject(id_object);
                }
            }
        }
        public uint CreateInnerTransfer(
           string accountName,
           TransferDirection direction,
           uint companyId,
           uint partnerId,
           uint documentId,
           string article,
           DateTime date,
           decimal sum,
           PaymentMethod paymentMethod,
           string comment,
           string documentType,
           string user)
        {
            if (partnerId == 0)
                return 0;

            if (string.IsNullOrEmpty(accountName))
                throw new Exception("Название счета не может быть пустым.");

            if (date == DateTime.Today)
                date = date.Date.Add(System.DateTime.Now.TimeOfDay);

            if (documentId == 0)
                documentId = partnerId;

            var values = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(accountName))
                values.Add("НазваниеСчета", accountName);

            if (companyId > 0)
                values.Add("СсылкаНаОрганизацию", companyId);


            values.Add("СтатусПлатежа", "Не оплачен");
            values.Add("ДатаПлатежа", date);
            values.Add("СсылкаНаКонтрагента", partnerId);
            values.Add("СсылкаНаДоговор", documentId);
            if (!string.IsNullOrEmpty(article))
                values.Add("СтатьяДвиженияДенежныхСредств", article);

            values.Add("Название", comment);
            values.Add("СуммаПлатежа", sum);
            switch (direction)
            {
                case TransferDirection.Input:
                    values.Add("СуммаПриход", sum);
                    values.Add("НаправлениеПлатежа", "Приход");
                    break;
                case TransferDirection.Output:
                    values.Add("СуммаРасход", sum);
                    values.Add("НаправлениеПлатежа", "Расход");
                    break;
            }
            switch (paymentMethod)
            {
                case PaymentMethod.Cash:
                    values.Add("СпособОплаты", "Наличные");
                    break;
                case PaymentMethod.Bank:
                    values.Add("СпособОплаты", "Безналичный перевод");
                    break;
                case PaymentMethod.Crypto:
                    values.Add("СпособОплаты", "Криптовалюта");
                    break;
                case PaymentMethod.eWallet:
                    values.Add("СпособОплаты", "Электронный кошелек");
                    break;
                default:
                    values.Add("СпособОплаты", "Внутренний перевод");
                    break;
            }

            if (string.IsNullOrEmpty(documentType))
                documentType = "ВнутреннийПеревод";

            if (string.IsNullOrEmpty(user))
                values.Add("РедакторОбъекта", user);

            using (var client = new WebDataClient())
            {
                return client.InsertObject(documentId, documentType, values);
            }
        }
        public void PayPayment(uint id_object, DateTime date)
        {
            using (var client = new WebDataClient())
            {
                var payment = client.GetValues(id_object, new[] {
                    "СуммаПриход", "СуммаРасход", "СсылкаНаКонтрагента",
                    "СтатусПлатежа", "НазваниеСчета"
                });

                if (payment.Value<string>("СтатусПлатежа") == "Оплачен")
                    throw new Exception("Платеж уже проведен!");

                var balance = client.GetValue<decimal?>(
                    payment.Value<uint>("СсылкаНаКонтрагента"),
                    payment.Value<string>("НазваниеСчета")) ?? 0.0m;

                balance += (payment.Value<decimal?>("СуммаПриход") ?? 0) - (payment.Value<decimal?>("СуммаРасход") ?? 0);
                client.SetObjectValue(
                    payment.Value<uint>("СсылкаНаКонтрагента"),
                    payment.Value<string>("НазваниеСчета"),
                    balance);

                var values = new Dictionary<string, object>();
                values.Add("ДатаПлатежа", date);
                values.Add("СтатусПлатежа", "Оплачен");
                values.Add("СуммаОстатокНаСчете", balance);
                client.SetObjectValues(id_object, values);
            }
        }
        public IEnumerable<StructPayment> GetStructSavings(uint placeId)
        {
            var query = CreateQueryItem<StructPayment>("СтруктурныйПлатеж", Level.All);
            query.AddConditionItem(Union.None, "СсылкаНаМесто", Operator.Equal, placeId);
            query.AddConditionItem(Union.And, "СтатусПлатежа", Operator.Equal, "Не оплачен");
            return this.GetList<StructPayment>(query);
        }
        public decimal GetStructSavingsBalance(uint partnerId)
        {
            using (var client = new WebDataClient())
            {
                var query = new QueryItem();
                query.AddType("СтруктурныйПлатеж", Level.All);
                query.AddProperty("СуммаПриход");
                query.AddConditionItem(Union.None, "СтатусПлатежа", Operator.Equal, "Не оплачен");
                query.AddConditionItem(Union.And, "СсылкаНаКонтрагента", Operator.Equal, partnerId);
                var items = client.Search(query).ResultItems();
                if (items.Length == 0)
                    return 0m;
                decimal sum = 0m;
                foreach (var item in items)
                    sum += item.Value<decimal?>("СуммаПриход") ?? 0;
                return sum;
            }
        }
        public void SetStructPaymentDetails(uint paymentId, uint placeId, int level)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("СсылкаНаМесто", placeId);
                values.Add("Глубина", level);
                client.SetObjectValues(paymentId, values);
            }
        }
        public IEnumerable<ExpectedPayment> GetExpectedPayments(uint orderId, DateTime toDate)
        {
            var query = CreateQueryItem<ExpectedPayment>("ОжидаемыйПлатеж", Level.All);
            query.AddConditionItem(Union.None, "СсылкаНаДоговор", Operator.Equal, orderId);
            query.AddConditionItem(Union.And, "ДатаПлатежа", Operator.LessOrEqual, toDate);
            query.AddConditionItem(Union.And, "СтатусПлатежа", Operator.Equal, "Не оплачен");
            return base.GetList<ExpectedPayment>(query);
        }
        public IEnumerable<ExpectedPayment> GetExpectedPayments(DateTime toDate)
        {
            var query = CreateQueryItem<ExpectedPayment>("ОжидаемыйПлатеж", Level.All);
            query.AddConditionItem(Union.And, "ДатаПлатежа", Operator.LessOrEqual, toDate);
            query.AddConditionItem(Union.And, "СтатусПлатежа", Operator.Equal, "Не оплачен");
            return base.GetList<ExpectedPayment>(query);
        }
        public IEnumerable<InnerTransfer> GetExpectedPayments(uint partnerId)
        {
            var query = CreateQueryItem<ExpectedPayment>("ОжидаемыйПлатеж", Level.All);
            query.AddConditionItem(Union.And, "СсылкаНаКонтрагента", Operator.Equal, partnerId);
            return base.GetList<ExpectedPayment>(query);
        }
        public IEnumerable<InnerTransfer> GetInnerTransfers(
            uint? partnerId,
            string account,
            string article,
            DateTime? begin,
            DateTime? end,
            uint? orderId,
            string filter)
        {
            var query = base.CreateQueryItem<InnerTransfer>("ВнутреннийПеревод", Level.All);
            query.AddOrder("ДатаПлатежа", Direction.Desc);
            query.AddConditionItem(Union.None, "СтатусПлатежа", Operator.Equal, "Оплачен");

            if (partnerId != null)
                query.AddConditionItem(Union.And, "СсылкаНаКонтрагента", Operator.Equal, partnerId);

            if(!string.IsNullOrWhiteSpace(account))
                query.AddConditionItem(Union.And, "НазваниеСчета", Operator.Equal, account);

            if (!string.IsNullOrWhiteSpace(article))
                query.AddConditionItem(Union.And, "СтатьяДвиженияДенежныхСредств", Operator.Equal, article);
            if (begin != null)
                query.AddConditionItem(Union.And, "ДатаПлатежа", Operator.MoreOrEqual, begin);
            if (end != null)
                query.AddConditionItem(Union.And, "ДатаПлатежа", Operator.LessOrEqual, end);
            if (orderId != null)
                query.AddConditionItem(Union.And, "СсылкаНаДоговор", Operator.Equal, orderId);
            if (!string.IsNullOrWhiteSpace(filter))
                query.AddConditionItem(Union.And, "Название", Operator.Like, "%" + filter.Trim() + "%");

            return this.GetList<InnerTransfer>(query);
        }
       public uint AddPayment(TransferDirection direction, uint companyId, 
            uint partnerId, uint orderId, string article,
           DateTime paymentDate, decimal sum, PaymentMethod paymentMethod, RateOfNDS rateOfNds, string desctiption, decimal paymentComission,
           string user)
        {
            using (WebDataClient client = new WebDataClient())
            {
                var id_payment = client.AddPayment(
                    direction,
                    companyId,
                    partnerId,
                    orderId,
                    article,
                    paymentDate,
                    Convert.ToDouble(sum),
                    paymentMethod,
                    rateOfNds,
                    desctiption,
                    user);

                if(paymentComission > 0)
                    client.SetObjectValue(id_payment, "КомиссияЗаВывод", paymentComission);

                return id_payment;
            }
        }
        public void UpdateDigitalPayment(
           uint id_object,
           string blockChain_transactionId,
           string paymentSystem,
           string transactionId)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("BlockChain.TransactionId", blockChain_transactionId);
                values.Add("ПлатежнаяСистема", paymentSystem);
                values.Add("Транзакция", transactionId);
                client.SetObjectValues(id_object, values);
            }
        }
        public void SetWithdrawDetails(uint id_object, string wallet, string currency, string paymentSystem)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("НомерКошелька", wallet);
                values.Add("ВылютаВывода", currency);
                values.Add("ПлатежнаяСистема", paymentSystem);
                client.SetObjectValues(id_object, values);
            }
        }

        public uint CreateReplenishRequest(
            string accountName,
            uint companyId,
            uint partnerId,
            string article,
            DateTime date,
            decimal sum,
            decimal currencySum,
            string currency,
            string paymentSystem,
            PaymentMethod paymentMethod,
            string comment,
            string documentType,
            string user)
        {
            if (partnerId == 0)
                return 0;

            var documentId = partnerId;

            if (string.IsNullOrEmpty(accountName))
                throw new Exception("Название счета не может быть пустым.");

            if (date == DateTime.Today)
                date = date.Date.Add(System.DateTime.Now.TimeOfDay);
            
            var values = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(accountName))
                values.Add("НазваниеСчета", accountName);

            if (companyId > 0)
                values.Add("СсылкаНаОрганизацию", companyId);


            values.Add("СтатусПлатежа", "Не оплачен");
            values.Add("ДатаПлатежа", date);
            values.Add("СсылкаНаКонтрагента", partnerId);
            values.Add("СсылкаНаДоговор", partnerId);
            if (!string.IsNullOrEmpty(article))
                values.Add("СтатьяДвиженияДенежныхСредств", article);

            values.Add("Название", comment);
            values.Add("СуммаПлатежа", sum);
            values.Add("СуммаПриход", sum);
            values.Add("НаправлениеПлатежа", "Приход");

            values.Add("ВылютаВывода", currency);
            values.Add("ПлатежнаяСистема", paymentSystem);
            values.Add("СуммаВВалюте", currencySum);
            

            switch (paymentMethod)
            {
                case PaymentMethod.Cash:
                    values.Add("СпособОплаты", "Наличные");
                    break;
                case PaymentMethod.Bank:
                    values.Add("СпособОплаты", "Безналичный перевод");
                    break;
                case PaymentMethod.Crypto:
                    values.Add("СпособОплаты", "Криптовалюта");
                    break;
                case PaymentMethod.eWallet:
                    values.Add("СпособОплаты", "Электронный кошелек");
                    break;
                default:
                    values.Add("СпособОплаты", "Внутренний перевод");
                    break;
            }

            if (string.IsNullOrEmpty(documentType))
                documentType = "ВнутреннийПеревод";

            if (string.IsNullOrEmpty(user))
                values.Add("РедакторОбъекта", user);

            using (var client = new WebDataClient())
            {
                return client.InsertObject(documentId, documentType, values);
            }
        }
        public void UpdateWebPaymentRequest(uint id_object, string transactionId, string address)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Транзакция", transactionId);
                values.Add("ВременныйАдрес", address);
                client.SetObjectValues(id_object, values);
            }
        }
        public WebPaymentRequest GetWebPaymentRequest(uint id_object)
        {
            var query = CreateQueryItem<WebPaymentRequest>("ЗапросНаВебПлатеж", Level.All);
            query.AddConditionItem(Union.And, "id_object", Operator.Equal, id_object);
            return base.GetInstance<WebPaymentRequest>(query);
        }
        public void UpdateWebPaymentRequest(uint id_object, decimal sum, decimal currencySum)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("СуммаПлатежа", sum);
                values.Add("СуммаПриход", sum);
                values.Add("СуммаВВалюте", currencySum);
                client.SetObjectValues(id_object, values);
            }
        }

        public uint CreateWithdrawalRequest(
           string accountName,
           uint companyId,
           uint partnerId,
           string article,
           DateTime date,
           decimal sum,
           decimal currencySum,
           string currency,
           string wallet,
           string tag,
           string paymentSystem,
           PaymentMethod paymentMethod,
           string comment,
           string documentType,
           string user)
        {
            if (partnerId == 0)
                return 0;

            var documentId = partnerId;

            if (string.IsNullOrEmpty(accountName))
                throw new Exception("Название счета не может быть пустым.");

            if (date == DateTime.Today)
                date = date.Date.Add(System.DateTime.Now.TimeOfDay);

            var values = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(accountName))
                values.Add("НазваниеСчета", accountName);

            if (companyId > 0)
                values.Add("СсылкаНаОрганизацию", companyId);


            values.Add("СтатусПлатежа", "Не оплачен");
            values.Add("ДатаПлатежа", date);
            values.Add("СсылкаНаКонтрагента", partnerId);
            values.Add("СсылкаНаДоговор", partnerId);
            if (!string.IsNullOrEmpty(article))
                values.Add("СтатьяДвиженияДенежныхСредств", article);

            values.Add("Название", comment);
            values.Add("СуммаПлатежа", sum);
            values.Add("СуммаРасход", sum);
            values.Add("НаправлениеПлатежа", "Расход");

            values.Add("ВылютаВывода", currency);
            values.Add("ПлатежнаяСистема", paymentSystem);
            values.Add("СуммаВВалюте", currencySum);

            values.Add("НомерКошелька", wallet);
            values.Add("Tag", tag);


            switch (paymentMethod)
            {
                case PaymentMethod.Cash:
                    values.Add("СпособОплаты", "Наличные");
                    break;
                case PaymentMethod.Bank:
                    values.Add("СпособОплаты", "Безналичный перевод");
                    break;
                case PaymentMethod.Crypto:
                    values.Add("СпособОплаты", "Криптовалюта");
                    break;
                case PaymentMethod.eWallet:
                    values.Add("СпособОплаты", "Электронный кошелек");
                    break;
                default:
                    values.Add("СпособОплаты", "Внутренний перевод");
                    break;
            }

            if (string.IsNullOrEmpty(documentType))
                documentType = "ВнутреннийПеревод";

            if (string.IsNullOrEmpty(user))
                values.Add("РедакторОбъекта", user);

            using (var client = new WebDataClient())
            {
                return client.InsertObject(documentId, documentType, values);
            }
        }
        public WithdrawalRequest GetWithdrawalRequest(uint id_object)
        {
            var query = CreateQueryItem<WithdrawalRequest>("ЗаявкаНаВыводСредств", Level.All);
            query.AddConditionItem(Union.And, "id_object", Operator.Equal, id_object);
            return base.GetInstance<WithdrawalRequest>(query);
        }
        public void SetWithdrawalDetails(uint id_object, DateTime? processedDate,
            string processedStatus, string wallet, string destTag, string comment, string user)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("ДатаОбработки", processedDate);
                values.Add("СтатусОбработки", processedStatus);
                values.Add("НомерКошелька", wallet);
                values.Add("Tag", destTag);
                values.Add("Комментарий", comment);
                values.Add("РедакторОбъекта", user);
                client.SetObjectValues(id_object, values);
            }
        }
        #endregion
        #region Dict
        public IEnumerable<DictItem> GetDictItems(string dictName)
        {
            var query = CreateQueryItem<DictItem>("ЭлементСправочника", Level.All);
            query.AddConditionItem(Union.And, "СсылкаНаСправочник/Название", Operator.Equal, dictName);
            query.AddOrder("Название", Direction.Asc);
            return base.GetList<DictItem>(query);
        }
        #endregion


        public IEnumerable<KbtFile> GetFiles(uint id_parent)
        {
            var query = base.CreateQueryItem<KbtFile>("Файл", Level.All);
            query.AddPlace(id_parent);
            return base.GetList<KbtFile>(query);
        }

        #region СЛужебные
        //protected T CreateInstance<T>(JObject item) where T : new()
        //{
        //    var instance = new T();
        //    var properties = typeof(T).GetProperties();
        //    foreach (var property in properties)
        //    {
        //        var attr = System.Attribute.GetCustomAttribute(property, typeof(PropertyAttribute)) as PropertyAttribute;
        //        if (attr != null)
        //        {
        //            var j = item.GetValue(attr.Name) as JValue;
        //            if (j != null)
        //            {
        //                var PT = property.PropertyType;

        //                if (PT == typeof(decimal))
        //                {
        //                    ;
        //                }

        //                var method = typeof(KBTech.Lib.Extensions).GetMethod("To", BindingFlags.Public | BindingFlags.Static);
        //                method = method.MakeGenericMethod(PT);
        //                try
        //                {

        //                }
        //                var value = method.Invoke(j, new[] { j.Value });
        //                property.SetValue(instance, value);
        //            }
        //        }
        //    }
        //    return instance;
        //}
        //protected IList<T> GetList<T>(QueryItem query) where T : new()
        //{
        //    using (var client = new WebDataClient())
        //    {
        //        var result = client.Search(query);
        //        var items = new List<T>();

        //        foreach (var item in result.ResultItems())
        //        {
        //            var instance = this.CreateInstance<T>(item);
        //            items.Add(instance);
        //        }
        //        return items;
        //    }
        //}
        //protected T GetInstance<T>(QueryItem query) where T : new()
        //{
        //    using (var client = new WebDataClient())
        //    {
        //        var result = client.Search(query);
        //        if (result.ResultCount() == 0)
        //            return default(T);
        //        var item = result.ResultItem(0);
        //        var instance = this.CreateInstance<T>(item);
        //        return instance;
        //    }
        //}
        #endregion

        public IEnumerable<Notice> GetUnreadedNotices(uint partnerId)
        {
            var query = base.CreateQueryItem<Notice>("Уведомление", Level.All);
            var col = query.AddConditionCollection(Union.And);
            col.AddConditionItem(Union.None, "Прочитано", Operator.IsNull);
            col.AddConditionItem(Union.Or, "Прочитано", Operator.Equal, false);
            query.AddConditionItem(Union.And, "СсылкаНаКонтрагента", Operator.Equal, partnerId);
            return base.GetList<Notice>(query);
        } 
        public void SetNoticeAsReaded(uint messageId)
        {
            using (var client = new WebDataClient())
            {
                var values = new Dictionary<string, object>();
                values.Add("Прочитано", true);
                client.SetObjectValues(messageId, values);
            }
        }
    }
}
